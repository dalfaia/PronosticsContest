using PronosContest.BLL;
using PronosContest.Code;
using PronosContest.Core;
using PronosContest.DAL.Pronos;
using PronosContest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PronosContest.Controllers
{
	public class PronosticsController : PronosContestControllerBase
	{
		public ActionResult Concours()
		{
			var concours = PronosContestWebService.GetService().PronosService.GetConcoursByUserID(this.UserID.Value);
			return View(concours);
		}
		[HttpGet]
		public ActionResult SearchConcours()
		{
			SearchConcoursModel model = new SearchConcoursModel();
			return View(model);
		}
		[HttpPost]
		public ActionResult SearchConcours(SearchConcoursModel pModel)
		{
			if (pModel != null)
			{
				pModel.Resultats = PronosContestWebService.GetService().PronosService.SearchConcours(pModel.Recherche, this.UserID.Value);
				return View(pModel);
			}
			return View();
		}
        public ActionResult InscrireConcours(int pConcoursID)
        {
            PronosContestWebService.GetService().PronosService.InscrireUserInConcours(this.UserID.Value, pConcoursID);
            return RedirectToAction("Concours");
        }

		public ActionResult SaisirPronostics(int pConcoursID, string pGroupe = "")
        {
            var concours = PronosContestWebService.GetService().PronosService.GetConcoursByID(pConcoursID);
            if (concours != null)
            {
                ViewBag.Title = concours.Competition.Libelle;
                if (concours.Competition != null)
                {
                    ViewBag.CompetitionLibelle = concours.Competition.Libelle;
                    ViewBag.CompetitionDateDebut = concours.Competition.DateDebut;
                    ViewBag.CompetitionDateFin = concours.Competition.DateFin;
                    ViewBag.IsReadOnly = concours.Competition.DateDebut < DateTime.Now;

                    List<GroupePronosticsModel> model = new List<GroupePronosticsModel>();
                    foreach (var grp in concours.Competition.Groupes.OrderBy(g => g.Lettre))
                    {
                        GroupePronosticsModel grpModel = new GroupePronosticsModel();
                        grpModel.ID = grp.ID;
                        grpModel.Name = "Groupe " + grp.Lettre;
                        grpModel.ShortName = grp.Lettre;
                        grpModel.IsChoosen = pGroupe == grpModel.Name;
                        grpModel.Classement = grp.ClassementWithPronostics(concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseGroupeID == grp.ID).ToList());
                        

                        foreach (var match in grp.Matchs.OrderBy(m => m.Date))
                        {
                            var prono = concours.Pronostics.Where(p => p.MatchID == match.ID && p.CompteUtilisateurID == this.UserID.Value).FirstOrDefault();
                            if (prono != null)
                            {
                                grpModel.MatchsPronostics.Add(new PronosticsModel()
                                {
                                    ConcoursID = concours.ID,
                                    MatchID = match.ID,
                                    EquipeAID = match.EquipeA.ID,
                                    EquipeBID = match.EquipeB.ID,
                                    EquipeAName = match.EquipeA.Libelle,
                                    EquipeBName = match.EquipeB.Libelle,
                                    EquipeAShortName = match.EquipeA.ShortName,
                                    EquipeBShortName = match.EquipeB.ShortName,
                                    ButsA = prono.ButsEquipeDomicile,
                                    ButsB = prono.ButsEquipeExterieur,
                                    LogoUrlEquipeA = match.EquipeA.Logo,
                                    LogoUrlEquipeB = match.EquipeB.Logo,
                                    DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString(),
                                    Etat = prono.EtatPronostic,
                                    IsReadOnly = concours.Competition.DateDebut < DateTime.Now
                                });
                            }
                            else
                            {
                                grpModel.MatchsPronostics.Add(new PronosticsModel()
                                {
                                    ConcoursID = concours.ID,
                                    MatchID = match.ID,
                                    EquipeAID = match.EquipeA.ID,
                                    EquipeBID = match.EquipeB.ID,
                                    EquipeAName = match.EquipeA.Libelle,
                                    EquipeBName = match.EquipeB.Libelle,
                                    EquipeAShortName = match.EquipeA.ShortName,
                                    EquipeBShortName = match.EquipeB.ShortName,
                                    LogoUrlEquipeA = match.EquipeA.Logo,
                                    LogoUrlEquipeB = match.EquipeB.Logo,
                                    DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString(),
                                    Etat = EtatPronostic.Empty,
                                    IsReadOnly = concours.Competition.DateDebut < DateTime.Now
                                });
                            }
                        }
                        model.Add(grpModel);
                    }
                    foreach (var grp in concours.Competition.PhasesFinales.OrderByDescending(g => (int)g.TypePhaseFinale))
                    {
                        GroupePronosticsModel grpModel = new GroupePronosticsModel();
                        grpModel.ID = grp.ID;
                        switch (grp.TypePhaseFinale)
                        {
                            case TypePhaseFinale.TrenteDeuxieme:
                                grpModel.Name = "32° de finale";
                                grpModel.ShortName = "32°";
                                break;
                            case TypePhaseFinale.Seizieme:
                                grpModel.Name = "16° de finale";
                                grpModel.ShortName = "16°";
                                break;
                            case TypePhaseFinale.Huitieme:
                                grpModel.Name = "Huitieme de finale";
                                grpModel.ShortName = "Huitièmes";
                                break;
                            case TypePhaseFinale.Quart:
                                grpModel.Name = "Quart de finales";
                                grpModel.ShortName = "Quarts";
                                break;
                            case TypePhaseFinale.Demi:
                                grpModel.Name = "Demi-finale";
                                grpModel.ShortName = "Demis";
                                break;
                            case TypePhaseFinale.Finale:
                                grpModel.Name = "Finale";
                                grpModel.ShortName = "Finale";
                                break;
                        }
                        grpModel.IsChoosen = pGroupe == grpModel.Name;

                        foreach (var match in grp.Matchs.OrderBy(m => m.Date))
                        {
                            var prono = concours.Pronostics.Where(p => p.MatchID == match.ID && p.CompteUtilisateurID == this.UserID.Value).FirstOrDefault();
                            PronosticsModel pronoModel = new PronosticsModel();
                            if (prono != null)
                            {
                                grpModel.MatchsPronostics.Add(new PronosticsModel()
                                {
                                    ConcoursID = concours.ID,
                                    MatchID = match.ID,
                                    EquipeAID = match.EquipeA.ID,
                                    EquipeBID = match.EquipeB.ID,
                                    EquipeAName = match.EquipeA.Libelle,
                                    EquipeBName = match.EquipeB.Libelle,
                                    EquipeAShortName = match.EquipeA.ShortName,
                                    EquipeBShortName = match.EquipeB.ShortName,
                                    ButsA = prono.ButsEquipeDomicile,
                                    ButsB = prono.ButsEquipeExterieur,
                                    LogoUrlEquipeA = match.EquipeA.Logo,
                                    LogoUrlEquipeB = match.EquipeB.Logo,
                                    DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString(),
                                    Etat = prono.EtatPronostic,
                                    IsReadOnly = concours.Competition.DateDebut < DateTime.Now
                                });
                            }
                            else
                            {
                                var nbPronosticsGroupes = concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseGroupe != null).Count();
                                var nbMatchsGroupes = 0;
                                foreach (var g in concours.Competition.Groupes)
                                    nbMatchsGroupes += g.Matchs.Count();

                                if (nbPronosticsGroupes == nbMatchsGroupes)
                                {
                                    if (match.EquipePossibleDomicile_Place != null && match.EquipePossibleExterieur_Place != null)
                                    {
                                        // Premiere phase finale (generation apres les groupes)
                                        

                                        var groupe_A = concours.Competition.Groupes.Where(g => match.EquipePossibleDomicile_Groupes.Contains(g.Lettre)).FirstOrDefault();
                                        var classement_A = groupe_A.ClassementWithPronostics(concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID).ToList());
                                        var equipe_A_ID = classement_A.ElementAt(match.EquipePossibleDomicile_Place.Value - 1).IDEquipe;
                                        var equipeA = groupe_A.Equipes.Where(e => e.ID == equipe_A_ID).FirstOrDefault();
                                        var groupe_B = concours.Competition.Groupes.Where(g => match.EquipePossibleExterieur_Groupes.Contains(g.Lettre)).FirstOrDefault();
                                        var classement_B = groupe_B.ClassementWithPronostics(concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID).ToList());
                                        var equipe_B_ID = classement_B.ElementAt(match.EquipePossibleExterieur_Place.Value - 1).IDEquipe;
                                        var equipeB = groupe_B.Equipes.Where(e => e.ID == equipe_B_ID).FirstOrDefault();
                                        if (equipeA != null && equipeB != null)
                                        {
                                            pronoModel.EquipeAName = equipeA.Libelle;
                                            pronoModel.EquipeAShortName = equipeA.ShortName;
                                            pronoModel.EquipeAID = equipeA.ID;
                                            pronoModel.LogoUrlEquipeA = equipeA.Logo;
                                            pronoModel.EquipeBName = equipeB.Libelle;
                                            pronoModel.EquipeBShortName = equipeB.ShortName;
                                            pronoModel.EquipeBID = equipeB.ID;
                                            pronoModel.LogoUrlEquipeB = equipeB.Logo;
                                            pronoModel.ConcoursID = concours.ID;
                                            pronoModel.MatchID = match.ID;
                                            pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
                                            pronoModel.Etat = EtatPronostic.Empty;
                                            pronoModel.IsReadOnly = false;
                                        }
                                    }
                                    else
                                    {
                                        // autres phases

                                    }
                                }
                                else
                                {
                                    pronoModel.EquipeAName = match.EquipePossibleDomicile_Libelle;
                                    pronoModel.EquipeAShortName = match.EquipePossibleDomicile_Libelle;
                                    pronoModel.EquipeBName = match.EquipePossibleExterieur_Libelle;
                                    pronoModel.EquipeBShortName = match.EquipePossibleExterieur_Libelle;
                                    pronoModel.ConcoursID = concours.ID;
                                    pronoModel.MatchID = match.ID;
                                    pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
                                    pronoModel.Etat = EtatPronostic.Empty;
                                    pronoModel.IsReadOnly = false;
                                }
                            }
                            grpModel.MatchsPronostics.Add(pronoModel);
                        }
                        model.Add(grpModel);
                    }
                    return View(model);
                }
            }
            return View();
        }

        private List<char> _getCombinaisonsMeilleurs3eme(Concours pConcours)
        {
            List<PhaseGroupe.ClassementGroupeModel> classements3eme = new List<PhaseGroupe.ClassementGroupeModel>();
            foreach (var gr in pConcours.Competition.Groupes)
            {
                var classement = gr.ClassementWithPronostics(pConcours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID).ToList());
                var equipe = classement.ElementAt(2);
                classements3eme.Add(equipe);
            }
            classements3eme.OrderByDescending(c => c.Points).OrderByDescending(c => c.Difference).OrderByDescending(c => c.ButsMarques);
            return new List<char>()
            {
                //classements3eme[0].
            };
        }

        [HttpGet]
        public ActionResult GetClassement(string pConcoursID, string pGroupeID)
        {
            if (pConcoursID != null && pGroupeID != null)
            {
                int concoursID = Helper.GetIntFromString(pConcoursID).Value;
                int groupeID = Helper.GetIntFromString(pGroupeID).Value;
                return PartialView("_Classement", GetClassementByGroupeID(concoursID, groupeID));
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<bool> SetScore(string pConcoursID, string pGroupeID, string pMatchID, string pEquipeID, string pValue)
        {
            int userID = this.UserID.Value;
            int concoursID = Helper.GetIntFromString(pConcoursID).Value;
            int matchID = Helper.GetIntFromString(pMatchID).Value;
            int equipeID = Helper.GetIntFromString(pEquipeID).Value;
            int value = Helper.GetIntFromString(pValue).Value;
            int groupeID = Helper.GetIntFromString(pGroupeID).Value;

            PronosContestWebService.GetService().PronosService.SetScore(userID, concoursID, matchID, equipeID, value);
            return true;
        }

        private List<PhaseGroupe.ClassementGroupeModel> GetClassementByGroupeID(int pConcoursID, int pGroupeID)
        {
            var concours = PronosContestWebService.GetService().PronosService.GetConcoursByID(pConcoursID);
            if (concours != null)
            {
                var grp = concours.Competition.Groupes.Where(g => g.ID == pGroupeID).FirstOrDefault();
                return grp.ClassementWithPronostics(concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseGroupeID == grp.ID).ToList());
            }
            return null;
        }
    }
}
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
                if (concours.Competition != null)
                {
                    ViewBag.CompetitionLibelle = concours.Competition.Libelle;
                    ViewBag.CompetitionDateDebut = concours.Competition.DateDebut;
                    ViewBag.CompetitionDateFin = concours.Competition.DateFin;

                    List<GroupePronosticsModel> model = new List<GroupePronosticsModel>();
                    foreach (var grp in concours.Competition.Groupes.OrderBy(g => g.Lettre))
                    {
                        GroupePronosticsModel grpModel = new GroupePronosticsModel();
                        grpModel.ID = grp.ID;
                        grpModel.Nom = "Groupe " + grp.Lettre;
                        grpModel.IsChoosen = pGroupe == grpModel.Nom;
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
                                    EquipeA = match.EquipeA.Libelle,
                                    EquipeB = match.EquipeB.Libelle,
                                    ButsA = prono.ButsEquipeDomicile,
                                    ButsB = prono.ButsEquipeExterieur,
                                    DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString()
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
                                    EquipeA = match.EquipeA.Libelle,
                                    EquipeB = match.EquipeB.Libelle,
                                    DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString()
                                });
                            }
                        }
                        model.Add(grpModel);
                    }
                    return View(model);
                }
            }
            return View();
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
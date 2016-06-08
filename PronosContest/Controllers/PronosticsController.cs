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
                                    NumeroMatch = match.NumeroMatch,
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
                                    IsReadOnly = concours.DateLimiteSaisie < DateTime.Now || match.Date < DateTime.Now
								});
                            }
                            else
                            {
                                grpModel.MatchsPronostics.Add(new PronosticsModel()
                                {
                                    ConcoursID = concours.ID,
                                    MatchID = match.ID,
                                    NumeroMatch = match.NumeroMatch,
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
									IsReadOnly = concours.DateLimiteSaisie < DateTime.Now || match.Date < DateTime.Now
								});
                            }
                        }
                        model.Add(grpModel);
                    }
                    foreach (var grp in concours.Competition.PhasesFinales.OrderByDescending(g => (int)g.TypePhaseFinale))
                    {
                        GroupePronosticsModel grpModel = new GroupePronosticsModel();
                        grpModel.ID = grp.ID;
                        grpModel.TypePhaseFinale = grp.TypePhaseFinale;
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
                                pronoModel.ConcoursID = concours.ID;
                                pronoModel.MatchID = prono.MatchID;
                                pronoModel.ButsA = prono.ButsEquipeDomicile;
                                pronoModel.ButsB = prono.ButsEquipeExterieur;
								pronoModel.PenaltiesA = prono.ButsPenaltiesEquipeDomicile;
								pronoModel.PenaltiesB = prono.ButsPenaltiesEquipeExterieur;
                                pronoModel.NumeroMatch = match.NumeroMatch;
                                pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
                                pronoModel.Etat = prono.EtatPronostic;
								pronoModel.IsReadOnly = concours.DateLimiteSaisie < DateTime.Now || match.Date < DateTime.Now;
                            }
                            var nbPronosticsGroupes = concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseGroupe != null).Count();
                            var nbMatchsGroupes = 0;
                            foreach (var g in concours.Competition.Groupes)
                                nbMatchsGroupes += g.Matchs.Count();

                            if (nbPronosticsGroupes == nbMatchsGroupes)
                            {
                                if (match.EquipePossibleDomicile_Place != null && match.EquipePossibleExterieur_Place != null)
                                {
									// Premiere phase finale (generation apres les groupes)
									var combinaison3emes = concours.Competition.GetCombinaisonClassementTroisiemes(concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID).ToList());
									var combinaison = concours.Competition.TableauCombinaisons.Where(tc => tc.Combinaisons.Intersect(combinaison3emes).Count() == combinaison3emes.Count).FirstOrDefault();

                                    var groupe_A = concours.Competition.Groupes.Where(g => match.EquipePossibleDomicile_Groupes.Contains(g.Lettre)).FirstOrDefault();
                                    var classement_A = groupe_A.ClassementWithPronostics(concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID).ToList());
                                    var equipe_A_ID = classement_A.ElementAt(match.EquipePossibleDomicile_Place.Value - 1).IDEquipe;
                                    var equipeA = groupe_A.Equipes.Where(e => e.ID == equipe_A_ID).FirstOrDefault();

									var groupe_B = new PhaseGroupe();
                                    if (match.EquipePossibleExterieur_Place == 3)
									{
										switch (groupe_A.Lettre)
										{
											case "A":
												groupe_B = concours.Competition.Groupes.Where(g => g.Lettre == combinaison.Adversaire1A.ToString()).FirstOrDefault();
                                                break;
											case "B":
												groupe_B = concours.Competition.Groupes.Where(g => g.Lettre == combinaison.Adversaire1B.ToString()).FirstOrDefault();
												break;
											case "C":
												groupe_B = concours.Competition.Groupes.Where(g => g.Lettre == combinaison.Adversaire1C.ToString()).FirstOrDefault();
												break;
											case "D":
												groupe_B = concours.Competition.Groupes.Where(g => g.Lettre == combinaison.Adversaire1D.ToString()).FirstOrDefault();
												break;
										}
									}
									else
									{
										groupe_B = concours.Competition.Groupes.Where(g => match.EquipePossibleExterieur_Groupes.Contains(g.Lettre)).FirstOrDefault();
									}
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
                                        pronoModel.NumeroMatch = match.NumeroMatch;
                                        pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
                                        pronoModel.Etat = EtatPronostic.Empty;
                                        pronoModel.IsReadOnly = concours.DateLimiteSaisie < DateTime.Now || match.Date < DateTime.Now;
                                    }
                                }
                                else
								{
									if (grp.TypePhaseFinale == TypePhaseFinale.Quart)
									{
										var nbPronosticsHuitiemes = concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseFinale != null && p.Match.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Huitieme).Count();
										if (nbPronosticsHuitiemes == (int)TypePhaseFinale.Huitieme)
										{
											var Match_A_ID = match.MatchVainqueurDomicileID.Value;
											var Match_B_ID = match.MatchVainqueurExterieurID.Value;
                                            var pronosHuitiemes = model.Where(m => m.TypePhaseFinale == TypePhaseFinale.Huitieme).FirstOrDefault();
                                            var match_A = pronosHuitiemes.MatchsPronostics.Where(m => m.NumeroMatch == Match_A_ID).FirstOrDefault();
											var match_B = pronosHuitiemes.MatchsPronostics.Where(m => m.NumeroMatch == Match_B_ID).FirstOrDefault();
											var equipeA = concours.Competition.Equipes.Where(e => e.ID == match_A.VanqueurID).FirstOrDefault();
											var equipeB = concours.Competition.Equipes.Where(e => e.ID == match_B.VanqueurID).FirstOrDefault();
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
                                                pronoModel.NumeroMatch = match.NumeroMatch;
                                                pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
												pronoModel.Etat = EtatPronostic.Empty;
												pronoModel.IsReadOnly = concours.DateLimiteSaisie < DateTime.Now || match.Date < DateTime.Now;
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
											pronoModel.NumeroMatch = match.NumeroMatch;
											pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
											pronoModel.Etat = EtatPronostic.Empty;
											pronoModel.IsReadOnly = true;
										}
									}
									else if (grp.TypePhaseFinale == TypePhaseFinale.Demi)
									{
										var nbPronosticsQuarts = concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseFinale != null && p.Match.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Quart).Count();
										if (nbPronosticsQuarts == (int)TypePhaseFinale.Quart)
										{
											var Match_A_ID = match.MatchVainqueurDomicileID.Value;
											var Match_B_ID = match.MatchVainqueurExterieurID.Value;
                                            var pronosQuarts = model.Where(m => m.TypePhaseFinale == TypePhaseFinale.Quart).FirstOrDefault();
                                            var match_A = pronosQuarts.MatchsPronostics.Where(m => m.NumeroMatch == Match_A_ID).FirstOrDefault();
                                            var match_B = pronosQuarts.MatchsPronostics.Where(m => m.NumeroMatch == Match_B_ID).FirstOrDefault();
											var equipeA = concours.Competition.Equipes.Where(e => e.ID == match_A.VanqueurID).FirstOrDefault();
											var equipeB = concours.Competition.Equipes.Where(e => e.ID == match_B.VanqueurID).FirstOrDefault();
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
                                                pronoModel.NumeroMatch = match.NumeroMatch;
                                                pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
												pronoModel.Etat = EtatPronostic.Empty;
												pronoModel.IsReadOnly = concours.DateLimiteSaisie < DateTime.Now || match.Date < DateTime.Now;
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
											pronoModel.NumeroMatch = match.NumeroMatch;
											pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
											pronoModel.Etat = EtatPronostic.Empty;
											pronoModel.IsReadOnly = true;
										}
									}
									else if (grp.TypePhaseFinale == TypePhaseFinale.Finale)
									{
										var nbPronosticsDemis = concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseFinale != null && p.Match.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Demi).Count();
										if (nbPronosticsDemis == (int)TypePhaseFinale.Demi)
										{
											var Match_A_ID = match.MatchVainqueurDomicileID.Value;
											var Match_B_ID = match.MatchVainqueurExterieurID.Value;
                                            var pronosDemis = model.Where(m => m.TypePhaseFinale == TypePhaseFinale.Demi).FirstOrDefault();
                                            var match_A = pronosDemis.MatchsPronostics.Where(m => m.NumeroMatch == Match_A_ID).FirstOrDefault();
                                            var match_B = pronosDemis.MatchsPronostics.Where(m => m.NumeroMatch == Match_B_ID).FirstOrDefault();
                                            var equipeA = concours.Competition.Equipes.Where(e => e.ID == match_A.VanqueurID).FirstOrDefault();
											var equipeB = concours.Competition.Equipes.Where(e => e.ID == match_B.VanqueurID).FirstOrDefault();
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
                                                pronoModel.NumeroMatch = match.NumeroMatch;
                                                pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
												pronoModel.Etat = EtatPronostic.Empty;
												pronoModel.IsReadOnly = concours.DateLimiteSaisie < DateTime.Now || match.Date < DateTime.Now;
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
											pronoModel.NumeroMatch = match.NumeroMatch;
											pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
											pronoModel.Etat = EtatPronostic.Empty;
											pronoModel.IsReadOnly = true;
										}
									}
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
                                pronoModel.NumeroMatch = match.NumeroMatch;
                                pronoModel.DateHeure = match.Date.ToShortDateString() + " à " + match.Date.ToShortTimeString();
                                pronoModel.Etat = EtatPronostic.Empty;
                                pronoModel.IsReadOnly = true;
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
        public async Task<bool> SetScore(string pConcoursID, string pGroupeID, string pMatchID, string pEquipeAID, string pEquipeBID, string pIsExterieur, string pValue)
        {
            int userID = this.UserID.Value;
            int concoursID = Helper.GetIntFromString(pConcoursID).Value;
            int matchID = Helper.GetIntFromString(pMatchID).Value;
            int equipeAID = Helper.GetIntFromString(pEquipeAID).Value;
            int equipeBID = Helper.GetIntFromString(pEquipeBID).Value;
            int value = Helper.GetIntFromString(pValue).Value;
            bool isExterieur = Helper.GetBoolFromString(pIsExterieur).Value;
            int groupeID = Helper.GetIntFromString(pGroupeID).Value;

            PronosContestWebService.GetService().PronosService.SetScore(userID, concoursID, matchID, equipeAID, equipeBID, isExterieur, value);
            return true;
        }

		[HttpGet]
		public async Task<bool> SetScorePenalties(string pConcoursID, string pGroupeID, string pMatchID, string pEquipeAID, string pEquipeBID, string pIsExterieur, string pValue)
		{
			int userID = this.UserID.Value;
			int concoursID = Helper.GetIntFromString(pConcoursID).Value;
			int matchID = Helper.GetIntFromString(pMatchID).Value;
			int equipeAID = Helper.GetIntFromString(pEquipeAID).Value;
			int equipeBID = Helper.GetIntFromString(pEquipeBID).Value;
			int value = Helper.GetIntFromString(pValue).Value;
			bool isExterieur = Helper.GetBoolFromString(pIsExterieur).Value;
			int groupeID = Helper.GetIntFromString(pGroupeID).Value;

			PronosContestWebService.GetService().PronosService.SetScorePenalties(userID, concoursID, matchID, equipeAID, equipeBID, isExterieur, value);
			return true;
		}

		private List<PhaseGroupe.ClassementGroupeModel> GetClassementByGroupeID(int pConcoursID, int pGroupeID)
        {
            var concours = PronosContestWebService.GetService().PronosService.GetConcoursByID(pConcoursID);
            if (concours != null)
            {
                var grp = concours.Competition.Groupes.Where(g => g.ID == pGroupeID).FirstOrDefault();
                if (grp != null)
                    return grp.ClassementWithPronostics(concours.Pronostics.Where(p => p.CompteUtilisateurID == this.UserID && p.Match.PhaseGroupeID == grp.ID).ToList());
            }
            return null;
        }
    }
}
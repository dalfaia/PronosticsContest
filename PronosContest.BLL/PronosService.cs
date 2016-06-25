using PronosContest.DAL;
using PronosContest.DAL.Authentification;
using PronosContest.DAL.Pronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PronosContest.BLL
{
	public class PronosService
	{
		private PronosContestContext _pronosContestContextDatabase;

		internal PronosService(PronosContestContext pPronosContestContextDatabase)
		{
			_pronosContestContextDatabase = pPronosContestContextDatabase;
		}
		public List<Competition> GetCompetitions()
		{
			return _pronosContestContextDatabase.Competitions.ToList();
		}
        public Competition GetCompetitionById(int pId)
        {
            return _pronosContestContextDatabase.Competitions.Where(c => c.ID == pId).FirstOrDefault();
        }
        public List<Concours> GetConcoursByUserID(int pId)
        {
            return _pronosContestContextDatabase.Concours.Where(c => c.ConcoursCompteUtilisateurs.Where(ccu => ccu.CompteUtilisateurID == pId).Any()).ToList();
        }
        public Concours GetConcoursByID(int pId)
        {
            return _pronosContestContextDatabase.Concours.Where(c => c.ID == pId).FirstOrDefault();
        }
        public List<Concours> SearchConcours(string pRecherche, int pUserID)
		{
			return _pronosContestContextDatabase.Concours.Where(c => !c.ConcoursCompteUtilisateurs.Where(ccu => ccu.CompteUtilisateurID == pUserID).Any() && (c.Competition.Libelle.Contains(pRecherche) || c.CompteUtilisateur.Email == pRecherche)).ToList();
		}
        public void InscrireUserInConcours(int pUserID, int pConcoursID)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            if (concours != null)
            {
                concours.ConcoursCompteUtilisateurs.Add(new ConcoursCompteUtilisateur()
                {
                    CompteUtilisateurID = pUserID,
                    Date = DateTime.Now
                });
                _pronosContestContextDatabase.SaveChanges();
            }
        }
        public Match GetMatchByID (int pId)
        {
            return _pronosContestContextDatabase.Matchs.Where(m => m.ID == pId).FirstOrDefault();
        }

        public void SetScore(int pUserID, int pConcoursID, int pMatchID, int pEquipeAID, int pEquipeBID, bool pIsExterieur, int pButs, bool pIsNewProno)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            var match = _pronosContestContextDatabase.Matchs.Where(m => m.ID == pMatchID).FirstOrDefault();
            if (concours != null && match != null && match.Date >= DateTime.Now)
            {
                var prono = concours.Pronostics.Where(p => p.MatchID == pMatchID && p.CompteUtilisateurID == pUserID && p.IsNouveauProno == pIsNewProno).FirstOrDefault();
                if (prono != null)
                {
                    prono.EquipeAID = pEquipeAID;
                    prono.EquipeBID = pEquipeBID;
                    if (!pIsExterieur)
                        prono.ButsEquipeDomicile = pButs;
                    else
                        prono.ButsEquipeExterieur = pButs;
                }
                else
                {
                    Pronostic newProno = new Pronostic()
                    {
                        CompteUtilisateurID = pUserID,
                        DateCreation = DateTime.Now,
                        EtatPronostic = EtatPronostic.EnCours,
                        MatchID = pMatchID,
                        TypePronostic = TypeDePronostic.ScoreExact,
                        EquipeAID = pEquipeAID,
                        EquipeBID = pEquipeBID,
                        IsNouveauProno = pIsNewProno
                    };
                    if (!pIsExterieur)
                        newProno.ButsEquipeDomicile = pButs;
                    else
                        newProno.ButsEquipeExterieur = pButs;
                    concours.Pronostics.Add(newProno);
                }
                _pronosContestContextDatabase.SaveChanges();
            }
        }
		public void SetScorePenalties(int pUserID, int pConcoursID, int pMatchID, int pEquipeAID, int pEquipeBID, bool pIsExterieur, int pButs, bool pIsNewProno)
		{
			var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            var match = _pronosContestContextDatabase.Matchs.Where(m => m.ID == pMatchID).FirstOrDefault();
            if (concours != null && match != null && match.Date >= DateTime.Now)
            {
				var prono = concours.Pronostics.Where(p => p.MatchID == pMatchID && p.CompteUtilisateurID == pUserID && p.IsNouveauProno == pIsNewProno).FirstOrDefault();
				if (prono != null)
				{
					prono.EquipeAID = pEquipeAID;
					prono.EquipeBID = pEquipeBID;
					if (!pIsExterieur)
						prono.ButsPenaltiesEquipeDomicile = pButs;
					else
						prono.ButsPenaltiesEquipeExterieur = pButs;
				}
				else
				{
					Pronostic newProno = new Pronostic()
					{
						CompteUtilisateurID = pUserID,
						DateCreation = DateTime.Now,
						EtatPronostic = EtatPronostic.EnCours,
						ButsEquipeDomicile = 0,
						ButsEquipeExterieur = 0,
						MatchID = pMatchID,
						TypePronostic = TypeDePronostic.ScoreExact,
						EquipeAID = pEquipeAID,
						EquipeBID = pEquipeBID,
                        IsNouveauProno = pIsNewProno
					};
					if (!pIsExterieur)
						newProno.ButsPenaltiesEquipeDomicile = pButs;
					else
						newProno.ButsPenaltiesEquipeExterieur = pButs;
					concours.Pronostics.Add(newProno);
				}
				_pronosContestContextDatabase.SaveChanges();
			}
		}

        public void SetScoreMatch(int pUserID, int pConcoursID, int pMatchID, int pEquipeAID, int pEquipeBID, bool pIsExterieur, int pButs)
        {
            var match = GetMatchByID(pMatchID);
            if (match != null)
            {
                match.EquipeAID = pEquipeAID;
                match.EquipeBID = pEquipeBID;
                if (!pIsExterieur)
                    match.ButsEquipeDomicile = pButs;
                else
                    match.ButsEquipeExterieur = pButs;

                _genererHuitiemes(pConcoursID);
                _genererQuarts(pConcoursID);
                _genererDemis(pConcoursID);
                _genererFinale(pConcoursID);

                _pronosContestContextDatabase.SaveChanges();
            }
        }

        private void _genererHuitiemes(int pConcoursID)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            if (concours != null)
            {
                if (concours.Competition.AllMatchs.Where(am => am.PhaseGroupe != null && am.ButsEquipeDomicile != null && am.ButsEquipeExterieur != null).Count() == concours.Competition.AllMatchs.Where(am => am.PhaseGroupe != null).Count())
                {
                    var combinaison3emes = concours.Competition.GetCombinaisonClassementTroisiemes();
                    var combinaison = concours.Competition.TableauCombinaisons.Where(tc => tc.Combinaisons.Intersect(combinaison3emes).Count() == combinaison3emes.Count).FirstOrDefault();
                    var matchsHuitiemes = concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Huitieme);
                    foreach (var m in matchsHuitiemes)
                    {
                        var groupe_A = concours.Competition.Groupes.Where(g => m.EquipePossibleDomicile_Groupes.Contains(g.Lettre)).FirstOrDefault();
                        var classement_A = groupe_A.Classement();
                        var equipe_A_ID = classement_A.ElementAt(m.EquipePossibleDomicile_Place.Value - 1).IDEquipe;
                        var equipeA = groupe_A.Equipes.Where(e => e.ID == equipe_A_ID).FirstOrDefault();

                        var groupe_B = new PhaseGroupe();
                        if (m.EquipePossibleExterieur_Place == 3)
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
                            groupe_B = concours.Competition.Groupes.Where(g => m.EquipePossibleExterieur_Groupes.Contains(g.Lettre)).FirstOrDefault();
                        }
                        var classement_B = groupe_B.Classement();
                        var equipe_B_ID = classement_B.ElementAt(m.EquipePossibleExterieur_Place.Value - 1).IDEquipe;
                        var equipeB = groupe_B.Equipes.Where(e => e.ID == equipe_B_ID).FirstOrDefault();
                        if (equipeA != null && equipeB != null)
                        {
                            m.EquipeAID = equipeA.ID;
                            m.EquipeBID = equipeB.ID;
                        }
                    }
                    _pronosContestContextDatabase.SaveChanges();
                }
            }
        }
        private void _genererQuarts(int pConcoursID)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            if (concours != null)
            {
                if (concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Huitieme && am.ButsEquipeDomicile != null && am.ButsEquipeExterieur != null).Count() == concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Huitieme).Count())
                {
                    var matchsQuarts = concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Quart);
                    foreach (var m in matchsQuarts)
                    {
                        var Match_A_ID = m.MatchVainqueurDomicileID.Value;
                        var Match_B_ID = m.MatchVainqueurExterieurID.Value;
                        var matchsHuitiemes = concours.Competition.PhasesFinales.Where(am => am.TypePhaseFinale == TypePhaseFinale.Huitieme).FirstOrDefault().Matchs;
                        var match_A = matchsHuitiemes.Where(mat => mat.NumeroMatch == Match_A_ID).FirstOrDefault();
                        var match_B = matchsHuitiemes.Where(mat => mat.NumeroMatch == Match_B_ID).FirstOrDefault();
                        var equipeA = concours.Competition.Equipes.Where(e => e.ID == match_A.VainqueurID).FirstOrDefault();
                        var equipeB = concours.Competition.Equipes.Where(e => e.ID == match_B.VainqueurID).FirstOrDefault();
                        if (equipeA != null && equipeB != null)
                        {
                            m.EquipeAID = equipeA.ID;
                            m.EquipeBID = equipeB.ID;
                        }
                    }
                    _pronosContestContextDatabase.SaveChanges();
                }
            }
        }
        private void _genererDemis(int pConcoursID)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            if (concours != null)
            {
                if (concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Quart && am.ButsEquipeDomicile != null && am.ButsEquipeExterieur != null).Count() == concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Huitieme).Count())
                {
                    var matchsDemis = concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Demi);
                    foreach (var m in matchsDemis)
                    {
                        var Match_A_ID = m.MatchVainqueurDomicileID.Value;
                        var Match_B_ID = m.MatchVainqueurExterieurID.Value;
                        var matchsQuarts = concours.Competition.PhasesFinales.Where(am => am.TypePhaseFinale == TypePhaseFinale.Quart).FirstOrDefault().Matchs;
                        var match_A = matchsQuarts.Where(mat => mat.NumeroMatch == Match_A_ID).FirstOrDefault();
                        var match_B = matchsQuarts.Where(mat => mat.NumeroMatch == Match_B_ID).FirstOrDefault();
                        var equipeA = concours.Competition.Equipes.Where(e => e.ID == match_A.VainqueurID).FirstOrDefault();
                        var equipeB = concours.Competition.Equipes.Where(e => e.ID == match_B.VainqueurID).FirstOrDefault();
                        if (equipeA != null && equipeB != null)
                        {
                            m.EquipeAID = equipeA.ID;
                            m.EquipeBID = equipeB.ID;
                        }
                    }
                    _pronosContestContextDatabase.SaveChanges();
                }
            }
        }
        private void _genererFinale(int pConcoursID)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            if (concours != null)
            {
                if (concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Demi && am.ButsEquipeDomicile != null && am.ButsEquipeExterieur != null).Count() == concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Huitieme).Count())
                {
                    var matchFinale = concours.Competition.AllMatchs.Where(am => am.PhaseFinale != null && am.PhaseFinale.TypePhaseFinale == TypePhaseFinale.Finale);
                    foreach (var m in matchFinale)
                    {
                        var Match_A_ID = m.MatchVainqueurDomicileID.Value;
                        var Match_B_ID = m.MatchVainqueurExterieurID.Value;
                        var matchsDemis = concours.Competition.PhasesFinales.Where(am => am.TypePhaseFinale == TypePhaseFinale.Demi).FirstOrDefault().Matchs;
                        var match_A = matchsDemis.Where(mat => mat.NumeroMatch == Match_A_ID).FirstOrDefault();
                        var match_B = matchsDemis.Where(mat => mat.NumeroMatch == Match_B_ID).FirstOrDefault();
                        var equipeA = concours.Competition.Equipes.Where(e => e.ID == match_A.VainqueurID).FirstOrDefault();
                        var equipeB = concours.Competition.Equipes.Where(e => e.ID == match_B.VainqueurID).FirstOrDefault();
                        if (equipeA != null && equipeB != null)
                        {
                            m.EquipeAID = equipeA.ID;
                            m.EquipeBID = equipeB.ID;
                        }
                    }
                    _pronosContestContextDatabase.SaveChanges();
                }
            }
        }
        public void SetScorePenaltiesMatch(int pUserID, int pConcoursID, int pMatchID, int pEquipeAID, int pEquipeBID, bool pIsExterieur, int pButs)
        {
            var match = GetMatchByID(pMatchID);
            if (match != null)
            {
                match.EquipeAID = pEquipeAID;
                match.EquipeBID = pEquipeBID;
                if (!pIsExterieur)
                    match.ButsPenaltiesEquipeDomicile = pButs;
                else
                    match.ButsPenaltiesEquipeExterieur = pButs;

                _pronosContestContextDatabase.SaveChanges();
            }
        }
    }
}

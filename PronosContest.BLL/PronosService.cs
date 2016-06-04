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

        public void SetScore(int pUserID, int pConcoursID, int pMatchID, int pEquipeID, int pButs)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            if (concours != null)
            { 
                var prono = concours.Pronostics.Where(p => p.MatchID == pMatchID && p.CompteUtilisateurID == pUserID).FirstOrDefault();
                if (prono != null)
                {
                    var match = prono.Match;
                    if (match.EquipeAID == pEquipeID)
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
                        TypePronostic = TypeDePronostic.ScoreExact
                    };
                    concours.Pronostics.Add(newProno);
                }
                _pronosContestContextDatabase.SaveChangesAsync();
            }
        }
    }
}

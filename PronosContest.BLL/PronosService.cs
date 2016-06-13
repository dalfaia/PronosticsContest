﻿using PronosContest.DAL;
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

        public void SetScore(int pUserID, int pConcoursID, int pMatchID, int pEquipeAID, int pEquipeBID, bool pIsExterieur, int pButs)
        {
            var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
            if (concours != null)
            { 
                var prono = concours.Pronostics.Where(p => p.MatchID == pMatchID && p.CompteUtilisateurID == pUserID).FirstOrDefault();
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
                        EquipeBID = pEquipeBID
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
		public void SetScorePenalties(int pUserID, int pConcoursID, int pMatchID, int pEquipeAID, int pEquipeBID, bool pIsExterieur, int pButs)
		{
			var concours = _pronosContestContextDatabase.Concours.Where(c => c.ID == pConcoursID).FirstOrDefault();
			if (concours != null)
			{
				var prono = concours.Pronostics.Where(p => p.MatchID == pMatchID && p.CompteUtilisateurID == pUserID).FirstOrDefault();
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
						EquipeBID = pEquipeBID
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

                _pronosContestContextDatabase.SaveChanges();
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

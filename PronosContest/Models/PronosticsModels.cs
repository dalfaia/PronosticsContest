using PronosContest.DAL.Pronos;
using PronosContest.DAL.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace PronosContest.Models
{
	public enum TypeScore
	{
		Domicile = 0,
		Exterieur = 1
	}
	public class SearchConcoursModel
	{
		[DataType(DataType.Text)]
		public string Recherche { get; set; }

		public List<Concours> Resultats { get; set; }

		public SearchConcoursModel()
		{
			this.Resultats = new List<Concours>();
		}
	}	

    public class PronosticsModel
    {
        public string DateHeure { get; set; }
        public int EquipeAID { get; set; }
        public int EquipeBID { get; set; }
        public string EquipeAName { get; set; }
        public string EquipeBName { get; set; }
        public string EquipeAShortName { get; set; }
        public string EquipeBShortName { get; set; }
        public string LogoUrlEquipeA { get; set; }
        public string LogoUrlEquipeB { get; set; }
        public int NumeroMatch { get; set; }
        public int? ButsA { get; set; }
        public int? ButsB { get; set; }
		public int? PenaltiesA { get; set; }
		public int? PenaltiesB { get; set; }
		public int MatchID { get; set; }
        public int ConcoursID { get; set; }
        public EtatPronostic Etat
		{
			get
			{
				if (this.ButsA != null && this.ButsB != null)
				{
					if (this.Resultat_ButsA != null && this.Resultat_ButsB != null)
					{
						if (this.ButsA == this.Resultat_ButsA && this.ButsB == this.Resultat_ButsB)
							return EtatPronostic.GagneScoreExact;
						if (this.ButsA > this.ButsB && this.Resultat_ButsA > this.Resultat_ButsB)
							return EtatPronostic.Gagne;
						else if (this.ButsA < this.ButsB && this.Resultat_ButsA < this.Resultat_ButsB)
							return EtatPronostic.Gagne;
						else
							return EtatPronostic.Perdu;
					}
					else
						return EtatPronostic.EnCours;
				}
				else
					return EtatPronostic.Empty;
			}
		}
		public int? Points { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsNewProno { get; set; }
		public int VanqueurID
		{
			get
			{
				if (this.ButsA > this.ButsB)
				{
					return this.EquipeAID;
				}
				else if (this.ButsA < this.ButsB)
				{
					return this.EquipeBID;
				}
				else
				{
					if (this.PenaltiesA != null && this.PenaltiesB != null)
					{
						if (this.PenaltiesA > this.PenaltiesB)
							return this.EquipeAID;
						else
							return this.EquipeBID;
					}
					else
						return 0;
				}
			}
		}
		public GroupePronosticsModel GroupeModel { get; set; }

		public int? Resultat_ButsA { get; set; }
		public int? Resultat_ButsB { get; set; }
		public int? Resultat_PenaltiesA { get; set; }
		public int? Resultat_PenaltiesB { get; set; }

		public PronosticsModel(int pConcoursID, Match pMatch, bool pIsReadOnly)
		{
			this.ConcoursID = pConcoursID;
			this.MatchID = pMatch.ID;
			this.NumeroMatch = pMatch.NumeroMatch;
			this.DateHeure = pMatch.Date.ToShortDateString() + " à " + pMatch.Date.ToShortTimeString();
			this.IsReadOnly = pIsReadOnly;
		}
		public void SetEquipes(Equipe pEquipeDomicile, Equipe pEquipeExterieur)
		{
			this.EquipeAName = pEquipeDomicile.Libelle;
			this.EquipeAShortName = pEquipeDomicile.ShortName;
			this.EquipeAID = pEquipeDomicile.ID;
			this.LogoUrlEquipeA = pEquipeDomicile.Logo;
			this.EquipeBName = pEquipeExterieur.Libelle;
			this.EquipeBShortName = pEquipeExterieur.ShortName;
			this.EquipeBID = pEquipeExterieur.ID;
			this.LogoUrlEquipeB = pEquipeExterieur.Logo;
		}
		public void SetEquipes(Match pMatch)
		{
			this.EquipeAID = pMatch.EquipeA.ID;
			this.EquipeBID = pMatch.EquipeB.ID;
			this.EquipeAName = pMatch.EquipeA.Libelle;
			this.EquipeBName = pMatch.EquipeB.Libelle;
			this.EquipeAShortName = pMatch.EquipeA.ShortName;
			this.EquipeBShortName = pMatch.EquipeB.ShortName;
			this.LogoUrlEquipeA = pMatch.EquipeA.Logo;
			this.LogoUrlEquipeB = pMatch.EquipeB.Logo;
		}
		public void SetLibellesEquipesProbables(Match pMatch)
		{
			this.EquipeAName = pMatch.EquipePossibleDomicile_Libelle;
			this.EquipeAShortName = pMatch.EquipePossibleDomicile_Libelle;
			this.EquipeBName = pMatch.EquipePossibleExterieur_Libelle;
			this.EquipeBShortName = pMatch.EquipePossibleExterieur_Libelle;
			this.IsReadOnly = true;
		}
		public void SetScore(Pronostic pProno)
		{
			this.ButsA = pProno.ButsEquipeDomicile;
			this.ButsB = pProno.ButsEquipeExterieur;
			this.PenaltiesA = pProno.ButsPenaltiesEquipeDomicile;
			this.PenaltiesB = pProno.ButsPenaltiesEquipeExterieur;
		}
		public void SetScore(Match pMatch)
		{
			this.ButsA = pMatch.ButsEquipeDomicile;
			this.ButsB = pMatch.ButsEquipeExterieur;
			this.PenaltiesA = pMatch.ButsPenaltiesEquipeDomicile;
			this.PenaltiesB = pMatch.ButsPenaltiesEquipeExterieur;
		}

		public void SetResultatsMatch(Match pMatch, int pPoints)
		{
			this.Resultat_ButsA = pMatch.ButsEquipeDomicile;
			this.Resultat_ButsB = pMatch.ButsEquipeExterieur;
			this.Resultat_PenaltiesA = pMatch.ButsPenaltiesEquipeDomicile;
			this.Resultat_PenaltiesB = pMatch.ButsPenaltiesEquipeExterieur;
			this.Points = pPoints;
        }
	}

	public enum TypeSaisiePronostics
	{
		ReadOnly = 0,
		SaisieAvantDateLimite = 1,
		SaisieAvantDateMatch = 2,
		SaisieOnly = 3
	}
    public class GroupePronosticsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsChoosen { get; set; }
        public List<PronosticsModel> MatchsPronostics { get; set; }
        public TypePhaseFinale? TypePhaseFinale { get; set; }
        public List<PhaseGroupe.ClassementGroupeModel> Classement { get; set; }

        public GroupePronosticsModel()
        {
            this.MatchsPronostics = new List<PronosticsModel>();
            this.Classement = new List<PhaseGroupe.ClassementGroupeModel>();
        }

		public GroupePronosticsModel(PhaseFinale pPhaseFinale, string pTitreGroupe, bool pIsNewPronos)
		{
			this.ID = pPhaseFinale.ID;
			this.TypePhaseFinale = pPhaseFinale.TypePhaseFinale;
			switch (this.TypePhaseFinale)
			{
				case DAL.Pronos.TypePhaseFinale.TrenteDeuxieme:
					this.Name = this.ShortName = "32° de finale";
					break;
				case DAL.Pronos.TypePhaseFinale.Seizieme:
					this.Name = this.ShortName = "16° de finale";
					break;
				case DAL.Pronos.TypePhaseFinale.Huitieme:
					this.Name = this.ShortName = "Huitieme de finale";
					break;
				case DAL.Pronos.TypePhaseFinale.Quart:
					this.Name = this.ShortName = "Quart de finales";
					break;
				case DAL.Pronos.TypePhaseFinale.Demi:
					this.Name = this.ShortName = "Demi-finale";
					break;
				case DAL.Pronos.TypePhaseFinale.Finale:
					this.Name = this.ShortName = "Finale";
					break;
			}
			if (pIsNewPronos)
			{
				this.Name += " - Nouveau";
				this.ShortName += " - Nouveau";
			}
			this.IsChoosen = pTitreGroupe == this.Name;
			this.MatchsPronostics = new List<PronosticsModel>();
			this.Classement = new List<PhaseGroupe.ClassementGroupeModel>();
		}
		public GroupePronosticsModel(PhaseGroupe pGroupe, string pTitreGroupe)
		{
			this.ID = pGroupe.ID;
			this.Name = "Groupe " + pGroupe.Lettre;
			this.ShortName = pGroupe.Lettre;
			this.IsChoosen = pTitreGroupe == this.Name;
			this.MatchsPronostics = new List<PronosticsModel>();
			this.Classement = new List<PhaseGroupe.ClassementGroupeModel>();
		}
	}

	public class ScoreViewModel
	{
		public TypeScore TypeScore { get; set; }
		public int? Buts { get; set; }
		public int? Penalties { get; set; }
		public bool IsReadOnly { get; set; }
    }

	public class InformationsPronosticViewModel
	{
        public int ConcoursID { get; set; }
        public int MatchID { get; set; }
        public bool IsNewProno { get; set; }
        public List<Pronostic> ListePronostics { get; set; } 

		public List<DonutItemModel> StatsParVainqueur
		{
			get
			{
				if (this.ListePronostics.Any())
				{
					var libelleDomicile = this.ListePronostics.First().EquipeA.Libelle;
					var libelleExterieur = this.ListePronostics.First().EquipeB.Libelle;
					var nbDomicile = this.ListePronostics.Where(lp => lp.VainqueurID == lp.EquipeAID).Count();
					var nbExterieur = this.ListePronostics.Where(lp => lp.VainqueurID == lp.EquipeBID).Count();
					var nbNul = this.ListePronostics.Where(lp => lp.VainqueurID == 0).Count();

					return new List<DonutItemModel>()
					{
						new DonutItemModel() { label = libelleDomicile, value = nbDomicile },
						new DonutItemModel() { label = "Nul", value = nbNul },
						new DonutItemModel() { label = libelleExterieur, value = nbExterieur }
					};
				}
				return new List<DonutItemModel>();
			}
		}
		public List<DonutItemModel> StatsParScore
		{
			get
			{
				return this.ListePronostics.GroupBy(lp => lp.Score).Select(g => new DonutItemModel() { label = g.Key, value = g.Count() }).ToList();
			}
		}
	}

	public class DonutItemModel
	{
		public string label { get; set; }
		public int value { get; set; }
	}
    public class StatsObjectDataClassementItemModel
    {
        public string label { get; set; }
        public List<List<int>> data { get; set; }
    }
    public class ConcoursClassementViewModel
    {
        public List<Concours.ClassementConcoursModel> Classement { get; set; }
        public List<Concours.ClassementConcoursModel> ClassementProvisoire { get; set; }
    }
}

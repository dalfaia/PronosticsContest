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
        public EtatPronostic Etat { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsNewProno { get; set; }
		public int VanqueurID {
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

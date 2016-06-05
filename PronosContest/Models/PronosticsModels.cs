using PronosContest.DAL.Pronos;
using PronosContest.DAL.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PronosContest.Models
{
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
        public int ButsA { get; set; }
        public int ButsB { get; set; }
        public int MatchID { get; set; }
        public int ConcoursID { get; set; }
        public EtatPronostic Etat { get; set; }
        public bool IsReadOnly { get; set; }
    }

    public class GroupePronosticsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsChoosen { get; set; }
        public List<PronosticsModel> MatchsPronostics { get; set; }
        public List<PhaseGroupe.ClassementGroupeModel> Classement { get; set; }

        public GroupePronosticsModel()
        {
            this.MatchsPronostics = new List<PronosticsModel>();
            this.Classement = new List<PhaseGroupe.ClassementGroupeModel>();
        }
    }
}

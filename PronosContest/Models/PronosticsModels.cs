﻿using PronosContest.DAL.Pronos;
using PronosContest.DAL.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
}

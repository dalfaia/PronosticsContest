using PronosContest.DAL.Authentification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	public enum TypeDePronostic
	{
		ResultatFinal = 0,
		ScoreExact = 1
	}
	public enum EtatPronostic
	{
        [Display(Name = "Non renseigné")]
        Empty = 0,
        [Display(Name = "En cours")]
        EnCours = 1,
        [Display(Name = "Gagné")]
        Gagne = 2,
        [Display(Name = "Perdu")]
        Perdu = 3
	}
	public class Pronostic
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }
		public TypeDePronostic TypePronostic { get; set; }
		public EtatPronostic EtatPronostic { get; set; }

		public DateTime DateCreation { get; set; }

        public int? EquipeAID { get; set; }
        public int? EquipeBID { get; set; }
        public int ButsEquipeDomicile { get; set; }
		public int ButsEquipeExterieur { get; set; }
		public int ButsPenaltiesEquipeDomicile { get; set; }
		public int ButsPenaltiesEquipeExterieur { get; set; }

		[ForeignKey("CompteUtilisateur")]
		public int CompteUtilisateurID { get; set; }
		[ForeignKey("Match")]
		public int MatchID { get; set; }
		[ForeignKey("Concours")]
		public int ConcoursID{ get; set; }
		#endregion

		public Pronostic()
		{

		}

		#region Propriétés de navigation
		public virtual CompteUtilisateur CompteUtilisateur { get; set; }
		public virtual Match Match { get; set; }
		public virtual Concours Concours { get; set; }
        [ForeignKey("EquipeAID")]
        public virtual Equipe EquipeA { get; set; }
        [ForeignKey("EquipeBID")]
        public virtual Equipe EquipeB { get; set; }
        #endregion
    }
}

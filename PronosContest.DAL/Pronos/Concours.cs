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
	
	public enum EtatConcours
	{
		EnCours = 0,
		Termine = 1
	}
	public class Concours
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }
		
		public EtatConcours EtatConcours { get; set; }		
		public DateTime DateDebut { get; set; }
		public DateTime? DateFin { get; set; }

		[ForeignKey("Competition")]
		public int CompetitionID { get; set; }
		[ForeignKey("CompteUtilisateur")]
		public int CompteUtilisateurID { get; set; }
		#endregion

		public Concours()
		{
			this.Pronostics = new List<Pronostic>();
			this.ConcoursCompteUtilisateurs = new List<ConcoursCompteUtilisateur>();
		}

		#region Propriétés de navigation
		public virtual ICollection<ConcoursCompteUtilisateur> ConcoursCompteUtilisateurs { get; set; }
		public virtual ICollection<Pronostic> Pronostics { get; set; }
		public virtual Competition Competition { get; set; }
		public virtual CompteUtilisateur CompteUtilisateur { get; set; }
		#endregion
	}
}

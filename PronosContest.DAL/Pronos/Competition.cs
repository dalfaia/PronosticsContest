using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	public enum TypeDeSport
	{
		Football = 0,
		Rugby = 1
	}

	public class Competition
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }

		[Required]
		[StringLength(256)]
		[Column(TypeName = "VARCHAR")]
		public string Libelle { get; set; }

		public TypeDeSport TypeSport { get; set; }

		public DateTime DateDebut { get; set; }
		public DateTime DateFin { get; set; }
		#endregion

		public Competition()
		{
			this.Groupes = new List<PhaseGroupe>();
			this.PhasesFinales = new List<PhaseFinale>();
		}

		#region Propriétés de navigation
		public virtual ICollection<PhaseGroupe> Groupes { get; set; }
		public virtual ICollection<PhaseFinale> PhasesFinales { get; set; }
		#endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	public enum TypePhaseFinale
	{
		TrenteDeuxieme = 32, 
		Seizieme = 16, 
		Huitieme = 8,
		Quart = 4,
		Demi = 2,
		Finale = 1
	}
	public class PhaseFinale
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }
		
		public TypePhaseFinale TypePhaseFinale { get; set; }

		[ForeignKey("Competition")]
		public int CompetitionID { get; set; }
		#endregion

		#region Propriétés de navigation
		public virtual ICollection<Match> Matchs { get; set; }
		public virtual Competition Competition { get; set; }
		#endregion
	}
}

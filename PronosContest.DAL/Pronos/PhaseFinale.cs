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
        [Display(Name = "32° de finale")]
		TrenteDeuxieme = 32,
        [Display(Name = "16° de finale")]
        Seizieme = 16,
        [Display(Name = "Huitièmes de finale")]
        Huitieme = 8,
        [Display(Name = "Quarts de finale")]
        Quart = 4,
        [Display(Name = "Demis-finales")]
        Demi = 2,
        [Display(Name = "Finale")]
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	public class PhaseGroupe
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }
		
		public string Lettre { get; set; }

		[ForeignKey("Competition")]
		public int CompetitionID { get; set; }
		#endregion

		public PhaseGroupe()
		{
			this.Equipes = new List<Equipe>();
			this.Matchs = new List<Match>();
		}

		#region Propriétés de navigation
		public virtual ICollection<Equipe> Equipes { get; set; }
		public virtual ICollection<Match> Matchs { get; set; }
		public virtual Competition Competition { get; set; }
		#endregion
	}
}

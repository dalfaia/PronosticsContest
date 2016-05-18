using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	public class Match
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }

		public int EquipeAID { get; set; }
		public int EquipeBID { get; set; }

		[ForeignKey("PhaseGroupe")]
		public int? PhaseGroupeID { get; set; }

		[ForeignKey("PhaseFinale")]
		public int? PhaseFinaleID { get; set; }

		public DateTime Date { get; set; }
		public string Stade { get; set; }
		#endregion

		#region Propriétés de navigation
		[ForeignKey("EquipeAID")]
		public virtual Equipe EquipeA { get; set; }
		[ForeignKey("EquipeBID")]
		public virtual Equipe EquipeB { get; set; }
		public virtual PhaseGroupe PhaseGroupe { get; set; }
		public virtual PhaseFinale PhaseFinale { get; set; }
		#endregion
	}
}

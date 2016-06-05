using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	public class Equipe
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }

		[ForeignKey("PhaseGroupe")]
		public int? PhaseGroupeID { get; set; }

		public string Libelle { get; set; }
        public string ShortName { get; set; }

        public string Logo { get; set; }
		#endregion

		#region Propriétés de navigation
		public virtual PhaseGroupe PhaseGroupe { get; set; }
		#endregion
	}
}

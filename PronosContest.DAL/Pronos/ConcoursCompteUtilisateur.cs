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
	public class ConcoursCompteUtilisateur
	{
		#region Propriétés primitives
        [Key, Column(Order = 0)]
        [ForeignKey("CompteUtilisateur")]
		public int CompteUtilisateurID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Concours")]
        public int ConcoursID { get; set; }

        public DateTime Date { get; set; }
        #endregion

        public ConcoursCompteUtilisateur()
		{
		}

		#region Propriétés de navigation
        [ForeignKey("ConcoursID")]
		public virtual Concours Concours { get; set; }
        [ForeignKey("CompteUtilisateurID")]
        public virtual CompteUtilisateur CompteUtilisateur { get; set; }
		#endregion
	}
}

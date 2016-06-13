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
		public DateTime DateLimiteSaisie { get; set; }

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

        public List<ClassementConcoursModel> Classement()
        {
            var classementFinal = new List<ClassementConcoursModel>();

            foreach (var concoursUser in this.ConcoursCompteUtilisateurs)
            {
                var user = concoursUser.CompteUtilisateur;
                if (user != null)
                {
                    var elementClassement = new ClassementConcoursModel();
                    elementClassement.CompteUtilisateurID = user.ID;
                    elementClassement.NomComplet = user.Prenom + " " + user.Nom;
                    foreach (var p in this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID && c.Match != null && c.Match.ButsEquipeDomicile != null && c.Match.ButsEquipeExterieur != null))
                    {
                        var match = this.Competition.AllMatchs.Where(m => m.ID == p.MatchID).FirstOrDefault();
                        if (match != null)
                        {
                            if (match.VainqueurID == p.VainqueurID)
                                elementClassement.NombrePronosGagnes += 1;
                            else if (match.ButsEquipeDomicile != null && match.ButsEquipeExterieur != null)
                                elementClassement.NombrePronosPerdus += 1;
                            if (match.ButsEquipeDomicile == p.ButsEquipeDomicile && match.ButsEquipeExterieur == p.ButsEquipeExterieur)
                                elementClassement.NombreScoreExact += 1;
                        }
                    }
                    classementFinal.Add(elementClassement);
                }
            }
            return classementFinal.OrderByDescending(c => c.NombreScoreExact).OrderByDescending(c => c.Points).ToList();
        }

        public class ClassementConcoursModel
        {
            public int CompteUtilisateurID { get; set; }
            public string NomComplet { get; set; }
            public int NombreScoreExact { get; set; }
            public int NombrePronosGagnes { get; set; }
            public int NombrePronosPerdus { get; set; }
            public int Points
            {
                get
                {
                    return (this.NombreScoreExact * 3) + this.NombrePronosGagnes;
                }
            }
        }
    }
}

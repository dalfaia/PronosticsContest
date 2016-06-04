using PronosContest.Core;
using PronosContest.DAL.Pronos;
using PronosContest.DAL.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Authentification
{
	public enum CompteUtilisateurRole
	{
		Administrateur = 0,
		Utilisateur = 1
	}
	public class CompteUtilisateur
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[StringLength(256)]
		[Column(TypeName = "VARCHAR")]
		public string Email { get; set; }

		[StringLength(256)]
		[Column(TypeName = "VARCHAR")]
		public string Prenom { get; set; }

		public string Nom { get; set; }
		
		public CompteUtilisateurRole Role { get; set; }

		[Required]
		public byte[] Password { get; set; }

		public Adresse Adresse { get; set; }

        public CompteUtilisateur()
        {
            this.Role = CompteUtilisateurRole.Utilisateur;
            this.Adresse = new Adresse();
            this.ConcoursCompteUtilisateurs = new List<ConcoursCompteUtilisateur>();
        }
        public CompteUtilisateur(string pEmail,string pPassword,string pNom, string pPrenom, Adresse pAdresse)
        {
            this.Email = pEmail;
            this.Password = pPassword.ToPasswordHash();
            this.Nom = pNom;
            this.Prenom = pPrenom;
            this.Role = CompteUtilisateurRole.Utilisateur;
            this.Adresse = new Adresse();
            this.ConcoursCompteUtilisateurs = new List<ConcoursCompteUtilisateur>();
        }

        public virtual ICollection<Concours> Concours { get; set; }
        public virtual ICollection<ConcoursCompteUtilisateur> ConcoursCompteUtilisateurs { get; set; }
    }
}

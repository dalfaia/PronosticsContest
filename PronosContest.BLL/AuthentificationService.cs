using PronosContest.Core;
using PronosContest.DAL;
using PronosContest.DAL.Authentification;
using PronosContest.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PronosContest.BLL
{
	public class AuthentificationService
	{
		private PronosContestContext _pronosContestContextDatabase;

		internal AuthentificationService(PronosContestContext pPronosContestContextDatabase)
		{
			_pronosContestContextDatabase = pPronosContestContextDatabase;
		}
		public IEnumerable<CompteUtilisateur> GetAllUser()
		{
			return _pronosContestContextDatabase.CompteUtilisateurs.ToList();
		}

        public CompteUtilisateur GetUserById(int pId)
        {
            return _pronosContestContextDatabase.CompteUtilisateurs.Where(cu => cu.ID == pId).FirstOrDefault();
        }
        public CompteUtilisateur Connexion(string pEmail, string pPassword)
        {
            byte[] passwordHashed = pPassword.ToPasswordHash();
            return _pronosContestContextDatabase.CompteUtilisateurs.Where(cu => cu.Email == pEmail && cu.Password == passwordHashed).FirstOrDefault();
        }
        public CompteUtilisateur Inscrire(string pEmail,string pPassword, string pNom,string pPrenom, Adresse pAdresse)
        {
            CompteUtilisateur newUser = new CompteUtilisateur(pEmail, pPassword, pNom, pPrenom, pAdresse);
            using (TransactionScope scope = new TransactionScope())
            {
                newUser = _pronosContestContextDatabase.CompteUtilisateurs.Add(newUser);
                _pronosContestContextDatabase.SaveChanges();
                scope.Complete();
            }
            return newUser;
        }
    }
}

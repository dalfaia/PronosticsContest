using PronosContest.DAL;
using PronosContest.DAL.Authentification;
using PronosContest.DAL.Pronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.BLL
{
	public class PronosService
	{
		private PronosContestContext _pronosContestContextDatabase;

		internal PronosService(PronosContestContext pPronosContestContextDatabase)
		{
			_pronosContestContextDatabase = pPronosContestContextDatabase;
		}
		public List<Competition> GetCompetitions()
		{
			return _pronosContestContextDatabase.Competitions.ToList();
		}
		public List<Concours> GetConcoursByUserID(int pId)
        {
            return _pronosContestContextDatabase.Concours.Where(c => c.CompteUtilisateurs.Where(user => user.ID == pId).Any()).ToList();
        }
    }
}

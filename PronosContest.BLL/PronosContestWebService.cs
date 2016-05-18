using PronosContest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PronosContest.BLL
{
    public class PronosContestWebService : IDisposable
    {
		private const string PRONOSCONTEST_WEB_SERVICE_NAME = "PronosContestWebService";

		private PronosContestContext _pronosContestContextDatabase;
		public static PronosContestWebServiceContext PronosContestWebServiceContext { get; set; }

		static PronosContestWebService()
		{
			PronosContestWebServiceContext = new PronosContestWebServiceContext();
		}
		private PronosContestWebService()
		{
			_pronosContestContextDatabase = new PronosContestContext();
		}
		public static PronosContestWebService GetService()
		{
			var pronosContestWebService = PronosContestWebServiceContext.HttpContext.Items[PRONOSCONTEST_WEB_SERVICE_NAME] as PronosContestWebService;
			if (pronosContestWebService == null)
			{
				pronosContestWebService = new PronosContestWebService();
				PronosContestWebServiceContext.HttpContext.Items[PRONOSCONTEST_WEB_SERVICE_NAME] = pronosContestWebService;
			}
			return pronosContestWebService;
		}
		
		void IDisposable.Dispose()
		{
			_pronosContestContextDatabase.Dispose();
			PronosContestWebServiceContext.HttpContext.Items.Remove(PRONOSCONTEST_WEB_SERVICE_NAME);
		}
		public AuthentificationService AuthenticationService
		{
			get
			{
				return new AuthentificationService(_pronosContestContextDatabase);
			}
		}
		public PronosService PronosService
		{
			get
			{
				return new PronosService(_pronosContestContextDatabase);
			}
		}

		public StartService StartService
		{
			get
			{
				return new StartService(_pronosContestContextDatabase);
			}
		}
	}
}

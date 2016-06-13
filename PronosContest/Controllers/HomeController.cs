using PronosContest.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PronosContest.Controllers
{
	public class HomeController : PronosContestControllerBase
	{
		public ActionResult Index()
		{
			//BLL.PronosContestWebService.GetService().StartService.StartDatabase();
			//BLL.PronosContestWebService.GetService().StartService.UpdateUrlMatchs();
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}
	}
}
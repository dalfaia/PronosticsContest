using PronosContest.BLL;
using PronosContest.DAL.Pronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PronosContest.Controllers
{
	public class PronosticsController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
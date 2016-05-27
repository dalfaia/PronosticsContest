using PronosContest.BLL;
using PronosContest.Code;
using PronosContest.Core;
using PronosContest.DAL.Pronos;
using PronosContest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PronosContest.Controllers
{
	public class PronosticsController : PronosContestControllerBase
	{
		public ActionResult Concours()
		{
			var concours = PronosContestWebService.GetService().PronosService.GetConcoursByUserID(this.UserID.Value);
			return View(concours);
		}
		[HttpGet]
		public ActionResult SearchConcours()
		{
			SearchConcoursModel model = new SearchConcoursModel();
			return View(model);
		}
		[HttpPost]
		public ActionResult SearchConcours(SearchConcoursModel pModel)
		{
			if (pModel != null)
			{
				pModel.Resultats = PronosContestWebService.GetService().PronosService.SearchConcours(pModel.Recherche, this.UserID.Value);
				return View(pModel);
			}
			return View();
		}

		public ActionResult SaisirPronostics()
        {
            return View();
        }
	}
}
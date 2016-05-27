using PronosContest.DAL.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PronosContest.Code
{
	public abstract class PronosContestControllerBase : Controller
	{
		protected int? UserID
		{
			get
			{
				if (Session["PronosContest.UserID"] != null && Session["PronosContest.UserID"] is int)
					return (int)Session["PronosContest.UserID"];
				return null;
			}
			set
			{
				Session["PronosContest.UserID"] = value;
			}
		}

		protected CompteUtilisateur CurrentUser
		{
			get
			{
				if (Session["PronosContest.CurrentUser"] != null && Session["PronosContest.CurrentUser"] is CompteUtilisateur)
					return (CompteUtilisateur)Session["PronosContest.CurrentUser"];
				return null;
			}
			set
			{
				Session["PronosContest.CurrentUser"] = value;
			}
		}

		public PronosContestControllerBase()
		{
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
		}
		public ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Public");
		}
	}
}

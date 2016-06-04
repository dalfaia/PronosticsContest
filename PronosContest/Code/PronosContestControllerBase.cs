using PronosContest.DAL.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PronosContest.Code
{
	public abstract class PronosContestControllerBase : Controller
	{
		protected int? UserID
		{
			get
			{
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                if (authManager.User != null)
                {
                    var userInfo = authManager.User.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (userInfo != null)
                        return Core.Helper.GetIntFromString(userInfo.Value);
                }
                return null;
			}
		}
        protected string UserName
        {
            get
            {
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                if (authManager.User != null)
                {
                    var userInfo = authManager.User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();
                    if (userInfo != null)
                        return userInfo.Value;
                }
                return null;
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

using PronosContest.BLL;
using PronosContest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PronosContest.Controllers
{
	[AllowAnonymous]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult LogIn(string returnUrl, string pEmail = "")
        {
			var model = new LogInModel {
                Email = pEmail,
				ReturnUrl = returnUrl
			};
            return View(model);
        }

        [HttpPost]
		public ActionResult LogIn(LogInModel pModel)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

            var user = PronosContestWebService.GetService().AuthenticationService.Connexion(pModel.Email, pModel.Password);
            if (user != null)
            {
                
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Prenom),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Surname, user.Nom)
                }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);
                return Redirect(GetRedirectUrl(pModel.ReturnUrl));
            }
            
			// user authN failed
			ModelState.AddModelError("", "Votre identifiants sont incorrects !");
			return View();
		}

        [HttpGet]
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut();
            return RedirectToAction("LogIn");
        }

        private string GetRedirectUrl(string returnUrl)
		{
			if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
			{
				return Url.Action("index", "home");
			}

			return returnUrl;
		}

		[HttpGet]
		public ActionResult Inscription()
		{
			var model = new InscriptionModel();
			return View(model);
		}

		[HttpPost]
		public ActionResult Inscription(InscriptionModel pModel)
		{
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (pModel != null)
            {
                var userCreated = PronosContestWebService.GetService().AuthenticationService.Inscrire(pModel.Email, pModel.Password, pModel.Nom, pModel.Prenom, pModel.Adresse);
                return RedirectToAction("LogIn", new { returnUrl = "", pEmail = userCreated.Email });
            }

            return View();
		}
	}
}
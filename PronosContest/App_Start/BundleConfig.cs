using System.Web;
using System.Web.Optimization;

namespace PronosContest
{
	public class BundleConfig
	{
		// Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/site.css", 
					  "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
					  "~/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/bower_components/morrisjs/morris.css",
                      "~/bower_components/font-awesome/css/font-awesome.min.css"
                      ));

			bundles.Add(new ScriptBundle("~/Content/js").Include(
					  "~/bower_components/jquery/dist/jquery.min.js",
					  "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                      "~/bower_components/raphael/raphael.js",
                      "~/bower_components/morrisjs/morris.min.js",
                      "~/bower_components/metisMenu/dist/metisMenu.min.js",
                      "~/bower_components/bootstrap/js/transition.js",
                      "~/bower_components/bootstrap/js/collapse.js",
                      "~/bower_components/flot/jquery.flot.js"
                    ));	
		}
	}
}

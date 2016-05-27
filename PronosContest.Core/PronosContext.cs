using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PronosContest.Core
{
    public static class PronosContext
    {
        public static int? UserID
        {
            get
            {
                return Helper.GetIntFromString(HttpContext.Current.Session["UserID"] as string);
            }
			set
			{
				HttpContext.Current.Session["UserID"] = value;
            }
        }
    }
}

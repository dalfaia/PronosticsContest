using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PronosContest.BLL
{
    public class PronosContestWebServiceContext
    {
		HttpContextBase _context = null;
		public PronosContestWebServiceContext()
		{
			return;
		}
		public PronosContestWebServiceContext(HttpContextBase pContexte)
		{
			_context = pContexte;
			return;
		}
		public HttpContextBase HttpContext
		{
			get
			{
				if (_context != null) return _context;
				return new HttpContextWrapper(System.Web.HttpContext.Current);
			}
		}
	}
}

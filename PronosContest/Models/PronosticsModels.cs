using PronosContest.DAL.Pronos;
using PronosContest.DAL.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PronosContest.Models
{
	public class SearchConcoursModel
	{
		[DataType(DataType.Text)]
		public string Recherche { get; set; }

		public List<Concours> Resultats { get; set; }

		public SearchConcoursModel()
		{
			this.Resultats = new List<Concours>();
		}
	}	
}

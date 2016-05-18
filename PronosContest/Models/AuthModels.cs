using PronosContest.DAL.Shared;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PronosContest.Models
{
	public class LogInModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[HiddenInput]
		public string ReturnUrl { get; set; }
	}

	public class InscriptionModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		public string Password2 { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Prenom { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Nom { get; set; }

		public Adresse Adresse { get; set; }

	}
}

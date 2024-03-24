using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{

		[Required(ErrorMessage = "FristName is Required ")]
		public string FName { get; set; }


		[Required(ErrorMessage = "LastName is Required ")]
		public string LName { get; set; }


		[Required(ErrorMessage = "Email is Required ")]

		[EmailAddress(ErrorMessage ="Invailed Email")]
		public string	Email { get; set; }


		[Required(ErrorMessage = "Password is Required ")]
		[DataType(DataType.Password)]
		
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword is Required ")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage ="Password Dosn`t match")]
		public string ConfirmPassword { get; set; }


		public bool IsAgree { get; set; }




	}
}

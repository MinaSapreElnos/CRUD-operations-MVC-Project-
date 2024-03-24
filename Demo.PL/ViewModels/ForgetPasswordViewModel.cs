using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is Required ")]

		[EmailAddress(ErrorMessage = "Invailed Email")]
		public string Email { get; set; }
	}
}

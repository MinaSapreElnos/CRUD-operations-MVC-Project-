using Demo.DAL.Models;
using Demo.PL.helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<AppLication_User> _userManager;
        private readonly SignInManager<AppLication_User> _signInManager;

        public AcountController(UserManager<AppLication_User> userManager,
            SignInManager<AppLication_User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new AppLication_User()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,

                    IsAgree = model.IsAgree,

                    FName = model.FName,

                    LName = model.LName,
                };

                var result = await _userManager.CreateAsync(User, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View(model);
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var result = await _userManager.CheckPasswordAsync(User, model.Password);
                    if (result)
                    {
                        var LoginResult = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (LoginResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password Is Incorrect ");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is Not Exsited");
                }

            }
            return View(model);
        }



        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }


        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]

        public async Task< IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(User);

                    var ResetPasswordLink = Url.Action("ResetPassword","Acount",new {email = User.Email ,Token=token},Request.Scheme);

                    var Email = new Email()
                    {

                        Subject = "Reset Password",

                        To = model.Email,

                        Body = ResetPasswordLink,

                    };
                    EmailSetting.SendEmail(Email);

                    return RedirectToAction(nameof( checkYourInpox ));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is Not Exsited");
                    //return View("ForgetPassword", model);
                }
                

            }
            return View("ForgetPassword", model);
        }

        public IActionResult checkYourInpox()
        {
            return View();
        }

        public IActionResult ResetPassword( string Email ,string Token)
        {
            TempData["Email"] = Email;
            TempData["token"] = Token;
            return View();
        }

        [HttpPost]

        public async Task< IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                string email= TempData["email"] as string;
                string token= TempData["token"] as string;

                var User = await _userManager.FindByEmailAsync(email);

             var Result=  await  _userManager.ResetPasswordAsync(User, token,model.NewPassword);
                if(Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var item in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }

            }
           
                return View(model);
            
        }



	}

    //Zxd7Pm7z@mE5Rhr

}

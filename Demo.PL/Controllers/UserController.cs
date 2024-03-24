using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController :Controller
	{
		private readonly UserManager<AppLication_User> _userManager;
		private readonly IMapper _mapper;
		public UserController(UserManager<AppLication_User> userManager ,IMapper mapper )
        {
			_userManager = userManager;
			_mapper = mapper;	
		}


		public async Task< IActionResult> Index( string Search)
		{
			if(string.IsNullOrEmpty(Search)) 
			{
				var Users = await _userManager.Users.Select(
					U => new UsersViewModel()
					{
					 Id = U.Id ,
					 FName =U.FName,
					 LName =U.LName,
					 Email =U.Email,
					 PhoneNumber =U.PhoneNumber,
					 Roles = _userManager.GetRolesAsync(U).Result
					}).ToListAsync();

				return View(Users);

			}
			else
			{
				var User = await _userManager.FindByEmailAsync(Search);
				var MappedUser = new UsersViewModel()
				{
					Id = User.Id,
					FName = User.FName,
					LName = User.LName,
					Email = User.Email,
					PhoneNumber = User.PhoneNumber,
					Roles = _userManager.GetRolesAsync(User).Result
				};
				return View ( new List<UsersViewModel> { MappedUser });
			}
		}



		public async Task< IActionResult> Details(string  Id ,string ViewName= "Details") 
		{
			if (Id is null)
				return BadRequest();

		 var User= await	_userManager.FindByIdAsync(Id);
			if(User is  null)
				return NotFound();

			var MappedUser = _mapper.Map< AppLication_User ,UsersViewModel>(User);

			return View( ViewName, MappedUser );
		}
    

		public async Task< IActionResult> Edit (string Id)
		{
			return await Details(Id ,"Edit"); 
		}

		[HttpPost]

		public async Task< IActionResult> Edit(UsersViewModel model , string Id)
		{
			if(Id!=model.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var User = await _userManager.FindByIdAsync(Id);
					User.FName = model.FName;
					User.LName = model.LName;
					User.PhoneNumber = model.PhoneNumber;

				  await	_userManager.UpdateAsync(User);

					return RedirectToAction(nameof(Index));
					
					

                }
				catch (Exception EX)
				{

					ModelState.AddModelError(string.Empty, EX.Message);
				}


			}
			return View(model);
		}
	

		public async Task< IActionResult> Delete(string Id) 
		{
            return await Details(Id, "Delete");
        }


		public async Task< IActionResult> ComfirmDelete(string Id) 
		{

			try
			{
                var User = await _userManager.FindByIdAsync(Id);
                await _userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));

            }
			catch (Exception EX)
			{

				ModelState.AddModelError(string.Empty,EX.Message);
				return View(nameof(Index));
			}

		}



		
    }

}

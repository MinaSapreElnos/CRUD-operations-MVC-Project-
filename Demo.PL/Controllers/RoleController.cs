using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
         public async Task<IActionResult> Index(string SearchView) 
        {
            if (string.IsNullOrEmpty(SearchView))
            {
                var Roles = await _roleManager.Roles.ToListAsync();

                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewMOdel>>(Roles);

                return View(MappedRole);

            }
            else
            {
                var Role = await _roleManager.FindByNameAsync(SearchView);

                var MappedRple = _mapper.Map<IdentityRole, RoleViewMOdel>(Role);

                return View(new List<RoleViewMOdel>() { MappedRple });


            }


        }

        public IActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleViewMOdel model)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = _mapper.Map<RoleViewMOdel,IdentityRole>(model); 

                await _roleManager.CreateAsync(MappedRole);
                return RedirectToAction("Index");
            }

            return View(model);
            }



        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();

            var Role = await _roleManager.FindByIdAsync(Id);
            if (Role is null)
                return NotFound();

            var MappedRole = _mapper.Map<IdentityRole, RoleViewMOdel>(Role);

            return View(ViewName, MappedRole);
        }


        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]

        public async Task<IActionResult> Edit(RoleViewMOdel model, string Id)
        {
            if (Id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(Id);
                    Role.Name = model.RoleName;
                 

                    await _roleManager.UpdateAsync(Role);

                    return RedirectToAction(nameof(Index));



                }
                catch (Exception EX)
                {

                    ModelState.AddModelError(string.Empty, EX.Message);
                }


            }
            return View(model);
        }


        public async Task<IActionResult> Delete(string Id)
        {
            return await Details(Id, "Delete");
        }


        public async Task<IActionResult> ComfirmDelete(string Id)
        {

            try
            {
                var Role = await _roleManager.FindByIdAsync(Id);
                await _roleManager.DeleteAsync(Role);  
                return RedirectToAction(nameof(Index));

            }
            catch (Exception EX)
            {

                ModelState.AddModelError(string.Empty, EX.Message);
                return View(nameof(Index));
            }

        }

    }



}

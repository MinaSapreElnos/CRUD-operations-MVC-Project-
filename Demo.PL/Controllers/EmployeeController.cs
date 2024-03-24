using AutoMapper;
using Demo.BLL.Interface;
using Demo.BLL.Repository;
using Demo.DAL.Models;
using Demo.PL.helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController( IUnitOfWork unitOfWork ,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task< IActionResult> Index()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            var mapedEMP = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mapedEMP);
        }



        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepository.GetAll();

            return View();
        }


        [HttpPost]
        public async Task< IActionResult> Create(EmployeeViewModel employeeVM) 
        {
            if (ModelState.IsValid)
            {
               employeeVM.ImageName = DecomentSetting.Upload(employeeVM.Image, "Imege");

                var EmployeeMaped=_mapper.Map<EmployeeViewModel,Employee>(employeeVM);

              await  _unitOfWork.EmployeeRepository.AddAsync(EmployeeMaped);

                var result= await _unitOfWork.completeAsync();

                if (result > 0)
                {
                    TempData["createMassage"] = "One Employee Is Created ";
                }
                return RedirectToAction(nameof(Index));
            }


            return View(employeeVM);

        }


        public async Task< IActionResult> Details(int? id ,string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

         var Employee= await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);

            if (Employee == null)
                return NotFound();

            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);

            return View(ViewName, MappedEmp); 
            
        }





        [HttpGet]
        public async Task< IActionResult> Edit(int? id)
        {
            ViewBag.departments= _unitOfWork.EmployeeRepository.GetAllAsync();

            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task< IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MapedEmploye = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(MapedEmploye);
                 await   _unitOfWork.completeAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }



        public async Task< IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            return await Details(id, "Delete");

            //var department = _IDepartmentRepository.GatById(id.Value);

            //return View(department);
        }

        [HttpPost]
        public async  Task< IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            var mappedEMP= _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
          _unitOfWork.EmployeeRepository.Delete(mappedEMP);

            var result =  await _unitOfWork.completeAsync();

            if (result > 0)
            {
                DecomentSetting.Delete(employeeVM.ImageName, "Imege");
            }

            return RedirectToAction(nameof(Index));
        }



    }
}

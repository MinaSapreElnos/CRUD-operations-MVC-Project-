using Demo.BLL.Interface;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController( IUnitOfWork unitOfWork )
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task< IActionResult > Index()
        {

            var Department = await _unitOfWork.DepartmentRepository.GetAllAsync();

           await  _unitOfWork.completeAsync();

            return View(Department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult > Create(Department department)
        {
            if (ModelState.IsValid)
            {

                 await _unitOfWork.DepartmentRepository.AddAsync(department);

                var result = await _unitOfWork.completeAsync();

                if (result > 0)
                {
                    TempData["massage"] = " Department is Created ";
                }

                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }


        public async  Task< IActionResult> Details(int? id ,string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //return Bad code 400 //
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);

            await _unitOfWork.completeAsync();

            if (department is null)
                return NotFound();

            return View(ViewName, department); ;

        }

        public async Task< IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();

            return View(department);

        }

        [HttpPost]
        public async Task< IActionResult> Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                 await   _unitOfWork.completeAsync();
                   
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            return View(department);
        }

        public async Task< IActionResult > Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            return  await Details(id, "Delete");

            //var department = _IDepartmentRepository.GatById(id.Value);

            //return View(department);
        }

        [HttpPost]
        public async  Task< IActionResult> Delete( [FromRoute] int id ,Department department )
        {
            if(id != department.Id)
                return BadRequest(); 

            _unitOfWork.DepartmentRepository.Delete(department);
          await  _unitOfWork.completeAsync();


            return RedirectToAction(nameof(Index));
        }

    }
   
}

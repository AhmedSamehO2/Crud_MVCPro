using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneat.BLL.Interfaces;
using Sneat.BLL.Specifications;
using Sneat.DAL.Entity;
using Sneat.PL.Helper;
using Sneat.PL.ViewModel;

namespace Sneat.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchname)
        {
            IEnumerable<Employee> model;
            if (string.IsNullOrEmpty(searchname))
            {
                var spec = new EmployeeWithDepartmentSpec();
                model = await _unitOfWork.EmployeeRep.GetAllAsyncWithSpec(spec);

            }
            else
            {
                model = _unitOfWork.EmployeeRep.GetEmpByName(searchname);
            }
            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(model);
            return View(result);
        }

        
        public async Task<IActionResult> Search(string searchname)
        {
            IEnumerable<Employee> model;
            if (string.IsNullOrEmpty(searchname))
            {
                var spec = new EmployeeWithDepartmentSpec();
                model = await _unitOfWork.EmployeeRep.GetAllAsyncWithSpec(spec);
            }
            else
            {
                model = _unitOfWork.EmployeeRep.GetEmpByName(searchname);
            }
            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(model);
            return PartialView("EmployeeTablePartialView", result);
        }




        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRep.getAllAsync().Result;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PhotoUrl = DocumentSettings.UploadFile(model.Photo, "Images");
                model.CvUrl = DocumentSettings.UploadFile(model.Cv, "Docs");

                var result = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRep.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            ViewBag.Departments = _unitOfWork.DepartmentRep.getAllAsync().Result;
            var spec = new EmployeeWithDepartmentSpec(id.Value);
            var model = await _unitOfWork.EmployeeRep.GetByIdAsyncWithSpec(spec);
            if (model is null)
                return NotFound();
            var result = _mapper.Map<EmployeeViewModel>(model);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model, [FromRoute] int id)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Photo is not null)
                    {
                        model.PhotoUrl = DocumentSettings.UploadFile(model.Photo,"Images");
                    }
                    var result = _mapper.Map<Employee>(model);
                    _unitOfWork.EmployeeRep.Update(result);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            ViewBag.Departments = _unitOfWork.DepartmentRep.getAllAsync().Result;
            var model = await _unitOfWork.EmployeeRep.GetByIdAsync(id.Value);
            if (model is null)
                return NotFound();
            var result = _mapper.Map<EmployeeViewModel>(model);
            return View(result);
        }

        [HttpPost]
        public async Task <IActionResult> Delete(EmployeeViewModel model, [FromRoute] int id)
        {
            if (id != model.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var result = _mapper.Map<Employee>(model);
                    _unitOfWork.EmployeeRep.Delete(result);
                var data =  await  _unitOfWork.CompleteAsync();
                    if(data>0)
                    {
                        DocumentSettings.DeleteFile(model.PhotoUrl, "Images");
                        DocumentSettings.DeleteFile(model.CvUrl, "Docs");
                    }
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task <IActionResult> Detalis(int id)
        {
            var spec = new EmployeeWithDepartmentSpec(id);
            var data = await _unitOfWork.EmployeeRep.GetByIdAsyncWithSpec(spec);
            var result = _mapper.Map<EmployeeViewModel>(data);

            return Json(result);
        }


    }
}

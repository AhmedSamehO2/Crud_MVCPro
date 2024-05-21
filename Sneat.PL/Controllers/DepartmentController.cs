using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneat.BLL.Interfaces;
using Sneat.DAL.Entity;
using Sneat.PL.ViewModel;

namespace Sneat.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task <IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRep.getAllAsync();
            var results = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(DepartmentViewModel model)
        {
          if(ModelState.IsValid)
            {
                var result = _mapper.Map<Department>(model);
                await _unitOfWork.DepartmentRep.AddAsync(result);
                int data = await _unitOfWork.CompleteAsync();
                if (data > 0)
                {
                    TempData["Message"] = "Department Is Created";
                }
              return  RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task <IActionResult> Update(int? id)
        {
            if (id is null)
                return BadRequest();
            var model = await _unitOfWork.DepartmentRep.GetByIdAsync(id.Value);
            if(model is null)
                return NotFound();
            var result = _mapper.Map<DepartmentViewModel>(model);
            return View(result);
        }

        [HttpPost]
        public async Task <IActionResult> Update(DepartmentViewModel model, [FromRoute] int id)
        {
            if(id !=model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _mapper.Map<Department>(model);
                    _unitOfWork.DepartmentRep.Update(result);
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


        public async Task <IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var model = await _unitOfWork.DepartmentRep.GetByIdAsync(id.Value);
            if(model is null)
                return NotFound();
            var result = _mapper.Map<DepartmentViewModel>(model);
            return View(result);
        }
        [HttpPost]
        public async Task <IActionResult> Delete(DepartmentViewModel model, [FromRoute] int id)
        {
            if(id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _mapper.Map<Department>(model);
                    _unitOfWork.DepartmentRep.Delete(result);
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
    }
}

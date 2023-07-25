using AutoMapper;
using Magic_Villa_Utility;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto;
using Magic_Villa_Web.Models.VM;
using Magic_Villa_Web.Services;
using Magic_Villa_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;

namespace Magic_Villa_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberServices _services;
        private readonly IVillaServices _villaServices;
        private readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberServices services, IMapper mapper, IVillaServices villaServices)
        {
            _services = services;
            _villaServices = villaServices;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<VillaNumberDto> list = new();
            var respone = await _services.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(respone.Result));
            }
            return View(list);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNoCreateVM villaNumberVM = new();
            var respone = await _villaServices.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                villaNumberVM.Villalist = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(respone.Result))
                                          .Select(i => new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.Id.ToString()
                                          });
            }
            return View(villaNumberVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNoCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _services.CreateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages != null && response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMessages", "Villa Number is already Exist");
                    }
                }
            }
            var resp = await _villaServices.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.Villalist = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateVillaNumber(int VillaNo)
        {
            VillaNoUpdateVM villaNoVM = new();
            var respone = await _services.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                VillaNumberDto model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(respone.Result));
                villaNoVM.VillaNumber = _mapper.Map<VillNoUpdatedDto>(model);
            }
            respone = await _villaServices.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                villaNoVM.Villalist = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(respone.Result))
                                          .Select(i => new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.Id.ToString()
                                          });
                return View(villaNoVM);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNoUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _services.UpdateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                //else
                //{
                //    if (response.ErrorMessages.Count > 0)
                //    {
                //        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                //    }
                //}
            }

            var resp = await _villaServices.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            {
                if (resp != null && resp.IsSuccess)
                {
                    model.Villalist = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                }
                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteVillaNumber(int VillaNo)
        {
            VillaNoDeleteVM villaNoVM = new();
            var respone = await _services.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                VillaNumberDto model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(respone.Result));
                villaNoVM.VillaNumber = model;
            }
            respone = await _villaServices.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                villaNoVM.Villalist = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(respone.Result))
                                          .Select(i => new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.Id.ToString()
                                          });
                return View(villaNoVM);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNoDeleteVM model)
        {

            var respone = await _services.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}

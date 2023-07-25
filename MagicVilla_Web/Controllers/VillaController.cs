using AutoMapper;
using Magic_Villa_Utility;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto;
using Magic_Villa_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Magic_Villa_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaServices _villaServices;
        private readonly IMapper _mapper;
        public VillaController(IVillaServices villaServices, IMapper mapper)
        {
            _villaServices = villaServices;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new();
            var respone = await _villaServices.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(respone.Result));
            }
            return View(list);
        }
        [Authorize(Roles ="admin")]
        public IActionResult CreateVilla()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var respone = await _villaServices.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Villa Created Succesfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Error Encountered";
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var respone = await _villaServices.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(respone.Result));
                return View(_mapper.Map<VillaUpdateDto>(model));
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var respone = await _villaServices.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Villa Updated Succesfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Error Encountered";
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var respone = await _villaServices.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteVilla(VillaDTO model)
        {

            var respone = await _villaServices.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                TempData["success"] = "Villa Deleted Succesfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error Encountered";
            return View(model);
        }
    }
}


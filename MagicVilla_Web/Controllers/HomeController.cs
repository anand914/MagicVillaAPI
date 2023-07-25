using AutoMapper;
using Magic_Villa_Utility;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto;
using Magic_Villa_Web.Services.IServices;
using MagicVilla_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
		private readonly IVillaServices _villaServices;
		private readonly IMapper _mapper;
        public HomeController(IVillaServices villaServices, IMapper mapper)
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

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
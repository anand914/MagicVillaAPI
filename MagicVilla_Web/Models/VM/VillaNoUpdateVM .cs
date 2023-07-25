﻿using Magic_Villa_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Magic_Villa_Web.Models.VM
{
    public class VillaNoUpdateVM
    {
        public VillaNoUpdateVM()
        {
            VillaNumber = new VillNoUpdatedDto();
        }
        public VillNoUpdatedDto VillaNumber { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> Villalist { get; set; }
    }
}

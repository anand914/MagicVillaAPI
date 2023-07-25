﻿using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Web.Models.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string? SpecialDetails { get; set; }
        public VillaDTO villa { get; set; }
    }
}

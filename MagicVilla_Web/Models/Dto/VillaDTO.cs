﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Web.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int Sqft { get; set; }
		public int Occupancy { get; set; }
		public string? Details { get; set; }
        public double Rate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Amenity { get; set; }
    }
}

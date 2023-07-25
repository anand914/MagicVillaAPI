﻿using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Web.Models.Dto
{
    public class VillaCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; } 
        public int Sqft { get; set; }
        public string ImageUrl { get; set; }
        public string? Amenity { get; set; }
    }
}

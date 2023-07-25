using System.ComponentModel.DataAnnotations;

namespace Magic_VillaAPI.Models.Dto
{
    public class VillNoUpdatedDto
    {
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string? SpecialDetails { get; set; }
    }
}

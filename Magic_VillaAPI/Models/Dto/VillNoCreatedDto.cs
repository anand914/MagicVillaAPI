using System.ComponentModel.DataAnnotations;

namespace Magic_VillaAPI.Models.Dto
{
    public class VillNoCreatedDto
    {
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string? SpecialDetails { get; set; }
    }
}

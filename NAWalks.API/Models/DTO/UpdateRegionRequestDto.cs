using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [MinLength(3 , ErrorMessage = "Code has to be a maximum of 3 charecters ")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 charecters ")]
        public string? Code { get; set; }
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 charecters ")]
        public string? Name { get; set; }
        public string? RegionImageUrl { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NZWalks.API.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File {  get; set; }
        [Required]
        public string fileName { get; set; }
       
        public string? fileDescription { get; set; }
    
    }
}

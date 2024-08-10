using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {

        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string fileName {  get; set; }
        public string? fileDescription {  get; set; }
        public string fileExtension { get; set; }
        public long fileSizeInBytes {  get; set; }
        public string filePath { get; set; }


    }
}

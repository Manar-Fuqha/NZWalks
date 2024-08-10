using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImagesController(IMapper mapper , IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requset)
        {
            ValidateUploadFile(requset);
            if (ModelState.IsValid)
            {
                //convert request dto to image
                var image = new Image()
                {
                    File = requset.File,
                    fileExtension = Path.GetExtension(requset.File.FileName),
                    fileDescription = requset.fileDescription,
                    fileName = requset.fileName,
                    fileSizeInBytes = requset.File.Length,

                };
                //get path from repository
                //use repository to upload image
                return Ok( await imageRepository.Upload(image));


            }

            return BadRequest(ModelState);
        }


        private void ValidateUploadFile(ImageUploadRequestDto requset)
        {
            var allowedExtensions = new string[] { ".jpg"  , ".jpeg" , ".png"};

            if (!allowedExtensions.Contains(Path.GetExtension(requset.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(requset.File.Length > 10485760) // if greater than 10 mega byte
            {
                ModelState.AddModelError("file", "File size more than 10MB , Please upload a smaller size file.");
            }
        }
    }
}

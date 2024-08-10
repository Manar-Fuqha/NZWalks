using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalImageRepository( NZWalksDbContext dbContext , IWebHostEnvironment webHostEnvironment , IHttpContextAccessor httpContextAccessor )
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Image> Upload(Image image)
        {
            var LocalFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images" ,
                $"{image.fileName}{image.fileExtension}");

            // upload image to loacl path
            using var stream = new FileStream(LocalFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);

            // https:localhost:1234/images/fileName.fileExtension

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.fileName}{image.fileExtension}";

            image.filePath = urlFilePath;
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}

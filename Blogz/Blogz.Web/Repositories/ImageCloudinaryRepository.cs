using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Blogz.Web.Repositories
{
    public class ImageCloudinaryRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public ImageCloudinaryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary:CloudName").Value?.ToString(),
                configuration.GetSection("Cloudinary:ApiKey").Value?.ToString(),
                configuration.GetSection("Cloudinary:ApiSecret").Value?.ToString());
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.Name
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}

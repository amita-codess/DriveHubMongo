using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DriveHubMongo.Model;
using Microsoft.Extensions.Options;

namespace DriveHubMongo.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                // No file selected
                if (file == null || file.Length == 0)
                    return "";

                // Allow only image files
                if (!file.ContentType.StartsWith("image/"))
                    return "";

                // Max file size: 10 MB
                if (file.Length > 10 * 1024 * 1024)
                    return "";

                await using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),

                    Folder = "DriveHub",

                    UseFilename = false,
                    UniqueFilename = true,
                    Overwrite = false,

                    Transformation = new Transformation()
                        .Width(1200)
                        .Crop("limit")
                        .Quality("auto")
                        .FetchFormat("auto")
                };

                var result = await _cloudinary.UploadAsync(uploadParams);

                if (result.Error != null)
                    return "";

                return result.SecureUrl?.ToString() ?? "";
            }
            catch
            {
                return "";
            }
        }
    }
}
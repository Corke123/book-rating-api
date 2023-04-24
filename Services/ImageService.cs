using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book_rating_api.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace book_rating_api.Services
{
    public class ImageService
    {
        private readonly IConfiguration configuration;
        private readonly Cloudinary cloudinary;

        public ImageService(IConfiguration configuration)
        {
            this.configuration = configuration;

            Account account = new Account(
                configuration.GetSection("Cloudinary:CloudName").Value,
                configuration.GetSection("Cloudinary:ApiKey").Value,
                configuration.GetSection("Cloudinary:ApiSecret").Value);

            cloudinary = new Cloudinary(account);
        }

        public async Task<KeyValuePair<string, string>> UploadImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var publicId = Guid.NewGuid().ToString();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(publicId, stream),
                    PublicId = publicId,
                    Folder = configuration.GetSection("Cloudinary:Folder").Value
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new CloudinaryException("Unable to upload image");
                }

                var result = new KeyValuePair<string, string>(uploadResult.PublicId, uploadResult.Url.ToString());
                return result;
            }

        }

        public async Task DeleteImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await cloudinary.DestroyAsync(deletionParams);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new CloudinaryException("Unable to delete image");
            }
        }

    }
}
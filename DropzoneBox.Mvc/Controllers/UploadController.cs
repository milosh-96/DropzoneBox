using DropzoneBox.Mvc.Infrastructure;
using DropzoneBox.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DropzoneBox.Mvc.Controllers
{
    public class UploadController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SftpService _sftpService;

        public UploadController(IConfiguration configuration, SftpService sftpService)
        {
            _configuration = configuration;
            _sftpService = sftpService;
        }

        [HttpPost]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            string[] allowedExtensions = { ".png", ".bmp", ".jpg" };
            if(file != null && file.Length > 0)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(ext) || !allowedExtensions.Contains(ext))
                {
                    throw new InvalidOperationException("supported file types are: " + string.Join(", ", allowedExtensions));
                }

                var filePath = Path.Combine(_configuration["Uploads:StoragePath"],file.FileName);
                await _sftpService.Upload(file.OpenReadStream(), filePath);

                return new JsonResult("uploaded");
            }
            return new JsonResult("there was no file to upload.");
        }
    }
}

using DropzoneBox.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DropzoneBox.Mvc.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly FtpService _ftpService;

        public DirectoryController(FtpService ftpService)
        {
            _ftpService = ftpService;
        }

        public IActionResult List()
        {
            return new JsonResult(_ftpService.List());
        }
    }
}

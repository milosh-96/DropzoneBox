using DropzoneBox.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DropzoneBox.Mvc.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly SftpService _sftpService;

        public DirectoryController(SftpService sftpService)
        {
            _sftpService = sftpService;
        }

        public IActionResult List()
        {
            return new JsonResult(_sftpService.List());
        }
    }
}

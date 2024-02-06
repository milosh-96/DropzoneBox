using DropzoneBox.Mvc.Application.Config;
using FluentFTP;
using System.Net;
using FtpConfig = DropzoneBox.Mvc.Application.Config.FtpConfig;

namespace DropzoneBox.Mvc.Infrastructure
{
    public class FtpService
    {
        private readonly IConfiguration _configuration;
        private Application.Config.FtpConfig _ftpConfig;
        public FtpService(IConfiguration configuration)
        {
            _configuration = configuration;
            _ftpConfig = _configuration.GetSection("Ftp").Get<FtpConfig>();
        }

        public async Task<bool> Upload(Stream stream, string filePath)
        {
            using (var client = new FtpClient(
                _ftpConfig.ServerName,
                new NetworkCredential(_ftpConfig.UserName, _ftpConfig.Password),
                port: _ftpConfig.Port
               ))
            {
                client.Connect();
                client.UploadStream(stream,filePath);
            }
            return true;

        }
        public async Task<List<string>> List()
        {
            var files = new List<string>();
            using (var client = new FtpClient(
                _ftpConfig.ServerName,
                new NetworkCredential(_ftpConfig.UserName, _ftpConfig.Password),
                port: _ftpConfig.Port
               ))
            {
                foreach (FtpListItem item in client.GetListing())
                {
                    files.Add(item.FullName);
                }
            }
            return files;
        }
}
}

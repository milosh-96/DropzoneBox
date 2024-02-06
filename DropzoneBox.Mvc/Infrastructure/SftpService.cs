using DropzoneBox.Mvc.Application.Config;
using FluentFTP;
using System.Net;

namespace DropzoneBox.Mvc.Infrastructure
{
    public class SftpService
    {
        private readonly IConfiguration _configuration;
        private SftpConfig _sftpConfig;
        public SftpService(IConfiguration configuration)
        {
            _configuration = configuration;
            _sftpConfig = _configuration.GetSection("Sftp").Get<SftpConfig>();
        }

        public async Task<bool> Upload(Stream stream, string filePath)
        {
            using (var client = new FtpClient(
                _sftpConfig.ServerName,
                new NetworkCredential(_sftpConfig.UserName, _sftpConfig.Password),
                port: _sftpConfig.Port
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
                _sftpConfig.ServerName,
                new NetworkCredential(_sftpConfig.UserName, _sftpConfig.Password),
                port: _sftpConfig.Port
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

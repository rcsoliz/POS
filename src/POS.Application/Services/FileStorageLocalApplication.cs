using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using POS.Application.Interfaces;
using POS.Infraestructure.FileStorage;

namespace POS.Application.Services
{
    public class FileStorageLocalApplication : IFileStorageLocalApplication
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileStoreLocal _fileStoreLocal;

        public FileStorageLocalApplication(IWebHostEnvironment env, 
            IHttpContextAccessor httpContextAccessor, 
            IFileStoreLocal fileStoreLocal)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _fileStoreLocal = fileStoreLocal;
        }

        public async Task<string> SaveFile(string container, IFormFile file)
        {
            var webRootPath = _env.WebRootPath;
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;

            return await _fileStoreLocal.SaveFile(container, file, webRootPath, scheme, host.Value);    
        }
        public async Task<string> EditFile(string container, IFormFile file, string route)
        {
            var webRootPath = _env.WebRootPath;
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;

            return await _fileStoreLocal.EditFile(container, file, route, webRootPath, scheme, host.Value);
        }

        public async Task RemoveFile(string route, string container)
        {
            var webRootPath = _env.WebRootPath;

            await _fileStoreLocal.RemoveFile(route, container, webRootPath);
        }


    }
}

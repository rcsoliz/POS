using Microsoft.AspNetCore.Http;

namespace POS.Infraestructure.FileStorage
{
    public class FileStorageLocal : IFileStoreLocal
    {
        public async Task<string> SaveFile(string container, IFormFile file, string webRootPath, string scheme, string host)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(webRootPath, container);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            string path = Path.Combine(folder, fileName);

            using (var memoryString = new MemoryStream())
            {
                await file.CopyToAsync(memoryString);
                var content = memoryString.ToArray();
                await File.WriteAllBytesAsync(path, content);
            }

            var currentUrl = $"{scheme}://{host}";
            var pathDb = Path.Combine(currentUrl, container, fileName).Replace("\\", "/");

            return pathDb;
        }
        public async Task<string> EditFile(string container, IFormFile file, string route, string webRootPath, string scheme, string host)
        {
            await RemoveFile(route, container, webRootPath);

            return await SaveFile(container, file, webRootPath, scheme, host);
        }

        public  Task RemoveFile(string route, string container, string webRootPath)
        {
            if(string.IsNullOrEmpty(route)) 
                return Task.CompletedTask;
             
            var fileName = Path.GetFileName(route);

            var directoryFile = Path.Combine(webRootPath, container, fileName);

            if(File.Exists(directoryFile))
                File.Delete(directoryFile);

            return Task.CompletedTask;

        }

    }
}

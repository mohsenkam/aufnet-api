using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aufnet.Backend.Services.Shared
{
    public interface IFileManager
    {
        /// <summary>
        /// Stores the files given as an IFormFile and returns its URI.
        /// </summary>
        /// <param name="file">The file to be stored.</param>
        /// <returns>URI of the stored file.</returns>
        Task<string> StoreFile(IFormFile file);

        /// <summary>
        /// Deletes the file located at the give path.
        /// </summary>
        Task DeleteFile(string oldFileUrl);
    }

    public class LocalFileManager : IFileManager
    {
        public async Task<string> StoreFile(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }

        // todo: implement this
        public Task DeleteFile(string oldFileUrl)
        {
            return null;
        }
    }
}

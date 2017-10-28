using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Admin;

using Aufnet.Backend.ApiServiceShared.Shared;
using Microsoft.AspNetCore.Http;

namespace Aufnet.Backend.Services.Admin.Configs
{
    public interface IAdminConfigsCategoryService
    {
        Task<IGetServiceResult<List<CategoryDto>>> GetCategoriesAsync(long? parentId);
        Task<IGetServiceResult<CategoryDto>> GetCategoryAsync(long id);
        /// <summary>
        /// Creates a Category given the deails.
        /// </summary>
        Task<IServiceResult> CreateCategoryAsync(CreateCategoryDto value);
        
        Task<IServiceResult> UpdateCategoryDisplayNameAsync(long id, string newDisplayName);
        /// <summary>
        /// Uploads the image to be associated with the category correspondig to the given id.
        /// If the category already has an image, the previous one will be deleted.
        /// </summary>
        /// <param name="id">Category id.</param>
        /// <param name="file">Image to be uploaded.</param>
        Task<IServiceResult> AddOrUpdateCategoryImageAsync( long id, IFormFile file);
        Task<IServiceResult> DeleteCategoryAsync(long id);
    }
}

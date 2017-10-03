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
        Task<IServiceResult> CreateCategoryAsync(CreateCategoryDto value);
        Task<IServiceResult> SaveImageAsync( long id, IFormFile file);
        Task<IServiceResult> UpdateCategoryDisplayNameAsync(long id, string newDisplayName);
        Task<IServiceResult> UpdateCategoryImageAsync(long id, IFormFile file);
        Task<IServiceResult> DeleteCategoryImageAsync(long id);
    }
}

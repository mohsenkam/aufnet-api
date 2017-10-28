using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Admin;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Data.Repository;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Admin.Configs
{
    public class AdminConfigsCategoryService : IAdminConfigsCategoryService
    {
        private readonly IRepository<Category> _catRepository;
        private readonly IFileManager _fileManager;

        public AdminConfigsCategoryService(IRepository<Category> catRepository, IFileManager fileManager)
        {
            _catRepository = catRepository;
            _fileManager = fileManager;
        }

        public async Task<IGetServiceResult<List<CategoryDto>>> GetCategoriesAsync(long? parentId)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<CategoryDto>>();

            try
            {

                var categories = _catRepository.Query();
                var selfJoin = from c1 in categories
                    join c2 in categories
                    on c1.Id equals c2.ParentId into gj
                    from subCat in gj.DefaultIfEmpty()
                    select new
                    {
                        Id = c1.Id,
                        DisplayName = c1.DisplayName,
                        ImageUrl = c1.ImageUrl,
                        ChildId = subCat != null ? subCat.Id : (long?) null,
                    };

                var grouped = from category in selfJoin
                    group category by new { category.Id, category.DisplayName, category.ImageUrl}
                    into gc
                    select new CategoryDto()
                    {
                        Id = gc.Key.Id,
                        DisplayName = gc.Key.DisplayName,
                        ImageUrl = gc.Key.ImageUrl,
                        SubCategories = gc.Select(g => g.ChildId).ToArray()
                    };
                getResult.SetData(grouped.ToList());
                return getResult;

            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<CategoryDto>> GetCategoryAsync(long id)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<CategoryDto>();

            try
            {
                var category = await _catRepository.GetByIdAsync(id);
                if (category == null) 
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EntityNotFound.Code,
                        ErrorCodesConstants.EntityNotFound.Message));
                    getResult.SetResult(serviceResult);
                }
                else
                {
                    getResult.SetData(new CategoryDto()
                    {
                        DisplayName = category.DisplayName,
                        ImageUrl = category.ImageUrl,
                        Id = category.Id
                    });
                }
                return getResult;

            }
            catch (Exception ex)
            {
                // log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IServiceResult> CreateCategoryAsync(CreateCategoryDto value)
        {
            var serviceResult = new ServiceResult();

            Category parent = null;
            if (value.ParentId != -1) // The root level is assumed to have the Id of -1
            {
                try
                {
                    parent = await _catRepository.GetByIdAsync(value.ParentId.Value);
                    if (parent == null) //Parent is set to an invalid entity
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                            ErrorCodesConstants.InvalidOperation.Message));
                        return serviceResult;
                    }
                }
                catch (Exception ex)
                {
                    // log ex
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                        ErrorCodesConstants.OperationFailed.Message));
                }
            }

            try
            {
                var category = new Category()
                {
                    ParentId = parent.Id,
                    DisplayName = value.DisplayName
                };
                await _catRepository.AddAsync(category);
                serviceResult.SetExteraData(new { category.Id }); // todo: Does it work??


            }
            catch (Exception ex)
            {
                // Log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
            }
            return serviceResult;
        }


        public async Task<IServiceResult> AddOrUpdateCategoryImageAsync( long id, IFormFile file )
        {
            var serviceResult = new ServiceResult();

            // Check if the category exists
            try
            {

                var category = await _catRepository.GetByIdAsync(id);
                if (category == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                var oldFileUrl = category.ImageUrl;

                // Read (upload) the file
                category.ImageUrl = await _fileManager.StoreFile(file);
                await _catRepository.UpdateAsync(category);

                if (oldFileUrl != null)
                    await _fileManager.DeleteFile(oldFileUrl);

                serviceResult.SetExteraData(new { file = file.FileName, size = file.Length });
                return serviceResult;
            }
            catch (Exception ex)
            {
                // todo: log the exception

                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                return serviceResult;
            }
        }

        public async Task<IServiceResult> UpdateCategoryDisplayNameAsync(long id, string newDisplayName)
        {
            var serviceResult = new ServiceResult();

            try
            {
                var category = await _catRepository.GetByIdAsync(id);
                if (category == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                }
                else
                {
                    category.DisplayName = newDisplayName;
                    await _catRepository.UpdateAsync(category);
                }
                return serviceResult;
            }
            catch (Exception ex)
            {
                // log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                return serviceResult;
            }
        }

        public async Task<IServiceResult> DeleteCategoryAsync(long id)
        {
            var serviceResult = new ServiceResult();

            try
            {
                var category = await _catRepository.GetByIdAsync(id);
                if (category == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                }
                else
                {
                    var hasActiveChild =
                        _catRepository.Query(c => c.ParentId == id).Any();

                    if (hasActiveChild) // Cannot delete
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                            ErrorCodesConstants.InvalidOperation.Message));
                        return serviceResult;
                    }

                    await _catRepository.ArchiveAsync(category); // Archive instead of Delete
                }
                return serviceResult;

            }
            catch (Exception ex)
            {
                // log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                return serviceResult;
            }
        }
    }
}
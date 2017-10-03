using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Admin;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Admin.Configs
{
    public class AdminConfigsCategoryService : IAdminConfigsCategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminConfigsCategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IGetServiceResult<List<CategoryDto>>> GetCategoriesAsync(long? parentId)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<CategoryDto>>();

            try
            {
                var categories = await _dbContext.Categories.Where(c => c.ParentCategory.Id == parentId)
                    .Select(c => new CategoryDto()
                    {
                        DisplayName = c.DisplayName,
                        ImageUrl = c.ImageUrl,
                        ParentId = c.ParentCategory.Id
                    })
                    .ToListAsync();
                getResult.SetData(categories);
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

        public async Task<IGetServiceResult<CategoryDto>> GetCategoryAsync(long id)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<CategoryDto>();

            try
            {
                var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
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
                        ParentId = category.ParentCategory.Id
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
            if (value.ParentId != null)
            {
                try
                {
                    parent = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == value.ParentId);
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
                await _dbContext.Categories.AddAsync(new Category()
                {
                    ParentCategory = parent,
                    DisplayName = value.DisplayName
                });

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> SaveImageAsync(long id, IFormFile file)
        {
            var serviceResult = new ServiceResult();

            // Check if the category exists
            try
            {

                var category =
                    await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                // Read (upload) the file
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                category.ImageUrl = filePath;
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
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

        public async Task<IServiceResult> UpdateCategoryImageAsync( long id, IFormFile file )
        {
            var serviceResult = new ServiceResult();

            // Check if the category exists
            try
            {

                var category =
                    await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                // Read (upload) the file
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                category.ImageUrl = filePath;
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();

                // TODO: Delete the old image (if exists) after the successful upload of the new image

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
                var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                }
                else
                {
                    category.DisplayName = newDisplayName;
                    _dbContext.Categories.Update(category);
                    await _dbContext.SaveChangesAsync();
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

        public async Task<IServiceResult> DeleteCategoryImageAsync(long id)
        {
            var serviceResult = new ServiceResult();

            try
            {
                var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                }
                else
                {
                    var hasActiveChild =
                        await _dbContext.Categories.AnyAsync(c => c.ParentCategory.Id == id && !c.IsArchived);

                    if (hasActiveChild) // Cannot delete
                    {
                        serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                            ErrorCodesConstants.InvalidOperation.Message));
                        return serviceResult;
                    }

                    category.IsArchived = true; // Logical delete
                    _dbContext.Categories.Update(category);
                    await _dbContext.SaveChangesAsync();
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
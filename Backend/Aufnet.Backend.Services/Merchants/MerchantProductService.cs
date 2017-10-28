using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Merchants;
using Aufnet.Backend.Data.Repository;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Merchants
{
    public class MerchantProductService : IMerchantProductService
    {
        private readonly IRepository<Merchant> _merRepository;
        private readonly IRepository<Product> _prRepository;
        private readonly IFileManager _fileManager;


        public MerchantProductService(IRepository<Merchant> merRepository, IRepository<Product> prRepository, IFileManager fileManager)
        {
            _merRepository = merRepository;
            _prRepository = prRepository;
            _fileManager = fileManager;
        }

        public async Task<IGetServiceResult<List<MerchantProductSummaryDto>>> GetProductsAsync(string username)
        {
            var serviceResult = new ServiceResult();

            var getResult = new GetServiceResult<List<MerchantProductSummaryDto>>();
            try
            {
                var products = await _prRepository.Query(p => p.Merchant.User.UserName == username)
                    .Select(p => new MerchantProductSummaryDto()
                    {
                        Id = p.Id,
                        Price = p.Price,
                        ProductName = p.Name
                    })
                    .ToListAsync();
                getResult.SetData(products);
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

        public async Task<IGetServiceResult<MerchantProductDetailsDto>> GetProductAsync(string username, long id)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<MerchantProductDetailsDto>();

            try
            {
                var product = await _prRepository.GetByIdAsync(id);
                if (product == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EntityNotFound.Code,
                        ErrorCodesConstants.EntityNotFound.Message));
                    getResult.SetResult(serviceResult);
                }
                else
                {
                    getResult.SetData(new MerchantProductDetailsDto()
                    {
                        Category = product.Category.DisplayName,
                        ImageUrl = product.ImageUrl,
                        Description = product.Description,
                        Price = product.Price,
                        Name = product.Name
                    });
                }
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

        public async Task<IServiceResult> CreateProduct(string username, CreateProductDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var merchant = await _merRepository.Query(m => m.User.UserName == username).FirstOrDefaultAsync();
                if (merchant == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                    return serviceResult;
                }
                await _prRepository.AddAsync( new Product()
                {
                    Description = value.Description,
                    IsAvailable = value.IsAvailable,
                    Name = value.ProductName,
                    Merchant = merchant,
                });
            }
            catch (Exception ex)
            {
                // todo: log the exception
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> UpdateProductDetails(string username, long productId, ProductUpdateDto value )
        {
            var serviceResult = new ServiceResult();
            try
            {
                var merchant = await _merRepository.Query(m => m.User.UserName == username).FirstOrDefaultAsync();
                if (merchant == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                    return serviceResult;
                }
                // Check if THIS user has this product, then fetch it
                var product = await _prRepository.Query(p => p.Merchant.User.UserName == username && p.Id == productId).FirstOrDefaultAsync();            
                if (product == null) // There is no such a product for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                product.Description = value.Description;
                product.IsAvailable = value.IsAvailable;
                product.Name = value.ProductName;
                await _prRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                // todo: log the exception
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> AddOrUpdateImageAsync(long id, IFormFile file)
        {
            var serviceResult = new ServiceResult();

            // Check if the product exists
            try
            {

                var product = await _prRepository.GetByIdAsync(id);
                if (product == null) // Validation
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                var oldFileUrl = product.ImageUrl;

                // Read (upload) the file
                product.ImageUrl = await _fileManager.StoreFile(file);
                await _prRepository.UpdateAsync(product);

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

        public async Task<IServiceResult> DeleteProduct(string username, long productId)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var merchant = await _merRepository.Query(m => m.User.UserName == username).FirstOrDefaultAsync();
                if (merchant == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                    return serviceResult;
                }
                await _prRepository.DeleteAsync(productId);
            }
            catch (Exception ex)
            {
                // todo: log the exception
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
            }
            return serviceResult;
        }
    }
}

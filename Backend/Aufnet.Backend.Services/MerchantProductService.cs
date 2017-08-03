using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Customer;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Microsoft.AspNetCore.Identity;


namespace Aufnet.Backend.Services
{
    public class MerchantProductService : IMerchantProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantProductService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IGetServiceResult<MerchantProductDto>> GetProductAsync(string username)
        {
            var serviceResult = new ServiceResult();

            

            //validatio
            var getResult = new GetServiceResult<MerchantProductDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var product = _context.Products.FirstOrDefault(p => p.ApplicationUser.UserName.Equals(username));

            MerchantProductDto mpDto;
            if (product == null)
                mpDto = null;
            else
            {
                mpDto = new MerchantProductDto()
                {
                    Description = product.Description,
                    IsAvailable = product.IsAvailable,
                    ProductName = product.ProductName,
                    Discount = product.Discount,

                };
            }
            getResult.SetData(mpDto);
            return getResult;
        }

        public async Task<IServiceResult> CreateProduct(string username, MerchantProductDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                        ErrorCodesConstants.NotExistingUser.Message));
                    
                    return serviceResult;
                }
               await _context.Products.AddAsync(new MerchantProduct()
                {
                   Description = value.Description,
                   IsAvailable = value.IsAvailable,
                   ProductName = value.ProductName,
                   Discount = value.Discount,
                   ApplicationUser = user,
                   ApplicationUserId = user.Id
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }

            return serviceResult;
        }

        public async Task<IServiceResult> UpdateProduct(string username, MerchantProductDto value)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                        ErrorCodesConstants.NotExistingUser.Message));
                    return serviceResult;
                }
                var product =
                    _context.Products.FirstOrDefault(p => p.ApplicationUser.UserName.Equals(username));
                if (product == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                product.Description = value.Description;
                product.IsAvailable = value.IsAvailable;
                product.ProductName = value.ProductName;
                product.Discount = value.Discount;
                //product.ApplicationUser = value.user,
                //product.ApplicationUserId = user.Id
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> DelteProduct(string username)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                        ErrorCodesConstants.NotExistingUser.Message));
                    return serviceResult;
                }
                var product =
                    _context.Products.FirstOrDefault(p => p.ApplicationUser.UserName.Equals(username));
                if (product == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }
    }
}

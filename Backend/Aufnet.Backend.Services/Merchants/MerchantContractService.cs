using System;
using System.IO;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Merchants
{
    public class MerchantContractService : IMerchantContractService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantContractService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IEmailService emailService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _emailService = emailService;
        }



        public Task<IServiceResult> UpateContractDetailsAsync(MerchantCreateDto value)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult> UpdateLogoAsync(long id, IFormFile file)
        {
            var serviceResult = new ServiceResult();
            try
            {
                // Check if the merchant exists
                var merchant = await _dbContext.Merchants.FirstOrDefaultAsync(m => m.Id == id);
                if (merchant == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                    return serviceResult;
                }

                // Read (upload) the file
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                merchant.Contract.LogoUri = filePath;
                _dbContext.Merchants.Update(merchant);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // todo: log the exception

                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code, 
                    ErrorCodesConstants.OperationFailed.Message));
            }
            serviceResult.SetExteraData(new { file = file.FileName, size = file.Length });
            return serviceResult;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Shared.Exceptions;
using Aufnet.Backend.ApiServiceShared.Shared.utils;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Merchant
{
    public class MerchantService : IMerchantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IEmailService emailService)
        {
            _userManager = userManager;
            _context = context;
            _emailService = emailService;
        }


        public async Task<IGetServiceResult<MerchantSignUpDto>> GetMerchantAsync(string username)
        {
            var serviceResult = new ServiceResult();
            //validatio
            var getResult = new GetServiceResult<MerchantSignUpDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            
            MerchantSignUpDto cpDto;
            
                cpDto = new MerchantSignUpDto()
                {
                    Email = user.Email

                };
            
            getResult.SetData(cpDto);
            return getResult;
        }

        public async Task<IGetServiceResult<List<MerchantProfileDto>>> SearchMerchantByAddress(AddressDto addressDto)
        {
            var getResult = new GetServiceResult<List<MerchantProfileDto>>();
            IQueryable<MerchantProfile> query = EntityFrameworkQueryableExtensions.Include<MerchantProfile, Point>(_context.MerchantProfiles, m => m.Location).Include(m => m.Address).Where(m => true);

            //if (addressDto.City != null)
            //{
            //    query = query.Where(c => c.Address.City == addressDto.City);
            //}
            if (addressDto.Country != null)
            {
                query = query.Where(c => c.Address.Country == addressDto.Country);
            }
            //if (addressDto.Detail != null)
            //{
            //    query = query.Where(c => c.Address.Detail == addressDto.Detail);
            //}
            if (addressDto.PostCode != null)
            {
                query = query.Where(c => c.Address.PostCode == addressDto.PostCode);
            }
            if (addressDto.State != null)
            {
                query = query.Where(c => c.Address.State == addressDto.State);
            }
            if (addressDto.BaseLocation != null)
            {
                query = query.Where(c => Math.Pow(addressDto.BaseLocation.Latitude - (double)c.Location.Latitude, 2) + Math.Pow(addressDto.BaseLocation.Longitude - (double)c.Location.Longitude, 2) < addressDto.Distance * addressDto.Distance);
            }

            if (addressDto.RegionDto != null)
            {
                var region = EntityFrameworkQueryableExtensions.Include<Region, Point>(_context.Regions, m=>m.Center).FirstOrDefault(r => r.Name.Equals(addressDto.RegionDto.Name));
                query = query.Where(c => Math.Pow(region.Center.Latitude - (double)c.Location.Latitude, 2) + Math.Pow(region.Center.Longitude - (double)c.Location.Longitude, 2) < addressDto.Distance * addressDto.Distance);
            }




            List<MerchantProfileDto> mpDtos;

            mpDtos = query.Select(q => new MerchantProfileDto()
            {
                BusinessName = q.BusinessName,
                LocationDto = new PointDto()
                {
                    Longitude = q.Location.Longitude,
                    Latitude = q.Location.Latitude
                },
                AddressDto = new AddressDto()
                {
                    //City = q.Address.City,
                    Country = q.Address.Country,
                    //Detail = q.Address.Detail,
                    PostCode = q.Address.PostCode,
                    State = q.Address.State

                }
            }).ToList();
            
            getResult.SetData(mpDtos);
            return getResult;


        }

        public async Task<IServiceResult> SaveLogoAsync(IFormFile file, IHeaderDictionary requestHeaders)
        {
            var serviceResult = new ServiceResult();

            // Check if the header is set
            var trackingId = requestHeaders["trackingId"];
            if (string.IsNullOrEmpty(trackingId))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code, ErrorCodesConstants.InvalidOperation.Message));
                return serviceResult;
            }

            // Check if the merchant exists
            try
            {


                var merchantContract = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<MerchantContract>(_context.MerchantContracts, mc => mc.TrackingId.Equals(trackingId));
                if (merchantContract == null)
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
                merchantContract.LogoUri = filePath;
                _context.MerchantContracts.Update(merchantContract);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // todo: log the exception

                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code, ErrorCodesConstants.OperationFailed.Message));
            }


            serviceResult.SetExteraData(new { file = file.FileName, size = file.Length });
            return serviceResult;
        }

        public async Task<IServiceResult> SendRegistrationEmailAsync(string trackingId)
        {
            var serviceResult = new ServiceResult();



            var merchantContract =
                await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<MerchantContract>(_context.MerchantContracts, mc => mc.TrackingId == trackingId);

            // Check if this trackingId is generated
            if (merchantContract == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                    ErrorCodesConstants.InvalidOperation.Message));
                return serviceResult;
            }

            // Check if this trackingId is not used
            if (merchantContract.IsTrackingIdUsed)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.RepeatedOperation.Code,
                    ErrorCodesConstants.RepeatedOperation.Message));
                return serviceResult;
            }

            var callbackUrl = "http://localhost:4200/auth/register/merchant?trackingId=" + trackingId;
            await _emailService.SendEmailAsync(new EmailModel
            {
                ToEmail = merchantContract.Email,
                Subject = "Complete your registration",
                Body = $"Follow the link to create your account: </br>{callbackUrl}"
            });

            return serviceResult;
        }

        public async Task<IGetServiceResult<List<MerchantContractSummaryDto>>> GetMerchantsContractsSummaryAsync(PagingParams pagingParams)
        {
            var getServiceResult = new GetServiceResult<List<MerchantContractSummaryDto>>();
            if (!pagingParams.IsValid)
            {
                var serviceResult = new ServiceResult();
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidArgument.Code,
                    ErrorCodesConstants.InvalidArgument.Message));
                getServiceResult.SetResult(serviceResult);
                return getServiceResult;
            }
            try
            {
                var queryable =  Queryable.Select<MerchantContract, MerchantContractSummaryDto>(_context.MerchantContracts, mc => new MerchantContractSummaryDto()
                {
                    Id = mc.Id,
                    BusinessName = mc.BusinessName,
                    Abn = mc.Abn,
                    Category = mc.Category,
                    ContractStartDate = mc.ContractStartDate,
                    SubCategory = mc.SubCategory,
                });
                if (pagingParams.Count != 0) // This is to support the paging in the future.
                    queryable.Skip(pagingParams.Offset * pagingParams.Count).Take(pagingParams.Count);
                var data = await queryable.ToListAsync();
                getServiceResult.SetData(data);
                getServiceResult.SetTotalCount(await EntityFrameworkQueryableExtensions.CountAsync<MerchantContract>(_context.MerchantContracts));
            }
            catch (Exception e)
            {
                // todo: log the exception
                var serviceResult = new ServiceResult();

                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code, ErrorCodesConstants.OperationFailed.Message));
                getServiceResult.SetResult(serviceResult);
            }

            return getServiceResult;
        }

        public async Task<IServiceResult> CreateAccountAsync(MerchantCreateDto value)
        {
            var serviceResult = new ServiceResult();


            var merchantContract = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<MerchantContract>(_context.MerchantContracts, m => m.Abn == value.Abn && m.BusinessName == value.BusinessName);
            if (merchantContract != null) // The merchant is already added to the database
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.RepeatedOperation.Code, ErrorCodesConstants.RepeatedOperation.Message));
                return serviceResult;
            }

            try
            {
                // Create the merchant
                merchantContract = new MerchantContract
                {

                    Abn = value.Abn,
                    Address = value.Address,
                    BusinessName = value.BusinessName,
                    Category = value.Category,
                    ContractStartDate = DateTime.Now,
                    Email = value.Email,
                    OwnerName = value.OwnerName,
                    Phone = value.Phone,
                    SubCategory = value.SubCategory,
                    TrackingId = UtilityMethods.GenerateTrackingId(DateTime.Now)
                };
                while (true) // Check if the tracking Id is unique
                {
                    if (Queryable.Count<MerchantContract>(_context.MerchantContracts, mc => mc.TrackingId.Equals(merchantContract.TrackingId)) == 0)
                        break;
                }
                await _context.MerchantContracts.AddAsync(merchantContract);
                await _context.SaveChangesAsync();
                serviceResult.SetExteraData(new {merchantContract.TrackingId});
            }
            catch (Exception e)
            {
                // todo: log the exception

                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code, ErrorCodesConstants.OperationFailed.Message));
            }

            return serviceResult;


        }

        

        public async Task<IServiceResult> SignUpAsync(MerchantSignUpDto value)
        {

            var serviceResult = new ServiceResult();

            var user = new ApplicationUser
            {
                UserName = value.Email,
                Email = value.Email,
            };
            var result = await _userManager.CreateAsync(user, value.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    serviceResult.AddError(new ErrorMessage("", error.Description));
                }

                return serviceResult;
            }

            //User will be created but we don't assign him/her a role, until the email is confirmed

            var token = _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = "aufnet.com.au/not_set_yet";
            //Url.Action(controller: "Auth", action: "ResetPassword",
            //values: new { userId = userId, code = code }, protocol: "https", host: "aufnet.com.au");
            await _emailService.SendEmailAsync(new EmailModel
            {
                ToEmail = value.Email,
                Subject = "Reset password for Gardenia Portal",
                Body = $"Follow the link to reset your password: </br>{callbackUrl}"
            });

            return serviceResult;

        }

        public async Task<IServiceResult> ConfirmEmailAsync(ConfirmEmailDto value)
        {

            var serviceResult = new ServiceResult();

            //validation
            if (String.IsNullOrEmpty(value.UserId))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "CurrentPassword"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(value.Code))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "NewPassword"));
                return serviceResult;
            }
            var user = await _userManager.FindByIdAsync(value.UserId);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code, ErrorCodesConstants.NotExistingUser.Message));
                return serviceResult;
            }
            //end validation

            var result = await _userManager.ConfirmEmailAsync(user, value.Code);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    serviceResult.AddError(new ErrorMessage("", error.Description));
                }
            }
            try
            {
                await AssignRole(user, RolesConstants.Merchant);
            }
            catch (InvalidArgumentException exception)
            {
                serviceResult.AddError(new ErrorMessage("", exception.Message));
                return serviceResult;
            }
            return serviceResult;
        }

        public async Task<IServiceResult> ChangePasswordAsync(string username, MerchantChangePasswordDto value)
        {
            var serviceResult = new ServiceResult();

            //validation
            if (String.IsNullOrEmpty(value.CurrentPassword))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "CurrentPassword"));
                return serviceResult;
            }
            if (String.IsNullOrEmpty(value.NewPassword))
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ArgumentMissing.Code, ErrorCodesConstants.ArgumentMissing.Message + "NewPassword"));
                return serviceResult;
            }
            //end validation

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code, ErrorCodesConstants.NotExistingUser.Message));
                return serviceResult;
            }

            var result = await _userManager.ChangePasswordAsync(user, value.CurrentPassword, value.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    serviceResult.AddError(new ErrorMessage("", error.Description));
                }
            }
            return serviceResult;
        }

        public async Task<IServiceResult> DeleteAsync(string username)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }
                var profile =
                    Queryable.FirstOrDefault<MerchantProfile>(_context.MerchantProfiles, cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile != null) //delete the user's profile before deleting the user
                {
                    _context.MerchantProfiles.Remove(profile);
                    _context.SaveChanges();
                }
                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public Task<IServiceResult> ResetPasswordByMail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> ResetPasswordByPhone(string phone)
        {
            throw new NotImplementedException();
        }



        private async Task AssignRole(ApplicationUser user, string role)
        {
            if (user == null)
                throw new InvalidArgumentException("The argument user is null");
            if (String.IsNullOrEmpty(role))
                throw new InvalidArgumentException("The argument role is null or empty");

            var existingRoles = await _userManager.GetRolesAsync(user);
            if (!existingRoles.Contains(role))
                await _userManager.AddToRoleAsync(user, role);
        }
    }
}

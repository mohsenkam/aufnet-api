using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Customer;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services.Base;
using Microsoft.AspNetCore.Identity;
using ZXing;
using ZXing.QrCode;
using Gender = Aufnet.Backend.Data.Models.Entities.Shared.Gender;

namespace Aufnet.Backend.Services
{
    public class CustomerProfilesService : ICustomerProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerProfilesService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IGetServiceResult<CustomerProfileDto>> GetProfileAsync(string username)
        {
            var serviceResult = new ServiceResult();

            

            //validatio
            var getResult = new GetServiceResult<CustomerProfileDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var profile = _context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));

            CustomerProfileDto cpDto;
            if (profile == null)
                cpDto = null;
            else
            {
                cpDto = new CustomerProfileDto()
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    DateOfBirth = profile.DateOfBirth,
                    JoiningDate = profile.JoiningDate,
                    Email = profile.Email,
                    PhoneNumber = profile.PhoneNumber,
                    //Gender todo: put in the proper place
                };
            }
            getResult.SetData(cpDto);
            return getResult;
        }

        public async Task<IServiceResult> CreateProfile(string username, CustomerProfileDto value)
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
               await _context.CustomerProfiles.AddAsync(new CustomerProfile()
                {
                    FirstName =  value.FirstName,
                    LastName = value.LastName,
                    PhoneNumber = value.PhoneNumber,
                    Email = value.Email,
                    DateOfBirth = value.DateOfBirth,
                    JoiningDate = value.JoiningDate, //Todo: set it here
                    Gender = (Gender) value.Gender,
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

        public async Task<IServiceResult> UpdateProfile(string username, CustomerProfileDto value)
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
                var profile =
                    _context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                profile.FirstName = value.FirstName;
                profile.LastName = value.LastName;
                profile.PhoneNumber = value.PhoneNumber;
                profile.Email = value.Email;
                profile.DateOfBirth = value.DateOfBirth;
                profile.JoiningDate = value.JoiningDate;
                profile.Gender = (Gender)value.Gender;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> DelteProfile(string username)
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
                var profile =
                    _context.CustomerProfiles.FirstOrDefault(cp => cp.ApplicationUser.UserName.Equals(username));
                if (profile == null) //there is no profile for this user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                _context.CustomerProfiles.Remove(profile);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public Task<IServiceResult> SetBarcode(string username)
        {
            //var QrcodeContent = username;
            var alt = username;
            var width = 250; // width of the Qr Code
            var height = 250; // height of the Qr Code
            var margin = 0;
            BarcodeWriterPixelData writer = new BarcodeWriterPixelData()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions { Height = height, Width = width, Margin = margin }
            };
            var pixelData = writer.Write(username);

            //using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            //{
            //    using (var ms = new System.IO.MemoryStream())
            //    {
            //        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //        try
            //        {
            //            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
            //            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            //        }
            //        finally
            //        {
            //            bitmap.UnlockBits(bitmapData);
            //        }

            //        // PNG or JPEG or whatever you want
            //        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        bitmap.Save("image.png", ImageFormat.Png);
            //        var base64str = Convert.ToBase64String(ms.ToArray());
            //    }
            //}
            throw new NotImplementedException();
        }
    }
}

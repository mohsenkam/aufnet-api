using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Customer;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Shared;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZXing;
using ZXing.QrCode;
using Gender = Aufnet.Backend.Data.Models.Entities.Shared.Gender;

namespace Aufnet.Backend.Services
{
    public class RegionService : IRegionService
    {
        private readonly ApplicationDbContext _context;
        
        public RegionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IGetServiceResult<RegionDto>> GetRegionAsync(string name)
        {
            var serviceResult = new ServiceResult();
            
            //validatio
            var getResult = new GetServiceResult<RegionDto>();
            var region = _context.Regions.Include(r => r.Center).FirstOrDefault(cp => cp.Name.Equals(name));
            if (region == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                    ErrorCodesConstants.ManipulatingMissingEntity.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            
            RegionDto rDto = new RegionDto()
                {
                    Name = region.Name,
                    Center = new PointDto()
                    {
                        Longitude = region.Center.Longitude,
                        Latitude = region.Center.Latitude
                    }
            };
            
            getResult.SetData(rDto);
            return getResult;
        }

        public async Task<IServiceResult> CreateRegion(RegionDto regionDto)
        {
            var serviceResult = new ServiceResult();
            try
            {
                
                await _context.Regions.AddAsync(new Region()
                {
                    Name = regionDto.Name,
                    Center = new Point()
                    {
                        Longitude = regionDto.Center.Longitude,
                        Latitude = regionDto.Center.Latitude
                    }
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }

            return serviceResult;
        }

        public async Task<IServiceResult> UpdateRegion(string name, RegionDto regionDto)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var region =  _context.Regions.Include(r=>r.Center).FirstOrDefault(r=>r.Name.Equals(name));
                if (region == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                region.Name = regionDto.Name;
                region.Center.Latitude = regionDto.Center.Latitude;
                region.Center.Longitude = regionDto.Center.Longitude;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResult.AddError(new ErrorMessage("", ex.Message));
            }
            return serviceResult;
        }

        public async Task<IServiceResult> DeleteRegion(string name)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var region = _context.Regions.Include(r => r.Center).FirstOrDefault(r => r.Name.Equals(name));
                if (region == null) //There is no such a user
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.ManipulatingMissingEntity.Code,
                        ErrorCodesConstants.ManipulatingMissingEntity.Message));
                    return serviceResult;
                }

                _context.Regions.Remove(region);
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Admin;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.ApiServiceShared.Shared.utils;
using Aufnet.Backend.Data.Models.Entities.Merchants;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Data.Repository;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Admin.Merchants
{
    public class AdminContractService : IAdminContractService
    {
        private readonly IRepository<Merchant> _merRepository;
        private readonly IRepository<Category> _catRepository;
        private readonly IRepository<Contract> _conRepository;
        private readonly IFileManager _fileManager;


        public AdminContractService(IRepository<Merchant> merRepository, IRepository<Category> catRepository, IFileManager fileManager, IRepository<Contract> conRepository)
        {
            _merRepository = merRepository;
            _catRepository = catRepository;
            _fileManager = fileManager;
            _conRepository = conRepository;
        }

        public async Task<IServiceResult> CreateContractAsync( MerchantCreateDto value )
        {
            var serviceResult = new ServiceResult();

            try
            {
                // ******************THE COMBINATION OF THE ABN AND BUSINESSNAME MUST BE UNIQUE********************
                var merchantExists = await _merRepository
                    .Query(m => m.Contract.Abn == value.Abn &&
                                m.Contract.BusinessName == value.BusinessName).AnyAsync();
                if (merchantExists) // The merchant is already added to the database
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.RepeatedOperation.Code,
                        ErrorCodesConstants.RepeatedOperation.Message));
                    return serviceResult;
                }

                var category = await _catRepository.GetByIdAsync(value.CategoryId);
                if (category == null) // There is no such a category
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidArgument.Code,
                        ErrorCodesConstants.InvalidArgument.Message));
                    return serviceResult;
                }

                // Create the contract
                var contract = new Contract
                {
                    Abn = value.Abn,
                    Address = value.Address,
                    BusinessName = value.BusinessName,
                    Category = category,
                    ContractStartDate = DateTime.Now,
                    Email = value.Email,
                    OwnerName = value.OwnerName,
                    Phone = value.Phone,
                };

                // Create the merchant
                var merchant = new Merchant();
                merchant.Contract = contract;
                await _merRepository.AddAsync(merchant);

                serviceResult.SetExteraData(new { merchant.Id }); // todo: Does it work??
            }
            catch (Exception ex)
            {
                // todo: log the exception

                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
            }

            return serviceResult;


        }

        public async Task<IServiceResult> SaveLogoAsync(long id, IFormFile file)
        {
            var serviceResult = new ServiceResult();
            try
            {
                // Check if the merchant exists
                var merchant = await _merRepository.GetByIdAsync(id);
                if (merchant == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.InvalidOperation.Code,
                        ErrorCodesConstants.InvalidOperation.Message));
                    return serviceResult;
                }

                merchant.Contract.LogoUri = await _fileManager.StoreFile(file);
                await _merRepository.UpdateAsync(merchant);
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

        public async Task<IGetServiceResult<AdminContractSummaryDto>> GetContract(long id)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<AdminContractSummaryDto>();

            try
            {
                var contract = await _conRepository.Query(c => c.Id == id)
                    .FirstOrDefaultAsync();
                var contractDto = new AdminContractSummaryDto()
                {
                    Abn = contract.Abn,
                    BusinessName = contract.BusinessName,
                    CategoryName = contract.Category.DisplayName,
                    ContractStartDate = contract.ContractStartDate,
                    Address = contract.Address.Raw
                };
                getResult.SetData(contractDto);
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

        public async Task<IGetServiceResult<List<AdminContractSummaryDto>>> GetConractsAsync()
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<AdminContractSummaryDto>>();

            try
            {
                var contracts = await _conRepository.Query()
                    .Select(c => new AdminContractSummaryDto()
                    {
                        Abn = c.Abn,
                        Address = c.Address.Raw,
                        BusinessName = c.BusinessName,
                        CategoryName = c.Category.DisplayName,
                        ContractStartDate = c.ContractStartDate
                    }).ToListAsync();
                getResult.SetData(contracts);
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
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Admin;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Microsoft.AspNetCore.Http;

namespace Aufnet.Backend.Services.Admin.Merchants
{
    public interface IAdminContractService
    {
        /// <summary>
        /// Creates a merchant entity, sets its contract to the one created based on the given parameter, and stores it in
        /// the database.
        /// </summary>
        /// <param name="value">The details for the merchant contract to be created.</param>
        /// <returns>The id of the created merchant if successful.</returns>
        Task<IServiceResult> CreateContractAsync( MerchantCreateDto value );
        /// <summary>
        /// Uploads the logo to be assoiated with the cotract of the merchant having the specified id.
        /// </summary>
        /// <param name="id">Merchant id</param>
        /// <param name="file">Logo</param>
        Task<IServiceResult> SaveLogoAsync( long id, IFormFile file);
        /// <summary>
        /// Gets the Contract corresponding to the given id.
        /// </summary>
        /// <param name="id">Contract id</param>
        Task<IGetServiceResult<AdminContractSummaryDto>> GetContract(long id);

        Task<IGetServiceResult<List<AdminContractSummaryDto>>> GetConractsAsync();
    }

    
}
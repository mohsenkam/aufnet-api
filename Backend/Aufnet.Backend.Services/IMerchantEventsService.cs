using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.ApiServiceShared.Shared;

namespace Aufnet.Backend.Services
{
    public interface IMerchantEventsService
    {
        Task<IGetServiceResult<MerchantEventsDto>> GetEventAsync(string username);
        Task<IServiceResult> CreateEvent(string username, MerchantEventsDto value);
        //Task<IServiceResult> UpdateFirstEvent(string username, MerchantEventsDto value);
        Task<IServiceResult> UpdateEvent(string username, MerchantEventsDto value);
        Task<IServiceResult> DeleteEvent(string username, int merchantEventId);
        Task<IGetServiceResult<List<MerchantEventsDto>>> SearchMerchantEvents(DateTime startDate, DateTime endDate);
    }
}
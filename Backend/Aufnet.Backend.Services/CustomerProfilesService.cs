using System;
using System.Threading.Tasks;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public class CustomerProfilesService : ICustomerProfileService
    {
        private readonly ApplicationDbContext _context;

        public CustomerProfilesService(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public Task<IServiceResult> CreateProfile()
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> UpdateProfile()
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> DelteProfile()
        {
            throw new NotImplementedException();
        }
    }
}

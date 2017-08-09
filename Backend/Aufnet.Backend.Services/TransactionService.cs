using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Transaction;
using Microsoft.AspNetCore.Identity;

namespace Aufnet.Backend.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IGetServiceResult<TransactionDto>> GetCustomerTransactionsAsync(string username,
            TransactionDto value)
        {
            var serviceResult = new ServiceResult();

            //validation
            var getResult = new GetServiceResult<TransactionDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var transaction =
                _context.Transactions.FirstOrDefault( t => t.ApplicationUser.UserName.Equals(username));
                
                //.Include(m => m.ApplicationUser)
                //    .Where(t => t.Customer.UserName == value.Customer.UserName)
                //    .FirstOrDefault(ct => ct.ApplicationUser.UserName.Equals(username));

            TransactionDto tDto;
            if (transaction == null)
                tDto = null;
            else
            {
                tDto = new TransactionDto()
                {
                    Id = (int) transaction.Id,
                    Title = transaction.Title,
                    Date = transaction.Date,
                    Amount = transaction.Amount,
                    PointNumber = transaction.PointNumber,
                    Customer = transaction.Customer,
                    Merchant = transaction.Merchant,
                };
            }
            getResult.SetData(tDto);
            return getResult;
        }

        public
            async Task<IGetServiceResult<TransactionDto>> GetMerchantTransactionsAsync(string username, TransactionDto value)
        {
            var serviceResult = new ServiceResult();

            //validation
            var getResult = new GetServiceResult<TransactionDto>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            var profile = _context.Transactions.ToList();

            TransactionDto tDto;
            if (profile == null)
                tDto = null;
            else
            {
                tDto = new TransactionDto()
                {
                    //Title = profile.Title,
                    //Date = profile.Date,
                    //Amount = profile.Amount,
                    //PointNumber = profile.PointNumber,
                };
            }
            getResult.SetData(tDto);
            return getResult;
        }

        public async Task<IServiceResult> CreateTransaction(string username, TransactionDto value)
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
               await _context.Transactions.AddAsync(new Transaction()
                {
                    Title =  value.Title,
                    Date = value.Date,
                    PointNumber = value.PointNumber,
                    Amount = value.Amount,
                    Customer = value.Customer,
                    Merchant = value.Merchant,
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

     }
}

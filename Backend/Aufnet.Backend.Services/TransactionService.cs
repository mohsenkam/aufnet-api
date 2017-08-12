using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Transaction;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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

        public async Task<IGetServiceResult<List<TransactionDto>>> GetCustomerTransactionsAsync(string username)
        {
            var serviceResult = new ServiceResult();
            //validation
            var getResult = new GetServiceResult<List<TransactionDto>>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            IQueryable<Transaction> transaction =
                _context.Transactions.Where(ct => ct.Customer == user);

            List<TransactionDto> tDtos = transaction.Select(t => new TransactionDto()
            {
                Id = (int)t.Id,
                Title = t.Title,
                Date = t.Date,
                Amount = t.Amount,
                PointNumber = t.PointNumber,
                Customer = t.Customer,
                Merchant = t.Merchant,
            }).ToList();

            getResult.SetData(tDtos);
            return getResult;
        }

        public async Task<IGetServiceResult<List<TransactionDto>>> GetMerchantTransactionsAsync(string username)
        {
            var serviceResult = new ServiceResult();
            //validation
            var getResult = new GetServiceResult<List<TransactionDto>>();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.NotExistingUser.Code,
                    ErrorCodesConstants.NotExistingUser.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
            IQueryable<Transaction> transaction =
                _context.Transactions.Where(ct => ct.Merchant == user);

            List<TransactionDto> tDtos = transaction.Select(t => new TransactionDto()
            {
                Id = (int)t.Id,
                Title = t.Title,
                Date = t.Date,
                Amount = t.Amount,
                PointNumber = t.PointNumber,
                Customer = t.Customer,
                Merchant = t.Merchant,
            }).ToList();

            getResult.SetData(tDtos);
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
                    Title = value.Title,
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

        public async Task<IGetServiceResult<TransactionDto>> GetCustomerTransactionAsync(string username,
           int transactionId)
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
                _context.Transactions.Where(t => t.Id == transactionId).FirstOrDefault();

            var tDto = new TransactionDto()
            {
                Id = (int)transaction.Id,
                Title = transaction.Title,
                Date = transaction.Date,
                Amount = transaction.Amount,
                PointNumber = transaction.PointNumber,
                Customer = transaction.Customer,
                Merchant = transaction.Merchant,
            };

            getResult.SetData(tDto);
            return getResult;
        }

        public async Task<IGetServiceResult<TransactionDto>> GetMerchantTransactionAsync(string username,
            int transactionId)
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
                _context.Transactions.Where(t => t.Id == transactionId).FirstOrDefault();

            TransactionDto tDtos = new TransactionDto()
            {
                Id = (int)transaction.Id,
                Title = transaction.Title,
                Date = transaction.Date,
                Amount = transaction.Amount,
                PointNumber = transaction.PointNumber,
                Customer = transaction.Customer,
                Merchant = transaction.Merchant,
            };

            getResult.SetData(tDtos);
            return getResult;
        }

    }
}
using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/merchants/{username}/transaction")]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET api/merchants/{username}/transaction/GetCustomerTransactionsAsync
        [HttpGet]
        [Route("GetCustomerTransactionsAsync")]
        public async Task<IActionResult> GetCustomerTransactionsAsync(string username)
        {
            //logic
            var result = await _transactionService.GetCustomerTransactionsAsync(username);
            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok(result.GetData());
        }


        // GET api/merchants/{username}/transaction/GetMerchantTransactionsAsync
        [HttpGet]
        [Route("GetMerchantTransactionsAsync")]
        public async Task<IActionResult> GetMerchantTransactionsAsync(string username)
        {
            //logic
            var result = await _transactionService.GetMerchantTransactionsAsync(username);
            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok(result.GetData());
        }


        // POST api/merchants/{username}/transaction
        [HttpPost]
        public async Task<IActionResult> Post(string username, [FromBody]TransactionDto value)
        {
            var result = await _transactionService.CreateTransaction(username, value);
            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok();
        }

        // GET api/merchants/{username}/transaction/GetCustomerTransactionAsync
        [HttpGet]
        [Route("GetCustomerTransactionAsync")]
        public async Task<IActionResult> GetCustomerTransactionAsync(string username, [FromBody] int value)
        {
            //logic
            var result = await _transactionService.GetCustomerTransactionAsync(username, value);
            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok(result.GetData());
        }


        // GET api/merchants/{username}/transaction/GetMerchantTransactionAsync
        [HttpGet]
        [Route("GetMerchantTransactionAsync")]
        public async Task<IActionResult> GetMerchantTransactionAsync(string username, [FromBody] int value)
        {
            //logic
            var result = await _transactionService.GetMerchantTransactionAsync(username, value);
            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok(result.GetData());
        }


    }
}
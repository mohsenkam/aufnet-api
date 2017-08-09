using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.Data.Models.Entities.Transaction;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/transactions/{username}/profile")]
    public class TransactionController: BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET api/customers/john/profile
        [HttpGet]
        public async Task<IActionResult> GetAsync(string username, TransactionDto value)
        {
            //logic
            var result = await _transactionService.GetCustomerTransactionsAsync(username ,  value);
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

        // POST api/transactions/john
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

    }
}

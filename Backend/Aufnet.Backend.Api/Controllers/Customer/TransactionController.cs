using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Transaction;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers.Customer
{
    [Route("api/customers/{username}/transaction")]
    public class TransactionController : BaseController
    {
        //private readonly ITransactionService _transactionService;

        //public TransactionController(ITransactionService transactionService)
        //{
        //    _transactionService = transactionService;
        //}


        //// GET api/customers/{username}/transaction/GetTransactionsAsync
        //[HttpGet]
        //[Route("GetTransactionsAsync")]
        //public async Task<IActionResult> GetTransactionsAsync(string username)
        //{
        //    //logic
        //    var result = await _transactionService.GetTransactionsAsync(username);
        //    if (result.HasError())
        //    {
        //        foreach (var error in result.GetResult().GetErrors())
        //        {
        //            ModelState.AddModelError(error.Code, error.Message);
        //        }

        //        return new ValidationFailedResult(ModelState);
        //    }

        //    return Ok(result.GetData());
        //}


        //// POST api/customers/{username}/transaction
        //[HttpPost]
        //public async Task<IActionResult> Post(string username, [FromBody] TransactionDto value)
        //{
        //    var result = await _transactionService.CreateTransaction(username, value);
        //    if (result.HasError())
        //    {
        //        foreach (var error in result.GetErrors())
        //        {
        //            ModelState.AddModelError(error.Code, error.Message);
        //        }

        //        return new ValidationFailedResult(ModelState);
        //    }
        //    return Ok();
        //}


        //// GET api/customers/{username}/transaction/GetTransactionDetailsAsync
        //[HttpGet]
        //[Route("GetTransactionDetailsAsync")]
        //public async Task<IActionResult> GetTransactionDetailsAsync(string username, [FromBody] int value)
        //{
        //    //logic
        //    var result = await _transactionService.GetTransactionDetailsAsync(username, value);
        //    if (result.HasError())
        //    {
        //        foreach (var error in result.GetResult().GetErrors())
        //        {
        //            ModelState.AddModelError(error.Code, error.Message);
        //        }

        //        return new ValidationFailedResult(ModelState);
        //    }

        //    return Ok(result.GetData());
        //}

    }
}
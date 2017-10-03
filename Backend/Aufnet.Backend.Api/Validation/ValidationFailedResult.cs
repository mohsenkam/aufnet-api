using Aufnet.Backend.ApiServiceShared.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Aufnet.Backend.Api.Validation
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            if (modelState.ContainsKey(ErrorCodesConstants.OperationFailed.Code))
                StatusCode = StatusCodes.Status500InternalServerError;
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}

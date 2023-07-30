using Microsoft.AspNetCore.Mvc.ModelBinding;
using SCCFantasy.Common.Models;

namespace SCCFantasy.Web.Extensions
{
    public static class ModelStateExtension
    {
        public static IEnumerable<ErrorMessage> ToErrorMessages(this ModelStateDictionary modelState)
        {
            IEnumerable<ErrorMessage> errors = modelState.IsValid
                ? null
                : modelState
                    .SelectMany(error => error.Value.Errors.Select(detail => new ErrorMessage(error.Key, detail.ErrorMessage)));
            return errors;
        }
    }
}

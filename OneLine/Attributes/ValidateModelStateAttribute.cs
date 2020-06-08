using Microsoft.AspNetCore.Mvc.Filters;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System.Linq;

namespace OneLine.Attributes
{
    /// <summary>
    /// This class defines an attribute that can be only applied to an action to validate the model state.
    /// If model state is invalid the request ends immediately and sends the error/s message/s in body with status code 200 to the client. 
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new ApiResponse<string[]>() { Status = ApiResponseStatus.Failed, Message = "InvalidModelState", Data = filterContext.ModelState.Values.SelectMany(s => s.Errors).Select(e => e.ErrorMessage).ToArray() }.ToJsonActionResult();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaiyoshaEPE.Utils
{
    public class ValidateModelAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var valid = context.ModelState.IsValid;
            if (!valid)
            {

            }
        }
    }
}

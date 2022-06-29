using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using QuanLyLopHoc;
using QuanLyLopHoc.Models;

namespace TaiyoshaEPE.Utils
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            (string fieldName, ModelStateEntry entry) = context.ModelState
                .First(x => x.Value.Errors.Count > 0);
            string errorSerialized = entry.Errors.First().ErrorMessage;

            ResponseModel rspns = new ResponseModel();
            rspns.Failed(errorSerialized);
            rspns.Result = entry.Errors;

            return new OkObjectResult(rspns);
        }
    }
}

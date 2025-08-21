using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]; // bu validasyonun hangi controller üzerinde çalıştığının bilgisini aldık
            var action = context.RouteData.Values["action"]; // contoller üzerinde hangi metodun çalıştığının bilgisini aldık

            //Dto yu aldık
            var param = context.ActionArguments
                .SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value; // parametre isminde dto geçeni al
            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null." + //400
                    $"Controller : {controller}" +
                    $"Action : {action}");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState); //422
            }
        }
    }
}

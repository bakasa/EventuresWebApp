using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.App.Controllers;
using Eventures.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Eventures.App.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger logger;

        public LogActionFilter(ILogger<EventsController> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string logMsg = $"{DateTime.Now.ToString("g")} Administrator {context.HttpContext.User.Identity.Name} create event {context.HttpContext.Request.Form["Name"]} ({context.HttpContext.Request.Form["Start"]} / {context.HttpContext.Request.Form["End"]})";
            logger.LogInformation(logMsg);
        }
    }
}
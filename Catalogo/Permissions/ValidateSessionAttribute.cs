using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Permissions
{
    public class ValidateSessionAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(HttpContext.Current.Session["user"] == null)
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
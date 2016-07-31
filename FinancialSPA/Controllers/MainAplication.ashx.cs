using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialSPA.Controllers
{
    /// <summary>
    /// Summary description for MainAplication
    /// </summary>
    public class MainAplication : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if(string.IsNullOrEmpty(context.Request.QueryString["Usuario"]))
            {}


            //context.Request.QueryString["LoginUser"];
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
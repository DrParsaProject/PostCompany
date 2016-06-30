using PostCompany.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PostCompany.Controllers
{
    public class LogOutController : ApiController
    {
        
        // POST api/logout
        public HttpResponseMessage Post()
        {
            Authentication.LogOut();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

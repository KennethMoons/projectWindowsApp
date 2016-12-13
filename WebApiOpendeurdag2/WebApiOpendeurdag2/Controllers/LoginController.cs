using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiOpendeurdag2.Controllers
{
    [Authorize]
    public class LoginController : ApiController
    {
        public bool GetLogin()
        {
            return true;
        }
    }
}

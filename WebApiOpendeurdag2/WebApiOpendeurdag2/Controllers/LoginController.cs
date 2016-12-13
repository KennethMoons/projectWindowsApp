using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiOpendeurdag2.Models;
using WebApiOpendeurdag2.Security;

namespace WebApiOpendeurdag2.Controllers
{
    public class LoginController : ApiController
    {
        [ResponseType(typeof(Gebruiker))]
        public IHttpActionResult GetLogin()
        {
            // Check if user is set
            if (!(HttpContext.Current.User is APIPrincipal))
            {
                return NotFound();
            }

            // Return current user
            APIPrincipal principal = (APIPrincipal)HttpContext.Current.User;

            return Ok(principal.User);
        }
    }
}

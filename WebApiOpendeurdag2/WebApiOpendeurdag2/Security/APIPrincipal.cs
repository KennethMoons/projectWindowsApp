using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebApiOpendeurdag2.Models;

namespace WebApiOpendeurdag2.Security
{
    public class APIPrincipal : IPrincipal
    {
        public APIPrincipal(Gebruiker user)
        {
            User = user;
            Identity = new GenericIdentity(user.Email);
        }

        public Gebruiker User { get; set; }
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return !String.IsNullOrEmpty(User.Rol) ? User.Rol.Equals(role) : false;
        }
    }
}
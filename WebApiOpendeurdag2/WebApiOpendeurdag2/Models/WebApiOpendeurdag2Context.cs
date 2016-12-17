using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Models
{
    public class WebApiOpendeurdag2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WebApiOpendeurdag2Context() : base("name=WebApiOpendeurdag2Context")
        {
            if (Environment.UserName.Equals("benoit"))
            {
                Database.Connection.ConnectionString = "Database=Opendeur;Data Source=ITEXT-BLAGAE;Integrated Security=True";
            }
        }

        public System.Data.Entity.DbSet<WebApiOpendeurdag2.Models.Campus> Campus { get; set; }

        public System.Data.Entity.DbSet<WebApiOpendeurdag2.Models.Gebruiker> Gebruikers { get; set; }

        public System.Data.Entity.DbSet<WebApiOpendeurdag2.Models.Infomoment> Infomoments { get; set; }

        public System.Data.Entity.DbSet<WebApiOpendeurdag2.Models.Opleiding> Opleidings { get; set; }

        public System.Data.Entity.DbSet<WebApiOpendeurdag2.Models.Newsitem> Newsitems { get; set; }

        public System.Data.Entity.DbSet<WebApiOpendeurdag2.Models.VoorkeurCampus> VoorkeurCampus { get; set; }

        public System.Data.Entity.DbSet<WebApiOpendeurdag2.Models.VoorkeurOpleiding> VoorkeurOpleidings { get; set; }
    }
}

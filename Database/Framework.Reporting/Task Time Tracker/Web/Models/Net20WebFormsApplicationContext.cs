using System.Data.Entity;
using TaskTimeTracker.Components.BusinessLayer;
using ApplicationContainer.UI.Web.Areas.V45.Models;

namespace Net20WebFormsApplication.Models
{
    public class Net20WebFormsApplicationContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Net20WebFormsApplication.Models.Net20WebFormsApplicationContext>());

        public Net20WebFormsApplicationContext() : base("name=Net20WebFormsApplicationContext")
        {
        }

        public DbSet<Person> People { get; set; }
		//public DbSet<Layer> Layers { get; set; }
    }
}

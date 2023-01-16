using System;
using System.Data.Entity;
using System.Linq;

namespace Day06MidExamTravel
{
    public class JingyuTravelDbContext : DbContext
    {
        // Your context has been configured to use a 'JingyuTravelDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Day06MidExamTravel.JingyuTravelDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'JingyuTravelDbContext' 
        // connection string in the application configuration file.
        public JingyuTravelDbContext()
            : base("name=JingyuTravelDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        //public virtual DbSet<Trip> Trips { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
using System.Data.Entity;

namespace SeniorApplication.App_Data.Context
{
    public class SeniorApplicationContext : DbContext
    {
        public SeniorApplicationContext() : base("name=SeniorApplicationContext")
        {
        }

        public System.Data.Entity.DbSet<SeniorApplication.Models.Product> Products { get; set; }
    }
}

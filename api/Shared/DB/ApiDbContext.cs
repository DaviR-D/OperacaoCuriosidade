using Api.Modules.Authentication.Domain;
using Api.Modules.Clients.Domain;
using Api.Modules.Logs.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Shared.DB
{
    public class ApiDbContext : DbContext
    { 
        public DbSet<User> Users {  get; set; }
        public DbSet<Client> Clients { get; set; }
        //public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("server=FORLOGIC762\\SQLEXPRESS;Database=ApiDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}

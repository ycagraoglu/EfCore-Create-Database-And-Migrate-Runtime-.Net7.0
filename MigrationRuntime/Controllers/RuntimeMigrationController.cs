using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Reflection;

namespace MigrationRuntime.Controllers
{
    [Route("Runtime")]
    [ApiController]
    //[Route("[controller]")]
    public class RuntimeMigrationController : ControllerBase
    {

        [HttpPost]
        [Route("CreateDatabase")]
        public void Post(string dbName)
        {
            SqlConnectionStringBuilder sqlconnectstring = new();

            sqlconnectstring.DataSource = @"localhost,1433";
            sqlconnectstring.InitialCatalog = dbName;
            sqlconnectstring.UserID = "sa";
            sqlconnectstring.Password = "your_password";
            sqlconnectstring.TrustServerCertificate = true;
            sqlconnectstring.Encrypt = false;
            string connectionString = sqlconnectstring.ToString();

            var options = new DbContextOptionsBuilder<AppDbContext>()
              .UseSqlServer(connectionString, sqlServer => sqlServer.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name))
              .Options;

            using var ctx = new AppDbContext(options);
            ctx.Database.Migrate();
        }
    }
}
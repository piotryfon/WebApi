using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithReact.Models
{
    public partial class ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("WebAppWithReactDbContext");
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseLoggerFactory(_loggerFactory);
#if DEBUG
                optionsBuilder.EnableSensitiveDataLogging();
#endif
            }
        }
    }
}

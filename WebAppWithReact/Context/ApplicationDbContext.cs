using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppWithReact.Models;

namespace WebAppWithReact.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        public ApplicationDbContext(DbContextOptions options,
                                    IConfiguration configuration,
                                    ILoggerFactory loggerFactory) : base(options)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }
        public DbSet<WebAppWithReact.Models.Users> Users { get; set; }
    }
}
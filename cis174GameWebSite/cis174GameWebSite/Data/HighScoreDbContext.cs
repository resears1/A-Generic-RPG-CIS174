using cis174GameWebSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cis174GameWebSite.Data
{
    public class HighScoreDbContext : DbContext
    {
        public HighScoreDbContext()
        {
        }

        public HighScoreDbContext(DbContextOptions<HighScoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<HighScoreViewModel> HighScoreViewModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=tcp:cis174person.database.windows.net,1433;Initial Catalog=cis174RpgGame;Persist Security Info=False;User ID=carl;Password=Xukw6774/;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    }
}

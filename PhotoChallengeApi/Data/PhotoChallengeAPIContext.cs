using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoChallengeAPI.Models;

namespace PhotoChallengeAPI.Data
{
    public class PhotoChallengeAPIContext : DbContext
    {
        public string DbPath { get; }

        public PhotoChallengeAPIContext()
        {
            DbPath = Path.Join(Directory.GetCurrentDirectory(), "PhotoDB.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<PhotoItem> PhotoItem { get; set; } = default!;
        public DbSet<PhotoSpecialItem> PhotoSpecialItem { get; set; } = default!;

    }
}


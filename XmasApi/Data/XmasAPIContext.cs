using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XmasAPI.Models;

namespace XmasAPI.Data
{
    public class XmasAPIContext : DbContext
    {
        public string DbPath { get; }

        public XmasAPIContext()
        {
            DbPath = System.IO.Path.Join(Directory.GetCurrentDirectory(), "XmasDB.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<XmasItem> XmasItem { get; set; } = default!;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XmasApi.Models;

namespace XmasApi.Data
{
    public class XmasApiContext : DbContext
    {
        public XmasApiContext (DbContextOptions<XmasApiContext> options)
            : base(options)
        {
        }

        public DbSet<XmasApi.Models.XmasItem> XmasItem { get; set; } = default!;
    }
}


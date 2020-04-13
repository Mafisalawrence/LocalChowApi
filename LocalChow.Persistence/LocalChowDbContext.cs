using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Persistence.Models
{
    public class LocalChowDbContext:DbContext
    {
        public LocalChowDbContext(DbContextOptions<LocalChowDbContext> options) : base(options)
        {
        }
        public DbSet<Store> Store { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}

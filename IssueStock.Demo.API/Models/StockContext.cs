using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueStock.Demo.API.Models
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {

        }

        public DbSet<StockItem> StockItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StockItem>().HasData(
            new StockItem() { Id = 1, Code = "C001", Name = "Company abc Pvt Ltd", Description = "abc Pvt Ltd description" },
            new StockItem() { Id = 2, Code = "C002", Name = "Company pqr Pvt Ltd", Description = "pqr Pvt Ltd description" },
            new StockItem() { Id = 3, Code = "C003", Name = "Company xyz Pvt Ltd", Description = "abc xyz Ltd description" }
            );
        }
    }
}

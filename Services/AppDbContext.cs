using Microsoft.EntityFrameworkCore;
using StoreManager.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace StoreManager.Services
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Port=3306;Database=store_manager;Uid=root;Pwd=root;",
                ServerVersion.AutoDetect(
                    "Server=localhost;Port=3306;Database=store_manager;Uid=root;Pwd=root;"
                )
            );
        }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }
    }
}

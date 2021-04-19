using AppContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppContext.DAL
{
    public class Context : DbContext
    {
        private readonly string dataSourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Data.db");
        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public Context()
        {
            if (!File.Exists(dataSourcePath))
                Database.Migrate();
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={dataSourcePath}");
            }
        }
    }
}

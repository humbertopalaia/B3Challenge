using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Repository
{
    public partial class MainDbContext : DbContext
    {
        private readonly string _connectionString;

        public MainDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;

        }



        public DbSet<Domain.Entities.Task> Task { get; set; }
        public DbSet<Domain.Entities.TaskStatus> TaskStatus { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
                optionsBuilder
                    .UseSqlServer(_connectionString);



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

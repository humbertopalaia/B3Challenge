﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace B3ChallengeRepository
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
            this.ChangeTracker.LazyLoadingEnabled= false;
        }

        public DbSet<B3ChallengeDomain.Task> Task { get; set; }
        public DbSet<B3ChallengeDomain.TaskStatus> TaskStatus { get; set; }
       


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

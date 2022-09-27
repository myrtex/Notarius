using Microsoft.EntityFrameworkCore;
using NotariusBack.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Service
{
    internal class NotariusDbContext : DbContext
    {
        public static NotariusDbContext db = new NotariusDbContext();

        public DbSet<User> Users { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Repository.Entity.Service> Services { get; set; }

        public DbSet<Deal> Deals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=D:\\Kember\\LogDB.db");
        }
    }
}

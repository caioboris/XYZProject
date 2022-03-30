using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZProject.Models;

namespace XYZProject.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> users { get; set; }

        public DbSet<Cliente> clientes { get; set; }

        public DbSet<Produto> produtos { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8OK7LBP\\SQLEXPRESS;Database=XYZProject;Trusted_Connection=True;MultipleActiveResultSets=true");


    }
}

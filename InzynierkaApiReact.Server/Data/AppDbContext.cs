using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InzynierkaApiReact.Server.Models;

namespace InzynierkaApiReact.Server.Data
{
    public class AppDbContext : DbContext 
    {
        //Ta klasa zarządza połączeniem z bazą danych.Z entity Framework
        //Zawiera konfigurację bazy danych i zdefiniowane tabele
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<InzynierkaApiReact.Server.Models.User> User { get; set; } = default!;
        public DbSet<InzynierkaApiReact.Server.Models.Product> Product { get; set; } = default!;
        public DbSet<InzynierkaApiReact.Server.Models.Planogram> Planogram { get; set; } = default!;
        public DbSet<InzynierkaApiReact.Server.Models.ProductLocalization> ProductLocalization { get; set; } = default!;
    }
}

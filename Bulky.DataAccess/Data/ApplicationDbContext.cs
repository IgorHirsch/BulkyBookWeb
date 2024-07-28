using Bulky.Models;  // Importiert die Modelle aus dem Namespace Bulky.Models
using Microsoft.EntityFrameworkCore;  // Importiert die Entity Framework Core Bibliothek

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext  // Definiert eine Klasse, die von DbContext erbt
    {
        // Konstruktor, der DbContextOptions<ApplicationDbContext> als Parameter akzeptiert und an die Basis-Klasse weiterleitet
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Definiert eine DbSet-Eigenschaft für die Kategorie-Entitäten
        public DbSet<Category> Categories { get; set; }

        // Überschreibt die OnModelCreating-Methode, um zusätzliche Konfigurationen für das Modell festzulegen
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fügt Seed-Daten für die Kategorie-Entitäten hinzu
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }
}
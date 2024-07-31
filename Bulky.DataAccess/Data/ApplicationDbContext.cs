using BulkyBook.Models;  // Importiert die Modelle aus dem Namespace Bulky.Models
using Microsoft.EntityFrameworkCore;  // Importiert die Entity Framework Core Bibliothek

namespace BulkyBook.DataAccess.Data
{
    // Definiert eine Klasse namens ApplicationDbContext, die von der Basisklasse DbContext erbt
    public class ApplicationDbContext : DbContext
    {
        // Konstruktor, der eine Instanz von DbContextOptions<ApplicationDbContext> als Parameter akzeptiert
        // und diese an den Konstruktor der Basisklasse weiterleitet
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Hier können bei Bedarf zusätzliche Initialisierungen vorgenommen werden
        }

        // Definiert eine DbSet-Eigenschaft für die Kategorie-Entitäten
        // Diese Eigenschaft repräsentiert eine Tabelle in der Datenbank, die Kategorie-Objekte enthält
        public DbSet<Category> Categories { get; set; }

        // Definiert eine DbSet-Eigenschaft für die Produkt-Entitäten
        // Diese Eigenschaft repräsentiert eine Tabelle in der Datenbank, die Produkt-Objekte enthält
        public DbSet<Product> Products { get; set; }

        // Überschreibt die OnModelCreating-Methode von DbContext, um zusätzliche Konfigurationen für das Modell festzulegen
        //Diese Methode wird überschrieben, um das Modell weiter zu konfigurieren und Seed-Daten (Startdaten) hinzuzufügen,
        //die automatisch in die Datenbank eingefügt werden, wenn die Migrationen ausgeführt werden.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fügt Seed-Daten für die Kategorie-Entitäten hinzu
            // Diese Daten werden in die Datenbank eingefügt, wenn die Migrationen angewendet werden
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );

            // Fügt Seed-Daten für die Produkt-Entitäten hinzu
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                }
            );
        }
    }
}
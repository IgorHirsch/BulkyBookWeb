using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    // Definiert eine Klasse ProductRepository, die von der generischen Repository-Klasse erbt und das IProductRepository-Interface implementiert
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        // Private Feldvariable, die den ApplicationDbContext speichert
        private ApplicationDbContext _db;

        // Konstruktor, der den ApplicationDbContext injiziert und an die Basis-Repository-Klasse weitergibt
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            // Weist den injizierten ApplicationDbContext der privaten Feldvariable zu
            _db = db;
        }

        // Implementiert die Update-Methode des IProductRepository-Interfaces zum Aktualisieren eines Produkt-Objekts
        public void Update(Product obj)
        {
            // Sucht das Produkt-Objekt in der Datenbank, das die gleiche Id wie das übergebene Objekt hat
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);

            // Wenn das Produkt-Objekt in der Datenbank gefunden wurde, aktualisiert die Eigenschaften des Objekts
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;         // Aktualisiert den Titel des Produkts
                objFromDb.ISBN = obj.ISBN;           // Aktualisiert die ISBN des Produkts
                objFromDb.Price = obj.Price;         // Aktualisiert den Preis des Produkts
                objFromDb.Price50 = obj.Price50;     // Aktualisiert den Preis für 50 oder mehr Einheiten
                objFromDb.ListPrice = obj.ListPrice; // Aktualisiert den Listenpreis des Produkts
                objFromDb.Price100 = obj.Price100;   // Aktualisiert den Preis für 100 oder mehr Einheiten
                objFromDb.Description = obj.Description; // Aktualisiert die Beschreibung des Produkts
                objFromDb.CategoryId = obj.CategoryId; // Aktualisiert die Kategorie-ID des Produkts
                objFromDb.Author = obj.Author;       // Aktualisiert den Autor des Produkts
                objFromDb.ProductImages = obj.ProductImages;
                // Wenn die ImageUrl im übergebenen Produktobjekt nicht null ist, aktualisiert die ImageUrl des Produkts
                //if (obj.ImageUrl != null)
                //{
                //    objFromDb.ImageUrl = obj.ImageUrl;
                //}
            }
        }
    }

    /* Erklärungen mit Logik

    1. Definition der Klasse ProductRepository:
       - Die Klasse `ProductRepository` erbt von der generischen `Repository<Product>`-Klasse und implementiert das `IProductRepository`-Interface.
       - Dies bedeutet, dass `ProductRepository` alle Methoden und Eigenschaften der generischen `Repository`-Klasse erbt und die spezifischen Methoden des `IProductRepository`-Interfaces implementiert.

    2. Private Feldvariable _db:
       - Die private Feldvariable `_db` speichert eine Instanz des `ApplicationDbContext`.
       - Diese Variable wird verwendet, um auf die Datenbank zuzugreifen und Operationen durchzuführen.

    3. Konstruktor:
       - Der Konstruktor akzeptiert eine Instanz des `ApplicationDbContext` als Parameter und übergibt diese an die Basisklasse `Repository` mittels des `base(db)`-Aufrufs.
       - Der Konstruktor weist die injizierte `ApplicationDbContext`-Instanz der privaten Feldvariable `_db` zu.

    4. Update-Methode:
       - Die `Update`-Methode implementiert die Methode des `IProductRepository`-Interfaces und dient zum Aktualisieren eines gegebenen Produkt-Objekts.
       - Die Methode sucht das bestehende Produkt in der Datenbank anhand der `Id` des übergebenen Objekts (`FirstOrDefault(u => u.Id == obj.Id)`).
       - Falls das Produkt gefunden wird (`objFromDb != null`), werden die Eigenschaften des gefundenen Objekts mit den Werten des übergebenen Objekts aktualisiert.
       - Wenn die `ImageUrl` des übergebenen Objekts nicht `null` ist, wird auch die `ImageUrl` des gefundenen Produkts aktualisiert.

    Dieses Muster ermöglicht es, ein bestehendes Produkt in der Datenbank effizient zu aktualisieren, indem nur die geänderten Eigenschaften gesetzt werden.
    */
}

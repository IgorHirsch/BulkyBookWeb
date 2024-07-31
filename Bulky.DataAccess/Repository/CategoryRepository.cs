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
    // Definiert eine Klasse CategoryRepository, die von der generischen Repository-Klasse erbt und das ICategoryRepository-Interface implementiert
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        // Private Feldvariable, die den ApplicationDbContext speichert
        private ApplicationDbContext _db;

        // Konstruktor, der den ApplicationDbContext injiziert und an die Basis-Repository-Klasse weitergibt
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            // Weist den injizierten ApplicationDbContext der privaten Feldvariable zu
            _db = db;
        }

        // Implementiert die Update-Methode des ICategoryRepository-Interfaces zum Aktualisieren eines Kategorie-Objekts
        public void Update(Category obj)
        {
            // Verwendet den ApplicationDbContext, um das gegebene Kategorie-Objekt zu aktualisieren
            _db.Categories.Update(obj);
        }
    }

    /* Erklärungen mit Logik

    1. Definition der Klasse CategoryRepository:
       - Die Klasse `CategoryRepository` erbt von der generischen `Repository<Category>`-Klasse und implementiert das `ICategoryRepository`-Interface.
       - Dies bedeutet, dass `CategoryRepository` alle Methoden und Eigenschaften der generischen `Repository`-Klasse erbt und die spezifischen Methoden des `ICategoryRepository`-Interfaces implementiert.

    2. Private Feldvariable _db:
       - Die private Feldvariable `_db` speichert eine Instanz des `ApplicationDbContext`.
       - Diese Variable wird verwendet, um auf die Datenbank zuzugreifen und Operationen durchzuführen.

    3. Konstruktor:
       - Der Konstruktor akzeptiert eine Instanz des `ApplicationDbContext` als Parameter und übergibt diese an die Basisklasse `Repository` mittels des `base(db)`-Aufrufs.
       - Der Konstruktor weist die injizierte `ApplicationDbContext`-Instanz der privaten Feldvariable `_db` zu.

    4. Update-Methode:
       - Die `Update`-Methode implementiert die Methode des `ICategoryRepository`-Interfaces und dient zum Aktualisieren eines gegebenen Kategorie-Objekts.
       - Die Methode verwendet den `ApplicationDbContext` (`_db`) und dessen `Categories.Update(obj)`-Methode, um das gegebene Kategorie-Objekt in der Datenbank zu aktualisieren.

    Dieses Muster stellt sicher, dass die `CategoryRepository`-Klasse spezifische Logik für Kategorien-Entitäten bereitstellt und gleichzeitig die generischen CRUD-Operationen der Basisklasse `Repository` nutzt.
    */
}

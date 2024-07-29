using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Der ApplicationDbContext stellt die Verbindung zur Datenbank bereit
        // und ermöglicht die Interaktion mit der Datenbank.
        private readonly ApplicationDbContext _db;

        // DbSet<T> ist eine Sammlung von Entitäten des Typs T, die für CRUD-Operationen verwendet wird.
        internal DbSet<T> dbSet;

        // Konstruktor, der den ApplicationDbContext injiziert und das DbSet initialisiert.
        public Repository(ApplicationDbContext db)
        {
            _db = db; // Der Datenbankkontext wird zugewiesen.
                      // Das DbSet wird für den Entitätstyp T initialisiert. Dies ermöglicht
                      // CRUD-Operationen für die Entität T.
            this.dbSet = _db.Set<T>();

            // Hinweis: _db.Categories wäre spezifisch für eine Category-Entität.
            // Für andere Entitätstypen wird hier das generische DbSet<T> verwendet.
        }

        // Methode zum Hinzufügen einer neuen Entität des Typs T in die Datenbank.
        public void Add(T entity)
        {
            // Fügt die gegebene Entität zum DbSet hinzu, damit sie beim nächsten
            // Aufruf von SaveChanges() in die Datenbank eingefügt wird.
            dbSet.Add(entity);
        }

        // Methode zum Abrufen einer einzelnen Entität des Typs T basierend auf einem Filter.
        public T Get(Expression<Func<T, bool>> filter)
        {
            // Erstellen einer Abfrage (IQueryable) auf dem DbSet für den Entitätstyp T.
            IQueryable<T> query = dbSet;

            // Anwenden des Filters auf die Abfrage. Der Filter ist eine Ausdrucksfunktion,
            // die Bedingungen definiert, nach denen die Entitäten ausgewählt werden.
            query = query.Where(filter);

            // Gibt die erste Entität zurück, die dem Filter entspricht, oder null,
            // wenn keine Entität gefunden wird.
            return query.FirstOrDefault();
        }

        // Methode zum Abrufen aller Entitäten des Typs T aus der Datenbank.
        public IEnumerable<T> GetAll()
        {
            // Erstellen einer Abfrage auf dem DbSet, um alle Entitäten abzurufen.
            IQueryable<T> query = dbSet;

            // Konvertiert die Abfrage in eine Liste und gibt alle Entitäten zurück.
            // Diese Methode holt alle Datensätze der Entität T.
            return query.ToList();
        }

        // Methode zum Entfernen einer einzelnen Entität des Typs T aus der Datenbank.
        public void Remove(T entity)
        {
            // Entfernt die gegebene Entität aus dem DbSet. Dies markiert die Entität
            // als gelöscht, sodass sie beim nächsten Aufruf von SaveChanges() entfernt wird.
            dbSet.Remove(entity);
        }

        // Methode zum Entfernen mehrerer Entitäten des Typs T aus der Datenbank.
        public void RemoveRange(IEnumerable<T> entities)
        {
            // Entfernt die angegebenen Entitäten aus dem DbSet. Alle angegebenen
            // Entitäten werden als gelöscht markiert und beim nächsten Aufruf von SaveChanges()
            // entfernt.
            dbSet.RemoveRange(entities);
        }
    }
}

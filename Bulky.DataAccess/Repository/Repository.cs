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
    // Definiert eine generische Repository-Klasse, die das IRepository<T>-Interface implementiert
    public class Repository<T> : IRepository<T> where T : class
    {
        // Private readonly Feldvariable für den ApplicationDbContext, der die Datenbankoperationen durchführt
        private readonly ApplicationDbContext _db;

        // Interne Feldvariable für das DbSet<T>, das die Entitäten der Klasse T repräsentiert
        internal DbSet<T> dbSet;

        // Konstruktor, der den ApplicationDbContext injiziert und das DbSet<T> initialisiert
        public Repository(ApplicationDbContext db)
        {
            // Weist den injizierten ApplicationDbContext der privaten Feldvariable zu
            _db = db;
            // Initialisiert das DbSet<T> basierend auf dem Typ T
            this.dbSet = _db.Set<T>();

            // Optionales Beispiel für das Einbinden von verwandten Entitäten (nicht unbedingt erforderlich)
            // Hier ist es als Kommentar, da es nicht zur Initialisierung gehört.
            // _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }

        // Fügt eine neue Entität vom Typ T zur Datenbank hinzu
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        // Ruft eine einzelne Entität basierend auf dem angegebenen Filter und optionalen verknüpften Eigenschaften ab
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            // Erzeugt eine IQueryable<T> Abfrage auf dem DbSet<T>
            IQueryable<T> query = dbSet;

            // Wendet den Filter auf die Abfrage an
            query = query.Where(filter);

            // Falls es optionale verknüpfte Eigenschaften gibt, werden diese in die Abfrage aufgenommen
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            // Gibt das erste Element zurück, das dem Filter entspricht, oder null, wenn kein Element gefunden wurde
            return query.FirstOrDefault();
        }

        // Ruft alle Entitäten ab, optional einschließlich verknüpfter Eigenschaften
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            // Erzeugt eine IQueryable<T> Abfrage auf dem DbSet<T>
            IQueryable<T> query = dbSet;

            // Falls es optionale verknüpfte Eigenschaften gibt, werden diese in die Abfrage aufgenommen
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            // Gibt eine Liste aller Entitäten zurück
            return query.ToList();
        }

        // Entfernt eine Entität vom Typ T aus der Datenbank
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        // Entfernt eine Sammlung von Entitäten vom Typ T aus der Datenbank
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }

    /* Erklärungen mit Logik

    1. Definition der generischen Repository-Klasse:
       - Die Klasse `Repository<T>` ist generisch und implementiert das `IRepository<T>`-Interface.
       - Der Typ T ist eine Referenztyp-Klasse, die die Datenbankentitäten repräsentiert.

    2. Private readonly Feldvariable _db:
       - Die Feldvariable `_db` speichert eine Instanz des `ApplicationDbContext`, die für Datenbankoperationen verwendet wird.

    3. Interne Feldvariable dbSet:
       - Die interne Feldvariable `dbSet` speichert eine Instanz von `DbSet<T>`, die die Entitäten vom Typ T verwaltet.
       - `DbSet<T>` wird durch den Aufruf `_db.Set<T>()` initialisiert, der das entsprechende `DbSet` für den Entitätstyp T zurückgibt.

    4. Konstruktor:
       - Der Konstruktor akzeptiert eine Instanz von `ApplicationDbContext` und initialisiert das `dbSet`-Feld.
       - Ein Kommentar zeigt ein Beispiel für das Einbinden verwandter Entitäten an, was hier jedoch nicht angewendet wird.

    5. Add-Methode:
       - Die `Add`-Methode fügt eine neue Entität vom Typ T zum `dbSet` hinzu, wodurch sie für die spätere Speicherung in der Datenbank markiert wird.

    6. Get-Methode:
       - Die `Get`-Methode sucht eine einzelne Entität basierend auf einem Filterausdruck (`filter`).
       - Optionale Verknüpfungen werden durch den `includeProperties`-Parameter berücksichtigt, der eine Liste von Eigenschaften enthält, die in die Abfrage aufgenommen werden sollen.
       - Die Methode gibt das erste gefundene Element oder `null` zurück.

    7. GetAll-Methode:
       - Die `GetAll`-Methode gibt alle Entitäten des Typs T zurück.
       - Verknüpfte Eigenschaften, die durch `includeProperties` spezifiziert sind, werden ebenfalls berücksichtigt.
       - Die Methode gibt eine Liste aller Entitäten zurück.

    8. Remove-Methode:
       - Die `Remove`-Methode entfernt eine einzelne Entität vom Typ T aus dem `dbSet`, wodurch sie für die Löschung in der Datenbank markiert wird.

    9. RemoveRange-Methode:
       - Die `RemoveRange`-Methode entfernt mehrere Entitäten vom Typ T aus dem `dbSet`, wodurch sie für die Löschung in der Datenbank markiert werden.

    Dieses Repository-Muster ermöglicht die Implementierung von generischen Datenzugriffsoperationen, die auf verschiedene Entitätstypen anwendbar sind, während spezifische Abfragen und Operationen für jeden Entitätstyp bereitgestellt werden können.
    */
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // Definiert eine generische Repository-Schnittstelle für Entitäten vom Typ T, 
    // wobei T eine Klasse sein muss.
    public interface IRepository<T> where T : class
    {
        // Gibt alle Instanzen der Entität T zurück.
        IEnumerable<T> GetAll();

        // Gibt eine einzelne Instanz der Entität T zurück, die einem bestimmten Filterkriterium entspricht.
        // Der Filter ist ein Lambda-Ausdruck, der eine Bedingung beschreibt, die die Entität erfüllen muss.
        T Get(Expression<Func<T, bool>> filter);

        // Fügt eine neue Instanz der Entität T in die Datenbank ein.
        void Add(T entity);

        // Entfernt eine einzelne Instanz der Entität T aus der Datenbank.
        void Remove(T entity);

        // Entfernt mehrere Instanzen der Entität T aus der Datenbank.
        void RemoveRange(IEnumerable<T> entity);
    }
}
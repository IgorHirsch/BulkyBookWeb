using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // Definiert ein generisches Interface IRepository<T> mit Einschränkung, dass T eine Klasse sein muss
    public interface IRepository<T> where T : class
    {
        // Deklariert eine Methode, die eine IEnumerable<T> zurückgibt
        // Diese Methode dient dazu, alle Entitäten vom Typ T abzurufen
        // Der optionale Parameter includeProperties erlaubt das Einschließen verknüpfter Entitäten
        IEnumerable<T> GetAll(string? includeProperties = null);

        // Deklariert eine Methode, die eine einzelne Entität vom Typ T zurückgibt
        // Diese Methode dient dazu, eine Entität zu finden, die dem übergebenen Filter entspricht
        // Der optionale Parameter includeProperties erlaubt das Einschließen verknüpfter Entitäten
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        // Deklariert eine Methode zum Hinzufügen einer neuen Entität vom Typ T
        void Add(T entity);

        // Deklariert eine Methode zum Entfernen einer einzelnen Entität vom Typ T
        void Remove(T entity);

        // Deklariert eine Methode zum Entfernen mehrerer Entitäten vom Typ T
        void RemoveRange(IEnumerable<T> entity);
    }

    /* Erklärungen mit Logik

    1. Definition des generischen Interfaces IRepository<T>:
       - Das Interface `IRepository<T>` wird definiert und ist generisch, was bedeutet, dass es für jede Klasse (angegeben durch T) verwendet werden kann.
       - Die Einschränkung `where T : class` stellt sicher, dass T eine Referenztypklasse ist.

    2. GetAll-Methode:
       - Deklariert eine Methode `GetAll`, die eine `IEnumerable<T>` zurückgibt, welche alle Entitäten vom Typ T enthält.
       - Der optionale Parameter `includeProperties` erlaubt es, verknüpfte Entitäten einzuschließen (z.B. mittels Eager Loading).

    3. Get-Methode:
       - Deklariert eine Methode `Get`, die eine einzelne Entität vom Typ T zurückgibt.
       - Diese Methode verwendet einen `Expression<Func<T, bool>>`-Filter, um die gesuchte Entität zu finden.
       - Der optionale Parameter `includeProperties` erlaubt das Einschließen verknüpfter Entitäten.

    4. Add-Methode:
       - Deklariert eine Methode `Add`, die eine neue Entität vom Typ T zur Datenbank hinzufügt.

    5. Remove-Methode:
       - Deklariert eine Methode `Remove`, die eine einzelne Entität vom Typ T aus der Datenbank entfernt.

    6. RemoveRange-Methode:
       - Deklariert eine Methode `RemoveRange`, die mehrere Entitäten vom Typ T aus der Datenbank entfernt.

    Dieses Interface definiert grundlegende CRUD-Operationen (Create, Read, Update, Delete), die für jede Entitätsklasse implementiert werden können. Es stellt sicher, dass jede Klasse, die dieses Interface implementiert, diese grundlegenden Datenbankoperationen unterstützt.
    */
}

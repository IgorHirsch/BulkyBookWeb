using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // Definiert ein Interface IProductRepository, das von einem generischen IRepository-Interface erbt
    public interface IProductRepository : IRepository<Product>
    {
        // Deklariert eine Methode zum Aktualisieren eines Produktobjekts
        // Diese Methode akzeptiert ein Produktobjekt als Parameter und hat keinen Rückgabewert
        void Update(Product obj);
    }

    /* Erklärungen mit Logik

    1. Definition des Interfaces IProductRepository:
       - Das Interface `IProductRepository` wird definiert und erbt von einem generischen Interface `IRepository<Product>`. Dies bedeutet, dass `IProductRepository` alle Methoden und Eigenschaften von `IRepository<Product>` übernimmt, die auf `Product`-Entitäten angewendet werden.

    2. Deklaration der Update-Methode:
       - Innerhalb des `IProductRepository`-Interfaces wird eine Methode `Update` deklariert. Diese Methode ist dafür vorgesehen, ein Produktobjekt zu aktualisieren.
       - Die `Update`-Methode akzeptiert ein `Product`-Objekt als Parameter und hat keinen Rückgabewert (`void`).
       - Da es sich um ein Interface handelt, wird nur die Methodensignatur deklariert. Die Implementierung der Methode erfolgt in der Klasse, die dieses Interface implementiert.

    Dieses Interface ermöglicht es, spezifische Methoden für die Produktverwaltung zu definieren und gleichzeitig die allgemeine Struktur und Funktionen des generischen `IRepository<Product>`-Interfaces zu nutzen. Die `Update`-Methode wird verwendet, um Änderungen an bestehenden Produktobjekten zu speichern.
    */
}

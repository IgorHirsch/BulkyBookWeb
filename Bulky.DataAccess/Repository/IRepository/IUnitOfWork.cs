using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // Definiert ein Interface IUnitOfWork, das das Unit of Work-Pattern implementiert
    public interface IUnitOfWork
    {
        // Eine Eigenschaft, die ein spezielles Repository für Kategorien bereitstellt
        ICategoryRepository Category { get; }

        // Eine Eigenschaft, die ein spezielles Repository für Produkte bereitstellt
        IProductRepository Product { get; }

        ICompanyRepository Company { get; }

        IShoppingCartRepository ShoppingCart { get; }

        IApplicationUserRepository ApplicationUser { get; }

        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IProductImageRepository ProductImage { get; }

        // Eine Methode, die alle Änderungen speichert (committed), die in der aktuellen Transaktion gemacht wurden
        void Save();
    }

    /* Erklärungen mit Logik

    1. Definition des Interfaces IUnitOfWork:
       - Das Interface `IUnitOfWork` wird definiert, um das Unit of Work-Pattern zu implementieren.
       - Das Unit of Work-Pattern soll sicherstellen, dass alle Änderungen in einer einzigen Transaktion durchgeführt und entweder vollständig erfolgreich sind oder bei einem Fehler zurückgesetzt werden.

    2. Category-Eigenschaft:
       - Deklariert eine Eigenschaft `Category`, die ein spezielles Repository für Kategorien bereitstellt.
       - `ICategoryRepository` ist eine benutzerdefinierte Schnittstelle für das Kategorien-Repository.

    3. Product-Eigenschaft:
       - Deklariert eine Eigenschaft `Product`, die ein spezielles Repository für Produkte bereitstellt.
       - `IProductRepository` ist eine benutzerdefinierte Schnittstelle für das Produkte-Repository.

    4. Save-Methode:
       - Deklariert eine Methode `Save`, die alle Änderungen speichert (committed), die in der aktuellen Transaktion gemacht wurden.
       - Diese Methode stellt sicher, dass alle Repository-Änderungen in der Datenbank gespeichert werden.

    Das `IUnitOfWork`-Interface ermöglicht eine zentrale Verwaltung von Transaktionen und stellt sicher, dass Änderungen in mehreren Repositories als eine atomare Operation behandelt werden. Dies verbessert die Konsistenz und Integrität der Daten.
    */
}

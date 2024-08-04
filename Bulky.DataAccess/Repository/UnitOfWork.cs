using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        // Der ApplicationDbContext stellt die Verbindung zur Datenbank bereit und ermöglicht Interaktionen mit der Datenbank.
        private ApplicationDbContext _db;

        // Implementiert die ICategoryRepository-Eigenschaft von IUnitOfWork.
        // Diese Eigenschaft stellt das Repository für die Category-Entitäten bereit.
        public ICategoryRepository Category { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IProductRepository Product { get; private set; }

        // Konstruktor, der den ApplicationDbContext injiziert und das CategoryRepository initialisiert.
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db; // Setzt den ApplicationDbContext, der für die Datenbankoperationen verwendet wird.

            // Initialisiert die Category-Eigenschaft mit einer neuen Instanz von CategoryRepository.
            // Das Repository verwendet denselben ApplicationDbContext.
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
        }

        // Methode zum Speichern aller Änderungen, die im aktuellen Kontext vorgenommen wurden.
        public void Save()
        {
            // Ruft die SaveChanges-Methode des ApplicationDbContext auf, um alle Änderungen an der Datenbank zu speichern.
            // Alle Änderungen, die an Entitäten im Kontext vorgenommen wurden, werden hierdurch permanent gemacht.
            _db.SaveChanges();
        }
    }
}

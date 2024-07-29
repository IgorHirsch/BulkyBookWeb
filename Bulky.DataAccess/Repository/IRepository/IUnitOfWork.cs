using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        // Eine Eigenschaft, die ein spezielles Repository für Kategorien bereitstellt.
        ICategoryRepository Category { get; }

        // Eine Methode, die alle Änderungen speichert (committed), die in der aktuellen Transaktion gemacht wurden.
        void Save();
    }
}

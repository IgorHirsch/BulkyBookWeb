using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // Definiert ein spezielles Repository für die Kategorie-Entität, das von einem allgemeinen Repository erbt.
    public interface ICategoryRepository : IRepository<Category>
    {
        // Fügt eine Methode zum Aktualisieren einer Kategorie hinzu.
        void Update(Category obj);
    }
}

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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        // Konstruktor, der den ApplicationDbContext injiziert und an die Basis-Repository-Klasse weitergibt
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // Methode zum Aktualisieren eines Kategorie-Objekts
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}

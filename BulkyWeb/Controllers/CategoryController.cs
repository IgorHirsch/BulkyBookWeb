// Importiert den Datenzugriffskontext
using Bulky.DataAccess.Data;

// Importiert die Modellklasse Category
using Bulky.Models;

// Importiert die MVC-Bibliothek
using Microsoft.AspNetCore.Mvc;

// Definiert den Namespace für den Controller
namespace BulkyWeb.Controllers
{
    // Definiert den CategoryController, der von der Controller-Basis-Klasse erbt
    public class CategoryController : Controller
    {
        // Deklariert ein Feld für den Datenbankkontext
        private readonly ApplicationDbContext _db;

        // Konstruktor, der den Datenbankkontext initialisiert
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Index-Aktion, um die Liste der Kategorien anzuzeigen
        public IActionResult Index()
        {
            // Ruft alle Kategorien aus der Datenbank ab
            List<Category> objCategoryList = _db.Categories.ToList();
            // Gibt die Kategorienliste an die View zurück
            return View(objCategoryList);
        }

        // GET-Aktion für die Erstellung einer neuen Kategorie
        public IActionResult Create()
        {
            // Gibt die Create-View zurück
            return View();
        }

        // POST-Aktion für die Erstellung einer neuen Kategorie
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Überprüft, ob der Name und die DisplayOrder gleich sind
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // Fügt ein Modellfehler hinzu
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            // Überprüft, ob das Modell gültig ist
            if (ModelState.IsValid)
            {
                // Fügt die neue Kategorie der Datenbank hinzu
                _db.Categories.Add(obj);
                // Speichert die Änderungen in der Datenbank
                _db.SaveChanges();
                // Setzt eine Erfolgsmeldung
                TempData["success"] = "Category created successfully";
                // Leitet zur Index-Aktion um
                return RedirectToAction("Index");
            }
            // Gibt die Create-View mit dem Modell zurück
            return View();
        }

        // GET-Aktion für die Bearbeitung einer Kategorie
        public IActionResult Edit(int? id)
        {
            // Überprüft, ob die ID null oder 0 ist
            if (id == null || id == 0)
            {
                // Gibt einen NotFound-Fehler zurück
                return NotFound();
            }
            // Findet die Kategorie in der Datenbank
            Category? categoryFromDb = _db.Categories.Find(id);

            // Alternative Methoden zum Finden einer Kategorie
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            // Überprüft, ob die Kategorie nicht gefunden wurde
            if (categoryFromDb == null)
            {
                // Gibt einen NotFound-Fehler zurück
                return NotFound();
            }
            // Gibt die Edit-View mit der gefundenen Kategorie zurück
            return View(categoryFromDb);
        }

        // POST-Aktion für die Bearbeitung einer Kategorie
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // Überprüft, ob das Modell gültig ist
            if (ModelState.IsValid)
            {
                // Aktualisiert die Kategorie in der Datenbank
                _db.Categories.Update(obj);
                // Speichert die Änderungen in der Datenbank
                _db.SaveChanges();
                // Setzt eine Erfolgsmeldung
                TempData["success"] = "Category updated successfully";
                // Leitet zur Index-Aktion um
                return RedirectToAction("Index");
            }
            // Gibt die Edit-View mit dem Modell zurück
            return View();
        }

        // GET-Aktion für das Löschen einer Kategorie
        public IActionResult Delete(int? id)
        {
            // Überprüft, ob die ID null oder 0 ist
            if (id == null || id == 0)
            {
                // Gibt einen NotFound-Fehler zurück
                return NotFound();
            }
            // Findet die Kategorie in der Datenbank
            Category? categoryFromDb = _db.Categories.Find(id);

            // Überprüft, ob die Kategorie nicht gefunden wurde
            if (categoryFromDb == null)
            {
                // Gibt einen NotFound-Fehler zurück
                return NotFound();
            }
            // Gibt die Delete-View mit der gefundenen Kategorie zurück
            return View(categoryFromDb);
        }

        // POST-Aktion für das Löschen einer Kategorie
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            // Findet die Kategorie in der Datenbank
            Category? obj = _db.Categories.Find(id);
            // Überprüft, ob die Kategorie nicht gefunden wurde
            if (obj == null)
            {
                // Gibt einen NotFound-Fehler zurück
                return NotFound();
            }
            // Entfernt die Kategorie aus der Datenbank
            _db.Categories.Remove(obj);
            // Speichert die Änderungen in der Datenbank
            _db.SaveChanges();
            // Setzt eine Erfolgsmeldung
            TempData["success"] = "Category deleted successfully";
            // Leitet zur Index-Aktion um
            return RedirectToAction("Index");
        }
    }
}
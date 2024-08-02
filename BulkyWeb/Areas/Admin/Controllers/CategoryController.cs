using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        // Das IUnitOfWork-Interface wird verwendet, um die Datenzugriffsoperationen zu kapseln.
        private readonly IUnitOfWork _unitOfWork;

        // Konstruktor der Controller-Klasse, der das UnitOfWork-Objekt injiziert.
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; // Der UnitOfWork wird für den Zugriff auf die Repositories gespeichert.
        }

        // Aktion, die die Liste aller Kategorien anzeigt.
        public IActionResult Index()
        {
            // Ruft alle Kategorien aus dem Repository ab und konvertiert sie in eine Liste.
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();

            // Gibt die Liste der Kategorien an die View zurück.
            return View(objCategoryList);
        }

        // Aktion, die das Formular zur Erstellung einer neuen Kategorie anzeigt.
        public IActionResult Create()
        {
            return View(); // Gibt die View zurück, in der der Benutzer eine neue Kategorie erstellen kann.
        }

        // POST-Aktion, die eine neue Kategorie erstellt.
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Validierung: Überprüft, ob der Name der Kategorie exakt dem DisplayOrder entspricht.
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // Wenn dies der Fall ist, wird ein ModelState-Fehler hinzugefügt, der im View angezeigt wird.
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            // Überprüft, ob das ModelState nach allen Validierungen gültig ist.
            if (ModelState.IsValid)
            {
                // Fügt die neue Kategorie dem Repository hinzu.
                _unitOfWork.Category.Add(obj);

                // Speichert alle Änderungen in der Datenbank.
                _unitOfWork.Save();

                // Setzt eine TempData-Nachricht, die beim nächsten Request angezeigt wird.
                TempData["success"] = "Category created successfully";

                // Leitet den Benutzer zur Index-Action um, um die aktualisierte Liste der Kategorien anzuzeigen.
                return RedirectToAction("Index");
            }

            // Wenn das ModelState ungültig ist, wird die View mit den aktuellen Daten zurückgegeben.
            return View();
        }

        // Aktion, die das Formular zum Bearbeiten einer Kategorie anzeigt.
        public IActionResult Edit(int? id)
        {
            // Überprüft, ob die ID null oder 0 ist.
            if (id == null || id == 0)
            {
                // Gibt einen 404-Fehler zurück, wenn die ID nicht vorhanden ist.
                return NotFound();
            }

            // Ruft die Kategorie aus dem Repository ab, die der angegebenen ID entspricht.
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            // Überprüft, ob die Kategorie nicht gefunden wurde.
            if (categoryFromDb == null)
            {
                // Gibt einen 404-Fehler zurück, wenn keine Kategorie gefunden wurde.
                return NotFound();
            }

            // Gibt die gefundene Kategorie an die View zurück.
            return View(categoryFromDb);
        }

        // POST-Aktion, die eine bestehende Kategorie aktualisiert.
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // Überprüft, ob das ModelState nach allen Validierungen gültig ist.
            if (ModelState.IsValid)
            {
                // Aktualisiert die Kategorie im Repository.
                _unitOfWork.Category.Update(obj);

                // Speichert alle Änderungen in der Datenbank.
                _unitOfWork.Save();

                // Setzt eine TempData-Nachricht, die beim nächsten Request angezeigt wird.
                TempData["success"] = "Category updated successfully";

                // Leitet den Benutzer zur Index-Action um, um die aktualisierte Liste der Kategorien anzuzeigen.
                return RedirectToAction("Index");
            }

            // Wenn das ModelState ungültig ist, wird die View mit den aktuellen Daten zurückgegeben.
            return View();
        }

        // Aktion, die das Formular zur Bestätigung des Löschens einer Kategorie anzeigt.
        public IActionResult Delete(int? id)
        {
            // Überprüft, ob die ID null oder 0 ist.
            if (id == null || id == 0)
            {
                // Gibt einen 404-Fehler zurück, wenn die ID nicht vorhanden ist.
                return NotFound();
            }

            // Ruft die Kategorie aus dem Repository ab, die der angegebenen ID entspricht.
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            // Überprüft, ob die Kategorie nicht gefunden wurde.
            if (categoryFromDb == null)
            {
                // Gibt einen 404-Fehler zurück, wenn keine Kategorie gefunden wurde.
                return NotFound();
            }

            // Gibt die gefundene Kategorie an die View zurück.
            return View(categoryFromDb);
        }

        // POST-Aktion, die das tatsächliche Löschen einer Kategorie durchführt.
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            // Ruft die Kategorie aus dem Repository ab, die der angegebenen ID entspricht.
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);

            // Überprüft, ob die Kategorie nicht gefunden wurde.
            if (obj == null)
            {
                // Gibt einen 404-Fehler zurück, wenn keine Kategorie gefunden wurde.
                return NotFound();
            }

            // Entfernt die Kategorie aus dem Repository.
            _unitOfWork.Category.Remove(obj);

            // Speichert alle Änderungen in der Datenbank.
            _unitOfWork.Save();

            // Setzt eine TempData-Nachricht, die beim nächsten Request angezeigt wird.
            TempData["success"] = "Category deleted successfully";

            // Leitet den Benutzer zur Index-Action um, um die aktualisierte Liste der Kategorien anzuzeigen.
            return RedirectToAction("Index");
        }
    }
}

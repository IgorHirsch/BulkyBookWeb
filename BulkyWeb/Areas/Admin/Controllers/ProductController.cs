using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    // Der Controller befindet sich im "Admin"-Bereich
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        // Dependency Injection: IUnitOfWork und IWebHostEnvironment werden in den Konstruktor übergeben
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Konstruktor des Controllers
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork; // Zuweisung des IUnitOfWork-Services
            _webHostEnvironment = webHostEnvironment; // Zuweisung des IWebHostEnvironment-Services
        }

        // GET: /Admin/Product/Index
        public IActionResult Index()
        {
            // Ruft alle Produkte einschließlich ihrer Kategorien ab
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            // Gibt die Liste an die View zurück
            return View(objProductList);
        }

        // GET: /Admin/Product/Upsert/{id?}
        public IActionResult Upsert(int? id)
        {
            // Erstellen eines ViewModels für die Produkt-View
            ProductVM productVM = new()
            {
                // Holt alle Kategorien und erstellt eine Liste für das Dropdown-Menü
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name, // Name der Kategorie als Text
                    Value = u.Id.ToString() // ID der Kategorie als Wert
                }),
                Product = new Product() // Neues Produkt-Objekt für die View
            };

            if (id == null || id == 0)
            {
                // Wenn keine ID angegeben ist, wird eine neue Produkt-View angezeigt (Create)
                return View(productVM);
            }
            else
            {
                // Wenn eine ID angegeben ist, wird das bestehende Produkt geladen und die Update-View angezeigt
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        // POST: /Admin/Product/Upsert
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            // Überprüft, ob das ModelState gültig ist
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; // Pfad zum WebRoot

                if (file != null)
                {
                    // Generiert einen neuen Dateinamen für die hochgeladene Datei
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product"); // Pfad zum Speicherort der Bilder

                    // Löscht das alte Bild, falls vorhanden
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Speichert das neue Bild
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Setzt die ImageUrl-Eigenschaft des Produkts auf den Pfad des neuen Bildes
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    // Wenn die ID des Produkts 0 ist, wird ein neues Produkt erstellt
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    // Wenn die ID des Produkts nicht 0 ist, wird ein bestehendes Produkt aktualisiert
                    _unitOfWork.Product.Update(productVM.Product);
                }

                // Speichert die Änderungen in der Datenbank
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully"; // Setzt eine Erfolgsnachricht in TempData
                return RedirectToAction("Index"); // Leitet zurück zur Index-Ansicht
            }
            else
            {
                // Wenn das ModelState ungültig ist, wird die View mit den aktuellen Daten neu geladen
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        #region API CALLS

        // GET: /Admin/Product/GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            // Holt alle Produkte einschließlich ihrer Kategorien und gibt sie als JSON zurück
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        // DELETE: /Admin/Product/Delete/{id}
        public IActionResult Delete(int? id)
        {
            // Holt das zu löschende Produkt basierend auf der ID
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                // Gibt eine Fehlermeldung zurück, wenn das Produkt nicht gefunden wurde
                return Json(new { success = false, message = "Error while deleting" });
            }

            // Löscht das Bild des Produkts, falls vorhanden
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            // Entfernt das Produkt aus der Datenbank
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            // Gibt eine Erfolgsnachricht zurück
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}

// Importiert die Modellklasse ErrorViewModel
using BulkyBook.Models;


// Importiert die MVC-Bibliothek
using Microsoft.AspNetCore.Mvc;

// Importiert die System.Diagnostics Bibliothek
using System.Diagnostics;

// Definiert den Namespace f�r den Controller
namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    // Definiert den HomeController, der von der Controller-Basis-Klasse erbt
    public class HomeController : Controller
    {
        // Deklariert ein Feld f�r den Logger
        private readonly ILogger<HomeController> _logger;

        // Konstruktor, der den Logger initialisiert
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index-Aktion, um die Startseite anzuzeigen
        public IActionResult Index()
        {
            // Gibt die Index-View zur�ck
            return View();
        }

        // Aktion, um die Datenschutzseite anzuzeigen
        public IActionResult Privacy()
        {
            // Gibt die Privacy-View zur�ck
            return View();
        }

        // Aktion, um die Fehlerseite anzuzeigen
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Gibt die Error-View mit einem ErrorViewModel zur�ck, das die aktuelle Anforderungs-ID enth�lt
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

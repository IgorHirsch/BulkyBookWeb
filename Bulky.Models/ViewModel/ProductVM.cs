using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    // Definiert eine ViewModel-Klasse für Produkte, die in der Ansicht verwendet wird
    public class ProductVM
    {
        // Eigenschaft für das Produktobjekt
        // Dies enthält die Details eines einzelnen Produkts
        public Product Product { get; set; }

        // Eigenschaft für die Liste von Auswahlmöglichkeiten
        // Die [ValidateNever]-Annotation sorgt dafür, dass diese Eigenschaft bei der Modellvalidierung ignoriert wird
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

    /* Erklärungen mit Logik

    1. Definition der ViewModel-Klasse ProductVM:
       - Die Klasse `ProductVM` wird verwendet, um Daten zwischen der Ansicht und dem Controller zu übermitteln.
       - Sie enthält Eigenschaften, die von der Ansicht (View) verwendet werden, um die Benutzeroberfläche zu rendern und Benutzereingaben zu verarbeiten.

    2. Product-Eigenschaft:
       - Die `Product`-Eigenschaft repräsentiert ein einzelnes Produktobjekt, das in der Ansicht angezeigt oder bearbeitet werden soll.
       - Diese Eigenschaft enthält alle Details des Produkts, wie z.B. Titel, Preis, Beschreibung, ISBN, etc.

    3. CategoryList-Eigenschaft:
       - Die `CategoryList`-Eigenschaft ist eine Sammlung von `SelectListItem`-Objekten, die für Drop-down-Listen oder Auswahlmenüs verwendet wird.
       - Diese Liste enthält alle verfügbaren Kategorien, aus denen der Benutzer eine auswählen kann.
       - Die `SelectListItem`-Objekte enthalten sowohl den Wert als auch die Anzeigezeichenfolge für jede Kategorie.

    4. [ValidateNever]-Annotation:
       - Die `[ValidateNever]`-Annotation wird verwendet, um sicherzustellen, dass die `CategoryList`-Eigenschaft nicht in die Modellvalidierung einbezogen wird.
       - Diese Annotation wird oft verwendet, um zu verhindern, dass bestimmte Eigenschaften in der Validierung berücksichtigt werden, was nützlich ist, wenn die Eigenschaft nur für die Anzeige und nicht für die Benutzereingabe verwendet wird.

    Das `ProductVM`-ViewModel ermöglicht es der Ansicht, sowohl das Produkt als auch die Liste der verfügbaren Kategorien anzuzeigen, ohne dass diese Listendaten in die Modellvalidierung einbezogen werden. Dies verbessert die Benutzeroberfläche und sorgt für eine klare Trennung zwischen den Daten und der Präsentationsebene.
    */
}

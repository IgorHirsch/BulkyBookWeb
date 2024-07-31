// Variable zum Speichern der DataTable-Instanz
var dataTable;

// Wird aufgerufen, wenn das Dokument vollständig geladen ist
$(document).ready(function () {
    // Ruft die Funktion zum Laden der DataTable auf, um die Tabelle mit Daten zu füllen
    loadDataTable();
});

// Funktion zum Laden und Initialisieren der DataTable
function loadDataTable() {
    // Initialisiert die DataTable mit den folgenden Konfigurationen
    dataTable = $('#tblData').DataTable({
        // Konfiguration für das Laden von Daten über AJAX
        "ajax": { url: '/admin/product/getall' }, // URL, von der die Daten geladen werden

        // Definiert die Spalten und deren Eigenschaften in der Tabelle
        "columns": [
            // Spalte für den Titel des Produkts
            { data: 'title', "width": "25%" }, // Zeigt den Produkttitel an und reserviert 25% der Breite für diese Spalte

            // Spalte für die ISBN des Produkts
            { data: 'isbn', "width": "15%" }, // Zeigt die ISBN des Produkts an und reserviert 15% der Breite

            // Spalte für den Listenpreis des Produkts
            { data: 'listPrice', "width": "10%" }, // Zeigt den Listenpreis des Produkts an und reserviert 10% der Breite

            // Spalte für den Autor des Produkts
            { data: 'author', "width": "15%" }, // Zeigt den Autor des Produkts an und reserviert 15% der Breite

            // Spalte für den Namen der Kategorie des Produkts
            { data: 'category.name', "width": "10%" }, // Zeigt den Namen der Kategorie an und reserviert 10% der Breite

            // Spalte für Aktions-Schaltflächen (Bearbeiten und Löschen)
            {
                data: 'id',
                // Definiert, wie die Daten in dieser Spalte gerendert werden
                "render": function (data) {
                    // Gibt HTML-Code für die Schaltflächen zurück, die Bearbeiten und Löschen ermöglichen
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>               
                        <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`;
                },
                "width": "25%"  // Breite der Aktionsspalte auf 25% setzen
            }
        ]
    });
}

// Funktion zum Löschen eines Produkts
function Delete(url) {
    // Zeigt eine Bestätigungsnachricht an, um den Benutzer vor dem Löschen zu warnen
    Swal.fire({
        title: 'Are you sure?', // Titel der Bestätigungsnachricht
        text: "You won't be able to revert this!", // Warnhinweis, dass das Löschen nicht rückgängig gemacht werden kann
        icon: 'warning', // Art der Nachricht (Warnung)
        showCancelButton: true, // Zeigt die Schaltfläche zum Abbrechen an
        confirmButtonColor: '#3085d6', // Farbe des Bestätigungsbuttons
        cancelButtonColor: '#d33', // Farbe des Abbrechen-Buttons
        confirmButtonText: 'Yes, delete it!' // Text des Bestätigungsbuttons
    }).then((result) => {
        // Wenn der Benutzer auf "Bestätigen" klickt
        if (result.isConfirmed) {
            // Führt eine AJAX-DELETE-Anfrage an die gegebene URL aus
            $.ajax({
                url: url, // Die URL für die DELETE-Anfrage
                type: 'DELETE', // HTTP-Methode für die Anfrage
                success: function (data) {
                    // Bei erfolgreichem Löschen wird die DataTable neu geladen, um die Änderungen anzuzeigen
                    dataTable.ajax.reload();
                    // Zeigt eine Erfolgsmeldung an
                    toastr.success(data.message);
                }
            });
        }
    });
}
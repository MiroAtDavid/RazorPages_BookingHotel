# Praktische Arbeit Sommersemster 23/24

Hinweise zur praktischen Arbeit in POS - Einzelarbeit pro Studierende(r)
    (muss beim Lehrer vorgeführt werden)



Implementieren Sie eine Webapplikation mit Razor Pages. Diese Applikation muss eine  eigenständige Implementierung und somit eine Implementierung unabhängig  von den Beispielen bzw. Übungsaufgaben des Unterrichts umfassen. Sie  können als Ausgangsbasis die Implementierung aus dem Wintersemester  verwenden oder stattdessen einfache Modelklassen implementieren.



**Mindestanforderungen:**

- Die Webapplikation unterstützt alle CRUD Operationen (Create, Read, Update, Delete) gemäß den Beispielen aus dem Unterricht.

- *Eine Indexseite soll alle in einer Datenbanktabelle gespeicherten Objekte in Tabellenform anzeigen und Links zu einer Detailseite, zu einer  Editierseite sowie zu einer Löschseite für jeden Eintrag in der Tabelle  bereitstellen. Ein Link zu einer Seite zum Hinzufügen eines Objektes in  die Datenbank soll zur Verfügung stehen und zu jedem Eintrag in der  Tabelle soll auch die Anzahl der verbundenen Objekte (siehe nächster  Punkt) angezeigt werden.*

- Eine Detailseite soll eine GUID als URL Parameter erhalten und die Daten des Objektes sowie eine Auflistung aller verbundenen Objekte darstellen.  (Die Detailseite des Beispiels aus dem Unterricht stellt den Namen von  einem Store und die dazugehörigen Offers dar.)

- Eine Editierseite soll eine GUID als URL Parameter erhalten und  Eingabefelder für die Änderung der Daten eines Objektes zur Verfügung  stellen. Die Eingabefelder sind mit sinnvollen Validierungen zu versehen und es ist zumindest eine SelectList zu verwenden, welche von der  Datenbank ausgelesene Objekte als Auswahlmöglichkeiten bereitstellt.

- Eine weitere Editierseite soll die Editierung von mehreren Objekten (Bulk Edit) ermöglichen.

- Eine Löschseite soll eine GUID als URL Parameter erhalten und die  Fragestellung, ob das spezifizierte Objekt aus der Datenbank gelöscht  werden soll, bereitstellen. Die Löschaktion kann entweder bestätigt oder abgebrochen werden.

- Eine Seite zum Hinzufügen eines Objektes in die Datenbank bietet für alle Eingabefelder entsprechende Validierungen an.

- Verwendung von zumindest einer DTO Klasse und vom AutoMapper für das Mapping  zwischen der jeweiligen Modelklasse und der entsprechenden DTO Klasse.

- Für alle verwendeten Modelklassen ist jeweils ein Repository für den Zugriff auf die Datenbank zu implementieren.

- Umsetzung einer Authentifizierung und Authorisierung über eine Login-Seite.  Definieren Sie Seiten, die allen Benutzern zur Verfügung stehen, und  Seiten, die nur bestimmten Benutzern zur Verfügung stehen.

- Während der Verwendung der Webapplikation dürfen dem Benutzer keine Exceptions  angezeigt werden und fehlerhafte Funktionalitäten dürfen nicht enthalten sein. Dies soll auch der Fall sein, wenn beispielsweise ein in der  SelectList ausgewähltes Objekt zum Zeitpunkt der Speicherung des  verbundenen Objektes nicht mehr in der Datenbank vorhanden ist. (Die  Detailseite des Beispiels aus dem Unterricht überprüft, ob beim  Hinzufügen eines Offer Objektes das ausgewählte Produkt noch in der  Datenbank gespeichert ist.)

  

**Erweiterungsmöglichkeiten für eine bessere Beurteilung:**



- Die CRUD Operationen sind für weitere Modelklassen implementiert.
- Abfrageoptimierungen, sodass beispielsweise in der Indexseite die Anzahl der verbundenen  Objekte ohne dem Auslesen der Objekte selbst ermittelt wird.
- Formatierungserweiterungen, sodass beispielsweise bestimmte Zeilen in einer Tabelle aufgrund einer  definierten Bedingung mit einer anderen Hintergrundfarbe dargestellt  werden.
- Verwendung von mehreren SelectLists.
- Verwendung von mehreren DTO Klassen, wobei das Mapping mit dem AutoMapper durchgeführt wird.
- Die Validierungen der Eingabefelder werden mit Hilfe in der Datenbank gespeicherter Werte durchgeführt.
- In Abhängigkeit vom Benutzer, der sich eingeloggt hat, werden die Links  zum Editieren oder Löschen eines Objektes angezeigt. (Die Indexseite des Beispiels aus dem Unterricht zeigt Stores an und die Editier- bzw.  Löschmöglichkeit für Stores wird nur dem Manager eines Stores  angezeigt.)
- Der Import von Daten einer hochgeladenen Datei wird zur Verfügung gestellt.
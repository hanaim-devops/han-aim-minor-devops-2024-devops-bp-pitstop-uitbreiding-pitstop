# Stappen om een JSON-dashboard te importeren in Grafana

1. **Open Grafana**:
    - Ga naar je Grafana-webinterface door in je webbrowser naar `http://localhost:3000` te navigeren (vervang `localhost` door het juiste IP-adres of domein als je Grafana op een andere machine draait).

2. **Inloggen**:
    - Log in met je gebruikersnaam en wachtwoord. De standaard gebruikersnaam en wachtwoord zijn vaak beide `admin`, tenzij je deze hebt gewijzigd.

3. **Ga naar het Dashboard-gedeelte**:
    - Klik in het linker menu op het "+"-icoon om het menu voor het toevoegen van items te openen.
    - Selecteer **"Import"** uit de opties.

4. **Importeren van het JSON-bestand**:
    - Je hebt nu de optie om een dashboard te importeren via een JSON-bestand of via een URL. Kies voor de optie **"Upload .json File"**.
    - Klik op de knop **"Choose File"** en selecteer het JSON-bestand dat je wilt importeren van je lokale machine.

5. **Controleer de instellingen**:
    - Na het uploaden van het JSON-bestand zal Grafana een voorbeeld van het dashboard weergeven. Hier kun je de datasources selecteren die je wilt gebruiken voor het dashboard.
    - Zorg ervoor dat je de juiste datasource selecteert die je eerder hebt geconfigureerd (bijvoorbeeld je MS SQL Server).

6. **Dashboard Importeren**:
    - Klik op de knop **"Import"** om het dashboard daadwerkelijk te importeren.
    - Grafana zal nu het dashboard creëren op basis van de gegevens in het JSON-bestand.

7. **Dashboard bekijken**:
    - Na succesvolle import krijg je een melding dat het dashboard is gemaakt. Klik op de link om het nieuwe dashboard te openen en te bekijken.

8. **Dashboard Opslaan (optioneel)**:
    - Je kunt het dashboard nu verder aanpassen, toevoegen aan een folder, of het dashboard opslaan met een andere naam als je dat wilt.

## Probleemoplossing

- **Foutmeldingen**: Als je een foutmelding krijgt tijdens het importeren, controleer dan of het JSON-bestand correct is en of het compatibel is met de huidige versie van Grafana.
- **Datasource niet gevonden**: Zorg ervoor dat de datasource die in het JSON-bestand wordt genoemd, bestaat in je Grafana-configuratie.

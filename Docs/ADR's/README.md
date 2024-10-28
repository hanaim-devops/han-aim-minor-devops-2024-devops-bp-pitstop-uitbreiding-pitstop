## ADR-001: Klant kan geen eigen reservering maken

### Context
Het systeem bevat een functie waarmee gebruikers reserveringen kunnen maken. Er was een overweging om klanten zelf een reservering te laten plaatsen in plaats van deze door een medewerker te laten uitvoeren.

### Beslissing
Er is besloten om de mogelijkheid voor klanten om zelf een reservering te maken niet te implementeren.

### Redenatie
- **Tijdsbesparing:** Om klanten zelfstandig reserveringen te laten maken, zou een inlogpagina moeten worden ontwikkeld. Dit zou extra ontwikkeltijd kosten.
- **Weinig toegevoegde waarde:** De functionaliteit die een medewerker biedt bij het maken van een reservering is vrijwel identiek aan wat een klant zelf zou doen. Hierdoor voegt het weinig functionele waarde toe om klanten zelfstandig te laten reserveren.
- **Alternatief overwogen:** Er was een optie om het formulier te ontwerpen vanuit de aanname dat een gebruiker al ingelogd was, maar ook dit zou extra tijd vergen met weinig extra voordelen.

### Gevolg
Door klanten geen eigen reserveringen te laten maken, kunnen we ontwikkeltijd inzetten voor andere functionaliteiten die meer waarde toevoegen.

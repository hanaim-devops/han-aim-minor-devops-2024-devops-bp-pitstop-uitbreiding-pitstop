# Notule sprint planning 2

Datum: 28-10-2024
Aanwezigen: iedereen (Jelmer, Jelle, Jorian, Dirk, Mitchel)
Notulist: Jelmer
Voorzitter: Mitchel
Sprintdoel: 
- Afronden van gekozen userstorys
- Toepassen van blog tooling
- Maken van documentatie

1. Test Plan en ADR's
Actiepunt: Besluit over het gebruik van een testplan.
Het kan nuttig zijn om hiervoor een Architectuur Beslissingsrecord (ADR) op te stellen, zodat er een helder onderzoeksdocument is.
Een ADR kan ook gelinkt worden aan de onderzoeksblogpost, om de context te verduidelijken en kennisdeling te bevorderen.
Review: Anderen kunnen de ADR van teamleden reviewen om zeker te stellen dat deze begrijpelijk is en praktisch toepasbaar.

2. Branching Strategie en Continuous Integration
Besluit: Wanneer mogelijk, push wijzigingen direct naar main om sneller vooruitgang te boeken en de aftekenlijst bij te werken.
Retro Feedbackpunt: Voor de retro bespreken we hoe we het vermijden van long-lived branches kunnen verbeteren.

3. Kubernetes en Prometheus Configuratie
Conclusie: Vanwege de complexiteit gaan Mitchel en ik gezamenlijk kijken naar de beste aanpak voor de Kubernetes-configuratie.
Samenwerking: We beoordelen of en hoe we beiden kunnen bijdragen aan de Kubernetes-implementatie om het proces te stroomlijnen.

4. Rancher Configuratie
Bart heeft aangegeven dat Rancher mogelijk wel werkt door lokaal alle images te bouwen. Dit is een alternatieve oplossing voor het probleem dat Rancher niet werkt met online Docker image van de Pitstop maker zelf.
Actie: Lokale images bouwen en kijken of Rancher werkt.

5. Migraties en Applicatiebeheer
Discussiepunt: EF-migraties tellen niet als volledig geautomatiseerde migraties.
Er wordt afgesproken dat er in de DOD (Definition of Done) criteria worden opgenomen. Bart stelt voor dat "done" betekent dat het in productie staat.

6. Keycloak voor Jorian
Jorian zijn vraag voor app configuratie is akkoord. Omdat hij onsuccesvol keycloak heeft geimplementeerd. Maar hij hoeft dit niet nog eens te doen voor een ander onderwerp.

7. C4 Model en Structurizr
Referentie: Bart heeft een voorbeeld in PrimeChecker geplaatst en gedeeld in Slack.
Aanbeveling: Gebruik Structurizr voor het C4-model; elk diagram in Structurizr is een "view."
Voorbeeld: Maak containers groen of rood om aan te geven waar in PITSTOP de uitbreiding nodig is.
Actiepunt: Eén of twee teamleden besteden 4 uur aan het C4-diagram.
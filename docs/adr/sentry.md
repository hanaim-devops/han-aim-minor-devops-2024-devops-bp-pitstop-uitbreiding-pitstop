# Integratie van Sentry voor foutmonitoring in Pitstop

**Status** : Besloten
**Datum** : 15-10-2024
**Auteur** : Gus Theunissen

## Context

Binnen ons Pitstop-project willen we robuuste foutmonitoring en prestatiebewaking implementeren om proactief te reageren op problemen en de kwaliteit van de service te waarborgen. We hebben Sentry overwogen voor deze doeleinden, omdat het een uitgebreide oplossing biedt voor fout- en prestatiemonitoring in .NET Core-applicaties.

Pitstop is een gedistribueerde microservice-architectuur, waarbij het cruciaal is om snel inzicht te krijgen in fouten en prestaties binnen elke servicecomponent.

## Beslissing

We hebben besloten om Sentry te integreren in het Pitstop-project voor foutmonitoring en prestatiebewaking. De belangrijkste reden hiervoor is de naadloze integratie met .NET Core, het uitgebreide traceer- en foutopsporingssysteem en de beschikbaarheid van cloud-hosted diensten, wat de implementatie en het beheer vereenvoudigt.

Daarnaast hebben we ervoor gekozen om Sentry te hosten via Sentry Cloud, omdat dit ons in staat stelt om snel te schalen, zonder dat we ons zorgen hoeven te maken over onderhoud en infrastructuurbeheer. Hoewel self-hosting een alternatief biedt, vereisen de voordelen van Sentry (zoals automatische updates, beveiliging, en beheer) minder overhead en minder operationele last.

We hebben de configuratie van Sentry via de SentrySdk toegevoegd in het project om fouten en prestaties te monitoren en hebben verschillende configuratie-opties overwogen om ervoor te zorgen dat de applicatie efficiënt blijft werken.

### Alternatieven

- Self-hosted Sentry: Self-hosting zou ons volledige controle over de infrastructuur geven, maar dit zou extra onderhouds- en beveiligingsverantwoordelijkheden met zich meebrengen. Omdat we de voorkeur geven aan minder operationele complexiteit, hebben we gekozen voor Sentry Cloud. Self-hosting blijft een optie indien er in de toekomst specifieke eisen zijn rondom databeheer.

### Consequenties

**Voordelen**:
- Sentry Cloud maakt het eenvoudig om snel te schalen en biedt robuuste foutbewaking zonder dat we infrastructuur hoeven te beheren.
- We profiteren van automatische updates en verbeteringen van de cloud-gebaseerde versie van Sentry.
- De integratie met onze .NET Core-applicaties is eenvoudig te configureren en biedt uitgebreide mogelijkheden voor foutmonitoring en tracing.

**Nadelen**:
- Er is een afhankelijkheid van een externe cloudservice, wat voor sommige teams een beperking kan zijn op het gebied van dataprivacy of uptime-garanties.
- Self-hosting zou ons meer controle geven over data, maar dit vereist meer technische overhead en onderhoud.

## Bronnen

- [Sentry](https://sentry.io/)
- [Sentry .NET SDK](https://docs.sentry.io/platforms/dotnet/)

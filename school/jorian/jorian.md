# Eigen bijdrage jorian
 
Als deliverable voor de individuele bijdrage in het beroepsproduct maak een eigen markdown bestand `<mijn-voornaam>.md` in je repo aan met tekst inclusief linkjes naar code en documentaties bestanden, pull requests, commit diffs. Maak hierin de volgende kopjes met een invulling.
 
Je schrapt verder deze tekst en vervangt alle andere template zaken, zodat alleen de kopjes over blijven. **NB: Aanwezigheid van template teksten na inleveren ziet de beoordelaar als een teken dat je documentatie nog niet af is, en hij/zij deze dus niet kan of hoeft te beoordelen**.
 
Je begin hier onder het hoofdkopje met een samenvatting van je bijdrage zoals je die hieronder uitwerkt. Best aan het einde schrijven. Zorg voor een soft landing van de beoordelaar, maar dat deze ook direct een beeld krijgt. Je hoeft geen heel verslag te schrijven. De kopjes kunnen dan wat korter met wat bullet lijst met links voor 2 tot 4 zaken en 1 of 2 inleidende zinnen erboven. Een iets uitgebreidere eind conclusie schrijf je onder het laatste kopje.


## 1. Code/platform bijdrage

Deze week heb ik gewerkt aan de flow van werk in github, ik er voor gezorgd dat er een aantal extra rules zijn bij het mergen van een pull-request, zoals het gebruik van 1 manier en het standaard verwijderen van een branch na het mergen. Voor de standaard manier hebben ik na overleg gekozen voor squash merging, deze keuze heb ik gemaakt omdat meer mensen wisten hoe mergen werkte ten opzichte van rebase. Ook heb ik een [ruleset](https://github.com/hanaim-devops/devops-bp-pitstop-uitbreiding-team-knoppert/settings/rules) aangemaakt die controleert of de pull-request is gereviewd.  

Competenties: *DevOps-1 Continuous Delivery*

Beschrijf hier kort je bijdrage vanuit je rol, developer (Dev) of infrastructure specialist (Ops). Als Developer beschrijf en geef je links van minimaal 2 en maximaal 4 grootste bijdrages qua code functionaliteiten of non-functionele requirements. Idealiter werk je TDD (dus ook commit van tests en bijbehorende code tegelijk), maar je kunt ook linken naar geschreven automatische tests (unit tests, acceptance tests (BDD), integratie tests, end to end tests, performance/load tests, etc.). Als Opser geef je je minimaal 2 maximaal 4 belangrijkste bijdragen aan het opzetten van het Kubernetes platform, achterliggende netwerk infrastructuur of configuration management (MT) buiten Kubernetes (en punt 2).
 
## 2. Bijdrage app configuratie/containers/kubernetes

Competenties: *DevOps-2 Orchestration, Containerization*
 
Beschrijf en geef hier links naar je minimaal 2 en maximaal 4 grootste bijdragen qua configuratie, of bijdrage qua 12factor app of container Dockerfiles en/of .yml bestanden of vergelijkbare config (rondom containerization en orchestration).

## 3. Bijdrage versiebeheer, CI/CD pipeline en/of monitoring

Competenties: *DevOps-1 - Continuous Delivery*, *DevOps-3 GitOps*, *DevOps-5 - SlackOps*

Beschrijf hier en geef links naar je bijdragen aan het opzetten en verder automatiseren van delivery pipeline, GitOps toepassing en/of het opzetten van monitoring, toevoegen van metrics en custom metrics en rapportages.

NB Het gebruik van *versiebeheer* ((e.g. git)) hoort bij je standaardtaken en deze hoef je onder dit punt NIET te beschrijven, het gaat hier vooral om documenteren van processtandaarden, zoals toepassen van een pull model.

## 4. Onderzoek

Ik heb mijn onderzoek gedaan naar keycloak ([keycloak onderzoek](https://github.com/hanaim-devops/devops-blog-jorianroelofsen)), dit heb ik ook in dit project proberen te verwerken.
Ik heb de eerste week gewerkt aan keycloak, dit ik heb dit niet werkend kunnen krijgen door een aantal [errors](https://github.com/hanaim-devops/devops-bp-pitstop-uitbreiding-team-knoppert/issues/9) die ik niet weg kreeg.
De 1e comment laat een error zien die ik kreeg na het maken van de 401 error, deze error duid erop dat de manier waarop en de setting die je meegeef geen toegang hebben tot keycloak. De errors uit de 2e comment duiden erop dat ik geen acces heb tot keycloak [issue 1](https://github.com/IdentityServer/IdentityServer4/issues/2337) [issue 2](https://github.com/IdentityServer/IdentityServer4/issues/2672).
Om deze errors op te lossen heb ik zelfs nog gebruik gemaakt van exact dezelfde code die is gegeven in de [documentatie](https://nikiforovall.github.io/keycloak-authorization-services-dotnet/examples/web-app-mvc.html), maar zelf met deze code kreeg ik dezelfde errors.

Competenties: *Nieuwsgierige houding*

Beschrijf hier voor het Course BP kort je onderzochte technologie met een link naar je blog post, of het toepassen ervan gelukt is en hoe, of waarom niet. Beschrijf evt. kort extra leerervaringen met andere technologieen of verdieping sinds het blog. 

Tijdens het grote project beschrijf je hier onderzoek naar het domein en nieuwe onderzochte/gebruikte DevOps technologieën. Wellicht heb je nogmaals de voor blog onderzochte technologie kunnen toepassen in een andere context. Verder heb je nu een complex domein waar je in moet verdiepen en uitvragen bij de opdrachtgever. Link bijvoorbeeld naar repo's met POC's of, domein modellen of beschrijf andere onderwerpen en link naar gebruikte bronnen.

Als de tijdens course onderzochte technologie wel toepasbaar is kun je dit uiteraard onder dit punt noemen. Of wellicht was door een teamgenoot onderzochte technologie relevant, waar jij je nu verder in verdiept hebt en mee gewerkt hebt, dus hier kunt beschrijven. Tot slot kun je hier ook juist een korte uitleg geef over WAAROM  jouw eerder onderzochte technologie dan precies niet relevant of inpasbaar was. Dit is voor een naieve buitenstaander niet altijd meteen duidelijk, maar kan ook heel interessant zijn. Bijvoorbeeld dat [gebruik van Ansible in combi met Kubernetes](https://www.ansible.com/blog/how-useful-is-ansible-in-a-cloud-native-kubernetes-environment) niet handig blijkt. Ook als je geen uitgebreid onderzoek hebt gedaan of ADR hebt waar je naar kunt linken, dan kun je onder dit kopje wel alsnog kort conceptuele kennis duidelijk maken.
 
## 5. Bijdrage code review/kwaliteit anderen en security

16-10-2024
[Code review](https://github.com/hanaim-devops/devops-bp-pitstop-uitbreiding-team-knoppert/pull/17)
gekeken naar de code over HPA, opmerking gegeven over het feit dat elke service een eigen stuk heeft. Dit kon niet gecombineerd worden, dus de code is goedgekeurd.
17-10-2024
[Code review](https://github.com/hanaim-devops/devops-bp-pitstop-uitbreiding-team-knoppert/pull/32)
Gekeken naar de code over logic voor DIYavond, heb hier opmerkingen gemaakt over de gebruikte datatypes. Deze zijn aangepast. Ook heb ik opmerkingen gemaakt over de gebruikte namen van de variabelen, de naamgeving was afwisselend nederland en engels. Dit is aangepast.
17-10-2024
[Code review](https://github.com/hanaim-devops/devops-bp-pitstop-uitbreiding-team-knoppert/pull/33)
Gekeken naar de code over de logic voor de DIYavond, hier heb ik opmerkingen gemaakt over het gebruik van async zonder await. De datacast van list naar IEnumerable. Ook heb ik een opmerking gegeven dat de propertys die null kunnen zijn nullable moeten zijn in de models. En als laatste heb ik een opmerking gemaakt over een mogelijke fout doordat een waarde null kan zijn. Die is netjes afgevangen.

Competenties: *DevOps-7 - Attitude*, *DevOps-4 DevSecOps*

Beschrijf hier en geef links naar de minimaal 2 en maximaal 4 grootste *review acties* die je gedaan hebt, bijvoorbeeld pull requests incl. opmerkingen. Het interessantst zijn natuurlijk gevallen waar code niet optimaal was. Zorg dat je minstens een aantal reviews hebt waar in gitlab voor een externe de kwestie ook duidelijk is, in plaats van dat je dit altijd mondeling binnen het team oplost.
 
## 6. Bijdrage documentatie

Competenties: *DevOps-6 Onderzoek*

Zet hier een links naar en geef beschrijving van je C4 diagram of diagrammen, README of andere markdown bestanden, ADR's of andere documentatie. Bij andere markdown bestanden of doumentatie kun je denken aan eigen proces documentatie, zoals code standaarden, commit- of branchingconventies. Tot slot ook user stories en acceptatiecriteria (hopelijk verwerkt in gitlab issues en vertaalt naar `.feature` files) en evt. noemen en verwijzen naar handmatige test scripts/documenten.
 
## 7. Bijdrage Agile werken, groepsproces, communicatie opdrachtgever en soft skills

Competenties: *DevOps-1 - Continuous Delivery*, *Agile*

Beschrijf hier minimaal 2 en maximaal 4 situaties van je inbreng en rol tijdens Scrum ceremonies. Beschrijf ook feedback of interventies tijdens Scrum meetings, zoals sprint planning of retrospective die je aan groespgenoten hebt gegeven.

Beschrijf tijdens het project onder dit kopje ook evt. verdere activiteiten rondom communicatie met de opdrachtgever of domein experts, of andere meer 'professional skills' of 'soft skilss' achtige zaken.
  
## 8. Leerervaringen

Competenties: *DevOps-7 - Attitude*

Geef tot slot hier voor jezelf minimaal 2 en maximaal **4 tops** en 2 dito (2 tot 4) **tips** á la professional skills die je kunt meenemen in je verdere loopbaan. Beschrijf ook de voor jezelf er het meest uitspringende hulp of feedback van groepsgenoten die je (tot dusver) hebt gehad tijdens het project.

## 9. Conclusie & feedback

Competenties: *DevOps-7 - Attitude*

Schrijf een conclusie van al bovenstaande punten. En beschrijf dan ook wat algemener hoe je terugkijkt op het project. Geef wat constructieve feedback, tips aan docenten/beoordelaars e.d. En beschrijf wat je aan devops kennis, vaardigheden of andere zaken meeneemt naar je afstudeeropdracht of verdere loopbaan. 

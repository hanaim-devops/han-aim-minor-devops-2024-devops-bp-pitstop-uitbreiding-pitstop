# Testplan

Dit testplan is geschreven voor de features toegevoegd door team Knoppert in het project Pitstop uitbreiding.

## Gesteste onderdelen

- Annuleren DIYavond

## Testen

Voor het testen van alle features zijn hier een aantal testcases gemaakt, hierbij word gezorgd dat sowieso de happy flow word getest, daarnaast word er ook gekeken naar de edge cases.

### Annuleren DIYavond

prerequisites: Doe de Happy flow van "maak DIYavond" voordat je deze testcases uitvoert.

Voor het testen van het annuleren van een DIYavond, zijn de volgende testcases gemaakt:

- De happy flow, hierbij word een DIYavond geannuleerd en gekeken of de status van de DIYavond is aangepast.

- De edge case, hierbij word een DIYavond geannuleerd die al geannuleerd is, hierbij word gekeken of er een error word gegeven.

- De edge case, hierbij word een DIYavond geannuleerd die al is geweest, hierbij word gekeken of er een error word gegeven.

#### Case 1: Happy flow

| **Action**| **Extra Info**| **Expected Result** |
|-----------|---------------|---------------------|
| 1. Ga naar de DIY management pagina                   | URL: `http://localhost:7005/DIYManagement`                   | De DIY management pagina wordt geladen.                      |
| 2. Klik op de gewenste avond                          | Selecteer de avond die je wilt annuleren.                    | Je wordt doorgestuurd naar de detailpagina van de avond.      |
| 3. Klik op de annuleer knop                           | de knop heet "cancel evening"                                  | Een popup wordt getoond dat de avond is geannuleerd |
| 4. Controleer de status op de overzichtspagina         | Ga terug naar de overzichtspagina om status te checken.       | De avond is geannuleerd en de status is aangepast naar een kruis. |

#### Case 2: annuleren van een al geannuleerde avond

| **Action**| **Extra Info**| **Expected Result** |
|-----------|---------------|---------------------|
| 1. Ga naar de DIY management pagina                   | URL: `http://localhost:7005/DIYManagement`                   | De DIY management pagina wordt geladen.                      |
| 2. Klik op de gewenste avond, de avond moet al geannuleerd zijn                          |                   | Je wordt doorgestuurd naar de detailpagina van de avond.      |
| 3. Klik op de annuleer knop                           | de knop heet "cancel evening"                                  | Een popup wordt getoond dat de avond al geannuleerd is |
| 4. Controleer de status op de overzichtspagina         | Ga terug naar de overzichtspagina om status te checken.       | De avond is geannuleerd en de status is niet aangepast. |

#### Case 3: annuleren van een al geweest avond

| **Action**| **Extra Info**| **Expected Result** |
|-----------|---------------|---------------------|
| 1. Ga naar de DIY management pagina                   | URL: `http://localhost:7005/DIYManagement`                   | De DIY management pagina wordt geladen.                      |
| 2. Klik op de gewenste avond, de avond moet al geweest zijn                          |                   | Je wordt doorgestuurd naar de detailpagina van de avond.      |
| 3. Klik op de annuleer knop                           | de knop heet "cancel evening"                                  | Een popup wordt getoond dat de avond al geweest is |
| 4. Controleer de status op de overzichtspagina         | Ga terug naar de overzichtspagina om status te checken.       | De avond is niet geannuleerd en de status is niet aangepast. |

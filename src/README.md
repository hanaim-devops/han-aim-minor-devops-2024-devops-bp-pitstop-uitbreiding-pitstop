# MinIO

Als je voor de eerste keer de applicatie gaat starten moet je in MinIO een bucket voor de applicatie maken. Als je dit al reeds hebt gedaan, negeer dan dit!

Helaas maakt MinIO niet automatisch een bucket aan als deze niet bestaat. Dat resultaat tot errors in de Thanos Sidecar. 

- Voor Docker, ga naar:

`http://localhost:9001`

- Voor de k8s omgeving, ga naar;

`http://localhost:30901`

Login met de gegevens uit de docker compose of k8s. Standaard zal dit zijn: 
Gebruikersnaam: minio

Wachtwoord: minio123

Wacht even en mogelijk ziet de Sidecar dat de bucket nu bestaat. Zo niet, herstart de omgeving.

Voor k8s zijn de locaties:

- [Prometheus](http://localhost:30900)
- [Thanos](http://localhost:30990)
- [MinIO](http://localhost:30901)

Voor Docker:

- [Prometheus](http://localhost:9090)
- [Thanos](http://localhost:19090)
- [MinIO](http://localhost:9001)

In Prometheus en Thanos kun je een query uitvoeren. Dat kan met standaard metrics van zelf Prometheus. Maar wij hebben dus enkele custom metrics gemaakt. Deze kan je uitvoeren in het query veld van Prometheus en Thanos.

Twee custom metrics die je kan uitvoeren zijn:

- `api_diy_getEvenings_request`
- `api_diy_CreateDIYEvening`

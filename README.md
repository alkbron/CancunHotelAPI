# Test Technique Alten (Hotel Cancun Api)

J'ai choisi de réaliser ce test en .NET 6 avec une base de données mongodb

## Comment lancer le projet ? 

```sh
$ git clone git@github.com:alkbron/CancunHotelAPI.git
$ cd CancunHotelAPI
$ sudo docker-compose -f docker-compose.yml  up --build -d
```

Une fois ces deux commandes lancées, on a l'api qui tourne en écoutant le port 2525 sur votre machine.
On peut donc se connecter au swagger en accédant à ```http://localhost:2525/swagger/index.html```

Une fois sur le swagger on peut tester les différentes features de l'Api, que nous allons détailler dans ce readme.

## Quelles features possède l'api ? 


### GET 
`localhost:2525/api/Reservations` : renvoie un json avec toutes les réservations en cours de la chambre d'hotel.
`localhost:2525/api/Reservations/{id}` : renvoie un json avec toutes les infos d'une réservation portant l'id {id}

### POST  
`localhost:2525/api/Reservations` : reserve la chambre.
### PUT
`localhost:2525/api/Reservations/{id}` : change une reservation portant l'id donné pour la chambre

Voici le modèle du JSON à passer dans le corps de la requête pour pouvoir reserver (POST), et modifier sa reservation (PUT): 
```json
{
  "dateFrom": "2022-02-28",
  "dateTo": "2022-03-02",
  "customerName": "Zied"
}
```

### Delete
DELETE `localhost:2525/api/Reservations/{id}` : supprime une reservation portant l'id donné pour la chambre

## Détails pratique
- Si vous voulez changer le port d'écoute de l'api, il suffit d'aller modifier le fichier `docker-compose.yml` et à la ligne 10 modifier le port. (Pour écouter sur le port 2526, on mettra à la ligne 10 `- 2526:80`)

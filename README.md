# GeoLocation[![Build Status](https://dev.azure.com/blazewskileszek/AcornShowcase/_apis/build/status/LeszekBlazewski.GeoLocation?branchName=master)](https://dev.azure.com/blazewskileszek/AcornShowcase/_build/latest?definitionId=12&branchName=master)

Api which provides GeoLocationData retrived from IpStack. Build on ASP .NET CORE 3.0 and MongoDb.

## Check it out

simply spin all the containers with:

```D
docker-compose up
```

Now navigate to:

https://localhost:44357/index.html and create some requests

http://localhost:8081/ to see your database

There is no data provided by default so please make first some POST requests in order to see them in DB.

## Short overview

Api provides geoLocationData retrived from IpStack APi and saves the results in MongoDb. Evrything is dockerized in order to speed up the development process. I have also included support for all of the filters which can be aplied when reaching IpStack API.

Some filters are not available for free subscription therefore following fields have null values in the response:
timeZone, currency, connection, security

You can pass either ip address or full URL to the POST endpoint.

**In order to get all fields in API response**:

***Remove the fields property from body request or set it to null.***

Wrong inputs are handled accordingly. Same functionality as in IpStack Api.

## Technologies used

* ASP .NET CORE 3.0 WEB API
* docker & docker-compose
* Swagger
* MongoDb
* NUnit
* Moq
* Automapper

Everything is build build upon containers. You can also find basic unit tests with mocks. Decided to go with Mongo becuase it can be simply dockerized, stores data in JSON so there is no need to further process the data. Included error handling for situations described in the provided document.

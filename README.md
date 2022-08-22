# GeolocationApi
## About the project

This project was a part of the recruitment process. 

The task to do was described as follow:

```The aim of this task is to build a simple API (backed by any kind of database). The application should be able to store geolocation data in the database, based on IP address or URL - you can use https://ipstack.com/ to get geolocation data. The API should be able to add, delete or provide geolocation data on the base of ip address or URL. ```




## Hot to run it?
The Api requires specifying valid ipstack api key(https://ipstack.com/) to work correctly.

Api key must be provided in GeolocationApi.Api appsettings

```
{
  "ConnectionStrings": {
    "DefaultConnection": "DataSource=Data\\database.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ipstackApiKey": "YOUR_IPSTACK_API_KEY"
}
```

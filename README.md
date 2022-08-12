# GeolocationApi

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

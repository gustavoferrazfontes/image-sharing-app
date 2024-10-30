# image-sharing-app
An Image sharing app use case study

## EF Core migrtions

add a new migration:

`dotnet ef migrations add <name of migration> --project <project source migration> --startup-project <api project>`

e.g:

`dotnet ef migrations add CreateUserTable --project .\ImageSharing.Auth.Infra\ImageSharing.Auth.Infra.csproj --startup-project .\ImageSharing.Auth.Api\ImageSharing.Auth.Api.csproj`
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /Project/Domain
COPY src/Domain/*.csproj .

WORKDIR /Project/Infrastructure
COPY src/Infrastructure/*.csproj .

WORKDIR /Project/App
COPY src/App/*.csproj .
RUN dotnet restore

WORKDIR /Project
COPY src/ .

WORKDIR /Project/App
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runetime
env ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080

COPY --from=build ./Project/App/out .
ENTRYPOINT ["dotnet", "BookingApp.dll"]
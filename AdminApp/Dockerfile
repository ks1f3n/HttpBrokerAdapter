FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY AdminApp/AdminApp.csproj AdminApp/
RUN dotnet restore AdminApp/AdminApp.csproj
COPY . .
WORKDIR /src/AdminApp
RUN dotnet build AdminApp.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AdminApp.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AdminApp.dll"]

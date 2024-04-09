FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Torc.BookLibrary.API/Torc.BookLibrary.API.csproj", "Torc.BookLibrary.API/"]
COPY ["Torc.BookLibrary.Business/Torc.BookLibrary.Business.csproj", "Torc.BookLibrary.Business/"]
COPY ["Torc.BookLibrary.Configuration/Torc.BookLibrary.Configuration.csproj", "Torc.BookLibrary.Configuration/"]
COPY ["Torc.BookLibrary.Data/Torc.BookLibrary.Data.csproj", "Torc.BookLibrary.Data/"]
RUN dotnet restore "Torc.BookLibrary.API/Torc.BookLibrary.API.csproj"
COPY . .
WORKDIR "/src/Torc.BookLibrary.API"
RUN dotnet build "Torc.BookLibrary.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Torc.BookLibrary.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Torc.BookLibrary.API.dll"]
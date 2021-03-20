FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY AppGmz.sln AppGmz.sln
COPY AppGmzAPI/AppGmzAPI.csproj AppGmzAPI/AppGmzAPI.csproj
COPY AppGmz.BL/AppGmz.BL.csproj AppGmz.BL/AppGmz.BL.csproj
COPY AppGmz.Core/AppGmz.Core.csproj AppGmz.Core/AppGmz.Core.csproj
COPY AppGmz.CQRS/AppGmz.CQRS.csproj AppGmz.CQRS/AppGmz.CQRS.csproj
COPY AppGmz.DAL/AppGmz.DAL.csproj AppGmz.DAL/AppGmz.DAL.csproj
COPY AppGmz.Models/AppGmz.Models.csproj AppGmz.Models/AppGmz.Models.csproj
COPY AppGmz.Services/AppGmz.Services.csproj AppGmz.Services/AppGmz.Services.csproj
RUN dotnet restore AppGmz.sln

COPY . .
WORKDIR /src/AppGmzAPI
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish
#RUN dotnet publish WebAppAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:5005
ENTRYPOINT ["dotnet", "AppGmzAPI.dll"]
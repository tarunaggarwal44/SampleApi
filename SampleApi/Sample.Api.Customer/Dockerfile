#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Sample.Api.Customer/Sample.Api.Customer.csproj", "Sample.Api.Customer/"]
COPY ["Sample.Api.Customer.Injections/Sample.Api.Customer.Injections.csproj", "Sample.Api.Customer.Injections/"]
COPY ["Sample.Api.Customer.Repositories/Sample.Api.Customer.Repositories.csproj", "Sample.Api.Customer.Repositories/"]
COPY ["Sample.Api.Customer.Contracts/Sample.Api.Customer.Contracts.csproj", "Sample.Api.Customer.Contracts/"]
COPY ["Sample.Api.Common.Contracts/Sample.Api.Common.Contracts.csproj", "Sample.Api.Common.Contracts/"]
COPY ["Sample.Api.Customer.Business/Sample.Api.Customer.Business.csproj", "Sample.Api.Customer.Business/"]
COPY ["Sample.Api.Common/Sample.Api.Common.csproj", "Sample.Api.Common/"]
COPY ["Sample.Api.Common.Logging/Sample.Api.Common.Logging.csproj", "Sample.Api.Common.Logging/"]
RUN dotnet restore "Sample.Api.Customer/Sample.Api.Customer.csproj"
COPY . .
WORKDIR "/src/Sample.Api.Customer"
RUN dotnet build "Sample.Api.Customer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Api.Customer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Api.Customer.dll"]
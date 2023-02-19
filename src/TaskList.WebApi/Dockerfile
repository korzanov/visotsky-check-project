FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskList.Presentation/TaskList.Presentation.csproj", "TaskList.Presentation/"]
RUN dotnet restore "TaskList.Presentation/TaskList.Presentation.csproj"
COPY . .
WORKDIR "/src/TaskList.Presentation"
RUN dotnet build "TaskList.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskList.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskList.Presentation.dll"]

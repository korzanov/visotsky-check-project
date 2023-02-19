#!/bin/bash
####################################################################################
#                       Migration with DataBase on Docker                          #
#                                                                                  #
# This migration works for PostgreSQL!                                             #
# In the code of ContextFactory must be used next way for get connection string:   #
# `Environment.GetEnvironmentVariable("PG_EF_MIGRATION_CONNECTION_STRING")`        #
#                                                                                  #
# Example to use:                                                                  #
#   ./add_migration.sh SomeNameOfMigration                                         #
#                                                                Dmitrii Korzanov  #
####################################################################################

# Get and verify migration name
if [ -z "$1" ]; then
  echo "ERROR: migration name is missing"
  exit 1
fi

migrationName="$1"
if [[ ! "$migrationName" =~ ^[a-zA-Z0-9_]+$ ]]; then
  echo "ERROR: migration name contains invalid characters"
  exit 1
fi

# Set Postgres connection variables
dbUser="postgres"
dbPassword="password"
dbName="migration_db"
dbHost="localhost"
dbPort="5435" 

# Create and run Postgres container
echo "INFO: Create and run Postgres container"
docker run --rm --name postgres_migration_container -e "POSTGRES_USER=$dbUser POSTGRES_PASSWORD=$dbPassword POSTGRES_DB=$dbName" -p "$dbPort":5432 -d postgres -v postgres_migration_volume

# Run `dotnet ef migrations add`
echo "Create migration"
export PG_EF_MIGRATION_CONNECTION_STRING="Host=$dbHost;Port=$dbPort;Database=$dbName;Username=$dbUser;Password=$dbPassword;"
dotnet ef migrations add "$migrationName" --context TaskListIdentityDbContext -o Identity/PostgresMigrations
unset PG_EF_MIGRATION_CONNECTION_STRING

# Stop Postgres container
echo "INFO: Stop Postgres container"
docker stop postgres_migration_container

# Delete Postgres container and volume
echo "INFO: Delete Postgres container"
docker rm postgres_migration_container
echo "INFO: Delete Postgres volume"
docker volume rm postgres_migration_volume

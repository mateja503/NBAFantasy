#!/bin/bash
set -e

echo "Starting custom initialization..."

# 1. Create the specific database and schema
# We connect to the default 'postgres' database to perform the creation
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "postgres" <<-EOSQL
    CREATE DATABASE nbafantasydb;
EOSQL

# 2. Now connect to the NEW database to create the schema
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "nbafantasydb" <<-EOSQL
    CREATE SCHEMA IF NOT EXISTS nba;
EOSQL

# 3. Running migrations from /scripts/create...

echo "Creating tables..."
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "nbafantasydb"  \
     -c "SET search_path TO nba, public;" \
	 -f "/scripts/create/create-objects.sql"

# 4. Running seeds from /scripts/seed...

echo "Seed..."
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "nbafantasydb"  \
     -c "SET search_path TO nba, public;" \
	 -f "/scripts/seed/seed.sql"
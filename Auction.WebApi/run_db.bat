docker run --name auction-db -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -e POSTGRES_DB=auction -p 5432:5432 -d postgres
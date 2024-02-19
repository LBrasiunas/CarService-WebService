# CarService-WebService
1st task for the Web services course.

## How to run with `docker-compose`
1. You need to have `Docker` installed on your system.
2. Pull the files to your desired location.
3. Open any terminal application.
4. Navigate to the pulled file location where the `Dockerfile` and `docker-compose.yml` files are located.
5. Run `docker-compose up`.
6. Wait till both services (database and service API) are up and running.
7. Open a browser and go to `http://localhost:5000/swagger/index.html`.

## Database migrations
* Command for adding a migration:
`dotnet ef --startup-project CarServiceApi --project Infrastructure migrations add <migration_name>`

* Command for updating database locally:
`dotnet ef --startup-project CarServiceApi --project Infrastructure database update`
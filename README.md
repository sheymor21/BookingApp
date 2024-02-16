# BookingApp

Simple API for Booking management using .Net core and Docker

# Dependencies

- .NET8
- PostgresDb
- FluentValidation
- Bogus

# How to Run

If you don't have .Net download from  [.NET](https://dotnet.microsoft.com/en-us/download)

If you have .NET , download the project in your PC:

~~~
git clone https://github.com/sheymor21/BookingApp.git
~~~

Before running the project write at the console for restore the dependencies:

~~~
dotnet restore
~~~

For run the project:

~~~
dotnet run
~~~

Or if you want the swagger interface use:

~~~
dotnet watch run
~~~

# How to run in Docker

If you don't have Docker yet, go to [Docker](https://www.docker.com/get-started/)

If you have Docker, run that command at the same path of the docker-compose-yaml:

~~~
docker compose up -d
~~~

If you want to see the logs when the containers are creating:

~~~
docker compose up
~~~

For create only the image of the aplication:

~~~
docker build .
~~~

# Author

- [sheymor21](https://github.com/sheymor21)
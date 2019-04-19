# Challenge Backend

## How to Execute
For execute the solution, you have to clone or download the repo and start the solution on visual studio as any other solution.

## Downloading users from Randomuser.me (Database Seed)
The download and persistence of the users from the endpoint will do on the initialization of the app on Program.cs file on Exam.API project. 

## Db Restore
IMPORTANT: If you want to reset the database, the only thing that you have to do is delete the Database.db file on the Exam.API project. This action will delete the SqlLite db that I used for the challenge and will generate a new one when the app runs again.

## Architecture
I've used the traditional N-Layer approach using the Repository with Unit of Work pattern and ASP.NET Core for solve the challenge.

Next I'll describe the solution structure.

### Exam.API (Presentation Layer)
This layer is a ASP.NET Core WebApi that consumes the Exam.Application layer for send and receive information without know about from the origin or destination of the data. This can be anything(DB,TextFile,Endpoint,Excel file).

### Exam.Application (Services Layer)
The objective of this layer is to be the nexus between the presentation layer and the domain layer. This layer always must receive and give back DTO objects.

The most important thing of this layer are the Services. They use business logic methods on the Domain Services that allow to work with Domain Entities.


### Exam.Common
This layer contains some shared features. Example: Extensions Methods.


### Exam.Domain
On this layer, we've have Business Logic implemented with Domain Services and the Domain Entities.

### Exam.Infraestructure
Here you'll find the implementation of the Repositories, Unit Of Work, DbContexts, etc.

### Exam.Tests
This layer contains a xUnit Project for Unit Testing.

## Algorithm for Oldest User
To accomplish this feature, I did an extension method for the List of Users and search the oldest user in the result set(already paged) comparing with a max value initialized with the Date of Today, when I found the oldest user mark the user with a boolean flag with true value.



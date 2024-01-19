# Ronwell Project Assignment

## Project Prerequisites

- .NET 8.0
- Visual Studio Community or Professional (v17.8.5)
- MSSQL Management Studio (v18)

## Project Structure

**This is a Blazor Web App project.**

The project consists of the Blazor Components that are placed in the `Components` directory. 
These components can communicate with the the `Employee` service in order to fully utilize the CRUD operations.
The data necessary for the project to start will be seeded only once when the project will have started on the first time (Please see below on [Seeded Data](seeded-data)).

## Project Startup

- Open the solution in Visual Studio
- Update the connection string in the `appsettings.json` to point to your local MSSQL instance
- Open the Package Manager Console and run the following command: `Update-Database`
- Start the project by pressing the `F5` key

| The project then will seed the necessary data in order to work

### Web app Navigation

In order to navigate to the Web app, you should click at the `Employers` Icon, depending on what account you will be working on, this tab may or may not show.

### Seeded Data

The seeded data will:
- Create a set of users or employers
- Asses the proper roles to each employee

### Log in with the employees accounts

Employees credentials:

| Email                 | Position             | Password    |
| --------------------- | -------------------- | ----------- |
| admin@example.com     | System Administrator | Admin123    |
| ceo@example.com       | CEO                  | Admin123    |
| manager@example.com   | Manager              | Manager123  |
| employer1@example.com | Employer             | Employer123 |

**The Admin, CEO and manager have the roles to manage the employees how they see fit. This is for the sake of demonstration only**

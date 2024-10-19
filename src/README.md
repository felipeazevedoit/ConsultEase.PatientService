Here is the complete `README.md` content in English for easy copy and paste:

```
# Project: {{ProjectName}}

This project is part of the template collection developed by **{{Author}}**.

## Description

This template was created to provide a ready-to-use structure for solution development using .NET, with support for:
- **MongoDB**, **MySQL**, and **SQL Server**
- **Logging** support using **Serilog**
- Integrated testing with **XUnit**, **Moq**, and **FluentAssertions**
- **Health Checks** configuration
- HTTP request resilience policy with **Polly**
- Support for **NuGet packages** for components

## Main Features

- Pre-configured **Infrastructure and Domain layers**
- Support for multiple databases like **MongoDB** and **MySQL**
- Implementation of **generic repositories** for CRUD operations on different databases
- **Authentication and Authorization** configurable
- Integration of services such as **Serilog** for logging
- Easy connection setup via **appsettings.json**

## Project Structure

The project follows a modular structure:

```bash
/src
  ├── {{ProjectName}}.Core.Domain
  ├── {{ProjectName}}.Core.Infrastructure.Repositories
  ├── {{ProjectName}}.Application
  ├── {{ProjectName}}.Infra
  ├── {{ProjectName}}.Tests
  ├── ...
```

Each layer is designed to keep the code decoupled, following the principles of **Clean Architecture** and **SOLID**.

## Configuration

### Database Connection

- The configuration for **MySQL**, **MongoDB**, or **SQL Server** should be done in the `appsettings.json` file:

```json
{
  "ConnectionStrings": {
    "SqlConnectionString": "Server=myServer;Database=myDB;User=myUser;Password=myPass;",
    "MySqlConnectionString": "Server=myServer;Database=myDB;User=myUser;Password=myPass;",
    "MongoDB": {
      "ConnectionString": "mongodb://myUser:myPass@myMongoServer",
      "DatabaseName": "myDatabase"
    }
  }
}
```

### Logging

This project uses **Serilog** for log management. The log outputs are configured in `appsettings.json` or directly in the **Program.cs** code:

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

### Tests

- Unit tests are set up with **XUnit** and **Moq**, and are organized in the `/Tests` directory.
- Run the tests using the following command:
```bash
dotnet test
```

## How to use this template

To create a new project based on this template, you can use the following command:

```bash
dotnet new {{ProjectName}} --name "YourProjectName" --author "YourName"
```

## Additional Instructions

1. **Connection String Setup**: Make sure to properly configure the `ConnectionString` in the `appsettings.json` file to ensure the connection with the chosen database.
2. **NuGet Packages**: When adding new components to your project, they will be published as **NuGet packages** directly in **Azure DevOps**.

## Contact

Developed by **{{Author}}** from **{{Company}}**.

Follow updates on the official repository: [GitHub Link](https://github.com/repository)
```

### Instructions to use:
1. Place this file in the root of your template project.
2. In the `template.json`, ensure that the placeholders like `ProjectName`, `Author`, and `Company` are configured with corresponding parameters to replace these values automatically during project generation.

This will allow you to generate a personalized `README.md` with every new project created from the template.
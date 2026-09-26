# Anime Library 

Full-stack web application for managing an anime library. Users can create, delete, or edit anime entries.

## Tech Stack

### Backend

- C#
- .NET 10
- ASP.NET Core
-Entity Framework Core
-SQLite
-FluentValidation

### Frontend

-Angular
-TypeScript
-Reactive Forms

## Features

- Manage anime list
- Create new anime entry
- Delete an anime entry
- Edit an anime entry
- Input validation

## Project Structure

### Backend - AnimeApi

- `DTOs/` - objects used for API requests and responses
- `Data/` - database context
- `Migrations/` - EF Core database migrations
- `Models/` - database entities
- `Validators/` - request validation
- `Program.cs` - application configuration and API endpoints

### Frontend - anime-client

- `src/app/models/` — TypeScript models
- `src/app/services/` — services for communication with the API
- `src/app/app.ts` — main Angular component
- `src/app/app.html` — application template
- `src/app/app.css` — application styles

## Running the project

### Backend

- `cd anime-library/AnimeApi` - navigate to the backend directory
- `dotnet restore` - restore dependencies
- `dotnet ef database update` - apply EF Core migrations to the database
- `dotnet run` - start the API

### Frontend 

- `cd anime-library/anime-client` - navigate to the frontend directory
- `npm install` - install frontend dependencies
- `ng serve` - start the development server
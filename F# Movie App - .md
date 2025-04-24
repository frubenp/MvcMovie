# Movie App - F# Web Application with ASP.NET Core MVC

This is a web application built using F# and ASP.NET Core MVC, designed to explore functional-first web development using modern .NET technologies.

The app implements a basic CRUD (Create, Read, Update, Delete) interface for managing a list of movies stored in a SQL Server database, and showcases several production-grade features such as dark mode support and localization (hu-HU).

#  Goals and motivation

- Learn and apply **F#** in real-world web development.
- Explore how **ASP.NET Core MVC** and Razor views can be effectively used with F#.
- Understand **Entity Framework Core** integration from F#.
- Experiment with **UI/UX enhancements** like dark mode toggling and form validations.

This project was made for a university assignment to explore F#.
The main focus of the application isn't about movies, this could've been done with books, games, songs etc... 
I have previously made a similar program in C# and I was wondering how I could implement it in F#.

#  Features

-  View, add, edit, and delete movies
-  Toggleable dark mode that persists between pages (using `localStorage`)
-  Strong typing using F# records and [CLIMutable] for model binding
-  Server-side validation (with optional client-side JS validation)
-  Localization middleware set up via `RequestLocalizationOptions`

The UI of the App is clean, minimalist and Bootstrap-based featuring a simple layout with:
A responsive navigation bar, a central movie list interface with filtering options, subtle styling improvements and 
a dark mode that visually switches the theme and also persists user preference.

# How to RUN LOCALLY

Open MvcMovie.fsproj with Microsoft Visual Studio
In Visual Studio, open SQL Server Object Explorer (can be found at View at the top navigation bar)
Drag the InitialCreate.sql file into the Explorer and run it inside the Explorer.
Select local and continue after running the sql file.
Refresh SQL Server Object Explorer and start debugging with http.
The debugging process might ask you to allow downloading packages and other things, you just have to click yes at every single one for the first launch.

#  Setup Instructions

1. Ensure you have the [.NET 7+ SDK](https://dotnet.microsoft.com/download) and SQL Server running.





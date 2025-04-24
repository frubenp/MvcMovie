IF DB_ID(N'MvcMovieContext-bedc57a1-6ffd-4ca3-bdeb-793090ed161d') IS NULL
BEGIN
    CREATE DATABASE [MvcMovieContext-bedc57a1-6ffd-4ca3-bdeb-793090ed161d];
END;
GO

USE [MvcMovieContext-bedc57a1-6ffd-4ca3-bdeb-793090ed161d];
GO

IF OBJECT_ID(N'[EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Movie] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [ReleaseDate] datetime2 NOT NULL,
    [Genre] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [Rating] nvarchar(max) NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
);

INSERT INTO [EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250423223336_Init', N'9.0.0');

COMMIT;
GO
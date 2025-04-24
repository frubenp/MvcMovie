namespace MvcMovie.Models

open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.DependencyInjection
open MvcMovie.Data

module SeedData =

    let initialize (serviceProvider: IServiceProvider) =
        use context =
            new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()
            )

        // Check if any movies already exist
        if (context.Movie.AsQueryable().Any()) then
            () // DB has been seeded, do nothing
        else
            context.Movie.AddRange(
                [
                    {
                        Id = 0
                        Title = "When Harry Met Sally"
                        ReleaseDate = DateTime.Parse("1989-1-11")
                        Genre = "Romantic Comedy"
                        Rating = "R"
                        Price = 7.99M
                    }
                    {
                        Id = 1
                        Title = "Ghostbusters "
                        ReleaseDate = DateTime.Parse("1984-3-13")
                        Genre = "Comedy"
                        Rating = "A"
                        Price = 8.99M
                    }
                    {
                        Id = 2
                        Title = "Ghostbusters 2"
                        ReleaseDate = DateTime.Parse("1986-2-23")
                        Genre = "Comedy"
                        Rating = "A"
                        Price = 9.99M
                    }
                    {
                        Id = 3
                        Title = "Rio Bravo"
                        ReleaseDate = DateTime.Parse("1959-4-15")
                        Genre =  "Western"
                        Rating = "None"
                        Price = 3.99M
                    }
                ]
            ) |> ignore

            context.SaveChanges() |> ignore

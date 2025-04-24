namespace MvcMovie.Controllers

open System
open System.Linq
open Microsoft.EntityFrameworkCore
open MvcMovie.Models
open Microsoft.AspNetCore.Mvc
open MvcMovie.Data
open Microsoft.AspNetCore.Mvc.Rendering
open System.Threading.Tasks

type MoviesController(context: MvcMovieContext) =
    inherit Controller()

    member this.Index(movieGenre: string, searchString: string) : Task<IActionResult> =
        task {
            if isNull context.Movie then
                return this.Problem("Entity set 'MvcMovieContext.Movie' is null.") :> IActionResult
            else
                let genreQuery =
                    query {
                        for m in context.Movie do
                        sortBy m.Genre
                        select m.Genre
                    }

                let mutable movies =
                    query {
                        for m in context.Movie do
                        select m
                    }

                if not (String.IsNullOrEmpty(searchString)) then
                    movies <- movies.Where(fun s -> s.Title.ToUpper().Contains(searchString.ToUpper()))

                if not (String.IsNullOrEmpty(movieGenre)) then
                    movies <- movies.Where(fun x -> x.Genre = movieGenre)

                let! genres = genreQuery.Distinct().ToListAsync()
                let! movieList = movies.ToListAsync()

                let vm: MovieGenreViewModel = {
                    Movies = (movieList)
                    Genres = (SelectList(genres))
                    MovieGenre = ""
                    SearchString = ""
                }

                return this.View(vm) :> IActionResult
        }

    member this.Create() : IActionResult =
        this.View() :> IActionResult

    [<HttpPost>]
    [<ValidateAntiForgeryToken>]
    member this.Create([<Bind("Id,Title,ReleaseDate,Genre,Price,Rating")>] movie: Movie) : Task<IActionResult> =
        task {
            if this.ModelState.IsValid then
                context.Add(movie) |> ignore
                context.SaveChanges() |> ignore
                return this.RedirectToAction("Index") :> IActionResult
            else
                return this.View(movie) :> IActionResult
        }

    member this.Edit(id: Nullable<int>) : Task<IActionResult> =
        task {
            if not id.HasValue then
                return this.NotFound() :> IActionResult
            else
                let! movie = context.Movie.FindAsync(id.Value).AsTask()
                match movie with
                | _ -> return this.View(movie) :> IActionResult
        }

    [<HttpPost>]
    [<ValidateAntiForgeryToken>]
    member this.Edit(id: int, [<Bind("Id,Title,ReleaseDate,Genre,Price,Rating")>] movie: Movie) =
        task {
            if id <> movie.Id then
                return this.NotFound() :> IActionResult
            elif this.ModelState.IsValid then
                context.Update(movie) |> ignore
                context.SaveChanges() |> ignore
                return this.RedirectToAction("Index") :> IActionResult
            else
                return this.View(movie) :> IActionResult
        }

    member this.Details(id: Nullable<int>) : Task<IActionResult> =
        task {
            if not id.HasValue then
                return this.NotFound() :> IActionResult
            else
                let idValue = id.Value
                let! movie = context.Movie.FirstOrDefaultAsync(fun m -> m.Id = idValue)
                match Option.ofObj movie with
                | Some m -> return this.View(m) :> IActionResult
                | None -> return this.NotFound() :> IActionResult
        }

    member this.Delete(id: Nullable<int>) : Task<IActionResult> =
        task {
            if not id.HasValue then
                return this.NotFound() :> IActionResult
            else
                let idValue = id.Value
                let! movie = context.Movie.FirstOrDefaultAsync(fun m -> m.Id = idValue)
                match Option.ofObj movie with
                | Some m -> return this.View(m) :> IActionResult
                | None -> return this.NotFound() :> IActionResult

        }

    [<HttpPost>]
    [<ActionName("Delete")>]
    [<ValidateAntiForgeryToken>]
    member this.DeleteConfirmed(id: int) : Task<IActionResult> =
        task {
            let! movie = context.Movie.FindAsync(id).AsTask()
            context.Movie.Remove(movie) |> ignore
            context.SaveChanges() |> ignore
            return this.RedirectToAction("Index") :> IActionResult
        }



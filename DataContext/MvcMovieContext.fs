namespace MvcMovie.Data

open System
open Microsoft.EntityFrameworkCore
open MvcMovie.Models

type MvcMovieContext(options: DbContextOptions<MvcMovieContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private movie: DbSet<Movie>
    member this.Movie
        with get() = this.movie
        and set v = this.movie <- v

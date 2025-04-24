namespace MvcMovie.Models

open System
open System.Collections.Generic
open Microsoft.AspNetCore.Mvc.Rendering
open MvcMovie.Models

[<CLIMutable>]
type MovieGenreViewModel = {
    Movies: List<Movie>
    Genres: IEnumerable<SelectListItem>
    MovieGenre: string
    SearchString: string
}

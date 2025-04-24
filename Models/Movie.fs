namespace MvcMovie.Models

open System
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema

[<CLIMutable>]
type Movie = {
    [<Key>]
    Id: int

    Title: string

    [<Display(Name = "Release Date")>]
    [<DataType(DataType.Date)>]
    ReleaseDate: DateTime

    Genre: string

    [<Column(TypeName = "decimal(18, 2)")>]
    Price: decimal

    Rating: string
}

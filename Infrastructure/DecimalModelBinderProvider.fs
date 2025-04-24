namespace MvcMovie.Infrastructure

open System
open System.Globalization
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc.ModelBinding

type DecimalModelBinder() =
    interface IModelBinder with
        member _.BindModelAsync(bindingContext: ModelBindingContext) =
            let valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName)
            if valueProviderResult = ValueProviderResult.None then
                Task.CompletedTask
            else
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult)
                let value = valueProviderResult.FirstValue
                match Decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture) with
                | true, dec ->
                    bindingContext.Result <- ModelBindingResult.Success(box dec)
                | false, _ ->
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Hibás formátum (pl.: 9,99)") |> ignore
                Task.CompletedTask

open Microsoft.AspNetCore.Mvc.ModelBinding

type DecimalModelBinderProvider() =
    interface IModelBinderProvider with
        member _.GetBinder(context: ModelBinderProviderContext) =
            if context.Metadata.ModelType = typeof<decimal> || context.Metadata.ModelType = typeof<Nullable<decimal>> then
                DecimalModelBinder() :> IModelBinder
            else null

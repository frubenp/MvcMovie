namespace MvcMovie

#nowarn "20"

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.EntityFrameworkCore
open MvcMovie.Data
open System.Globalization
open Microsoft.AspNetCore.Localization
open MvcMovie.Infrastructure
open Microsoft.AspNetCore.Mvc.ModelBinding
open Microsoft.AspNetCore.Mvc



module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddDbContext<MvcMovieContext>(fun options ->
            options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext") |> string) |> ignore)

        builder
            .Services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation()

        builder.Services.AddRazorPages()
        builder.Services.Configure<MvcOptions>(fun (options: MvcOptions) ->
            options.ModelBinderProviders.Insert(0, DecimalModelBinderProvider())
        )

        let app = builder.Build()

        if not (builder.Environment.IsDevelopment()) then
            app.UseExceptionHandler("/Home/Error")
            app.UseHsts() |> ignore // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

        let supportedCultures = [| CultureInfo("hu-HU") |]
        let localizationOptions = 
            RequestLocalizationOptions()
                .SetDefaultCulture("hu-HU")
                .AddSupportedCultures(supportedCultures |> Array.map (fun c -> c.Name))
                .AddSupportedUICultures(supportedCultures |> Array.map (fun c -> c.Name))
        app.UseRequestLocalization(localizationOptions)
        app.UseHttpsRedirection()

        app.UseStaticFiles()
        app.UseRouting()
        app.UseAuthorization()

        app.MapControllerRoute(name = "default", pattern = "{controller=Home}/{action=Index}/{id?}")

        app.MapRazorPages()

        app.Run()

        exitCode

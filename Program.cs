using BlazorApp1.View;
using HouseRentApp;
using HouseRentApp.Components.Database;
using HouseRentApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        String connectionString = "server=localhost;database=rentapp;user=root;password=parolaproiect123;";

// Adaugă DbContext în containerul de dependențe pentru MySQL
        builder.Services.AddDbContext<HousingContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)))
                .EnableSensitiveDataLogging() // Opțional, pentru a vedea datele din interogări
                .LogTo(Console.WriteLine, LogLevel.Information)); // Loghează interogările SQL în consolă
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
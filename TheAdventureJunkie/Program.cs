using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Repositories;
using TheAdventureJunkie.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using TheAdventureJunkie.Data;
using TheAdventureJunkie.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDatabase().BindConfiguration("ConnectionStrings");

builder.Services.AddRepositories();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddOIDC(builder.Configuration);
builder.Services.ConfigureAuthentication();

var app = builder.Build();

app.EnsureDatabaseCreated();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAntiforgery();
app.MapRazorPages();

app.Run();

using Microsoft.EntityFrameworkCore;
using TheAdventureJunkie.Models;
using Microsoft.AspNetCore.Identity;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Repositories;
using TheAdventureJunkie.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TheAdventureJunkieDbContextConnection") ?? throw new InvalidOperationException("Connection string 'TheAdventureJunkieDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TheAdventureJunkieDbContext>(options => {
	options.UseSqlServer(builder.Configuration["ConnectionStrings:TheAdventureJunkieDbContextConnection"]);
});

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<TheAdventureJunkieDbContext>();

builder.Services.AddTransient<IEventRepository,EventRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>(sp => ShoppingCartService.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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


DbInitializer.Seed(app);
app.Run();

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Data;
using TheAdventureJunkie.Repositories;
using TheAdventureJunkie.Services;

namespace TheAdventureJunkie.Configuration
{
    public static class Extensions
    {

        public static IServiceCollection AddOIDC(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<OidcProviderOptions>(OidcProviderOptions.Google)
                .Bind(config.GetSection("Authentication:Google"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddOptions<OidcProviderOptions>(OidcProviderOptions.Microsoft)
                .Bind(config.GetSection("Authentication:Microsoft"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>(sp => ShoppingCartService.GetCart(sp));
            return services;
        }

        public static AuthenticationBuilder ConfigureAuthentication(this IServiceCollection services)
        {
            var builder = services.AddAuthentication();
            builder.AddGoogle(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var monitor = serviceProvider.GetRequiredService<IOptionsMonitor<OidcProviderOptions>>();
                var google = monitor.Get(OidcProviderOptions.Google);
                options.ClientId = google.ClientId;
                options.ClientSecret = google.ClientSecret;
            });
            builder.AddMicrosoftAccount(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var monitor = serviceProvider.GetRequiredService<IOptionsMonitor<OidcProviderOptions>>();
                var microsoft = monitor.Get(OidcProviderOptions.Microsoft);
                options.ClientId = microsoft.ClientId;
                options.ClientSecret = microsoft.ClientSecret;
            });
            return builder;
        }

        public static OptionsBuilder<ConnectionStringsOptions> AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<TheAdventureJunkieDbContext>(
                (sp, opts) =>
                {
                    opts.UseSqlServer(sp.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value.TheAdventureJunkieDbContextConnection);
                });

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<TheAdventureJunkieDbContext>();
            return services.AddOptionsWithValidateOnStart<ConnectionStringsOptions>().ValidateDataAnnotations();
        }

        public static void EnsureDatabaseCreated(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TheAdventureJunkieDbContext>();
            dbContext.Database.Migrate();
            DbInitializer.Seed(app);
        }
    }
}

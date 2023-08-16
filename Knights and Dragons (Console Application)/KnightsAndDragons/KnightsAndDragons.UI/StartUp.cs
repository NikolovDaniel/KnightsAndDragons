using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using KnightsAndDragons.Infrastructure.Repositories;
using KnightsAndDragons.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using KnightsAndDragons.Infrastructure.Serializers;

namespace KnightsAndDragons.UI;

class StartUp
{
    static async Task Main(string[] args)
    {
        KnightsAndDragonsDbContext context = new KnightsAndDragonsDbContext();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Create a new service collection
        var service = new ServiceCollection();

        // Registering the repositories and services
        service.AddDbContext<KnightsAndDragonsDbContext>(opt =>
        opt.UseSqlServer(Configuration.ConnectionString));
        service.AddScoped<IDragonRepository, DragonRepository>();
        service.AddScoped<IKnightRepository, KnightRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IDragonService, DragonService>();
        service.AddScoped<IKnightService, KnightService>();
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IJsonSerializer, JsonSerializer>();
        service.AddScoped<IJsonDeserializer, JsonDeserializer>();
        service.AddScoped<IJsonSerializerServices, JsonSerializerService>();
        service.AddScoped<IJsonDeserializerServices, JsonDeserializerServices>();

        // Create a new service provider from the service collection
        var serviceProvider = service.BuildServiceProvider();

        // Use the service provider to resolve dependencies
        var dragonService = serviceProvider.GetService<IDragonService>();
        var knightService = serviceProvider.GetService<IKnightService>();
        var userService = serviceProvider.GetService<IUserService>();
        var jsonSerializerService = serviceProvider.GetService<IJsonSerializerServices>();
        var jsonDeserializerService = serviceProvider.GetService<IJsonDeserializerServices>();

        // Pass the dependencies
        Game game = new Game(dragonService!,  knightService!, userService!, jsonSerializerService!, jsonDeserializerService!);

        await game.Start();
    }
}


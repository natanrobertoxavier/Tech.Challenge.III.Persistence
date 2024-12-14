using RabbitMq.Package.Contract;
using RabbitMq.Package.Handlers;
using RabbitMq.Package.Settings;
using Serilog;
using Tech.Challenge.Persistence.Api.MessagesHandler;
using Tech.Challenge.Persistence.Infrasctructure.Provider;
using Tech.Challenge.Persistence.Infrasctructure.Queue;

namespace Tech.Challenge.Persistence.Api;

public static class Initializer
{
    public static void AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddSerilog(services);
        AddRabbitMqService(services, configuration);
        AddRabbitMqSettings(services, configuration);
    }

    private static void AddSerilog(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton(Log.Logger);
    }

    private static void AddRabbitMqService(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
    }

    private static void AddRabbitMqSettings(IServiceCollection services, IConfiguration configuration)
    {
        var config = new RabbitMqSettings();

        configuration.GetSection("RabbitMqSettings").Bind(config);

        var connectionString = new RabbitMqQueueConnection()
        {
            ConnectionString = config.ComposedConnectionString
        };

        services.AddSingleton<IConnectionProvider<RabbitMqQueueConnection>>(_ =>
            new RabbitMqConnectionProvider(connectionString));

        //services.AddSingleton<IRabbitMqQueueHandler<RabbitMqQueueConnection>, RabbitMqQueueHandler>();

        services
            .AddQueueHandler(config.ComposedConnectionString)
            .DeclareQueues(
                new RabbitMqQueue(
                    exchangeName: RabbitMqConstants.ContactPersistenceExchange,
                    routingKeyName: RabbitMqConstants.RegisterContactRoutingKey,
                    queueName: RabbitMqConstants.RegisterContactQueueName),
                new RabbitMqQueue(
                    exchangeName: RabbitMqConstants.ContactPersistenceExchange,
                    routingKeyName: RabbitMqConstants.DeleteContactRoutingKey,
                    queueName: RabbitMqConstants.DeleteContactQueueName)
                )
            ;

        services.AddHostedService<DeleteContactQueueHandler>(); 

    }
}

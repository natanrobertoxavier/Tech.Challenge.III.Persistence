using RabbitMq.Package.Contract;
using RabbitMq.Package.Listeners;
using RabbitMq.Package.Models;
using System.Diagnostics;
using Tech.Challenge.Persistence.Infrasctructure.Queue;

namespace Tech.Challenge.Persistence.Api.MessagesHandler;

public abstract class BaseHandler<T>(
    string queueName,
    int maxRequeue,
    Serilog.ILogger logger,
    IConnectionProvider<RabbitMqQueueConnection> connectionProvider,
    IRabbitMqQueueHandler<RabbitMqQueueConnection> queueHandler) : RabbitMqQueueListener(
        connectionProvider.ConnectionFactory,
        queueName)
{
    private readonly int _maxRequeue = maxRequeue;
    private readonly Serilog.ILogger _logger = logger;
    private readonly IRabbitMqQueueHandler _queueHandler = queueHandler;

    protected override async Task MessageReceived(RabbitMqMessageReceivedArgs args)
    {
        try
        {
            await _queueHandler.ProcessMessage<T>(args, (queue) => ProcessMessage(queue), (ex) => throw ex, maxRequeue: _maxRequeue);
        }
        catch (Exception ex)
        {
            _logger.Error($"An error occurer");
            MessageReceivedError(ex);
        }
    }

    public abstract Task ProcessMessage(T queue);

    public abstract void MessageReceivedError(Exception ex);
}

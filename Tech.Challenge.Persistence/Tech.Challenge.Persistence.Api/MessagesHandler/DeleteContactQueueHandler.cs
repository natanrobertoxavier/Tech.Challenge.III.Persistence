
using RabbitMq.Package.Contract;
using Tech.Challenge.Persistence.Api.Models;
using Tech.Challenge.Persistence.Infrasctructure.Queue;

namespace Tech.Challenge.Persistence.Api.MessagesHandler;

public class DeleteContactQueueHandler : BaseHandler<DeleteContactModel>
{
    private readonly Serilog.ILogger _logger;
    public DeleteContactQueueHandler(
        Serilog.ILogger logger, 
        IConnectionProvider<RabbitMqQueueConnection> connectionProvider, 
        IRabbitMqQueueHandler<RabbitMqQueueConnection> queueHandler) 
        : base(
            RabbitMqConstants.DeleteContactQueueName, 
            1, 
            logger, 
            connectionProvider, 
            queueHandler)
    {
        _logger = logger;
    }

    public override Task ProcessMessage(DeleteContactModel message)
    {
        var t = message;
        throw new NotImplementedException();
    }

    public override void MessageReceivedError(Exception ex)
    {
        _logger.Error("");
    }
}

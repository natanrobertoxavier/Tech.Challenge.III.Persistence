namespace Tech.Challenge.Persistence.Infrasctructure.Queue;
using RabbitMq.Package.Models;

public class RabbitMqQueueConnection : ConnectionBase
{
    public override string ConnectionString { get; set; }
}

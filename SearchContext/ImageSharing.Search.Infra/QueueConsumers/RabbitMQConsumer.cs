using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ImageSharing.Search.Infra.QueueConsumers;

public class RabbitMQConsumer
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly IConnection _connection;

    public RabbitMQConsumer()
    {

        var factory = new ConnectionFactory()
        {
            HostName = "172.17.0.1",
        };

        _connection = factory.CreateConnection();
    }

    public void Execute()
    {

        while (true)
        {


            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: "auth-queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };

            channel.BasicConsume(queue: "auth-queue",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}

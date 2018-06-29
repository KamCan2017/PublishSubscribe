using RabbitMQ.Client;
using System;
using System.Text;

namespace PubSubFactory
{
    public class Publisher
    {
        /// <summary>
        /// Publish a payload
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <param name="exchange">The exchange name.</param>
        /// <param name="severity">The type of severity.</param>
        public void PublishEvent(string payload, string exchange, string severity)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(durable: true);
                channel.ExchangeDeclare(exchange: exchange,
                                        type: "direct");

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = Encoding.UTF8.GetBytes(payload);
                channel.BasicPublish(exchange: exchange,
                                     routingKey: severity,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, payload);
            }
        }
    }
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace PubSubFactory
{
    public class Subscriber
    {
        public delegate void Callback(string payload, string routinKey);

        /// <summary>
        /// Subscribe a event
        /// </summary>
        /// <param name="exchange">The exchange name</param>
        /// <param name="severity">The severities.</param>
        /// <param name="callback">The callbak method to invoke</param>
        public void SubscribeEvent(string exchange, string[] severities, Callback callback)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            {
                channel.ExchangeDeclare(exchange: exchange,
                                        type: "direct");

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var queueName = channel.QueueDeclare(durable: true).QueueName;

                foreach(string severity in severities)
                   channel.QueueBind(queue: queueName,
                                     exchange: exchange,
                                     routingKey: severity);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    callback(Encoding.UTF8.GetString(ea.Body), ea.RoutingKey);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);                
            }
        }
    }
}

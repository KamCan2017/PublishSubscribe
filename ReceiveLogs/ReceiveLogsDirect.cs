using CommonSource;
using Newtonsoft.Json;
using PubSubFactory;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ReceiveLogs
{
    class ReceiveLogsDirect
    {
        public static void Init(string[] args)
        {
            args = new[] { "warning", "info" };
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                        Environment.GetCommandLineArgs()[0]);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }

            Subscriber.Callback callback = (palyoad, severity) =>
            {
                var model = JsonConvert.DeserializeObject<Model>(palyoad);
                Console.WriteLine(" [x] Received '{0}':'{1}'",
                                severity, model.Message);
            };

            var consumer = new Subscriber();
            consumer.SubscribeEvent(Exchange.Payload, args, callback);

            Console.WriteLine(" [*] Waiting for messages.");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            //#region old version
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.ExchangeDeclare(exchange: "direct_logs",
            //                            type: "direct");
            //    var queueName = channel.QueueDeclare().QueueName;

            //    if (args.Length < 1)
            //    {
            //        Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
            //                                Environment.GetCommandLineArgs()[0]);
            //        Console.WriteLine(" Press [enter] to exit.");
            //        Console.ReadLine();
            //        Environment.ExitCode = 1;
            //        return;
            //    }

            //    foreach (var severity in args)
            //    {
            //        channel.QueueBind(queue: queueName,
            //                          exchange: "direct_logs",
            //                          routingKey: severity);
            //    }

            //    Console.WriteLine(" [*] Waiting for messages.");

            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var message = Encoding.UTF8.GetString(body);
            //        var routingKey = ea.RoutingKey;
            //        Console.WriteLine(" [x] Received '{0}':'{1}'",
            //                          routingKey, message);
            //    };
            //    channel.BasicConsume(queue: queueName,
            //                         autoAck: true,
            //                         consumer: consumer);

            //    Console.WriteLine(" Press [enter] to exit.");
            //    Console.ReadLine();
            //}

            //#endregion
        }
    }
}

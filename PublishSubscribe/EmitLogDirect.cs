using CommonSource;
using Newtonsoft.Json;
using PubSubFactory;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishSubscribe
{
    class EmitLogDirect
    {
        public static void Init(string[] args)
        {
            var severity = (args.Length > 0) ? args[0] : "info";
            var message = (args.Length > 1)
                          ? string.Join(" ", args.Skip(1).ToArray())
                          : "Hello World!";

            var model = new Model() { Message = message, Severity = severity };

            var payload = JsonConvert.SerializeObject(model);

            var publisher = new Publisher();
            publisher.PublishEvent(payload, Exchange.Payload, severity);

            //#region old version

            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    //Exchange type direct: allows filtering messages based on their severity
            //    //a message goes to the queues whose binding key exactly matches the routing key of the message.
            //    channel.ExchangeDeclare(exchange: "direct_logs",
            //                            type: "direct");

            //    var severity = (args.Length > 0) ? args[0] : "info";
            //    var message = (args.Length > 1)
            //                  ? string.Join(" ", args.Skip(1).ToArray())
            //                  : "Hello World!";

            //    var body = Encoding.UTF8.GetBytes(message);
            //    channel.BasicPublish(exchange: "direct_logs",
            //                         routingKey: severity,
            //                         basicProperties: null,
            //                         body: body);
            //    Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
            //}

            //#endregion

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    
}

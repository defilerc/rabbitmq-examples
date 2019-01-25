using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { 
                HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(
                    exchange: "logs", type: "fanout");

                var message = args.Length > 0 ? string.Join(" ", args) : "Hello World";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "logs",
                     routingKey: "",
                     basicProperties: null,
                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}

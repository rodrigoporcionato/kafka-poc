using Confluent.Kafka;
using kafka.infra;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace kafka.producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("kafka producer test");

            var config = new ProducerConfig
            {
                 BootstrapServers = Configuration.Host,
                 ClientId = Dns.GetHostName()
            };


            while (true)
            {
                Console.WriteLine("digite 'sim' para iniciar");
                var op = Console.ReadLine();                
                if (int.TryParse(op, out _) && Convert.ToInt32(op) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(op); i++)
                    {
                        var orderMesssage = new OrdersModel
                        {
                            Id = new Random().Next(1, 9999),
                            Name = $"product {Guid.NewGuid()}",
                            Price = new Random().Next(1000, 9999),
                        };

                        SendMessageProducer(config, orderMesssage);
                    }
                }
            }

        }

        private static void SendMessageProducer(ProducerConfig config, OrdersModel orderMesssage)
        {           
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var t = producer.ProduceAsync(Configuration.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(orderMesssage) });
                _ = t.ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine($"error to offset: {task.Result.Value}");
                    }
                    else
                    {
                        Console.WriteLine($"Wrote to offset: {task.Result.Offset}");
                    }
                });
            }
        }
    }
}

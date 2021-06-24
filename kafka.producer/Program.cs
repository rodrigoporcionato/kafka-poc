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

            var orderMesssage = new OrdersModel
            {
                 Id = new Random().Next(1,9999),
                 Name = $"product {Guid.NewGuid()}",
                 Price = new Random().Next(1000, 9999),
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                //await producer.ProduceAsync(Configuration.Topic, new Message<Null, string> { Value = "teste" });
               var result = await producer.ProduceAsync(Configuration.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(orderMesssage) });
                
            }

        }
    }
}

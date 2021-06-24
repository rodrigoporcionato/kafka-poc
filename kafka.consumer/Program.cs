using Confluent.Kafka;
using kafka.infra;
using System;
using System.Net;

namespace kafka.consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("consumidor kafka");


            var config = new ConsumerConfig
            {
                BootstrapServers = Configuration.Host,
                ClientId = Dns.GetHostName(),
                GroupId = $"{Configuration.Topic}-group-0",
            };


            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
               consumer.Subscribe(Configuration.Topic);

                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(TimeSpan.FromSeconds(5000));
                        Console.WriteLine($"Offset.Value?{consumeResult?.Offset.Value}, MSG={consumeResult?.Message?.Value}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERRO NO CONSUMO DA MSSG: {ex}");
                    }                    
                }                
                
            }
        }
    }
}

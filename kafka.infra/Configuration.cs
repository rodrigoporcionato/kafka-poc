using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kafka.infra
{
    public static class Configuration
    {
        public static string Host { get; set; } = "localhost:9092";

        public static string Topic { get; set; } = "topic-orders";

        public static object FakeMesssage { get; set; } = new
        {
            name = "test",
            id = Guid.NewGuid(),
            value = new Random().Next(1,100)
        };
    }
}

using Confluent.Kafka;

namespace Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Gets all messages that were sended before to kinaction_helloworld topc

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092,localhost:9093,localhost:9094",
                GroupId = "kinaction_helloconsumer",
                AutoOffsetReset = AutoOffsetReset.Latest,
            };

            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                c.Subscribe("kinaction_helloworld");

                try
                {
                    while(true)
                    {
                        var cr = c.Consume(1000);

                        if (cr != null)
                            Console.WriteLine($"Consumed message '{cr.Value}' ofsset: '{cr.TopicPartitionOffset}' Partition: {cr.Partition}");
                    }                   
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
            }
        }
    }
}
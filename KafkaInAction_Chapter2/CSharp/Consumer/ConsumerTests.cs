using Confluent.Kafka;

namespace Consumer
{
    public class ConsumerTests
    {
        [Test]
        public void AllMessageFromBegginnignTest()
        {
            // Gets all messages that were sended before to kinaction_helloworld topc

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092,localhost:9093,localhost:9094",
                GroupId = $"kinaction_helloconsumer_AllMessageFromBegginnignTest_{Guid.NewGuid()}",
                AutoOffsetReset = AutoOffsetReset.Earliest,                
            };

            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                c.Subscribe("kinaction_helloworld");

                try
                {
                    var cr = c.Consume(500);

                    Assert.IsNotNull(cr);

                    Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");

                    while (cr != null)
                    {
                        cr = c.Consume(500);
                        if( cr != null )
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                    Assert.Fail(e.Message);
                }
            }
        }

        [Test]
        public void Wait30SecondsLatestMessageTest()
        {
            //send message via CMD kafka-console-producer
            //  CMD: kafka-console-producer --bootstrap-server localhost:9092 --topic kinaction_helloworld 
            //and recieve here

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092,localhost:9093,localhost:9094",
                GroupId = $"kinaction_helloconsumer_Wait30SecondsLatestMessage",
                AutoOffsetReset = AutoOffsetReset.Latest,
            };

            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                c.Subscribe("kinaction_helloworld");

                try
                {
                    var cr = c.Consume(30000);

                    Assert.IsNotNull(cr);

                    Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");

                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                    Assert.Fail(e.Message);
                }
            }
        }
    }
}
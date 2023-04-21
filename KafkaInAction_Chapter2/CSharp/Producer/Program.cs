using Confluent.Kafka;

namespace Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092,localhost:9093,localhost:9094" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                int number = 0;
                while (true)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        var responce = p.ProduceAsync("kinaction_helloworld", new Message<Null, string> { Value = $"hello world again! {number++}" });
                        responce.Wait();
                        Console.WriteLine($"{responce.Result.Value} {responce.Result.Status} {responce.Result.Partition}");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
        }
    }
}
using Confluent.Kafka;

namespace Producer
{
    public class ProducerTests
    {
        [Test]
        public void HelloWorldProducer()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092,localhost:9093,localhost:9094" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var responce = p.ProduceAsync("kinaction_helloworld", new Message<Null, string> { Value = "hello world again!" });
                    responce.Wait();
                    Assert.That(responce.Result.Status, Is.EqualTo(PersistenceStatus.Persisted));
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }
    }
}
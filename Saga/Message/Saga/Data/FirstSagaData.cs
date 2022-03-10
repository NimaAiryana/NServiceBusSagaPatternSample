using NServiceBus;

namespace Saga.Message.Saga.Data
{
    public class FirstSagaData : ContainSagaData
    {
        public string CorrelationId { get; set; }
    }
}

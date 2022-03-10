using NServiceBus;

namespace Common.Message.Events
{
    public abstract class EventBase : IEvent, IMessage
    {
        protected EventBase(string correlationId)
        {
            CorrelationId = correlationId;
        }

        public DateTime CreatedAt { get; } = DateTime.Now;
        public string CorrelationId { get; set; }
        public string GetCorrelationId()
        {
            return CorrelationId;
        }
    }
}

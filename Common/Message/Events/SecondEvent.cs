namespace Common.Message.Events
{
    public class SecondEvent : EventBase
    {
        public SecondEvent(Guid eventId, string correlationId) : base(correlationId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}

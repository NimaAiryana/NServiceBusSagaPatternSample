namespace Common.Message.Events
{
    public class FirstEvent : EventBase
    {
        public FirstEvent(Guid eventId, string correlationId) : base(correlationId)
        {
            EventId = eventId;
        }

        public Guid EventId  { get; set; }
    }
}
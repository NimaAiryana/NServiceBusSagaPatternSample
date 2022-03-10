namespace Common.Message.Events
{
    public class SagaTimeoutEvent : EventBase
    {
        public SagaTimeoutEvent(string correlationId) : base(correlationId)
        {
        }
    }
}

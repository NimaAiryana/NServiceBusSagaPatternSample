using NServiceBus;

namespace Saga.Message
{
    public class Bus : IBus
    {
        private readonly IMessageSession _messageSession;

        public Bus(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task Publish(object message, PublishOptions options)
        {
            await _messageSession.Publish(message, options).ConfigureAwait(false);

        }

        public async Task Publish<T>(Action<T> messageConstructor, PublishOptions publishOptions)
        {
            await _messageSession.Publish<T>(messageConstructor, publishOptions).ConfigureAwait(false);
        }

        public async Task Send(object message, SendOptions options)
        {
            await _messageSession.Send(message, options).ConfigureAwait(false);
        }

        public async Task Send<T>(Action<T> messageConstructor, SendOptions options)
        {
            await _messageSession.Send<T>(messageConstructor, options).ConfigureAwait(false);
        }

        public Task Subscribe(Type eventType, SubscribeOptions options)
        {
            return _messageSession.Subscribe(eventType, options);
        }

        public Task Unsubscribe(Type eventType, UnsubscribeOptions options)
        {
            return _messageSession.Unsubscribe(eventType, options);
        }
    }
}

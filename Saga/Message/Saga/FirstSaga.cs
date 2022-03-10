using Common.Message.Events;
using NServiceBus;
using Saga.Message.Saga.Data;

namespace Saga.Message.Saga
{
    public class FirstSaga : Saga<FirstSagaData>,
        IAmStartedByMessages<FirstEvent>,
        IHandleMessages<SecondEvent>,
        IHandleTimeouts<SagaTimeoutEvent>
    {
        public Task Handle(FirstEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine("First event started: " + message.EventId);
            Console.WriteLine("First event correlationid started: " + message.CorrelationId);

            return Task.CompletedTask;
        }

        public Task Handle(SecondEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine("Second event started: " + message.EventId);
            Console.WriteLine("Second event correlationid started: " + message.CorrelationId);

            MarkAsComplete();

            return Task.CompletedTask;
        }

        public Task Timeout(SagaTimeoutEvent state, IMessageHandlerContext context)
        {
            Console.WriteLine("timeout event correlationid started: " + state.CorrelationId);

            MarkAsComplete();

            return Task.CompletedTask;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<FirstSagaData> mapper)
        {
            mapper.ConfigureMapping<FirstEvent>(message => message.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);

            mapper.ConfigureMapping<SecondEvent>(message => message.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);
            
            mapper.ConfigureMapping<SagaTimeoutEvent>(message => message.CorrelationId)
                .ToSaga(sagaData => sagaData.CorrelationId);
        }
    }
}

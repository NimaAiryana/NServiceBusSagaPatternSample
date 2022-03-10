using Common.Message.Events;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Saga.Message;
using System.Threading;

namespace Saga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IBus _bus;

        public EventController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public void RaiseEvent()
        {
            var id = Guid.NewGuid();
            var firstEvent = new FirstEvent(id, id.ToString());

            _bus.Publish(firstEvent).ConfigureAwait(true);

            Thread.Sleep(5000);

            var secondEvent = new SecondEvent(id, id.ToString());
            _bus.Publish(secondEvent).ConfigureAwait(true);
        }
    }
}

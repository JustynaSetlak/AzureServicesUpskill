using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orders.EventHandler.Events;
using Orders.EventHandler.Interfaces;

namespace Orders.ApiControllers
{
    [Route("api/search")]
    [ApiController]
    public class OrderSearchController : ControllerBase
    {
        private readonly IEventDispatchService _eventDispatchService;

        public OrderSearchController(IEventDispatchService eventDispatchService)
        {
            _eventDispatchService = eventDispatchService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<EventGridEvent> eventGridEvents)
        {
            foreach (var eventGridEvent in eventGridEvents)
            {
                if (eventGridEvent.EventType == EventTypes.EventGridSubscriptionValidationEvent)
                {
                    var result = _eventDispatchService.DispatchEventGridSubscriptionValidationEvent(eventGridEvent);
                    return Ok(result);
                }

                await _eventDispatchService.Dispatch(eventGridEvent);
            }

            return NoContent();
        }
    }
}
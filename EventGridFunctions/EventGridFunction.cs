// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using Newtonsoft.Json;
using ApiWithEventGrid.Models;

namespace EventGridFunctions
{
    public static class EventGridFunction
    {
        [FunctionName("UserRegisteredEventHandler")]
        public static void Run(
            [EventGridTrigger] EventGridEvent eventGridEvent,
            ILogger log)
        {
            var data = JsonConvert.DeserializeObject<UserDto>(eventGridEvent.Data.ToString());
            log.LogInformation($"Sending email to user: {data.Email}");
            log.LogInformation($"User Data: FirstName={data.FirstName}, LastName={data.LastName}, Email={data.Email}");
        }
    }
}

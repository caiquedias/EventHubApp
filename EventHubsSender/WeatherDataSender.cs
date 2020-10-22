using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubsSender
{
    public interface IWeatherDataSender
    {
        Task SendDataAsync(WeatherData data);
    }
    public class WeatherDataSender : IWeatherDataSender
    {
        private EventHubProducerClient _eventHubClient;

        public WeatherDataSender(string eventHubConnectionString, string eventHubName)
        {
            try
            {
                _eventHubClient = new EventHubProducerClient(eventHubConnectionString, eventHubName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task SendDataAsync(WeatherData data)
        {
            try
            {
                //IEnumerable<EventData> eventData = new List<EventData>();
                //eventData.Append(CreateEventData(data));

                using EventDataBatch eventBatch = await _eventHubClient.CreateBatchAsync();
                eventBatch.TryAdd(CreateEventData(data));

                await _eventHubClient.SendAsync(eventBatch) ;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static EventData CreateEventData(WeatherData data)
        {
            var dataAsJson = JsonConvert.SerializeObject(data);
            var eventData = new EventData(Encoding.UTF8.GetBytes(dataAsJson));
            return eventData;
        }
    }
}

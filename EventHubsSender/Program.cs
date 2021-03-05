using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using IntegrationFlow.Models;
using Newtonsoft.Json;

namespace EventHubsSender
{
    class Program
    {
        private const string connectionString = "Endpoint=sb://poc-arquitetura.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+NjmNQHmXYJQSBmPK8HViAd7T9t+XZtcU/QbBQofXj8=";
        private const string eventHubName = "poc-arquitetura-eventhub";
        private const string eventHubResponseName = "integrationflow-resposta";
        private const string storageV2conn = "DefaultEndpointsProtocol=https;AccountName=stpocarquiteturav2;AccountKey=NbBbFdDQuLnbLqLClExwAFZWGW7P2UcLq1hJ4XcUYsWB96C+nWcN0Ayg/gt91v3GiYbhu3l13roq/MRvRX7djw==;EndpointSuffix=core.windows.net";


        static async Task Main()
        {
            //await SendWeatherEvent();
            await CreateEvent(TicketEvent.Create());
            Console.WriteLine("Evento criado, por favor, continue os testes");
            Console.Read();
        }

        static async Task SendWeatherEvent()
        {
            SenderHelper helper = new SenderHelper(connectionString, eventHubName);

            for (int i = 0; i < 5; i++)
            {
               await helper.DataSender();
            }

            Console.Read();
        }

        static async Task SendEvents()
        {
            // Create a producer client that you can use to send events to an event hub
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                // Create a batch of events 
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Evento de teste 1")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Evento de teste 2")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Evento de teste 3")));

                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("A batch of 3 events has been published.");
                Console.Read();
            }
        }

        public static async Task CreateEvent(CreateTicketMODEL ticket)
        {
            try
            {
                string eventHubConnectionString = connectionString;
                string eventHubName = eventHubResponseName;

                EventHubProducerClient _eventHubClient = new EventHubProducerClient(eventHubConnectionString, eventHubName);
                using (EventDataBatch eventBatch = await _eventHubClient.CreateBatchAsync())
                {
                    eventBatch.TryAdd(CreateEventData(ticket));
                    await _eventHubClient.SendAsync(eventBatch);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static EventData CreateEventData(Object data)
        {
            var dataAsJson = JsonConvert.SerializeObject(data);
            var eventData = new EventData(Encoding.UTF8.GetBytes(dataAsJson));
            return eventData;
        }
    }
}

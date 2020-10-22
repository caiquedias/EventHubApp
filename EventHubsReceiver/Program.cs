using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using System;

namespace EventHubsReceiver
{
    class Program
    {
        private const string ehubNamespaceConnectionString = "Endpoint=sb://poc-arquitetura.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+NjmNQHmXYJQSBmPK8HViAd7T9t+XZtcU/QbBQofXj8=";
        private const string eventHubName = "poc-arquitetura-eventhub";
        private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=stpocarquitetura;AccountKey=adAoWPqDqYQ/nogykgRo+lDmVWvqUSVeoRV9cluyRA5yZVWr3ayTkl09W0QS/b3gA+2pvT95YLm7er5F1lPLpA==;EndpointSuffix=core.windows.net";
        private const string blobContainerName = "apim";

        static async Task Main()
        {
            try
            {
                // Read from the default consumer group: $Default
                string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

                // Create a blob container client that the event processor will use 
                BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

                // Create an event processor client to process events in the event hub
                EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, ehubNamespaceConnectionString, eventHubName);


                // Register handlers for processing events and handling errors
                processor.ProcessEventAsync += ProcessEventHandler;
                processor.ProcessErrorAsync += ProcessErrorHandler;

                // Start the processing
                await processor.StartProcessingAsync();

                // Wait for 10 seconds for the events to be processed
                await Task.Delay(TimeSpan.FromMinutes(3));

                // Stop the processing
                await processor.StopProcessingAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            Console.WriteLine("\tRecevied event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));

            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}

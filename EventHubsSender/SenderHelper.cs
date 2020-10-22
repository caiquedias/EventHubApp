using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventHubsSender
{
    public class SenderHelper
    {
        WeatherDataSender weatherDataSender;

        public SenderHelper(string conn, string eventhub)
        {
            weatherDataSender = new WeatherDataSender(conn, eventhub);
        }

        public async Task SendDataAsync(WeatherData WeatherData)
        {
            try
            {
                await weatherDataSender.SendDataAsync(WeatherData);
                Console.WriteLine($"Sent data: {WeatherData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public async Task DataSender()
        {
            await SendDataAsync(CreateWeatherData());
        }

        public WeatherData CreateWeatherData()
        {
            return new WeatherData
            {
                Temperature = 35, // in degree celcius  
                WindSpeed = 300, // Kmph  
                WindDirection = WeatherData.Direction.North
            };
        }
    }
}


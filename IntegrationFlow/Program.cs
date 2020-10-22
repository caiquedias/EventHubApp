using IntegrationFlow.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationFlow
{
    public class Program
    {

        private const string listAllTickets = "?order_type=desc";
        private const string host = "https://dorconsultoria.freshservice.com";

        static async Task Main()
        {
            await GetATicket("37406");//28
            await GetAllTickets();
        }

        static async Task GetAllTickets()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(host);

                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("w0PpZ5coxeU6VqBqBu"));

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {base64authorization}");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = httpClient.GetAsync("api/v2/tickets"+listAllTickets).Result;

                    var tickets = response.Content.ReadAsStringAsync().Result;

                    Tickets TicketsList = JsonConvert.DeserializeObject<Tickets>(tickets);

                    var listaMovimentacao = TicketsList.tickets.Where(item => (item.category != null ? item.category.ToString() : "") == "MOVIMENTACAO").ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        static async Task GetATicket(string TicketID)
        {
            string responseMessage;

            try
            {
                if (string.IsNullOrEmpty(TicketID))
                {
                    responseMessage = "This HTTP triggered function executed successfully. Pass a ticket data in the query string or in the request body for a personalized response.";
                    Console.WriteLine(responseMessage);
                }
                else
                {
                    responseMessage = $"Hello, {TicketID} acquired. This HTTP triggered function executed successfully.";
                    Console.WriteLine(responseMessage);
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(host);

                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("w0PpZ5coxeU6VqBqBu"));

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {base64authorization}");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = httpClient.GetAsync("api/v2/tickets/" + TicketID).Result;

                    var ticketJson = response.Content.ReadAsStringAsync().Result;

                    TicketMODEL ticket = JsonConvert.DeserializeObject<TicketMODEL>(ticketJson);
                    JObject jObject = JObject.Parse(ticketJson);
                    string attachmentName = (string)jObject.SelectToken("ticket.attachments[0].name");

                    var anexo = ticket.ticket.attachments.FirstOrDefault().attachment_url;
                    var anexoJson = (string)jObject.SelectToken("ticket.attachments[0].attachment_url");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}

using IntegrationFlow.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
            //await GetATicket("37406");//28
            //await GetAllTickets();
            //await GetAgents();
            await GetGroups();
        }

        static Task GetAllTickets()
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

                    return Task.CompletedTask;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        static Task GetATicket(string TicketID)
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

                    return Task.CompletedTask;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        static Task GetAgents()
        {
            string responseMessage;

            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(host);

                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("w0PpZ5coxeU6VqBqBu"));

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {base64authorization}");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = httpClient.GetAsync("api/v2/agents").Result;

                    var Json = response.Content.ReadAsStringAsync().Result;

                    AgentsMODEL agents = JsonConvert.DeserializeObject<AgentsMODEL>(Json);

                    List<Agent> agent = agents.agents.Where(a => a.group_ids.Where(b => b.Equals(15000743028)).Any()).ToList();

                    List<Agent> teste = agents.agents.Where(a => a.first_name.Equals("Wellington")).ToList();//Wellington Silva

                    return Task.CompletedTask;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static Task GetGroups()
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(host);

                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("w0PpZ5coxeU6VqBqBu"));

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {base64authorization}");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = httpClient.GetAsync("api/v2/groups").Result;

                    var Json = response.Content.ReadAsStringAsync().Result;

                    GroupsMODEL agents = JsonConvert.DeserializeObject<GroupsMODEL>(Json);

                    var gruposFiltrados = agents.groups.Where(item => item.name.ToUpper().Contains("MOVIMENTAÇÃO")).ToList();

                    string texto = string.Empty;

                    foreach(var item in gruposFiltrados)
                        texto += $"{item.name}: {item.id}\n";

                    GravarLog(texto, @"C:\Estimativas\Projeto Automacao Flow SPRINT 1\InfoFlow\");

                    return Task.CompletedTask;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void GravarLog(string texto, string caminho)
        {
            try
            {
                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho);

                caminho = $"{caminho}\\ResultOf_";

                StreamWriter sw;
                if (!File.Exists(caminho + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt"))
                    sw = File.CreateText(caminho + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt");
                else
                    sw = new StreamWriter(caminho + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt", true);
                sw.WriteLine(texto);
                sw.Close();
                sw.Dispose();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}

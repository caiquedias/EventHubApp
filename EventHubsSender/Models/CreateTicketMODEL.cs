using System;
using System.Collections.Generic;
using System.Text;

namespace EventHubsSender
{
    public class CreateTicketMODEL
    {
        public string type { get; set; }
        public int ticket_id { get; set; }
        public string name { get; set; }
        public string requester_id { get; set; }
        public string reply { get; set; }
        public string subject { get; set; }
        public string email { get; set; }
        public int priority { get; set; }
        public int status { get; set; }
        public List<string> cc_emails { get; set; }
        public int source { get; set; }
        public long group_id { get; set; }
        public Dictionary<string, string> custom_fields { get; set; }
        public long responder_id { get; set; }
    }
}

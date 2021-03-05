using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationFlow.Models
{
    public class GroupsMODEL
    {
        public List<Group> groups { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Group
    {
        public object id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object escalate_to { get; set; }
        public string unassigned_for { get; set; }
        public List<object> agent_ids { get; set; }
        public List<object> members { get; set; }
        public object business_hours_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool auto_ticket_assign { get; set; }
    }
}

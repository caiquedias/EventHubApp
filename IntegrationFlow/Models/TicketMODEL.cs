using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationFlow.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class TicketMODEL
    {
        public TicketDetail ticket { get; set; }
    }

    public class Attachment
    {
        public string content_type { get; set; }
        public int size { get; set; }
        public string name { get; set; }
        public string attachment_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class TicketDetail
    {
        public List<object> cc_emails { get; set; }
        public List<object> fwd_emails { get; set; }
        public List<object> reply_cc_emails { get; set; }
        public bool fr_escalated { get; set; }
        public bool spam { get; set; }
        public object email_config_id { get; set; }
        public object group_id { get; set; }
        public int priority { get; set; }
        public string requester_id { get; set; }
        public object responder_id { get; set; }
        public int source { get; set; }
        public int status { get; set; }
        public string subject { get; set; }
        public object to_emails { get; set; }
        public string sla_policy_id { get; set; }
        public object department_id { get; set; }
        public int id { get; set; }
        public string type { get; set; }
        public DateTime due_by { get; set; }
        public DateTime fr_due_by { get; set; }
        public bool is_escalated { get; set; }
        public string description { get; set; }
        public string description_text { get; set; }
        public List<Dictionary<string,string>> custom_fields { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int urgency { get; set; }
        public int impact { get; set; }
        public object category { get; set; }
        public object sub_category { get; set; }
        public object item_category { get; set; }
        public bool deleted { get; set; }
        public List<Attachment> attachments { get; set; }
    }
}

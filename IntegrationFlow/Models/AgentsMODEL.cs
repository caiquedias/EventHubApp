using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationFlow.Models
{ 
    public class AgentsMODEL
    {
        public List<Agent> agents { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class Scopes
    {
        public string ticket { get; set; }
        public string solution { get; set; }
        public string problem { get; set; }
        public string change { get; set; }
        public string release { get; set; }
        public string asset { get; set; }
        public string contract { get; set; }
    }

    public class Agent
    {
        public bool active { get; set; }
        public string address { get; set; }
        public string background_information { get; set; }
        public bool can_see_all_tickets_from_associated_departments { get; set; }
        public DateTime created_at { get; set; }
        //public List<Dictionary<string,string>> custom_fields { get; set; }
        public List<object> department_ids { get; set; }
        public string email { get; set; }
        public object external_id { get; set; }
        public string first_name { get; set; }
        public bool has_logged_in { get; set; }
        public object id { get; set; }
        public string job_title { get; set; }
        public string language { get; set; }
        public DateTime last_active_at { get; set; }
        public DateTime last_login_at { get; set; }
        public string last_name { get; set; }
        public object location_id { get; set; }
        public string mobile_phone_number { get; set; }
        public bool occasional { get; set; }
        public object reporting_manager_id { get; set; }
        public List<object> role_ids { get; set; }
        public Scopes scopes { get; set; }
        public int? scoreboard_level_id { get; set; }
        public string signature { get; set; }
        public string time_format { get; set; }
        public string time_zone { get; set; }
        public DateTime updated_at { get; set; }
        public string work_phone_number { get; set; }
        public List<object> group_ids { get; set; }
        public List<object> member_of { get; set; }
    }
}

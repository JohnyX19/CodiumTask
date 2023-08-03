using System;
using System.Collections.Generic;


namespace CodiumTask
{

    public class MessageData
    {
        public string MessageID { get; set; }
        public DateTime GeneratedDate { get; set; }
        public EventData Event { get; set; }
    }

    public class EventData
    {
        public int ID { get; set; }
        public long ProviderEventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public List<OddsData> OddsList { get; set; }
    }

    public class OddsData
    {
        public int ID { get; set; }
        public long ProviderOddsID { get; set; }
        public string OddsName { get; set; }
        public double OddsRate { get; set; }
        public string Status { get; set; }
        public long ProviderEventID { get; set; }
    }

}

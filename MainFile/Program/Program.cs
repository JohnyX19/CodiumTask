using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CodiumTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DbClass db = new DbClass();
            Random random = new Random();

            string jsonFilePath = @" --cesta k súboru-- ";
            string jsonContent = File.ReadAllText(jsonFilePath);
            List<MessageData> messageData = JsonConvert.DeserializeObject<List<MessageData>>(jsonContent);

            List<EventData> eventsJson = new List<EventData>();
            
            foreach (MessageData d in messageData)
            {
                eventsJson.Add(d.Event);
            }

            Parallel.ForEach(eventsJson, (eventItem) =>
            {
                List<EventData> eventsFromDb = db.GetProviderEvents();

                if (eventsFromDb.Where(ev => ev.ProviderEventID == eventItem.ProviderEventID).FirstOrDefault() == null)
                {
                    Task.Delay(random.Next(0, 10000));
                    db.InsertEvent(eventItem);

                    foreach(OddsData oddsItem in eventItem.OddsList)
                    {
                        OddsData insertedOdds = new OddsData
                        {
                            ProviderOddsID = oddsItem.ProviderOddsID,
                            OddsName = oddsItem.OddsName,
                            OddsRate = oddsItem.OddsRate,
                            Status = oddsItem.Status,
                            ProviderEventID = eventItem.ProviderEventID
                        };
                        db.InsertOdds(insertedOdds);

                    }
                }
                else
                {
                    if (eventsFromDb.Where(ev => ev.ProviderEventID == eventItem.ProviderEventID).FirstOrDefault().EventDate != eventItem.EventDate)
                    {
                        db.UpdateEvent(eventItem);
                    }

                    List<OddsData> oddsByEventFromDb = db.GetProviderOddsByEvent(eventItem.ProviderEventID);

                    foreach(OddsData oddsInEvent in eventItem.OddsList)
                    {
                        if(oddsByEventFromDb.Where(oe => oe.ProviderOddsID == oddsInEvent.ProviderOddsID).FirstOrDefault() == null)
                        {
                            oddsInEvent.ProviderEventID = oddsInEvent.ProviderEventID != eventItem.ProviderEventID ? eventItem.ProviderEventID : oddsInEvent.ProviderEventID;

                            db.InsertOdds(oddsInEvent);
                        }
                        else
                        {
                            if(oddsByEventFromDb.Where(oe => oe.ProviderOddsID == oddsInEvent.ProviderOddsID).FirstOrDefault().Status != oddsInEvent.Status)
                            {
                                db.UpdateOddsStatus(oddsInEvent);
                            }

                            if (oddsByEventFromDb.Where(oe => oe.ProviderOddsID == oddsInEvent.ProviderOddsID).FirstOrDefault().OddsRate != oddsInEvent.OddsRate)
                            {
                                db.UpdateOddsRate(oddsInEvent);
                            }
                        }
                    }

                }

            });
            
            Console.ReadKey();
        }
    }
}

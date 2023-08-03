using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodiumTask
{
    public class DbClass
    {
        string connectionString = "Data Source=;Initial Catalog=;Integrated Security=SSPI;";

        public string InsertOdds(OddsData odds)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "INSERT INTO ProviderOdds (ProviderOddsID, OddsName, OddsRate, Status, ProviderEventID) VALUES (@ProviderOddsID, @OddsName, @OddsRate, @Status, @ProviderEventID)";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderOddsID", odds.ProviderOddsID);
                        command.Parameters.AddWithValue("@OddsName", odds.OddsName);
                        command.Parameters.AddWithValue("@OddsRate", odds.OddsRate);
                        command.Parameters.AddWithValue("@Status", odds.Status);
                        command.Parameters.AddWithValue("@ProviderEventID", odds.ProviderEventID);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public string UpdateOddsStatus(OddsData odds)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE ProviderOdds SET Status = @Status WHERE ProviderOddsID = @ProviderOddsID";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderOddsID", odds.ProviderOddsID);
                        command.Parameters.AddWithValue("@Status", odds.Status);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public string UpdateOddsRate(OddsData odds)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE ProviderOdds SET OddsRate = @OddsRate WHERE ProviderOddsID = @ProviderOddsID";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderOddsID", odds.ProviderOddsID);
                        command.Parameters.AddWithValue("@OddsRate", odds.OddsRate);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public string InsertEvent(EventData eventdata)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "INSERT INTO ProviderEvent (ProviderEventID, EventName, EventDate) VALUES (@ProviderEventID, @EventName, @EventDate)";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderEventID", eventdata.ProviderEventID);
                        command.Parameters.AddWithValue("@EventName", eventdata.EventName);
                        command.Parameters.AddWithValue("@EventDate", eventdata.EventDate);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public string UpdateEvent(EventData eventdata)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE ProviderEvent SET EventDate = @EventDate WHERE ProviderEventID = @ProviderEventID";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderEventID", eventdata.ProviderEventID);
                        command.Parameters.AddWithValue("@EventDate", eventdata.EventDate);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public List<EventData> GetProviderEvents()
        {
            List<EventData> eventList = new List<EventData>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM ProviderEvent";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EventData newEvent = new EventData
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    ProviderEventID = Convert.ToInt64(reader["ProviderEventID"]),
                                    EventName = reader["EventName"].ToString(),
                                    EventDate = Convert.ToDateTime(reader["EventDate"])
                                };
                                eventList.Add(newEvent);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return eventList;
        }

        public List<OddsData> GetProviderOdds()
        {
            List<OddsData> oddsList = new List<OddsData>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM ProviderOdds";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OddsData newOdds = new OddsData 
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    ProviderOddsID = Convert.ToInt64(reader["ProviderOddsID"]),
                                    OddsName = reader["OddsName"].ToString(),
                                    OddsRate = Convert.ToDouble(reader["OddsRate"]),
                                    Status = reader["Status"].ToString(),
                                    ProviderEventID = Convert.ToInt64(reader["ProviderEventID"])
                                };
                                oddsList.Add(newOdds);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return oddsList;
        }

        public List<OddsData> GetProviderOddsByEvent(long eventId)
        {
            List<OddsData> oddsList = new List<OddsData>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM ProviderOdds WHERE ProviderEventID = @ProviderEventID";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderEventID", eventId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OddsData newOdds = new OddsData
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    ProviderOddsID = Convert.ToInt64(reader["ProviderOddsID"]),
                                    OddsName = reader["OddsName"].ToString(),
                                    OddsRate = Convert.ToDouble(reader["OddsRate"]),
                                    Status = reader["Status"].ToString(),
                                    ProviderEventID = Convert.ToInt64(reader["ProviderEventID"])
                                };

                                oddsList.Add(newOdds);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return oddsList;
        }
    }
}

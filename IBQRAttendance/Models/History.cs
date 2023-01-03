using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Models
{
    public class History
    {
        public string drivername { get; set; }
        public decimal fare { get; set; }
        public string destination { get; set; }
        public string pickup_location { get; set; }
        public string created { get; set; }

        public static List<History> GetDriverHistory(int id)
        {
            var histories = new List<History>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGeBookHistory", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var item = new History();
                        item.drivername = row["drivername"].ToString();
                        item.fare = Convert.ToDecimal(row["fare"].ToString());
                        item.destination = row["destination"].ToString();
                        item.pickup_location = row["pickup_location"].ToString();
                        item.created = row["created"].ToString();
                        histories.Add(item);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return histories;
        }
    }
}
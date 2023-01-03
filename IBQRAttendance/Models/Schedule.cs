using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Models
{
    public class Schedule
    {
        public string Days { get; set; }
        public string Name { get; set; }
        public string Dates { get; set; }
        public List<Schedule> GetSchedules(int id)
        {
            var schedules = new List<Schedule>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGetSchedule", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", id);
                    cmd.CommandTimeout = 2000;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var schedule = new Schedule();
                        schedule.Dates = row["Dates"].ToString();
                        schedule.Days = row["WeekName"].ToString();
                        schedule.Name = row["DriverName"].ToString();
                        schedules.Add(schedule);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return schedules;
        }
    }
    public class Report
    {
        public int USERID { get; set; }
        public string MESSAGE { get; set; }

        public bool SendReport(Report report)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spSaveReports", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FIRSTNAME", report.USERID);
                    cmd.Parameters.AddWithValue("@LASTTNAME", report.MESSAGE);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return true;
        }
    }
}
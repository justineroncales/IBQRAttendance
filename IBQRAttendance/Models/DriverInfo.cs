using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Models
{
    public class DriverInfo
    {
       // static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString);
        public int dri_id { get; set; }
        public string dri_fullname { get; set; }
        public string dri_plateno { get; set; }
        public bool dri_isbooked { get; set; }

        static DriverInfo()
        {

        }
        public static List<DriverInfo> GetDriverInfos()
        {
            var driverInfos = new List<DriverInfo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                //using (conn)
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGetDrivers",conn );
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var driver = new DriverInfo();
                        driver.dri_id = Convert.ToInt32(row["dri_id"]);
                        driver.dri_fullname = row["dri_fullname"].ToString();
                        driver.dri_plateno = row["dri_plateno"].ToString();
                        driver.dri_isbooked = Convert.ToBoolean(row["dri_isbooked"]);
                        driverInfos.Add(driver);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            

            return driverInfos;
        }

    }
}
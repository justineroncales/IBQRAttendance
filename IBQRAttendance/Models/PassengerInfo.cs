using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Models
{
    public class PassengerInfo
    {
        //static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString);
        public int pi_id { get; set; }
        public string pi_lname { get; set; }
        public string pi_fname { get; set; }
        public string pi_mname { get; set; }
        public string pi_address { get; set; }
        public string pi_num { get; set; }
        public string pi_email { get; set; }
        public string pi_username { get; set; }
        public string pi_password { get; set; }

        public string DeviceID { get; set; }
        public string OTP { get; set; }
        public PassengerInfo()
        {

        }
        public static PassengerInfo Login(PassengerInfo passengerInfo)
        {
            var p = new PassengerInfo();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", passengerInfo.pi_username);
                cmd.Parameters.AddWithValue("@PASSWORD", passengerInfo.pi_password);
                cmd.Parameters.AddWithValue("@DeviceID", passengerInfo.DeviceID);
                cmd.Parameters.AddWithValue("@OTP", passengerInfo.OTP);
                cmd.CommandTimeout = 2000;
                SqlDataAdapter sqlData = new SqlDataAdapter();
                sqlData.SelectCommand = cmd;
                sqlData.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    p.pi_fname = row["pi_fname"].ToString();
                    p.pi_lname = row["pi_lname"].ToString();
                    p.pi_num = row["pi_num"].ToString();
                    p.pi_email = row["pi_email"].ToString();
                    p.pi_id = Convert.ToInt32(row["pi_id"]);
                }

            }
            return p;
        }
        public static bool Register(PassengerInfo passengerInfo)
        {
            var result = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spRegisterPassenger", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FNAME", passengerInfo.pi_fname);
                cmd.Parameters.AddWithValue("@LNAME", passengerInfo.pi_lname);
                cmd.Parameters.AddWithValue("@MNAME", passengerInfo.pi_mname);
                cmd.Parameters.AddWithValue("@ADDRESS", passengerInfo.pi_address);
                cmd.Parameters.AddWithValue("@EMAIL", passengerInfo.pi_email);
                cmd.Parameters.AddWithValue("@USERNAME", passengerInfo.pi_username);
                cmd.Parameters.AddWithValue("@PASSWORD", passengerInfo.pi_password);
                cmd.Parameters.AddWithValue("@PHONENOS", passengerInfo.pi_num);
                cmd.Parameters.Add("@RESULT", SqlDbType.Bit, 25).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(cmd.Parameters["@RESULT"].Value);
            }
            return result;
        }
        public static bool ChangePassword(PassengerInfo passengerInfo)
        {
            var result = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spChangePassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PI_ID", passengerInfo.pi_id);
                cmd.Parameters.AddWithValue("@USERNAME", passengerInfo.pi_username);
                cmd.Parameters.AddWithValue("@PASSWORD", passengerInfo.pi_password);
                cmd.Parameters.Add("@RESULT", SqlDbType.Bit, 1).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(cmd.Parameters["@RESULT"].Value);
            }
            return result;
        }
        public static bool Authenticate(PassengerInfo passengerInfo)
        {
            var result = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGetOTP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DeviceID", passengerInfo.DeviceID);
                cmd.Parameters.AddWithValue("@OTP", passengerInfo.OTP);
                cmd.Parameters.Add("@RESULT", SqlDbType.Bit, 25).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(cmd.Parameters["@RESULT"].Value);
            }
            return result;
        }
    }
}
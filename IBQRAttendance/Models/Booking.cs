using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Models
{
    public class Booking
    {
        //static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString);
        public int book_id { get; set; }
        public int dri_id { get; set; }
        public int pi_id { get; set; }
        public int NumberofPassenger { get; set; }
        public string Destination { get; set; }
        public string PickupLocation { get; set; }
        public decimal fare { get; set; }
        public Booking()
        {

        }

        public static int Booked(Booking booking)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBookedAtrike", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pi_id", booking.pi_id);
                cmd.Parameters.AddWithValue("@numberofpassenger", booking.NumberofPassenger);
                cmd.Parameters.AddWithValue("@destination", booking.Destination);
                cmd.Parameters.AddWithValue("@pickuplocation", booking.PickupLocation);
                cmd.Parameters.AddWithValue("@fare", booking.fare);
                cmd.Parameters.Add("@bookid", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@bookid"].Value);
            }
            return result;
        }
        public static bool CancelBooked(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCancel", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@book_id", id);
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public static bool IsBooked(int id)
        {
            var result = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCheckIfBooked", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.Add("@RESULT", SqlDbType.Bit, 1).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(cmd.Parameters["@RESULT"].Value);
            }
            return result;
        }
    }
}
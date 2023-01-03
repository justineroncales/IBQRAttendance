using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Models
{
    public class Users
    {
        public int USERID { get; set; }
        public int DRIVERID { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required!")]
        public string FIRSTNAME { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required!")]
        public string LASTNAME { get; set; }

        [Display(Name = "Middle Name")]
        [Required(ErrorMessage = "Middle Name Required!")]
        public string MIDDLENAME { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string EMAIL { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required!")]
        public string ADDRESS { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [Display(Name = "Phone Number")]
        public string PHONENUMBER { get; set; }

        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public int CLUSTERID { get; set; }
        public double LATITUDE { get; set; }
        public double LONGITUDE { get; set; }
        public bool RegisterDriver(Users users)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spRegisterDriver", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FIRSTNAME", users.FIRSTNAME);
                    cmd.Parameters.AddWithValue("@LASTTNAME", users.LASTNAME);
                    cmd.Parameters.AddWithValue("@MIDDLETNAME", users.MIDDLENAME);
                    cmd.Parameters.AddWithValue("@PHONENUMBER", users.PHONENUMBER);
                    cmd.Parameters.AddWithValue("@ADDRESS", users.ADDRESS);
                    cmd.Parameters.AddWithValue("@USERNAME", users.USERNAME);
                    cmd.Parameters.AddWithValue("@PASSWORD", users.PASSWORD);
                    cmd.Parameters.AddWithValue("@CLUSTERID", users.CLUSTERID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
            return true;
        }
        public List<Users> GetDriverLocation(int id)
        {
            DataTable dt = new DataTable();
            List<Users> _user = new List<Users>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGetDriversLocation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERID", id);
                    cmd.CommandTimeout = 2000;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var item = new Users();
                        item.LATITUDE = Convert.ToDouble(row["LATITUDE"]);
                        item.LONGITUDE = Convert.ToDouble(row["LONGITUDE"]);
                        item.FIRSTNAME = row["FIRSTNAME"].ToString();
                        item.LASTNAME = row["LASTNAME"].ToString();
                        item.PHONENUMBER = row["CONTACT"].ToString();
                        _user.Add(item);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
            return _user;
        }
        public Users Login(Users users)
        {
             var _user = new Users();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spLogin", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERNAME", users.USERNAME);
                    cmd.Parameters.AddWithValue("@PASSWORD", users.PASSWORD);
                    cmd.CommandTimeout = 2000;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        _user.FIRSTNAME = row["FIRSTNAME"].ToString();
                        _user.LASTNAME = row["LASTNAME"].ToString();
                        _user.USERID = Convert.ToInt32(row["USERSID"]);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return _user;
        }
        public bool UpdateLocation(Users users)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateDriver", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DRIVERID", users.DRIVERID);
                    cmd.Parameters.AddWithValue("@LATITUDE", users.LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", users.LONGITUDE);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }
        public bool Register(Users users)
        {
            var result = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spRegisterUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FNAME", users.FIRSTNAME);
                    cmd.Parameters.AddWithValue("@LNAME", users.LASTNAME);
                    cmd.Parameters.AddWithValue("@MNAME", users.MIDDLENAME);
                    cmd.Parameters.AddWithValue("@ADDRESS", users.ADDRESS);
                    cmd.Parameters.AddWithValue("@EMAIL", users.EMAIL);
                    cmd.Parameters.AddWithValue("@PHONENOS", users.PHONENUMBER);
                    cmd.Parameters.AddWithValue("@USERNAME", users.USERNAME);
                    cmd.Parameters.AddWithValue("@PASSWORD", users.PASSWORD);
                    cmd.Parameters.AddWithValue("@CLUSTERID", users.CLUSTERID);
                    cmd.Parameters.Add("@RESULT", SqlDbType.Bit, 25).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@RESULT"].Value);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return result;
        }
        public Users LoginD(Users users)
        {
            var _user = new Users();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spLoginDr", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERNAME", users.USERNAME);
                    cmd.Parameters.AddWithValue("@PASSWORD", users.PASSWORD);
                    cmd.CommandTimeout = 2000;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        _user.FIRSTNAME = row["FIRSTNAME"].ToString();
                        _user.LASTNAME = row["LASTNAME"].ToString();
                        _user.USERID = Convert.ToInt32(row["DRIVERID"]);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return _user;
        }
        public List<Users> GetUsersContact(int id)
        {
            var _user = new List<Users>();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllUsers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERID", id);
                    cmd.CommandTimeout = 2000;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var item = new Users();
                        item.PHONENUMBER = row["CONTACT"].ToString();
                        _user.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return _user;
        }
        public List<Users> GetDriverPerC(int id)
        {
            var _user = new List<Users>();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGetDriverPerC", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERID", id);
                    cmd.CommandTimeout = 2000;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var item = new Users();
                        item.USERID = Convert.ToInt32(row["DRIVERID"]);
                        item.FIRSTNAME = row["FIRSTNAME"].ToString();
                        _user.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return _user;
        }
    }

    public class Cluster
    {
        public string Place { get; set; }
        public int ClusterId { get; set; }

        public List<Cluster> getListofCluster()
        {

            var clusters = new List<Cluster>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["jplstrike"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGeClusters", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    sqlData.SelectCommand = cmd;
                    sqlData.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var cluster = new Cluster();
                        cluster.ClusterId = Convert.ToInt32(row["CLUSTERID"]);
                        cluster.Place = row["PLACE"].ToString();
                        clusters.Add(cluster);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return clusters;


        }
    }
}
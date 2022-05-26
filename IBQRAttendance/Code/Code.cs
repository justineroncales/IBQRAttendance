using IBQRAttendance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Code
{
    public class Codes
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["IBQRAttendance"].ConnectionString);
        public bool Register(GlobalClass model)
        {
            try
            {
                bool result = false;
                using (conn)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spRegister", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FIRSTNAME", model.users.FIRSTNAME);
                    cmd.Parameters.AddWithValue("@LASTNAME", model.users.LASTNAME);
                    cmd.Parameters.AddWithValue("@MIDDLENAME", model.users.MIDDLENAME);
                    cmd.Parameters.AddWithValue("@EMAIL", model.users.EMAIL);
                    cmd.Parameters.AddWithValue("@ADDRESS", model.users.ADDRESS);
                    cmd.Parameters.AddWithValue("@PHONENUMBER", model.users.PHONENUMBER);
                    cmd.Parameters.AddWithValue("@QRVALUE", model.qRCodeModel.QRCodeText);
                    cmd.Parameters.AddWithValue("@DEPARTMENTID", Convert.ToInt32(model.users.department));
                    cmd.Parameters.AddWithValue("@LEVELID", Convert.ToInt32(model.users.level));
                    cmd.Parameters.AddWithValue("@URLPATH", model.qRCodeModel.QRCodeImagePath);
                    cmd.Parameters.Add("@Result", SqlDbType.Bit, 1).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Login(QRCodeModel model)
        {
            int result;
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QRVALUE", model.QRCodeText);
                cmd.Parameters.AddWithValue("@TIME", model.QRTime);
                cmd.Parameters.Add("@Result", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@Result"].Value);
            }

            return result;
        }
        public bool RemoveFromList(int USERID)
        {
            bool result;
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spRemoveFromList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.Add("@Result", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
            }
            return result;
        }

        public List<QRCodeModel> GetTimeList(int deptid)
        {
            List<QRCodeModel> users = new List<QRCodeModel>();

            using (conn)
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGetTimeList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DEPARTMENTID", deptid);
                cmd.CommandTimeout = 0;
                SqlDataAdapter sqlData = new SqlDataAdapter();
                sqlData.SelectCommand = cmd;
                sqlData.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    var list = new QRCodeModel();
                    list.DAYS = row["DAYS"].ToString();
                    list.STAMPS = row["STAMPS"].ToString();
                    list.FULLNAME = row["FULLNAME"].ToString();
                    list.ACTIVITY = row["ACTIVITY"].ToString();
                    list.LEVEL = row["LEVEL"].ToString();
                    list.DEPARTMENT = row["DEPARTMENT"].ToString();
                    users.Add(list);
                }
            }
            return users;
        }
        public List<Users> GetRegiteredList()
        {
            List<Users> users = new List<Users>();

            using (conn)
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGetRegisteredList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter sqlData = new SqlDataAdapter();
                sqlData.SelectCommand = cmd;
                sqlData.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    var list = new Users();
                    list.USERID = Convert.ToInt32(row["USERSID"].ToString());
                    list.FIRSTNAME = row["FIRSTNAME"].ToString();
                    list.LASTNAME = row["LASTNAME"].ToString();
                    list.MIDDLENAME = row["MIDDLENAME"].ToString();
                    list.EMAIL = row["EMAIL"].ToString();
                    list.ADDRESS = row["ADDRESS"].ToString();
                    list.PHONENUMBER = row["PHONENUMBER"].ToString();
                    list._LEVEL = row["LEVEL"].ToString();
                    list._DEPARTMENT = row["DEPARTMENT"].ToString();
                    list.URLPATH = row["URLPATH"].ToString();
                    users.Add(list);
                }
            }
            return users;
        }
    }
}
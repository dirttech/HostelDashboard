using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Configuration.Provider;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace App_Code.AdminLoginHostel
{


    #region AdminUsers Object

    public class AdminLogin
    {
        #region Constructer

        public AdminLogin()
        {

        }
        #endregion

        #region Fields & Properties


        private int id=0;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string userName = "";
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password = "";
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        #endregion

    }


    #endregion

    public static class AdminLogin_S
    {

        #region Feilds

        private static string connString = ConfigurationManager.ConnectionStrings["HostelAppConnectionString"].ConnectionString;

        private static DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["HostelAppConnectionString"].ProviderName);
        private static string parmPrefix = "@";

        #endregion

        #region Methods

        public static AdminLogin Loging(string UserName)
        {
            AdminLogin adminDetail = new AdminLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT IDs,UserName,Password" +
                                         " FROM Admin_Info WHERE UserName = @UserName";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "UserName";
                        dpID.Value = UserName;
                        cmd.Parameters.Add(dpID);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    adminDetail = new AdminLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        adminDetail.Id = rdr.GetInt32(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        adminDetail.UserName = rdr.GetString(1);
                                    }

                                    if (!rdr.IsDBNull(2))
                                    {
                                        adminDetail.Password = rdr.GetString(2);
                                    }

                                }
                            }
                            else
                            {
                                return null;
                            }
                        }



                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return adminDetail;

        }

        public static AdminLogin NewLoging(string UserName, string password)
        {
            AdminLogin adminDetail = new AdminLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT IDs,UserName,Password" +
                                         " FROM Admin_Info WHERE UserName = @UserName AND Password=@password";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "UserName";
                        dpID.Value = UserName;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpass = provider.CreateParameter();
                        dpass.ParameterName = parmPrefix + "password";
                        dpass.Value = password;
                        cmd.Parameters.Add(dpass);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    adminDetail = new AdminLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        adminDetail.Id = rdr.GetInt32(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        adminDetail.UserName = rdr.GetString(1);
                                    }

                                    if (!rdr.IsDBNull(2))
                                    {
                                        adminDetail.Password = rdr.GetString(2);
                                    }
                                   

                                }
                            }
                            else
                            {
                                return null;
                            }
                        }



                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return adminDetail;

        }
        
        public static bool InsertUser(AdminLogin insertAdmin)
        {
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery;
                        sqlQuery = "INSERT INTO Admin_Info" +
                               "(UserName,Password) " +
                               "VALUES(@userName,@password)";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                       

                        DbParameter sUserName = provider.CreateParameter();
                        sUserName.ParameterName = parmPrefix + "userName";
                        sUserName.Value = insertAdmin.UserName;
                        cmd.Parameters.Add(sUserName);


                        DbParameter sPassword = provider.CreateParameter();
                        sPassword.ParameterName = parmPrefix + "password";
                        sPassword.Value = insertAdmin.Password;
                        cmd.Parameters.Add(sPassword);


                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }

        }

       
      

        #endregion

    }
}
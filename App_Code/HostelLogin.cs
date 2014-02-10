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
using System.Security.Cryptography;

namespace App_Code.HostelLogin
{


    #region Users Object

    public class UserLogin
    {
        #region Constructer

        public UserLogin()
        {
            //this.UserId = Guid.NewGuid();
        }
        #endregion

        #region Fields & Properties

        private string password = "";
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string groupId="";
        public string GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        private string building = "";
        public string Building
        {
            get { return building; }
            set { building = value; }
        }

        #endregion

    }


    #endregion

    public static class UserLogin_S
    {

        #region Feilds

        private static string connString = ConfigurationManager.ConnectionStrings["HostelAppConnectionString"].ConnectionString;

        private static DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["HostelAppConnectionString"].ProviderName);
        private static string parmPrefix = "@";

        #endregion

        #region Methods

        public static UserLogin Loging(string Group_id, string build)
        {
            UserLogin userDetail = new UserLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id, building, password" +
                                         " FROM hostel_login WHERE group_id=@grp_id AND building=@build";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "grp_id";
                        dpID.Value = Group_id;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpBuild = provider.CreateParameter();
                        dpBuild.ParameterName = parmPrefix + "build";
                        dpBuild.Value = build;
                        cmd.Parameters.Add(dpBuild);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.GroupId = rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.Building = rdr.GetString(1);
                                    }                                   
                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.Password = rdr.GetString(2);
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
            return userDetail;

        }

      
        public static UserLogin NewLoging(string Group_id, string password, string build)
        {
            UserLogin userDetail = new UserLogin();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id, building, password" +
                                         " FROM hostel_login WHERE group_id=@grp_id AND password =@password AND building=@build";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "grp_id";
                        dpID.Value = Group_id;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpass = provider.CreateParameter();
                        dpass.ParameterName = parmPrefix + "password";
                        dpass.Value = password;
                        cmd.Parameters.Add(dpass);

                        DbParameter dbuild = provider.CreateParameter();
                        dbuild.ParameterName = parmPrefix + "build";
                        dbuild.Value = build;
                        cmd.Parameters.Add(dbuild);
                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.GroupId = rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.Building = rdr.GetString(1);
                                    }

                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.Password = rdr.GetString(2);
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
            return userDetail;

        }

        public static List<UserLogin> ListOfAllGroups()
        {
            List<UserLogin> AllUsers = new List<UserLogin>();           

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id,building" +
                                         " FROM hostel_login";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;                

                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                   UserLogin userDetail = new UserLogin();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.GroupId = rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.Building = rdr.GetString(1);
                                    }
                                    AllUsers.Add(userDetail);
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        conn.Close();
                        return AllUsers;
                    }
                }
            }
            catch (Exception exp)
            {
                return null;
            }        

        }

        public static bool InsertUser(UserLogin insertUser)
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
                        sqlQuery = "INSERT INTO hostel_login" +
                               "(group_id,password, building) " +
                               "VALUES(@grp_id,@password,@building)";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "grp_id";
                        sNewId.Value = insertUser.GroupId;
                        cmd.Parameters.Add(sNewId);
                       
                        DbParameter sPassword = provider.CreateParameter();
                        sPassword.ParameterName = parmPrefix + "password";
                        sPassword.Value = insertUser.Password;
                        cmd.Parameters.Add(sPassword);

                        DbParameter sBuilding = provider.CreateParameter();
                        sBuilding.ParameterName = parmPrefix + "building";
                        sBuilding.Value = insertUser.Building;
                        cmd.Parameters.Add(sBuilding);

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

        public static bool ResetPassword(UserLogin newPwdUser)
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
                        sqlQuery = "UPDATE hostel_login " +
                               "SET password = @password WHERE group_id = @grpId AND building=@build";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;



                        DbParameter sUserId = provider.CreateParameter();
                        sUserId.ParameterName = parmPrefix + "grpId";
                        sUserId.Value = newPwdUser.GroupId;
                        cmd.Parameters.Add(sUserId);


                        DbParameter sPassword = provider.CreateParameter();
                        sPassword.ParameterName = parmPrefix + "password";
                        sPassword.Value = newPwdUser.Password;
                        cmd.Parameters.Add(sPassword);

                        DbParameter sBuild = provider.CreateParameter();
                        sBuild.ParameterName = parmPrefix + "build";
                        sBuild.Value = newPwdUser.Building;
                        cmd.Parameters.Add(sBuild);

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
/*
        public static bool ResetUsername(UserLogin newPwdUser)
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
                        sqlQuery = "UPDATE login_Info " +
                               "SET UserName = @usrname WHERE userID = @userId";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;



                        DbParameter sUserId = provider.CreateParameter();
                        sUserId.ParameterName = parmPrefix + "userId";
                        sUserId.Value = newPwdUser.UserId;
                        cmd.Parameters.Add(sUserId);


                        DbParameter sUsername = provider.CreateParameter();
                        sUsername.ParameterName = parmPrefix + "usrname";
                        sUsername.Value = newPwdUser.UserName;
                        cmd.Parameters.Add(sUsername);



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

        public static bool UpdateProfile(UserLogin newPwdUser)
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
                        sqlQuery = "UPDATE login_Info " +
                               "SET FullName = @fullnames, mobile=@mobiles, email=@emails WHERE userID = @userId";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;



                        DbParameter sUserId = provider.CreateParameter();
                        sUserId.ParameterName = parmPrefix + "userId";
                        sUserId.Value = newPwdUser.UserId;
                        cmd.Parameters.Add(sUserId);


                        DbParameter sname = provider.CreateParameter();
                        sname.ParameterName = parmPrefix + "fullnames";
                        sname.Value = newPwdUser.FullName;
                        cmd.Parameters.Add(sname);

                        DbParameter sMobile = provider.CreateParameter();
                        sMobile.ParameterName = parmPrefix + "mobiles";
                        sMobile.Value = newPwdUser.Mobile;
                        cmd.Parameters.Add(sMobile);

                        DbParameter sEmail = provider.CreateParameter();
                        sEmail.ParameterName = parmPrefix + "emails";
                        sEmail.Value = newPwdUser.EMail;
                        cmd.Parameters.Add(sEmail);



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
*/
        #endregion

    }
}
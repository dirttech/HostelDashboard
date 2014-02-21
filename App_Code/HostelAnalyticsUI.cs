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

namespace App_Code.HostelAnalytics
{

    public class HostelAnalyticUI
    {
        #region Constructer

        public HostelAnalyticUI()
        {
            //this.Id = Guid.NewGuid();
        }

        #endregion

        #region Fields & Properties

        private Guid id;
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        private string event_id = "";
        public string Event_id
        {
            get { return event_id; }
            set { event_id = value; }
        }

        private string user_id = "";
        public string User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public DateTime dated ;
        public DateTime Dated
        {
            get { return dated; }
            set { dated = value; }
        }

        #endregion

    }

    public static class HostelAnalytic_UI
    {
        #region Feilds

        private static string connString = ConfigurationManager.ConnectionStrings["AnalyticsConnectionString"].ConnectionString;

        private static DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["AnalyticsConnectionString"].ProviderName);
        private static string parmPrefix = "@";

        #endregion

        #region Methods

        public static List<HostelAnalyticUI> GetAnalyticListBetweenTime(DateTime fromTime, DateTime toTime)
        {
            List<HostelAnalyticUI> AnalyticsObjList = new List<HostelAnalyticUI>();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {

                        string sqlQuery = "SELECT event_id,user_id,dated,id FROM event_log_hostel WHERE dated BETWEEN @fromDate AND @toDate ORDER BY user_id,dated ASC";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;


                        DbParameter dpFromDate = provider.CreateParameter();
                        dpFromDate.ParameterName = parmPrefix + "fromDate";
                        dpFromDate.Value = fromTime;
                        cmd.Parameters.Add(dpFromDate);

                        DbParameter dpToDate = provider.CreateParameter();
                        dpToDate.ParameterName = parmPrefix + "toDate";
                        dpToDate.Value = toTime;
                        cmd.Parameters.Add(dpToDate);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                HostelAnalyticUI fetchObj;
                                while (rdr.Read())
                                {
                                    fetchObj = new HostelAnalyticUI();

                                    if (!rdr.IsDBNull(0))
                                    {
                                        fetchObj.Event_id = rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        fetchObj.User_id = rdr.GetString(1);
                                    }
                                    if (!rdr.IsDBNull(2))
                                    {
                                        fetchObj.Dated = rdr.GetDateTime(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        fetchObj.ID = rdr.GetGuid(3);
                                    }
                                    AnalyticsObjList.Add(fetchObj);
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
                return AnalyticsObjList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

     
        #endregion
    }
}
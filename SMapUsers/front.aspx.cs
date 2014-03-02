using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App_Code.FetchingEnergyss;
using App_Code.FetchingEnergySmap;
using App_Code.Utility;
using App_Code.HostelMapping;
using WebAnalytics;

public partial class Users_front : System.Web.UI.Page
{
    public float[] energyArray = new float [14];

    public int[] timeSample;
    public string[] fromTimeArray;
    public string[] toTimeArray;

    public static string username = "";
    public static string building = "";
    public double[] valueSample;

    protected void logOut_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
        Response.Redirect("~/Default.aspx");
    }
    protected void CheckLogin()
    {
        if (Session["UserName"] == null || Session["UserName"] == "")
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            nameTitle.InnerText = "Welcome " + Session["UserName"].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        if (IsPostBack == false)
        {
            try
            {
                building = Session["Building"].ToString();
                username = Session["UserName"].ToString();
                GroupMapping grpMap=Group_Mapping.MapGroup(username,building);
                Avg_Value(DateTime.Today.AddDays(-1), DateTime.Today.AddMinutes(-1), grpMap);
                if (grpMap != null)
                {
                    try
                    {
                        WebAnalytics.LoggerService LG = new LoggerService();

                        LoggingEvent logObj = new LoggingEvent();
                        logObj.EventID = "Hostel Home Page";
                        logObj.UserID = grpMap.GroupId;
                        bool sts = LG.LogEventHostel(logObj);

                    }
                    catch (Exception exp)
                    {

                    }

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Room No", typeof(string));
                    dt.Columns.Add("Occupant Names", typeof(string));

                    for (int i = 0; i < grpMap.Occupants.RoomNos.Count; i++)
                    {
                        dt.Rows.Add(grpMap.Occupants.RoomNos[i], grpMap.Occupants.OccupantNames[i]);
                    }
                    occupantList.DataSource = dt;
                    occupantList.DataBind();
                }
            }
            catch (Exception exp)
            {

            }
        }        
        else
        {
            
        }       
    }


    protected void Avg_Value(DateTime fromdate, DateTime todate, GroupMapping grpMap)
    {
        try
        {
            double[] energyArray1;
            int[] timeArray1;
            double yourValue = 0;
            int metCt = 0;
            if (grpMap != null)
            {
                FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.AddMinutes(10).ToString("MM/dd/yyyy HH:mm"), building, grpMap.Meters.MeterId, out timeArray1, out energyArray1);
                for (int i = 0; i < timeArray1.Length; i++)
                {
                    yourValue = yourValue + energyArray1[i];
                }
                if (metCt == 1)
                {
                    yourValue = yourValue * 2;
                }
            }
            string str1 = "";
            str1 = "Previous day (" + DateTime.Now.AddDays(-1).ToString("dd MMM yyyy") + "), You! have consumed <font color='#f18221'>" + Math.Round(yourValue / 1000, 2).ToString() + " KWhrs </font>";
        

            double[] avgEnergyArray1;
            int[] avgTimeArray1;
            
            double avgValue = 0;
            int meterCount = 0;
            List<GroupMapping> allMeters = Group_Mapping.ListAllGroups();
            for (int j = 0; j < allMeters.Count; j++)
            {
                FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), allMeters[j].Building, allMeters[j].Meters.MeterId, out avgTimeArray1, out avgEnergyArray1);
                for (int i = 0; i < avgTimeArray1.Length; i++)
                {
                    meterCount++;
                    avgValue = avgValue + avgEnergyArray1[i];
                }
            }
            avgValue = avgValue / (meterCount / 2);

            double percent = 0; string str2 = "";

            percent = ((avgValue - yourValue) / avgValue) * 100;

            percent = Math.Round(percent, 2, MidpointRounding.ToEven);

            if (percent > 0)
            {
                str2 = "which is <font color='#f18221'>" + Convert.ToDouble(percent).ToString() + "% </font> " + " less " + "than " + "your fellow neighbours.";
            }
            else
            {
                str2 = "which is <font color='#f18221'>" + Convert.ToDouble(Math.Abs(percent)).ToString() + "% </font> " + " more " + "than " + "your fellow neighbours.";
            }
            topLine.InnerHtml = str1 + str2;

        }
        catch (Exception exp)
        {

        }
    }
    protected void occupantList_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
    
    }
}
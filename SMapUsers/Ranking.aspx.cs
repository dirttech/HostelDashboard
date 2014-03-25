using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using App_Code.FetchingEnergyss;
using App_Code.FetchingEnergySmap;
using App_Code.Utility;
using System.Web.Script.Serialization;
using App_Code.HostelMapping;
using WebAnalytics;

public partial class Ranking : System.Web.UI.Page
{
    public JavaScriptSerializer javaSerial = new JavaScriptSerializer();

    public string[] groupNames;public double[] energyValues;public string[] timeValues;

    public int[] avgTimeStamps;
    public int[] timeStamps;

    public static string username = "";
    public static string building = "";

    public double[] values;
    public string[] timeSeries;

    public double[] avgValues;
    public string[] avgTimeSeries;

  
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

        if (Session["UserName"] != null && Session["Building"] != null)
        {
            building = Session["Building"].ToString();
            username = Session["UserName"].ToString();
            if (IsPostBack == false)
            {
                DateTime frdate = DateTime.Now.AddDays(-1);
                DateTime todate = DateTime.Now.AddHours(-1);
               
                GroupMapping grpMap = Group_Mapping.MapGroup(username, building);
                meterTypeList.Items.Add(new ListItem("Per Occupant", grpMap.OccupantCount.ToString()));
                Plot_Avg_Graph(frdate, todate);
            }
        }
        else
        {
            //meterTypeList.SelectedValue = meter_type;
            Response.Write("<script>alert('Sorry! Your Meter is not registered yet.');</script>");
            //Session["UserName"] = null;
            //Response.Redirect("LoginPage.aspx");
        }
        if (IsPostBack == false)
        {
            try
            {
                WebAnalytics.LoggerService LG = new LoggerService();

                LoggingEvent logObj = new LoggingEvent();
                logObj.EventID = "Group Ranking Page";
                logObj.UserID = username;
                bool sts = LG.LogEventHostel(logObj);

            }
            catch (Exception exp)
            {

            }
        }
    }

    protected void Plot_Avg_Graph(DateTime fromdate, DateTime todate)
    {
        try
        {
            GroupMapping grpMap = Group_Mapping.MapGroup(username, building);
            double[] energyArray1;
            int[] timeArray1;
            double yourValue = 0;
            if (grpMap != null)
            {
                FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), building, grpMap.Meters.MeterId, out timeArray1, out energyArray1);
                if (timeArray1.Length > 1)
                {
                    Utilities ut1 = Utilitie_S.EpochToDateTime(timeArray1[0]);
                    messg.Text = ut1.Date.ToString("dd MMM") + " - " + todate.ToString("dd MMM");

                    for (int i = 0; i < timeArray1.Length; i++)
                    {
                            yourValue = yourValue + energyArray1[i];
                    }
                }
                yourValue = Math.Round(yourValue / (1000*Convert.ToInt32(meterTypeList.SelectedValue)),2);
            }

            List<GroupMapping> allGroups = Group_Mapping.ListHostelGroups(building);
           
            groupNames = new string[1]; energyValues = new double[1]; timeValues = new string[1];
            if (allGroups != null)
            {
                groupNames = new string[allGroups.Count]; energyValues = new double[allGroups.Count]; timeValues = new string[allGroups.Count];
                for (int it = 0; it < allGroups.Count; it++)
                {
                    double[] energyArray11;
                    int[] timeArray11;
                    double yourValue11 = 0;
                    int metCt11 = 0;
                    if (allGroups[it] != null)
                    {
                        FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), building, allGroups[it].Meters.MeterId, out timeArray11, out energyArray11);
                        for (int iit = 0; iit < timeArray11.Length; iit++)
                        {
                                yourValue11 = yourValue11 + energyArray11[iit];
                        }
                        if (metCt11 == 1)
                        {
                            yourValue11 = yourValue11 * 2;
                        }
                        if (Convert.ToInt32(meterTypeList.SelectedValue) > 1)
                        {
                            yourValue11 = yourValue11 / allGroups[it].OccupantCount;
                        }
                        groupNames[it] = allGroups[it].GroupName;
                        energyValues[it] = Math.Round(yourValue11/1000,2);                       
                    }
                }
                if (energyValues.Length > 1)
                {
                    var sortedPairs = energyValues.Select((x, i) => new { Value = x, Keys = groupNames[i] }).OrderBy(x => x.Value).ThenBy(x => x.Keys).ToArray();
                    double[] energyValuer = sortedPairs.Select(x => x.Value).ToArray();
                    string[] groupNamer = sortedPairs.Select(x => x.Keys).ToArray();
                   
                    energyValues = energyValuer;
                    groupNames = groupNamer;

                    for (int i = 0; i < energyValues.Length; i++)
                    {
                        energyValues[i] = i + 1;
                    }

                    HtmlTable ht = new HtmlTable();

                    HtmlTableRow tr = new HtmlTableRow();
                    tr.Attributes.Add("style", "background-color:lightblue;font-size:small;height:50px;cell-padding:5px;text-align:center;");
                    for (int i = 0; i < energyValues.Length; i++)
                    {
                        HtmlTableCell td = new HtmlTableCell();
                        td.InnerText = groupNames[i];
                        tr.Cells.Add(td);
                    }
                    ht.Rows.Add(tr);

                    HtmlTableRow rr = new HtmlTableRow();
                    rr.Attributes.Add("style", "background-color:lightgray;font-size:large;font-weight:bolder;height:50px;text-align:center;");
                    for (int i = 0; i < energyValues.Length; i++)
                    {
                        HtmlTableCell tt = new HtmlTableCell();
                        tt.InnerText = energyValues[i].ToString();
                        rr.Cells.Add(tt);
                    }
                    ht.Rows.Add(rr);
                    container.Controls.Clear();
                    container.Controls.Add(ht);

                }
 

            }            
        }
        catch (Exception exp)
        {

        }
    }

    protected void plot_Click(object sender, EventArgs e)
    {
        
    }
    
    protected void meterTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime frdate = DateTime.Now.AddDays(-1);
        DateTime todate = DateTime.Now.AddHours(-1);
        Plot_Avg_Graph(frdate, todate);
        LastTFHours.Attributes.Add("class", "topButtons-Selected");
        WeekConsumption.Attributes.Remove("class");
        DayConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "Group Ranking Page View Changed To "+meterTypeList.SelectedItem.Text;
            logObj.UserID = username;
            bool sts = LG.LogEventHostel(logObj);

        }
        catch (Exception exp)
        {

        }
    }

    protected void logOut_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
        Response.Redirect("~/Default.aspx");
    }

    protected void LastTFHours_Click(object sender, EventArgs e)
    {
        DateTime frdate = DateTime.Now.AddDays(-1);
        DateTime todate = DateTime.Now.AddHours(-1);
        Plot_Avg_Graph(frdate, todate);
        LastTFHours.Attributes.Add("class", "topButtons-Selected");
        WeekConsumption.Attributes.Remove("class");
        DayConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "Group Ranking Page Last 24 Hours";
            logObj.UserID = username;
            bool sts = LG.LogEventHostel(logObj);

        }
        catch (Exception exp)
        {

        }
    }
    protected void DayConsumption_Click(object sender, EventArgs e)
    {

    }
    protected void WeekConsumption_Click(object sender, EventArgs e)
    {

    }
    protected void timeSet_Click(object sender, EventArgs e)
    {
        DateTime sampleDate = DateTime.ParseExact(date1.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                               System.Globalization.CultureInfo.InvariantCulture);
        DateTime frDate = new DateTime(sampleDate.Year, sampleDate.Month, sampleDate.Day, 0, 0, 1);
        DateTime toDate = new DateTime(sampleDate.Year, sampleDate.Month, sampleDate.Day, 23, 59, 59);
        Plot_Avg_Graph(frDate, toDate);
        DayConsumption.Attributes.Add("class", "topButtons-Selected");
        LastTFHours.Attributes.Remove("class");
        WeekConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "Group Ranking Page Day Consumption";
            logObj.UserID = username;
            bool sts = LG.LogEventHostel(logObj);

        }
        catch (Exception exp)
        {

        }
    }
    
    protected void selectWeek_Click(object sender, EventArgs e)
    {
        DateTime sampleDate = DateTime.ParseExact(date2.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                              System.Globalization.CultureInfo.InvariantCulture);
        DateTime frDate = new DateTime(sampleDate.Year, sampleDate.Month, sampleDate.Day, 0, 0, 1);
        DateTime toDate = frDate.AddDays(7);
        Plot_Avg_Graph(frDate, toDate);
        WeekConsumption.Attributes.Add("class", "topButtons-Selected");
        LastTFHours.Attributes.Remove("class");
        DayConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "Group Ranking Page Week Consumption";
            logObj.UserID = username;
            bool sts = LG.LogEventHostel(logObj);

        }
        catch (Exception exp)
        {

        }
    }
}
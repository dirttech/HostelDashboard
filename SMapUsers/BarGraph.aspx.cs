using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App_Code.FetchingEnergySmap;
using App_Code.Utility;
using System.Web.Script.Serialization;
using App_Code.HostelMapping;
using WebAnalytics;

public partial class BarGraph : System.Web.UI.Page
{

    public JavaScriptSerializer javaSerial = new JavaScriptSerializer();
    public double[] energyArray;
    public int[] timeArray;
    
    public static string[] timeSeries;

    public static string username = "";
    public static string building = "";

    protected void logOut_Click(object sender, EventArgs e)
    {
       
        Session["UserName"] = null;
        Response.Redirect("~/Default.aspx");
    }
    protected void CheckLogin()
    {
        if (Session["UserName"] == null || Session["UserName"]=="" )
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
              
        if (Session["UserName"] != null && Session["Building"]!=null)
        {
            building = Session["Building"].ToString();
            username = Session["UserName"].ToString();
        }
         else
         {
             Response.Write("<script>alert('Sorry! Your Meter is not registered yet.');</script>");
             //Session["UserName"] = null;
             //Response.Redirect("LoginPage.aspx");
         }
        if (IsPostBack == false)
        {
            DateTime frdate = DateTime.Now.AddDays(-1);
            DateTime todate = DateTime.Now.AddHours(1);
            Plot_Bar_Graph("hour", "1", frdate, todate);
            GroupMapping grpMap = Group_Mapping.MapGroup(username, building);
            if (grpMap != null)
            {
                meterTypeList.Items.Add(new ListItem("Per Occupant", grpMap.OccupantCount.ToString()));
                try
                {
                    WebAnalytics.LoggerService LG = new LoggerService();

                    LoggingEvent logObj = new LoggingEvent();
                    logObj.EventID = "Hostel My Consumption Page";
                    logObj.UserID = grpMap.GroupId;
                    bool sts = LG.LogEventHostel(logObj);

                }
                catch (Exception exp)
                {

                }
            }
        }   
    }
    protected void submitDate_Click(object sender, EventArgs e)
    {
       
    }
    protected void Plot_Bar_Graph(string min_hour,string width,DateTime fromdate, DateTime todate)
    {
        try
        {
            GroupMapping grpMap = Group_Mapping.MapGroup(username, building);
            if (grpMap != null)
            {
               FetchEnergyDataS_Map.FetchHostelData(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), min_hour,width, building, "Energy", grpMap.Meters.MeterId, out timeArray, out energyArray);
                
               if (timeArray.Length > 1)
                {
                    for (int l = 0; l < energyArray.Length; l++)
                    {
                        if (Convert.ToInt32(meterTypeList.SelectedValue) > 1)
                        {
                            energyArray[l] =Math.Round(energyArray[l] / (Convert.ToInt32(meterTypeList.SelectedValue)),2);
                        }
                    }
                    Utilities ut1 = Utilitie_S.EpochToDateTime(timeArray[0]);
                    Utilities ut2 = Utilitie_S.EpochToDateTime(timeArray[timeArray.Length - 1]);
                    messg.Text = ut1.Date.ToString("dd MMM") + " - " + ut2.Date.ToString("dd MMM");
                   
                    timeSeries = Utilitie_S.TimeFormatterBar(timeArray);
                   
                }
            }
        }
        catch (Exception e)
        {

        }
    }
  
    protected void meterTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime frdate = DateTime.Now.AddDays(-1);
        DateTime todate = DateTime.Now.AddHours(1);
        Plot_Bar_Graph("hour", "1", frdate, todate);
        LastTFHours.Attributes.Add("class", "topButtons-Selected");
        WeekConsumption.Attributes.Remove("class");
        DayConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "My Consumption Page View Changed To "+meterTypeList.SelectedItem.Text;
            logObj.UserID = username;
            bool sts = LG.LogEventHostel(logObj);

        }
        catch (Exception exp)
        {

        }
    }
    protected void LastTFHours_Click(object sender, EventArgs e)
    {
        DateTime frdate=DateTime.Now.AddDays(-1);
        DateTime todate = DateTime.Now.AddHours(1); 
        Plot_Bar_Graph("hour","1", frdate, todate);
        LastTFHours.Attributes.Add("class", "topButtons-Selected");
        WeekConsumption.Attributes.Remove("class");
        DayConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "My Consumption Page Last 24 Hours";
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
        DateTime toDate = new DateTime(sampleDate.AddDays(1).Year, sampleDate.AddDays(1).Month, sampleDate.AddDays(1).Day, 1, 0, 1);
        Plot_Bar_Graph("hour", "1", frDate, toDate);
        DayConsumption.Attributes.Add("class", "topButtons-Selected");
        LastTFHours.Attributes.Remove("class");
        WeekConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "My Consumption Page Day Consumption";
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
        DateTime toDate = new DateTime(sampleDate.AddDays(8).Year, sampleDate.AddDays(8).Month, sampleDate.AddDays(8).Day, 0, 0, 1);
        Plot_Bar_Graph("hour", "24", frDate, toDate);
        WeekConsumption.Attributes.Add("class", "topButtons-Selected");
        LastTFHours.Attributes.Remove("class");
        DayConsumption.Attributes.Remove("class");

        try
        {
            WebAnalytics.LoggerService LG = new LoggerService();

            LoggingEvent logObj = new LoggingEvent();
            logObj.EventID = "My Consumption Page Week Consumption";
            logObj.UserID = username;
            bool sts = LG.LogEventHostel(logObj);

        }
        catch (Exception exp)
        {

        }
    }
}
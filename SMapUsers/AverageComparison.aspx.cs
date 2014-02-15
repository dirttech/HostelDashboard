using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using App_Code.FetchingEnergyss;
using App_Code.FetchingEnergySmap;
using App_Code.Utility;
using System.Web.Script.Serialization;
using App_Code.HostelMapping;

public partial class AverageComparison : System.Web.UI.Page
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
        if (IsPostBack == false)
        {
        }
        CheckLogin();

        if (Session["UserName"] != null && Session["Building"] != null)
        {
            building = Session["Building"].ToString();
            username = Session["UserName"].ToString();
            if (IsPostBack == false)
            {
                DateTime frdate = DateTime.Now.AddDays(-1);
                DateTime todate = DateTime.Now.AddHours(-1);
                Plot_Avg_Graph(frdate, todate);
                GroupMapping grpMap = Group_Mapping.MapGroup(username, building);
                meterTypeList.Items.Add(new ListItem("Per Occupant", grpMap.OccupantCount.ToString()));   
            }
        }
        else
        {
            //meterTypeList.SelectedValue = meter_type;
            Response.Write("<script>alert('Sorry! Your Meter is not registered yet.');</script>");
            //Session["UserName"] = null;
            //Response.Redirect("LoginPage.aspx");
        }
    }

    protected void Plot_Avg_Graph(DateTime fromdate, DateTime todate)
    {
        try
        {
            GroupMapping grpMap = Group_Mapping.MapGroup(username, building);
            double[] energyArray1;
            int[] timeArray1;
            double[] energyArray2;
            int[] timeArray2;
            double yourValue = 0;
            int metCt = 0;
            if (grpMap != null)
            {
                FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), fromdate.AddMinutes(10).ToString("MM/dd/yyyy HH:mm"), building, grpMap.Meters.MeterId, out timeArray1, out energyArray1);
                FetchEnergyDataS_Map.FetchAverageConsumption(todate.ToString("MM/dd/yyyy HH:mm"), todate.AddMinutes(10).ToString("MM/dd/yyyy HH:mm"), building, grpMap.Meters.MeterId, out timeArray2, out energyArray2);
                if (timeArray1.Length > 1)
                {
                    Utilities ut1 = Utilitie_S.EpochToDateTime(timeArray1[0]);
                    Utilities ut2 = Utilitie_S.EpochToDateTime(timeArray2[0]);
                    messg.Text = ut1.Date.ToString("dd MMM") + " - " + ut2.Date.ToString("dd MMM");

                    for (int i = 0; i < timeArray1.Length; i++)
                    {

                        if (energyArray1[i] != -1 && energyArray2[i] != -1)
                        {
                            metCt++;
                            yourValue = yourValue + (energyArray2[i] - energyArray1[i]);
                        }
                    }
                }
                if (metCt == 1)
                {
                    yourValue = yourValue * 2;
                }
                yourValue = Math.Round(yourValue / (1000*Convert.ToInt32(meterTypeList.SelectedValue)),2);
            }

            List<GroupMapping> allGroups = Group_Mapping.ListAllGroups(building);
           
            groupNames = new string[1]; energyValues = new double[1]; timeValues = new string[1];
            if (allGroups != null)
            {
                groupNames = new string[allGroups.Count]; energyValues = new double[allGroups.Count]; timeValues = new string[allGroups.Count];
                for (int it = 0; it < allGroups.Count; it++)
                {
                    double[] energyArray11;
                    int[] timeArray11;
                    double[] energyArray22;
                    int[] timeArray22;
                    double yourValue11 = 0;
                    int metCt11 = 0;
                    if (allGroups[it] != null)
                    {
                        FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), fromdate.AddMinutes(10).ToString("MM/dd/yyyy HH:mm"), building, allGroups[it].Meters.MeterId, out timeArray11, out energyArray11);
                        FetchEnergyDataS_Map.FetchAverageConsumption(todate.ToString("MM/dd/yyyy HH:mm"), todate.AddMinutes(10).ToString("MM/dd/yyyy HH:mm"), building, allGroups[it].Meters.MeterId, out timeArray22, out energyArray22);
                        for (int iit = 0; iit < timeArray11.Length; iit++)
                        {
                            if (energyArray11[iit] != -1 && energyArray22[iit] != -1)
                            {
                                metCt++;
                                yourValue11 = yourValue11 + (energyArray22[iit] - energyArray11[iit]);
                            }
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
                if (energyValues.Length > 3)
                {
                    var sortedPairs = energyValues.Select((x, i) => new { Value = x, Keys = groupNames[i] }).OrderBy(x => x.Value).ThenBy(x => x.Keys).ToArray();
                    double[] energyValuer = sortedPairs.Select(x => x.Value).ToArray();
                    string[] groupNamer = sortedPairs.Select(x => x.Keys).ToArray();
                    int med = energyValuer.Length / 2;

                    energyValues = new double[4];
                    groupNames = new string[4];

                    energyValues[0] = energyValuer[0]; groupNames[0] = groupNamer[0];
                    energyValues[3] = energyValuer[energyValuer.Length - 1]; groupNames[3] = groupNamer[energyValuer.Length - 1];

                    if (yourValue > energyValuer[med])
                    {
                        energyValues[1] = energyValuer[med]; groupNames[1] = groupNamer[med];
                        energyValues[2] = yourValue; groupNames[2] = "You!";
                    }
                    else
                    {
                        energyValues[2] = energyValuer[med]; groupNames[2] = groupNamer[med];
                        energyValues[1] = yourValue; groupNames[1] = "You!";
                    }
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
    }
}
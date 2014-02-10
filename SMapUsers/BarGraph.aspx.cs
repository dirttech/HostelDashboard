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
            meterTypeList.Items.Add(new ListItem("Per Occupant", grpMap.OccupantCount.ToString()));
        }   
    }
    protected void submitDate_Click(object sender, EventArgs e)
    {
       
    }
    protected void Plot_Bar_Graph(string min_hour,string width,DateTime fromdate, DateTime todate)
    {
        try
        {
            double[] energyArray1;
            int[] timeArray1;
            double[] energyArray2;
            int[] timeArray2;
            GroupMapping grpMap = Group_Mapping.MapGroup(username, building);
            if (grpMap != null)
            {
                FetchEnergyDataS_Map.FetchHostelData(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), min_hour,width, building, "Energy", grpMap.Meters.MeterId[0].ToString(), out timeArray1, out energyArray1);        
                
                FetchEnergyDataS_Map.FetchHostelData(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), min_hour,width, building, "Energy", grpMap.Meters.MeterId[1].ToString(), out timeArray2, out energyArray2);

               
                if (timeArray1.Length > 1)
                {
                    Utilities ut1 = Utilitie_S.EpochToDateTime(timeArray1[0]);
                    Utilities ut2 = Utilitie_S.EpochToDateTime(timeArray1[timeArray1.Length - 1]);
                    messg.Text = ut1.Date.ToString("dd MMM") + " - " + ut2.Date.ToString("dd MMM");

                    energyArray = new double[timeArray1.Length - 1];
                    timeSeries = Utilitie_S.TimeFormatter(timeArray1);
                    for (int i = 0; i < timeArray1.Length - 1; i++)
                    {
                        if (energyArray1[i + 1] == -1 || energyArray2[i + 1] == -1)
                        {
                            energyArray[i] = -1;
                            energyArray[i + 1] = -1;
                            i = i + 1;
                        }
                        else
                        {
                            energyArray[i] = Math.Round(((energyArray1[i + 1] + energyArray2[i + 1]) - (energyArray1[i] + energyArray2[i]))/(1000*Convert.ToInt32(meterTypeList.SelectedValue)),2);
                            if (i == 0 && (energyArray1[i] == -1 || energyArray2[i] == -1))
                            {
                                energyArray[i] = -1;
                            }
                        }
                    }
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
    }
    protected void LastTFHours_Click(object sender, EventArgs e)
    {
        DateTime frdate=DateTime.Now.AddDays(-1);
        DateTime todate = DateTime.Now.AddHours(1); 
        Plot_Bar_Graph("hour","1", frdate, todate);
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
        DateTime toDate = new DateTime(sampleDate.AddDays(1).Year, sampleDate.AddDays(1).Month, sampleDate.AddDays(1).Day, 2, 0, 1);
        Plot_Bar_Graph("hour", "1", frDate, toDate);
        DayConsumption.Attributes.Add("class", "topButtons-Selected");
        LastTFHours.Attributes.Remove("class");
        WeekConsumption.Attributes.Remove("class");
    }
    protected void selectWeek_Click(object sender, EventArgs e)
    {
        DateTime sampleDate = DateTime.ParseExact(date2.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                               System.Globalization.CultureInfo.InvariantCulture);
        DateTime frDate = new DateTime(sampleDate.Year, sampleDate.Month, sampleDate.Day, 0, 0, 1);
        DateTime toDate = new DateTime(sampleDate.AddDays(9).Year, sampleDate.AddDays(9).Month, sampleDate.AddDays(9).Day, 0, 0, 1);
        Plot_Bar_Graph("hour", "24", frDate, toDate);
        WeekConsumption.Attributes.Add("class", "topButtons-Selected");
        LastTFHours.Attributes.Remove("class");
        DayConsumption.Attributes.Remove("class");
    }
}
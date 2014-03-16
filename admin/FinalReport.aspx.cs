using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App_Code.FetchingEnergyss;
using App_Code.FetchingEnergySmap;
using App_Code.HostelLogin;
using App_Code.Utility;
using App_Code.HostelMapping;

public partial class FinalReport : System.Web.UI.Page
{
    public string[] groupNames;public double[] energyValues;public string[] timeValues;
    public static string building = "";
    public static string meterTyped2 = "Light Backup";

    protected void CheckLogin()
    {
        if (Session["AdminUserName"] == null || Session["AdminUserName"] == "")
        {
            Response.Redirect("adminLogin.aspx");
        }
        else
        {

        }
    }

    protected void calculatePrint(DateTime fromdate, DateTime todate)
    {
        try
        {            
            List<GroupMapping> allGroups = Group_Mapping.ListAllGroups();

            groupNames = new string[1]; energyValues = new double[1]; timeValues = new string[1];
            if (allGroups != null)
            {
                groupNames = new string[allGroups.Count]; energyValues = new double[allGroups.Count]; timeValues = new string[allGroups.Count];
                for (int it = 0; it < allGroups.Count; it++)
                {
                    double[] energyArray11;
                    int[] timeArray11;
                    if (allGroups[it] != null)
                    {
                        FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), allGroups[it].Building, allGroups[it].Meters.MeterId, out timeArray11, out energyArray11);
                                              
                        groupNames[it] = allGroups[it].GroupId;
                        energyValues[it] = Math.Round(energyArray11[it] / 1000, 2);
                    }
                }
            }
        }
        catch (Exception exp)
        {

        }
    }

  

    protected void Page_Load(object sender, EventArgs e)
    {
        //CheckLogin();
        UNameOfPrinter.InnerText = "";
       
    }

    protected void printBill_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime sampleDate = DateTime.ParseExact(fromDate.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                                 System.Globalization.CultureInfo.InvariantCulture);
            DateTime frDate = new DateTime(sampleDate.Year, sampleDate.Month, sampleDate.Day, 0, 0, 1);
            DateTime sampleDate2 = DateTime.ParseExact(toDated.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                                System.Globalization.CultureInfo.InvariantCulture);
            DateTime toDate = new DateTime(sampleDate2.Year, sampleDate2.Month, sampleDate2.Day, 23, 59, 59);
            calculatePrint(frDate, toDate);
        }
        catch (Exception exp)
        {

        }
    }
    protected void buildingSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void prnt_ServerClick(object sender, EventArgs e)
    {
        try
        {
            DateTime sampleDate = DateTime.ParseExact(fromDate.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                                 System.Globalization.CultureInfo.InvariantCulture);
            DateTime frDate = new DateTime(sampleDate.Year, sampleDate.Month, sampleDate.Day, 0, 0, 1);
            DateTime sampleDate2 = DateTime.ParseExact(toDated.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                                System.Globalization.CultureInfo.InvariantCulture);
            DateTime toDate = new DateTime(sampleDate2.Year, sampleDate2.Month, sampleDate2.Day, 23, 59, 59);
            calculatePrint(frDate, toDate);
        }
        catch (Exception exp)
        {

        }
    }
}
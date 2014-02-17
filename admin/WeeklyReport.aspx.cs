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

public partial class WeeklyReport : System.Web.UI.Page
{
    public string[] groupNames;public double[] energyValues;public string[] timeValues;
    public double[] energyArray; public int[] timeArray;
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
            GroupMapping grpMap = Group_Mapping.MapGroup(uid.Value, buildingSelect.SelectedValue);
            double[] energyArray1;
            int[] timeArray1;
            double[] energyArray2;
            int[] timeArray2;
            double yourValue = 0;
            int metCt = 0;
            if (grpMap != null)
            {
                generateDashes(7, todate, grpMap);
                reportHead.InnerText = reportType.SelectedValue;
                group_id.InnerText = "Group: " + grpMap.GroupId;
                group_building.InnerText = grpMap.Building;
                group_wing.InnerText = "Total occupants: " + grpMap.OccupantCount.ToString();
                dated.InnerText = "Date: " + DateTime.Today.ToString("dd/MM/yy");
                dayed.InnerText = "Day: " + DateTime.Today.DayOfWeek;

                FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), fromdate.AddMinutes(10).ToString("MM/dd/yyyy HH:mm"), buildingSelect.SelectedValue, grpMap.Meters.MeterId, out timeArray1, out energyArray1);
                FetchEnergyDataS_Map.FetchAverageConsumption(todate.AddMinutes(-10).ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), buildingSelect.SelectedValue, grpMap.Meters.MeterId, out timeArray2, out energyArray2);
                if (timeArray1.Length > 1)
                {
                    Utilities ut1 = Utilitie_S.EpochToDateTime(timeArray1[0]);
                    Utilities ut2 = Utilitie_S.EpochToDateTime(timeArray2[0]);
                    prev_day.InnerText = "Your previous week: " + ut1.Date.ToString("dd MMM")+" to "+ut2.Date.ToString("dd MMM yyyy");

                    for (int i = 0; i < timeArray1.Length; i++)
                    {
                        if (energyArray1[i] != -1 && energyArray2[i] != -1)
                        {
                            metCt++;
                            yourValue = yourValue + (energyArray2[i] - energyArray1[i]);
                        }
                    }
                }
                yourValue = Math.Round(yourValue / 1000, 2);
                elec_units.InnerHtml = "Electricity Consumption = <b>" + yourValue + "</b> kW-h";
                elec_cost.InnerHtml = "Cost Equivalent Consumption = <b>Rs " + yourValue * 10 + "</b>";

                List<GroupMapping> allGroups = Group_Mapping.ListAllGroups(buildingSelect.SelectedValue);

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
                            FetchEnergyDataS_Map.FetchAverageConsumption(fromdate.ToString("MM/dd/yyyy HH:mm"), fromdate.AddMinutes(10).ToString("MM/dd/yyyy HH:mm"), buildingSelect.SelectedValue, allGroups[it].Meters.MeterId, out timeArray11, out energyArray11);
                            FetchEnergyDataS_Map.FetchAverageConsumption(todate.AddMinutes(-10).ToString("MM/dd/yyyy HH:mm"), todate.ToString("MM/dd/yyyy HH:mm"), buildingSelect.SelectedValue, allGroups[it].Meters.MeterId, out timeArray22, out energyArray22);
                            for (int iit = 0; iit < timeArray11.Length; iit++)
                            {
                                if (energyArray11[iit] != -1 && energyArray22[iit] != -1)
                                {
                                    metCt++;
                                    yourValue11 = yourValue11 + (energyArray22[iit] - energyArray11[iit]);
                                }
                            }
                            groupNames[it] = allGroups[it].GroupName;
                            energyValues[it] = Math.Round(yourValue11 / 1000, 2);
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

                        energyValues[0] = energyValuer[0]; groupNames[0] = groupNamer[0]+" (Lowest)";
                        energyValues[3] = energyValuer[energyValuer.Length - 1]; groupNames[3] = groupNamer[energyValuer.Length - 1] + " (Highest)";

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
        }
        catch (Exception exp)
        {

        }
    }

    protected void generateDashes(int numberOfDays, DateTime toDate, GroupMapping grpMap)
    {
        try
        {
            dashes.Controls.Clear();
            FetchEnergyDataS_Map.FetchHostelData(toDate.AddDays(-numberOfDays).ToString("MM/dd/yyyy HH:mm"), toDate.ToString("MM/dd/yyyy HH:mm"), "hour", "24", grpMap.Building, "Energy", grpMap.Meters.MeterId, out timeArray, out energyArray);
            for (int i = 0; i < energyArray.Length; i++)
            {
                HtmlGenericControl dash = new HtmlGenericControl("div");
                dash.Attributes.Add("class", "dash");

                HtmlGenericControl dayed = new HtmlGenericControl("p");
                dayed.Attributes.Add("class", "dayed");
                Utilities utt=Utilitie_S.EpochToDateTime(timeArray[i]);
                if (utt != null)
                {
                    dayed.InnerText = utt.Date.DayOfWeek.ToString();
                }

                HtmlGenericControl energier = new HtmlGenericControl("h4");
                energier.Attributes.Add("class", "energier");
                energier.InnerText = energyArray[i] + " kW-h";

                dash.Controls.Add(dayed);
                dash.Controls.Add(energier);
                dashes.Controls.Add(dash);
            }
        }
        catch (Exception exp)
        {

        }
    }

    protected void generateSideBarItems()
    {
        sideBar.Controls.Clear();
        List<GroupMapping> AllGroups = Group_Mapping.ListAllGroups(buildingSelect.SelectedValue);
        if (AllGroups != null)
        {
            Table sideTable = new Table();
            sideTable.ID = "sideTable";

            for (int i = 0; i < AllGroups.Count; i++)
            {

                TableRow wrapper = new TableRow();
                wrapper.ID = "wrapper" + i;

                TableCell cell = new TableCell();
                cell.ID = "cell" + i;
                cell.Style.Add("width", "250px");
                cell.Style.Add("height", "40px");
                cell.Style.Add("border-bottom-style", "groove");

                HtmlGenericControl nameLabel = new HtmlGenericControl("label");
                nameLabel.ID = "nameLabel" + i;
                nameLabel.InnerText = AllGroups[i].GroupId;
                nameLabel.Style.Add("font-size", "large");
                nameLabel.Attributes.Add("class", "clicker");
                nameLabel.Attributes.Add("UID", AllGroups[i].GroupId);
                nameLabel.Attributes.Add("Build", AllGroups[i].Building);
                ListBox1.Items.Add(AllGroups[i].Building);
                nameLabel.Attributes.Add("onclick", "JavaScript:CopyHidden(this)");

                //cell.Controls.Add(check);
                cell.Controls.Add(nameLabel);
                wrapper.Cells.Add(cell);
                sideTable.Rows.Add(wrapper);

            }
            sideBar.Controls.Add(sideTable);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        UNameOfPrinter.InnerText = "";
        generateSideBarItems();

    }

    protected void printBill_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime sampleDate = DateTime.ParseExact(fromDate.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                                 System.Globalization.CultureInfo.InvariantCulture);
            DateTime frDate = new DateTime(sampleDate.Year, sampleDate.Month, sampleDate.Day, 0, 0, 1);
            DateTime toDate = new DateTime(sampleDate.AddDays(7).Year, sampleDate.AddDays(7).Month, sampleDate.AddDays(7).Day, 23, 59, 59);
            calculatePrint(frDate, toDate);
        }
        catch (Exception exp)
        {

        }
    }
    protected void buildingSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        generateSideBarItems();
    }
}
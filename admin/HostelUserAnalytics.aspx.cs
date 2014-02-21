using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using App_Code.HostelAnalytics;

public partial class admin_UserAnalytics : System.Web.UI.Page
{
    double pageViews = 0, uniqueVisitors = 0, totalVisitors = 0,avgTimeSpent=0,totalTimeSpent;
    double energyTipsCount = 0, energyPageCount = 0, averagePageCount = 0, hostelLoginCount=0, hostelHomePage=0,hostelContactUs=0;
    double avgPageViewChange = 0, energyPageViewChange = 0;
    double energy24Hours=0, energyDTD=0, energyDefault=0, energyWeek=0;
    double avg24Hours = 0, avgDTD = 0, avgDefault=0, avgWeek = 0;
    string[,] userFlow;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        if (IsPostBack == false)
        {
            fromDate.Value = DateTime.Today.AddDays(-30).ToString("dd/MM/yyyy hh:mm:ss");
            toDate.Value = DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss");
        }
        try
        {
            DateTime frDate = DateTime.ParseExact(fromDate.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                               System.Globalization.CultureInfo.InvariantCulture);
            DateTime tDate = DateTime.ParseExact(toDate.Value + ",000", "dd/MM/yyyy HH:mm:ss,fff",
                                               System.Globalization.CultureInfo.InvariantCulture);
            if (tDate != null && frDate != null)
            {
                List<HostelAnalyticUI> antList = HostelAnalytic_UI.GetAnalyticListBetweenTime(frDate, tDate);
                timePer.InnerHtml = frDate.ToString("dd MMM yy") + " - " + tDate.ToString("dd MMM yy");
                if (antList != null)
                {
                    //Sequence should be maintained as it is critical for correct value of page views and average time spent of general analytics section

                    PageViews(antList);

                    UserFlow(antList);

                    GeneralInfo(antList);
                }
                else
                {
                    Response.Write("<script>alert('No data for selected time period.');</script>");
                }
            }
        }
        catch (Exception exp)
        {
            Response.Write("<script>alert('No data for selected time period.');</script>");
        }
    }

    protected void GeneralInfo(List<HostelAnalyticUI> antList)
    {
            pageViews = antList.Count-hostelContactUs-hostelLoginCount;
            string prevUser = antList[0].User_id;
            DateTime prevTime = antList[0].dated;
            for (int i = 1; i < antList.Count; i++)
            {
                if (antList[i].User_id != prevUser)
                {
                    uniqueVisitors++;
                    prevUser = antList[i].User_id;
                    if (antList[i].dated.AddMinutes(-10) > prevTime)
                    {
                        totalVisitors++;
                    }
                }
                else
                {
                    if (antList[i].dated.AddMinutes(-10) > prevTime)
                    {
                        totalVisitors++;
                    }
                    else
                    {
                        totalTimeSpent = totalTimeSpent + (antList[i].dated - prevTime).Seconds;
                    }
                } 
                prevTime = antList[i].dated;
            }
            avgTimeSpent = totalTimeSpent/pageViews;
            totalVisitors = totalVisitors + uniqueVisitors;


            general_analytics.Controls.Clear();
            
            HtmlGenericControl h1 = new HtmlGenericControl("h2");
            h1.ID = "genView";
            h1.InnerText = "General Analytics";

            HtmlGenericControl hr1 = new HtmlGenericControl("hr");

            HtmlTable genTable = new HtmlTable();
            HtmlTableRow row1 = new HtmlTableRow();

            HtmlTableCell pageCell = new HtmlTableCell();
            pageCell.InnerHtml = "Page Views: " + pageViews;
            pageCell.Attributes.Add("class", "gen_label");

            HtmlTableCell visitorCell = new HtmlTableCell();
            visitorCell.InnerHtml = "Unique Visitors: " + uniqueVisitors;
            visitorCell.Attributes.Add("class", "gen_label");

            HtmlTableCell visitsCell = new HtmlTableCell();
            visitsCell.InnerHtml = "Number of visits: " + totalVisitors;
            visitsCell.Attributes.Add("class", "gen_label");

            HtmlTableCell timeCell = new HtmlTableCell();
            timeCell.InnerHtml = "Avg Time Spent(page/Sec): " + Math.Round(avgTimeSpent, 2);
            timeCell.Attributes.Add("class", "gen_label");

            row1.Cells.Add(pageCell);
            row1.Cells.Add(visitorCell);
            row1.Cells.Add(visitsCell);
            row1.Cells.Add(timeCell);
            genTable.Rows.Add(row1);
            //general_analytics.Controls.Add(h1);
            //general_analytics.Controls.Add(hr1);
            general_analytics.Controls.Add(genTable);
    }

    protected void PageViews(List<HostelAnalyticUI> antList)
    {
        for (int i = 0; i < antList.Count; i++)
        {
            if(antList[i].Event_id.Contains("My Consumption"))
            {
                energyPageCount++;
                if (antList[i].Event_id.Contains("24 Hours"))
                {
                    energy24Hours++;
                }
                else if (antList[i].Event_id.Contains("Day Consumption"))
                {
                    energyDTD++;
                }                
                else if(antList[i].Event_id.Contains("Week Consumption"))
                {
                    energyWeek++;
                }
                else if (antList[i].Event_id.Contains("View Changed"))
                {
                    energyPageViewChange++;
                }
                else
                {
                    energyDefault++;
                }
            }
            else if(antList[i].Event_id.Contains("My Comparison"))
            {
                averagePageCount++;
                if(antList[i].Event_id.Contains("24 Hours"))
                {
                    avg24Hours++;
                }
                else if (antList[i].Event_id.Contains("Day Consumption"))
                {
                    avgDTD++;
                }
                else if (antList[i].Event_id.Contains("Week Consumption"))
                {
                    avgWeek++;
                }
                else if (antList[i].Event_id.Contains("View Changed"))
                {
                    avgPageViewChange++;
                }
                else
                {
                    avgDefault++;
                }
            }
            else if(antList[i].Event_id.Contains("Energy Saving Tips"))
            {
                energyTipsCount++;
            }
            else if (antList[i].Event_id.Contains("Hostel Login"))
            {
                hostelLoginCount++;
            }
            else if (antList[i].Event_id.Contains("Home Page"))
            {
                hostelHomePage++;
            }
            else if (antList[i].Event_id.Contains("Contact Us"))
            {
                hostelContactUs++;
            }
        }

        page_views.Controls.Clear();

        HtmlGenericControl h1 = new HtmlGenericControl("h2");
        h1.ID = "pageView";
        h1.InnerText = "Page Views";

        HtmlGenericControl hr1 = new HtmlGenericControl("hr");

        HtmlTable pageTable = new HtmlTable();

        HtmlTableRow loginRow = new HtmlTableRow();
        HtmlTableCell loginCell1 = new HtmlTableCell();
        loginCell1.Attributes.Add("class", "page_names");
        loginCell1.InnerHtml = "Successful Login: ";
        HtmlTableCell loginCell2 = new HtmlTableCell();
        loginCell2.Attributes.Add("class", "page_no");
        loginCell2.InnerHtml = hostelLoginCount.ToString();
        loginRow.Cells.Add(loginCell1);
        loginRow.Cells.Add(loginCell2);
        pageTable.Rows.Add(loginRow);

        HtmlTableRow homeRow = new HtmlTableRow();
        HtmlTableCell homeCell1 = new HtmlTableCell();
        homeCell1.Attributes.Add("class", "page_names");
        homeCell1.InnerHtml = "Hostel Home Page: ";
        HtmlTableCell homeCell2 = new HtmlTableCell();
        homeCell2.Attributes.Add("class", "page_no");
        homeCell2.InnerHtml = hostelHomePage.ToString();
        homeRow.Cells.Add(homeCell1);
        homeRow.Cells.Add(homeCell2);
        pageTable.Rows.Add(homeRow);

        HtmlTableRow energyRow = new HtmlTableRow();
        HtmlTableCell energyCell1 = new HtmlTableCell();
        energyCell1.Attributes.Add("class", "page_names");
        energyCell1.InnerHtml = "Energy Consumption Page: ";
        HtmlTableCell energyCell2 = new HtmlTableCell();
        energyCell2.Attributes.Add("class", "page_no");
        energyCell2.InnerHtml = energyPageCount.ToString();
        energyRow.Cells.Add(energyCell1);
        energyRow.Cells.Add(energyCell2);
        pageTable.Rows.Add(energyRow);

        HtmlTableRow energyHelpRow = new HtmlTableRow();
        HtmlTableCell energyHelpCell = new HtmlTableCell();
        energyHelpCell.Attributes.Add("class", "help_cell");
        energyHelpCell.InnerHtml ="Default - "+energyDefault+ "<br /> Day Consumption - " + energyDTD + "<br /> 24 Hours - " + energy24Hours + "<br /> Week Consumption - " + energyWeek + "<br /> View Change - " + energyPageViewChange;
        energyHelpRow.Cells.Add(energyHelpCell);
        pageTable.Rows.Add(energyHelpRow);

        HtmlTableRow averageRow = new HtmlTableRow();
        HtmlTableCell averageCell1 = new HtmlTableCell();
        averageCell1.Attributes.Add("class", "page_names");
        averageCell1.InnerHtml = "Average Comparison Page: ";
        HtmlTableCell averageCell2 = new HtmlTableCell();
        averageCell2.Attributes.Add("class", "page_no");
        averageCell2.InnerHtml = averagePageCount.ToString();
        averageRow.Cells.Add(averageCell1);
        averageRow.Cells.Add(averageCell2);
        pageTable.Rows.Add(averageRow);

        HtmlTableRow averageHelpRow = new HtmlTableRow();
        HtmlTableCell averageHelpCell = new HtmlTableCell();
        averageHelpCell.Attributes.Add("class", "help_cell");
        averageHelpCell.InnerHtml = "Default - "+avgDefault+ "<br /> Day Consumption - " + avgDTD + "<br /> 24 Hours - " + avg24Hours + "<br /> Week Consumption - " + avgWeek + "<br /> View Change - " + avgPageViewChange;
        averageHelpRow.Cells.Add(averageHelpCell);
        pageTable.Rows.Add(averageHelpRow);


        HtmlTableRow trackRow = new HtmlTableRow();
        HtmlTableCell trackCell1 = new HtmlTableCell();
        trackCell1.Attributes.Add("class", "page_names");
        trackCell1.InnerHtml = "Energy Tips Page: ";
        HtmlTableCell trackCell2 = new HtmlTableCell();
        trackCell2.Attributes.Add("class", "page_no");
        trackCell2.InnerHtml = energyTipsCount.ToString();
        trackRow.Cells.Add(trackCell1);
        trackRow.Cells.Add(trackCell2);
        pageTable.Rows.Add(trackRow);

        HtmlTableRow contactRow = new HtmlTableRow();
        HtmlTableCell contactCell1 = new HtmlTableCell();
        contactCell1.Attributes.Add("class", "page_names");
        contactCell1.InnerHtml = "Contact Us Page: ";
        HtmlTableCell contactCell2 = new HtmlTableCell();
        contactCell2.Attributes.Add("class", "page_no");
        contactCell2.InnerHtml = hostelContactUs.ToString();
        contactRow.Cells.Add(contactCell1);
        contactRow.Cells.Add(contactCell2);
        pageTable.Rows.Add(contactRow);

        //page_views.Controls.Add(h1);
        //page_views.Controls.Add(hr1);
        page_views.Controls.Add(pageTable);
    }

    protected void UserFlow(List<HostelAnalyticUI> antList)
    {
        userFlow=new string[6,6];
        for (int l = 0; l < 6; l++)
        {
            for (int k = 0; k < 6; k++)
            {
                userFlow[l, k] = "0";
            }
        }

        userFlow[0, 1] = "Hostel Home Page";
        userFlow[0, 2] = "My Consumption Page";
        userFlow[0, 3] = "My Comparison Page";
        userFlow[0, 4] = "Energy Tips Page";
        userFlow[0, 5] = "Contact Us Page";
        userFlow[0, 0] = "Login Events";        

        string prevUser = antList[0].User_id;
        string prevPage = antList[0].Event_id;
        DateTime prevTime = antList[0].dated;

        int i = 1, j = 1;
        bool newSession = true;
        for(i=1;i<antList.Count;i++)
        {
            if (newSession == true)
            {
                RecordFlowEvent(j, antList[i].Event_id);
                newSession = false;
                j++;
            }
            else
            {
                if (antList[i].User_id != prevUser)
                {
                    j = 1;
                    RecordFlowEvent(j, antList[i].Event_id);
                    prevUser = antList[i].User_id;
                    prevTime = antList[i].dated;
                    prevPage = antList[i].Event_id;
                }
                else if (antList[i].dated.AddMinutes(-10) > prevTime)
                {
                    j = 1;
                    RecordFlowEvent(j, antList[i].Event_id);
                    prevTime = antList[i].dated;
                    prevPage = antList[i].Event_id;
                }
                else if(antList[i].Event_id!=prevPage)
                {
                    j++;
                    if (j < 6)
                    {
                        RecordFlowEvent(j, antList[i].Event_id);
                    }
                    prevPage = antList[i].Event_id;
                    prevTime = antList[i].dated;
                }
            }
        }

        user_flows.Controls.Clear();

        HtmlGenericControl h1 = new HtmlGenericControl("h2");
        h1.ID = "userFlow";
        h1.InnerText = "User Flow";

        HtmlGenericControl hr1 = new HtmlGenericControl("hr");

        Table flowTable = new Table();
        TableHeaderRow th = new TableHeaderRow();
        TableHeaderCell thcell1 = new TableHeaderCell();
        thcell1.Text = "Pages";
        th.Cells.Add(thcell1);
        for (int g = 1; g < 6; g++)
        {
            TableHeaderCell thcell2 = new TableHeaderCell();
            thcell2.Text = "Interaction - "+g;
            th.Cells.Add(thcell2);
        }
        flowTable.Rows.Add(th);
        for (int l = 0; l < 6; l++)
        {
            TableRow flowRow = new TableRow();
            flowRow.ID = "flowRow" + l;
            for (int k = 0; k < 6; k++)
            {
                TableCell flowCell = new TableCell();
                flowCell.ID = "flowCell" + k;
                flowCell.Text = userFlow[k, l];
                if (k == 0)
                {
                    flowCell.Attributes.Add("class", "flow_name");
                }
                else
                {
                    flowCell.Attributes.Add("class", "flow_no");
                }
                flowRow.Cells.Add(flowCell);
            }
            flowTable.Rows.Add(flowRow);
        }

        //user_flows.Controls.Add(h1);
        //user_flows.Controls.Add(hr1);
        user_flows.Controls.Add(flowTable);
    }

    protected void RecordFlowEvent(int interactionNo, string eventId)
    {
            if (eventId.Contains("My Comparison"))
            {
                userFlow[interactionNo, 3] = (Convert.ToInt32(userFlow[interactionNo, 3]) + 1).ToString();
            }
            else if (eventId.Contains("My Consumption"))
            {
                userFlow[interactionNo, 2] = (Convert.ToInt32(userFlow[interactionNo, 2]) + 1).ToString();
            }
            else if (eventId.Contains("Energy Saving Tips"))
            {
                userFlow[interactionNo, 4] = (Convert.ToInt32(userFlow[interactionNo, 4]) + 1).ToString();
            }
            else if (eventId.Contains("Contact Us"))
            {
                userFlow[interactionNo, 5] = (Convert.ToInt32(userFlow[interactionNo, 5]) + 1).ToString();
            }
            else if (eventId.Contains("Hostel Login"))
            {
                userFlow[interactionNo, 0] = (Convert.ToInt32(userFlow[interactionNo, 0]) + 1).ToString();
            }
            else if (eventId.Contains("Home Page"))
            {
                userFlow[interactionNo,1]=(Convert.ToInt32(userFlow[interactionNo,1])+1).ToString();
            }
            else
            {
                userFlow[interactionNo, 0] = (Convert.ToInt32(userFlow[interactionNo, 0]) + 1).ToString();
            }
        
    }

    protected void timeSet_Click(object sender, EventArgs e)
    {

    }
}
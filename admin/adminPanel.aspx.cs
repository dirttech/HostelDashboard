﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_adminPanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();

    }
    protected void CheckLogin()
    {
        if (Session["AdminUserName"] == null || Session["AdminUserName"] == "")
        {
            Response.Redirect("adminLogin.aspx");
        }
        else
        {
            nameTitle.InnerText = "Welcome " + Session["AdminUserName"].ToString();
        }
    }

    protected void logOut_Click(object sender, EventArgs e)
    {
        Session["AdminUserName"] = null;
        Response.Redirect("adminLogin.aspx");
    }
    public void usrInfo_Click(object sender, EventArgs e)
    {
        belowFrame.Attributes.Add("src", "userInfo.aspx");
       
    }
    protected void daily_report_ServerClick(object sender, EventArgs e)
    {
        belowFrame.Attributes.Add("src", "DailyReport.aspx");
    }
    protected void weekly_report_ServerClick(object sender, EventArgs e)
    {
        belowFrame.Attributes.Add("src", "WeeklyReport.aspx");
    }
    protected void hostel_analytics_ServerClick(object sender, EventArgs e)
    {
        belowFrame.Attributes.Add("src", "HostelUserAnalytics.aspx");
    }
    protected void final_report_ServerClick(object sender, EventArgs e)
    {
        belowFrame.Attributes.Add("src", "FinalReport.aspx");
    }
}
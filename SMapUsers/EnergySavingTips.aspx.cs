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

public partial class Energy_Tips : System.Web.UI.Page
{
    public static string username = "";
    public static string building = "";

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

                try
                {
                    WebAnalytics.LoggerService LG = new LoggerService();

                    LoggingEvent logObj = new LoggingEvent();
                    logObj.EventID = "Hostel Energy Saving Tips Page";
                    logObj.UserID = username;
                    bool sts = LG.LogEventHostel(logObj);

                }
                catch (Exception exp)
                {

                }
            }
            catch (Exception exp)
            {
                Response.Write("<script>alert('Sorry! Your Meter is not registered yet.');</script>");
            }
        }        
        

    }
  }
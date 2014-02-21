using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code.HostelLogin;
using App_Code.Utility;
using WebAnalytics;

public partial class _Default : System.Web.UI.Page
{
    protected void CheckLogin()
    {
        if (Session["UserName"] == null || Session["UserName"] == "")
        {

        }
        else
        {
            Response.Redirect("~/SMapUsers/front.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
    }

    protected void loginUser_Click(object sender, EventArgs e)
    {
        UserLogin();
    }

    protected void UserLogin()
    {
        UserLogin usr = UserLogin_S.NewLoging(usrName.Value, pwd.Value,RadioButtonList1.SelectedValue);
        if (usr != null)
        {
            Session["UserName"] = usrName.Value;
            Session["Building"] = RadioButtonList1.SelectedValue;

            try
            {
                WebAnalytics.LoggerService LG = new LoggerService();

                LoggingEvent logObj = new LoggingEvent();
                logObj.EventID = "Hostel Login";
                logObj.UserID = usr.GroupId;
                bool sts = LG.LogEventHostel(logObj);

            }
            catch (Exception exp)
            {

            }
           
            Response.Redirect("~/SMapUsers/front.aspx");
        }
        else
        {
            msg.Text = "Wrong Username/Password";
        }

    }

}


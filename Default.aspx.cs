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
        UserLogin usr = UserLogin_S.NewLoging(usrName.Value, pwd.Value, "Girls Hostel");
        if (usr != null)
        {
            Session["UserName"] = usrName.Value;
            Session["Building"] = "Girls Hostel";
           
            Response.Redirect("~/SMapUsers/front.aspx");
        }
        else
        {
            msg.Text = "Wrong Username/Password";
        }

    }

}


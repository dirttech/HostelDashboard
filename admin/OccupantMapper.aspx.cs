using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code.HostelLogin;

public partial class Occupant_Mapper : System.Web.UI.Page
{
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
    }

    protected void store_Click(object sender, EventArgs e)
    {
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code.HostelLogin;

public partial class CreateUser : System.Web.UI.Page
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
    protected void insertUser_Click(object sender, EventArgs e)
    {
        UserLogin usr = UserLogin_S.Loging(usrName.Text,buildingList.SelectedValue);
        if (usr != null)
        {
            if (usr.GroupId == usrName.Text)
            {
                Label1.Text = "Not available!";
                green.Text = "";
                usrName.Focus();
            }
            else
            {
                UserLogin newUser = new UserLogin();
                newUser.GroupId = usrName.Text;
                newUser.Password = pwd.Text;
                newUser.Building = buildingList.SelectedValue;
                UserLogin_S.InsertUser(newUser);
                green.Text = "Registered!";
                Label1.Text = "";
               
            }
        }

        else
        {
                UserLogin newUser = new UserLogin();
                newUser.GroupId = usrName.Text;
                newUser.Password = pwd.Text;
                newUser.Building = buildingList.SelectedValue;
                bool status = UserLogin_S.InsertUser(newUser);
                if (status == true)
                {
                    green.Text = "Registered!";
                    Label1.Text = "";
                   
                }
               
            
        }


        
    }

    

    protected void store_Click(object sender, EventArgs e)
    {
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code.HostelLogin;
using App_Code.HostelMapping;

public partial class Hostel_Mapper : System.Web.UI.Page
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
        try
        {
            List<int> metIds = meter_ids.Text.Split(',').Select(int.Parse).ToList();
            List<string> roomNos = room_nos.Text.Split(',').ToList();

            GroupMapping grp = new GroupMapping();
            grp.Building = buildingList.SelectedValue;
            grp.GroupId = groupList.SelectedValue;
            grp.GroupName = groupName.Text;
            grp.RoomNos = room_nos.Text;
            grp.OccupantCount = Convert.ToInt32(occupant_count.Text);
            bool gsts=Group_Mapping.InsertGroupMap(grp);

            MeterMapping mtr = new MeterMapping();
            mtr.Building = buildingList.SelectedValue;
            mtr.GroupId = groupList.SelectedValue;
            mtr.MeterId = metIds;
            bool msts = Group_Mapping.InsertMeterMap(mtr);

            OccupantMapping occ = new OccupantMapping();
            occ.GroupId = groupList.SelectedValue;
            occ.RoomNos = roomNos;
            occ.Building = buildingList.SelectedValue;
            occ.OccupantNames = roomNos;
            bool osts = Group_Mapping.InsertOccupantMap(occ);

            if (msts == true && gsts == true && osts==true)
            {
                green.Text = "Success!";
                groupName.Text = ""; room_nos.Text = ""; occupant_count.Text = ""; meter_ids.Text = "";
            }
            else
            {
                green.Text = "Some thing went wrong!";
            }
        }
        catch (Exception exp)
        {
            green.Text = "Some thing went wrong! Check for validation violations!";
        }
    }
    protected void store_Click(object sender, EventArgs e)
    {
       
    }
}
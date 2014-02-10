<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HostelMapper.aspx.cs" Inherits="Hostel_Mapper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/Default.css" />
  
    <title>Create Hostel Mapping</title>
    
<link rel="shortcut icon" href="../images/dashboard_icon.png" />
    <style type="text/css">

    td
{
  font-family:Verdana;
      
}

span
{
 margin:0px;
 padding:0px;
 font-family:Verdana;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">

    <table id="creat" runat="server">
        <tr>
            <td colspan="2">
                  <div class="HeadingLeftTop">
                      Hostel Map</div></td>
            <td>
                &nbsp;</td>
        </tr>
       
        <tr>
            <td>
                Building</td>
                        <td>
                            <asp:DropDownList ID="buildingList" runat="server" AutoPostBack="True">
                                <asp:ListItem Selected="True">Girls Hostel</asp:ListItem>
                                <asp:ListItem>Boys Hostel</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
         <tr>
            <td>
                Group ID</td>
            <td>
                            <asp:DropDownList ID="groupList" DataTextField="group_id" DataValueField="group_id" runat="server" DataSourceID="SqlDataSource1">
                               
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:HostelAppConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:HostelAppConnectionString.ProviderName %>" SelectCommand="SELECT group_id FROM hostel_login WHERE building=@build">
                                <SelectParameters>
                                    <asp:ControlParameter Name="build" ControlID="buildingList" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
        </tr>
         <tr>
            <td>
                Group Name</td>
            <td>
                <asp:TextBox ID="groupName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="groupName" Display="Dynamic" ErrorMessage="*Required" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
                    <tr>
                        <td>
                            Meter ID&#39;s (in commas)</td>
            <td>
                <asp:TextBox ID="meter_ids" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="meter_ids" Display="Dynamic" ErrorMessage="*Required" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Room No&#39;s (in commas)</td>
            <td>
                <asp:TextBox ID="room_nos" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="room_nos" Display="Dynamic" ErrorMessage="*Required" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Number of occupants</td>
            <td>
                <asp:TextBox ID="occupant_count" runat="server" type="number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="occupant_count" Display="Dynamic" ErrorMessage="*Required" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">
                <asp:Button ID="insertUser" runat="server" onclick="insertUser_Click" 
                    Text="Create" class="customButton"  />
                <asp:Label ID="green" runat="server" Font-Bold="False" ForeColor="#009933"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>



    <div>
    
    
    </div>
    </form>
</body>
</html>

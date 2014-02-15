<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OccupantMapper.aspx.cs" Inherits="Occupant_Mapper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/Default.css" />
  
    <title>Occupant Mapper</title>
    
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
                      Map Occupants</div>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Select
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
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" Width="403px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:BoundField DataField="group_id" HeaderText="Group Id" />
                                    <asp:BoundField DataField="room_no" HeaderText="Room No"/>
                                    <asp:TemplateField HeaderText="Occupants">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="500" Text='<%# Bind("occupant_names") %>'></asp:TextBox>
                                        </EditItemTemplate>                                       
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="true" EditText="edit" />
                                </Columns>
                                
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:HostelAppConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:HostelAppConnectionString.ProviderName %>" SelectCommand="SELECT group_id,room_no,occupant_names FROM room_occupant_names WHERE building=@build"
                                 UpdateCommand="UPDATE room_occupant_names SET occupant_names =@occupant_names WHERE building=@build AND room_no=@room_no">
                                <SelectParameters>
                                    <asp:ControlParameter Name="build" ControlID="buildingList" PropertyName="SelectedValue" />                                    
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:ControlParameter Name="build" ControlID="buildingList" PropertyName="SelectedValue" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">
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

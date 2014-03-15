<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminPanel.aspx.cs" Inherits="admin_adminPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Panel</title>
    
<link rel="shortcut icon" href="../images/dashboard_icon.png" />
    <script type="text/javascript">
        function pagleLoad(page1) {
            frames['belowFrame'].location.href = page1;
        }
    
    </script>
     <style type="text/css">
      a
      {
          font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
          text-decoration:none;
          cursor:pointer;
      }
        a:hover
        {
             text-decoration:none;
        }
        
        
  
  </style>
    
<link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/Default.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="navigationTop">
     <a id="user_info" runat="server" onserverclick="usrInfo_Click" >User Info</a>
     <a id="daily_report" runat="server" onserverclick="daily_report_ServerClick">Daily Report</a>
        <a id="weekly_report" runat="server" onserverclick="weekly_report_ServerClick">Weekly Report</a>
        <a id="final_report" runat="server" onserverclick="final_report_ServerClick">Final Report</a>
        <a id="hostel_analytics" runat="server" onserverclick="hostel_analytics_ServerClick">Hostel Analytics</a>
     </div>
       <asp:LinkButton ID="logOut" runat="server"  
        style="color:Black; font-weight:bold;  position:absolute; top:5px; right:20px;" 
        onclick="logOut_Click">LOG OUT</asp:LinkButton>
     <a style="color:Black;  font-size:large;  position:absolute; top:10px; left:20px;" id="nameTitle" runat="server">Welcome</a>
        <iframe id="belowFrame"   runat="server" style="height:900px; width:100%; border:none;padding:0px; margin-top:-4px;" src="userInfo.aspx"></iframe>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Home</title>
    <link rel="Stylesheet" type="text/css" href="Scripts/LoginStyle.css" />
<noscript>     
<body scroll="no" style="text-align: center; overflow:hidden; z-index:1000;"><center><table border="0" style="height: 98%; top:50px; z-index:1000; width: 100%; right: 1%; left: 1%; background: #F5E391;; position: fixed">
                                     <tr><td align="center">
<div style="position: fixed; font-size: 18px; z-index: 2; cursor: help; background: #F5E391; width: 480px; color: black; padding: 5px 5px 5px 5px; border: 1px solid; border-color: #000000; height: auto; text-align: left; left: 15%; top:30%;">
<span style="font: bold 20px Arial; color:#000000; background: #F5E391; vertical-align: top">NO SCRIPT ERROR:</span><br/> 
     Sorry this site will not function properly without the use of scripts.
     The scripts are safe and will not harm your computer in anyway. 
     Adjust your settings to allow scripts for this site and reload the site.</div> 
          </td></tr></table></center> </body>
</noscript>
<script src="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/md5.js"></script>
<script>
    function extractPass() {
        var msg = document.getElementById('<%=pwd.ClientID%>').value;

        var hash = CryptoJS.MD5(msg);

        document.getElementById('<%=psHid.ClientID%>').value = hash;


    }
</script>

    <link rel="shortcut icon" href="images/dashboard_icon.png" />
    <style type="text/css">
        a
        {
            text-decoration:underline;
            color:White;
            
        }
        li#log
        {
          background-color:skyblue;   
        }
        a:hover
        {
            color:orange;
        }
        h1
        {
            color:#446b96;
            font-family:Verdana;
        }
        h2
        {
            color:black;
            font-weight:normal;
            font-family:Verdana;
        }
        h3
        {
            color:white;
            font-weight:bolder;
            font-family:Verdana;
        }
    
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <table  style="background-repeat:no-repeat; -webkit-background-size: cover; background-color:#446b96;
        -moz-background-size: cover;
        -o-background-size: cover; background-size:cover; width:1024px;margin:0 auto; height:680px;margin-top:-8px; padding-bottom:-10px;">
            <tr ><td colspan="2" style="text-align:center;background-color:lightgray;height:30px;">
                    
               <h1>Towards Energy Conservation!</h1>
                <h2>Monitor your realtime Electricity Consumption.</h2>

            </td></tr>
        <tr>
        <td style="width:600px; vertical-align:top; padding-left:40px;text-align:center;">
      <h3>Act Smart and Reduce your electricity bill.</h3>
      <img src="images/login_cover.png" />
        </td>
        <td style="vertical-align:top;"><br />
            <a href="admin/adminLogin.aspx" style="float:right;padding-right:22px; font-family:Verdana;">Admin Login</a>
<div class="LoginContainer" >
  <div class="login" >
    <h1>Login</h1>

    <p><input type="text" name="login" value="" placeholder="Username   " runat="server" id="usrName" /></p>
      <p><input type="password" name="password" value="" placeholder="Password" runat="server" id="pwd" />
      <input runat="server" type="hidden" id="psHid" />
      </p>
     
     
      <p class="submit">
          <asp:Button ID="loginUser" runat="server" Text="Login" 
              onclick="loginUser_Click" OnClientClick="extractPass()" /></p>
              <p><asp:Label ID="msg" Text="" runat="server" style="color:#c4376b; font-weight:bold;"></asp:Label></p>
   
  </div>
 
</div>
</td>
        </tr>      
        </table>
    </form>
</body>
</html>

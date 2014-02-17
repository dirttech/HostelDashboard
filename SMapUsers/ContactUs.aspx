<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Contact_Us" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/Default.css" />
<link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/wheather/weather.css" /> 
<link rel="shortcut icon" href="../images/dashboard_icon.png" />
 
    <script type="text/jscript" src="../Scripts/jquery-1.4.1.min.js"></script>
 
    
    <title>Contact Us</title>
  
    <style type="text/css">
        #weather
        {
              font: 13px 'Open Sans', "Helvetica Neue", Helvetica, Arial, "Lucida Grande", sans-serif;
              background: #0091c2;
              
              height:350px;
              opacity:0.9;
        }
          a
      {
          font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
          text-decoration:none;
      }
        a:hover
        {
             text-decoration:none;
        }
        p, h2, h3
        {
            font-family: Verdana;
        }
        
        h3
        {
             color: #0091c2;
        }
       
        .style1
        {
            margin:0 auto;
            border-left:2px solid gray;
            padding-left:50px;
        }
        li#cont
        {
          background-color:skyblue;   
        }
        td,p,h3
        {
             font-family:Verdana;
             font-weight:normal;
             font-variant:normal;
             font-style:normal;
             line-height:normal;
             
        }
        #optionsDiv
    {
      display:none;  
      text-decoration:none;
      border-radius:2px;
      -webkit-box-shadow: 0px 0px 8px 0px #000000;
-moz-box-shadow: 0px 0px 8px 0px #000000;
box-shadow: 0px 0px 8px 0px #000000;
 text-align:center;
 vertical-align:bottom;
 color:#1a9cc8;
 line-height:20px;
    }

        input
        {
            width:200px;
            height:20px;
        }
        h3
        {
          font-weight:normal;
        }
    </style>


     <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     
</head>
<body>
    <form id="form1" runat="server">
     <div id="navigationTop">
     <a href="front.aspx">Home</a>

     <a href="BarGraph.aspx">My Consumption</a>

     <a href="AverageComparison.aspx" >My Comparison</a>
         
    <a href="EnergySavingTips.aspx">Energy Tips</a>
     <a href="ContactUs.aspx" >Contact Us</a>
     </div>
       
     <a style="color:Black;  font-size:large;  position:absolute; top:10px; left:20px;" id="nameTitle" runat="server">Welcome</a>
     <img src="../images/icons/option-icon.png" height="20px" style="color:Black; font-weight:bold;  position:absolute; top:15px; right:20px; cursor:pointer;" id="options" />
     <div style="position:absolute; right:15px; top:45px; background-color:White; width:150px; height:170px; z-index:10;" id="optionsDiv">
     <br />
    
     <asp:LinkButton ID="logOut" runat="server"  
        
      style=" color:black;"  onclick="logOut_Click">LOG OUT</asp:LinkButton>
     
     <hr />
     </div>
     <br />
     
    <script type="text/javascript">

        $(document).ready(function () {
            $('#options').click(function () {

                $('#optionsDiv').toggle("slow");
            });

        });

    </script>

  
<br />
    <table style="margin: 0 auto;">
        <tr>
            <td style="vertical-align:top; width:400px;  padding-right:50px;">
           
                <h3>Energy Lab</h3>
                <p>3rd Floor, B-Wing, Academic Block<br />IIIT Delhi</p><hr style="display:block;"/><br />
                <p>Okhla Industrial Estate,Phase III<br />
(Near Govind Puri Metro Station)<br />
New Delhi, India - 110020</p>
                
                </td>
            <td style="vertical-align:top;">
                
                
                <table class="style1">
                    <tr>
                        <td colspan="2" style="color: #0091c2;"><br />
                            Got Query? Suggestion? Opinion?<br />
                            Share with us!<br /><hr style=" display:block"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name</td>
                        <td>
                            <asp:TextBox ID="names" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  
                                ControlToValidate="names" ErrorMessage="*" Font-Bold="True" 
                                SetFocusOnError="True" ValidationGroup="cont"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Team</td>
                        <td>
                            <asp:TextBox ID="team" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="cont" runat="server" 
                                ControlToValidate="team" ErrorMessage="*" Font-Bold="True" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Message</td>
                        <td>
                            <asp:TextBox ID="message" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="cont" runat="server" 
                                ControlToValidate="message" ErrorMessage="*" Font-Bold="True" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" >
                            <asp:Label ID="msg" runat="server" ForeColor="#009933" style="width:200px;"></asp:Label>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:HostelAppConnectionString %>" 
                                ProviderName="<%$ ConnectionStrings:HostelAppConnectionString.ProviderName %>" 
                               InsertCommand="Insert INTO suggestions (Name,Team,Message) Values (@name, @team, @messg)">

                                <InsertParameters>
                                    <asp:ControlParameter ControlID="names" Name="name" PropertyName="Text" />
                                    <asp:ControlParameter ControlID="team" Name="team" PropertyName="Text"/>
                                    <asp:ControlParameter ControlID="message" Name="messg" PropertyName="Text"/>
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:Button ID="Button1" runat="server" Text="Share" style="float:right;margin-right:17px;" class="customButton"
                                Width="100px" Height="35px" onclick="Button1_Click" ValidationGroup="cont" />
                        </td>
                    </tr>
                </table>
                
                
                </td>
        </tr>
    </table>

    </form>
</body>
</html>

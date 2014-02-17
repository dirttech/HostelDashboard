<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AverageComparison.aspx.cs" Inherits="AverageComparison" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">


 <script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.customSelect.js"></script>
   <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/css/bootstrap-combined.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Scripts/calender/bootstrap-datetimepicker.min.css" />
<style type="text/css">

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
     .page_names
        {
            color:#0091c2;
            font-size:large;
            font-family:Verdana;
           padding-left:10px;
           line-height:normal;
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
    .topButtons
    {
        vertical-align:middle;
    }
    .topButtons>a
    {
        color:white;
        padding:5px;
        margin-right:10px;
        font-size:medium;
        background-color: black;
        border-radius:5px;
    }
    .topButtons>a:hover
    {
        background-color:white;
        color:black;
        text-decoration:none;
        padding:5px;
        margin-right:10px;
        font-size:medium;
        border-radius:5px;
    }
    .topButtons-Selected
    {
        background-color: white !important;
        color: black !important;
        text-decoration: none;
        padding: 5px;
        margin-right: 10px;
        font-size: medium;
        border-radius: 5px;
    }
    .top-bar
    {
        background-color:#0099CC;
        border-radius:5px;
        vertical-align:middle;
    }
    .topper
    {
        float: left;
        color: gold;
        vertical-align: middle;
        line-height: 35px;
        font-size: large;
        padding-left: 10px;
        font-family:cursive;
    }
</style>

    <title>My Comparison</title>
    <link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/Default.css" />
    <link rel="shortcut icon" href="../images/dashboard_icon.png" />
      
   <script type="text/javascript">
       jQuery(document).ready(function ($) {
           $('#options').click(function () {

               $('#optionsDiv').toggle("slow");
           });

           $('select.styled').customSelect();
           /* -OR- set a custom class name for the stylable element */
           //            $('.mySelectBoxClass').customSelect({ customClass: 'myOwnClassName' });
       });
</script>
    <script type="text/javascript">
   
        var en=<%=new JavaScriptSerializer().Serialize(energyValues) %>;
        var gr=<%=new JavaScriptSerializer().Serialize(groupNames) %>;
   
      jQuery(document).ready(function ($) {
            

            $('#container').highcharts({
            chart: {
                type: 'bar',
                marginRight: 130,
                marginBottom: 50
            },
            title: {
                text: 'Energy Consumption Comparisons',
                x: -20 //center
            },
            subtitle: {
                text: '',
                x: -20
            },
            xAxis: {
             
               categories: gr
               
                   },
            yAxis: {
                title: {
                    
                    text: 'Energy(Kilo-Watt Hrs)'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: 'KWHr'
            },
            legend: {
                layout: 'vertical',
                align: 'right',
               
                verticalAlign: 'top',
                x: -10,
                y: 100,
                borderWidth: 0
            },
             plotOptions: {                
                bar: {
                         dataLabels: {
                             enabled: true
                        }                     
                 },
                series: {
                    stacking: $('#hiddenPlotType').attr('value')
                }
            },
            series: [
            {
                name: 'Energy Consumption',
                data: en
            }
            
            ]
        });
    });
   

    
    </script>
</head>
<body>
<script src="../Scripts/high_charts/js/highcharts.js"></script>
    <script src="../Scripts/high_charts/js/modules/exporting.js"></script>
    <form id="form1" runat="server">
        
            <div id="timeDiv" runat="server" style=" display:none; position:absolute; left:550px; top:150px;-moz-border-radius:8px;	-webkit-border-radius:8px;	border-radius:8px; 
                           box-shadow: 0px 0px 10px rgba(0,0,0,0.2); width:500px; height:170px; background-color:#0d96c5; opacity:0.99; z-index:12;">
                        <img id="closeButton" runat="server" height="30" style="position:absolute; height:30px; top:5px; right:5px; cursor:pointer;" src="~/images/closeButton.png" alt="close" />
                        <br />
                        
                        <table style="color:white;" >
                            <tr>
                                <td class="page_names" style="color:black; line-height:normal;">&nbsp;&nbsp;&nbsp;
                                    Select day!
                                    <hr style="border-color:black;"/>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;From:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="datetimepicker1" class="input-append date">
                                        <input type="text" id="date1" runat="server" style=" margin-left:30px;height:20px;"/> 
                                        <span class="add-on">
                                        </span>
                                    </div>       
                                </td>
                                <td>
                                     <asp:Button ID="timeSet" runat="server" Text="Set Time" class="customButton" style="margin-left:10px; margin-top:-2px;" OnClick="timeSet_Click"/>      
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td align="right">
                                  
                                </td>
                            </tr>
                        </table>
                        
             </div>

               <div id="weekDiv" runat="server" style=" display:none; position:absolute; left:550px; top:150px;-moz-border-radius:8px;	-webkit-border-radius:8px;	border-radius:8px; 
                           box-shadow: 0px 0px 10px rgba(0,0,0,0.2); width:500px; height:170px; background-color:#0d96c5; opacity:0.99; z-index:12;">
                        <img id="close2" runat="server" height="30" style="position:absolute; height:30px; top:5px; right:5px; cursor:pointer;" src="~/images/closeButton.png" alt="close" />
                        <br />
                        
                        <table style="color:white;" >
                            <tr>
                                <td class="page_names" style="color:black; line-height:normal;">&nbsp;&nbsp;&nbsp;
                                    Select start date!
                                    <hr style="border-color:black;"/>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;From:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="datetimepicker2" class="input-append date">
                                        <input type="text" id="date2" runat="server" style=" margin-left:30px;height:20px;"/> 
                                        <span class="add-on">
                                        </span>
                                    </div>       
                                </td>
                                <td>
                                     <asp:Button ID="selectWeek" runat="server" Text="Set Time" class="customButton" style="margin-left:10px; margin-top:-2px;" OnClick="selectWeek_Click"/>      
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td align="right">
                                  
                                </td>
                            </tr>
                        </table>
                        
             </div>

     <div id="navigationTop">
     <a href="front.aspx">Home</a>
 
     <a href="BarGraph.aspx">My Consumption</a>

     <a href="AverageComparison.aspx" >My Comparison</a>
         
    <a href="EnergySavingTips.aspx">Energy Tips</a>
     <a href="ContactUs.aspx" >Contact Us</a>
     </div>
      <img src="../images/icons/option-icon.png" height="20px" style=" height:20px;color:Black; font-weight:bold;  position:absolute; top:15px; right:20px; cursor:pointer;" id="options" />
     <div style="position:absolute; right:15px; top:45px; background-color:White; width:150px; height:170px; z-index:10;" id="optionsDiv">
     <br /> 
 
     <asp:LinkButton ID="logOut" runat="server"  
        
      style=" color:black;"  onclick="logOut_Click">LOG OUT</asp:LinkButton>
     
     <hr />
     </div>
     <a style="color:Black;  font-size:large;  position:absolute; top:10px; left:20px;" id="nameTitle" runat="server">Welcome</a>     
     
     <br />
    <div>
   <div style="text-align:right;margin:0 auto;width:900px;" class="top-bar">
    <asp:Label runat="server" ID="messg" class="topper"></asp:Label>
    <div style="vertical-align:middle;display:inline-block;margin-bottom:8px;">
    <span class="topButtons">
     <asp:LinkButton ID="LastTFHours" runat="server" OnClick="LastTFHours_Click"  class="topButtons-Selected">Last 24 Hours</asp:LinkButton>
   </span ><span class="topButtons">
    <asp:LinkButton ID="DayConsumption" runat="server" OnClick="DayConsumption_Click" OnClientClick="SelectDate() return false">Day Consumption</asp:LinkButton>
    </span><span class="topButtons">
    <asp:LinkButton ID="WeekConsumption" runat="server" OnClick="WeekConsumption_Click" OnClientClick="SelectDate() return false">Week Consumption</asp:LinkButton>
        </span>
        </div>
    <asp:DropDownList ID="meterTypeList" runat="server" AutoPostBack="True" 
        class="styled" onselectedindexchanged="meterTypeList_SelectedIndexChanged">
        <asp:ListItem Value="1">Total Usage</asp:ListItem>
      
    </asp:DropDownList>
   

</div>
    
     <div id="container" style="width: 900px; height: 550px; margin: 0 auto"></div>
  
    </div>


    <script type="text/javascript"
     src="../Scripts/calender/jquery.min.js">
    </script> 
    <script type="text/javascript"
     src="../Scripts/calender/bootstrap.min.js">
    </script>
    <script type="text/javascript"
     src="../Scripts/calender/bootstrap-datetimepicker.min.js">
    </script>
    <script type="text/javascript"
     src="../Scripts/calender/bootstrap-datetimepicker.pt-BR.js">
    </script>
   
             <script type="text/javascript">
                 jQuery(document).ready(function ($) {
                     $('#datetimepicker1').datetimepicker({
                         format: 'dd/MM/yyyy hh:mm:ss',
                         pick12HourFormat: true
                     });
                     $('#datetimepicker2').datetimepicker({
                         format: 'dd/MM/yyyy hh:mm:ss',
                         pick12HourFormat: true
                     });
                    
                     $('#DayConsumption').click(function (e) {
                         var offset = $(this).offset();
                         $("#timeDiv").show("drop");
                         e.preventDefault();
                     });
                     $('#WeekConsumption').click(function (e) {
                         var offset = $(this).offset();
                         $("#weekDiv").show("drop");
                         e.preventDefault();
                     });
                     $('#closeButton').click(function () {
                         $("#timeDiv").hide("drop");
                     });
                     $('#close2').click(function () {
                         $("#weekDiv").hide("drop");
                     });
                     $('#selectWeek').click(function () {
                         $("#weekDiv").hide("drop");
                     });
                 });
           </script>   
    
    </form>
</body>
</html>

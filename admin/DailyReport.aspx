<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyReport.aspx.cs" Inherits="DailyReport" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <script type="text/javascript">
     function CopyHidden(ths) {
         var hid = ths.getAttribute("UID");
         document.getElementById('<%=uid.ClientID%>').setAttribute("value", hid);
         var tp = ths.innerText;
         document.getElementById('<%=hidName.ClientID%>').setAttribute("value", tp);
         return false;
     }
     function AllSelectedCopy() {
         
     }
     function printDiv(divID) {
         window.print();
     }
    </script>
       <link rel="Stylesheet" type="text/css" media="print" href="../Scripts/printBill.css" />
  <link rel="stylesheet" type="text/css" media="screen" href="../Scripts/calender/bootstrap-datetimepicker.min.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/css/bootstrap-combined.min.css" rel="stylesheet" / >
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
                  credits:{
                      enabled:false
                  },
                  exporting: {
                      buttons: {
                          contextButtons: {
                              enabled: false,
                              menuItems: null
                          }
                      },
                      enabled: false
                  },
                  title: {
                      text: '',
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
                    
                          text: 'Electricity Consumption(Kilo-Watt Hrs)'
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
                      enabled:false,
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
<link rel="shortcut icon" href="../images/dashboard_icon.png" />

  <link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/Default.css" />
  <link rel="Stylesheet" type="text/css" media="print" href="../Scripts/Default.css" />
 
   <title>Print Bills</title>
    <style type="text/css" media="all">
        .SideUpperLabel
        {
          font-family:@Arial Unicode MS;
          font-size:large;
          color:Navy !important;
          -webkit-print-color-adjust: exact; 
          padding-left:5px;
          padding-top:4px;
          font-weight:normal;   
        }
        .customSelect
        {
            height: 30px;
            line-height: normal;
            padding-top: 2px;
            width: 250px;
        }
      
         td
        {
            font-family:Verdana; 
            height:25px;
        }
        .tabStyle
        {
          min-width:700px !important;
           -webkit-print-color-adjust: exact; 
          max-width:1000px !important;
        }
        .tplbl
        {
         background-color:#FFFF99 !important;
         -webkit-print-color-adjust: exact;  
        }
        .reportHead
        {
            text-transform:uppercase;
            font-size:xx-large;
        }
        .normalSpanRight
        {
            font-size:medium;
            float:right;
        }
        .normalSpanLeft
        {
            font-size:medium;
        }
        .specialSpanRight
        {
            font-size:medium;
            float:right;
        }
        .specialSpanLeft
        {
            font-size:medium;
        }
        .calculations
        {
             width:1000px;
             z-index:1; 
        }
        .chkbox
        {
           margin-left:70px;
        }
      /*   @media print
        {
            hr {page-break-before:always}
            
        }
        */
    </style>

    
</head>
<body>
    <script src="../Scripts/high_charts/js/highcharts.js"></script>
    <script src="../Scripts/high_charts/js/modules/exporting.js"></script>
 
    <form id="form1" runat="server">
    <div class="SideBar" style="height:500px;">
    <div class="HeadingLeftTop" style="opacity:0.9; width:93.3%">
     <label id="Heading" runat="server" style=" font-size:x-large;">
     
        <asp:RadioButtonList ID="printMode" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem  Value="all">All&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="selected">Selected&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="single" Selected="True">Individual</asp:ListItem>
        </asp:RadioButtonList>
     
     </label>    
    </div>
      <div id="sideBar" runat="server" style="background-color:skyblue; padding-left:20px; ">
      
    
    </div>
    </div>
    <input type="hidden" ID="selectedBoxes" runat="server" style="" />
    <input id="hidName" type="hidden" runat="server" value="LNT"/>
<input id="uid" type="hidden" runat="server" />
    <asp:ListBox ID="ListBox1" runat="server" Visible="false"></asp:ListBox>

    <div id="hoverDiv"><br />
    <table class="printHide">

    <tr><td style="color:White; font-size:x-large;">Report Interval</td><td>
         <div id="datetimepicker1" class="input-append date">
                  <input type="text" id="fromDate" runat="server" style=" margin-left:30px;height:20px;"/>
                  <span class="add-on">
                    <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                  </span>
                </div>       
                </td><td>
                    &nbsp;&nbsp;
                <asp:DropDownList runat="server" class="customSelect" ID="buildingSelect" AutoPostBack="True" OnSelectedIndexChanged="buildingSelect_SelectedIndexChanged">
                    <asp:ListItem>Girls Hostel</asp:ListItem>
                    <asp:ListItem>Boys Hostel</asp:ListItem>                   
                </asp:DropDownList>    
                
                </td>  
    <td><input type="button" id="prnt" onclick="printDiv('reportBody');" value="Print" class="customButton" style="margin-top:-4px;margin-left:10px;" /></td>
            </tr></table>     
    </div>
    <div class="HeadingLeftTop" style="opacity:0.9; width:200px; position:absolute; right:300px;display:none; ">
       
      <label id="Label1" runat="server" style=" font-size:x-large;">Current Bill</label> 
     <label id="UNameOfPrinter" runat="server" style=" font-size:large;"></label>  
       </div>
         
    <div id="reportBody" runat="server">
        <table class="calculations">
            <tr>
                <td>
                    <span class="reportHead" id="reportHead" runat="server"></span>
                </td>
                <td><span class="normalSpanRight" id="group_id" runat="server"></span></td>
            </tr>
            <tr>
                <td colspan="2"><hr /></td>
            </tr>
            <tr>
                <td><span id="group_building" runat="server" class="normalSpanLeft"></span></td>
                <td><span id="dated" runat="server" class="normalSpanRight"></span></td>
            </tr>
            <tr>
                <td><span id="group_wing" runat="server" class="normalSpanLeft"></span></td>
                <td><span id="dayed" runat="server" class="normalSpanRight"></span></td>
            </tr>
           
            <tr>
                <td colspan="2"><hr /></td>
            </tr>
            <tr>
                <td><span id="prev_day" runat="server" class="specialSpanLeft"></span></td>
                <td><span id="elec_units" runat="server" class="specialSpanRight"></span></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><span id="elec_cost" runat="server" class="specialSpanRight"></span></td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="container" style="width:800px;margin:0 auto;">

                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
     
     <div id="printOptions" runat="server" style=" display:none; position:absolute; left:700px; top:120px;-moz-border-radius:8px;	-webkit-border-radius:8px;	
        border-radius:8px; box-shadow: 0px 0px 10px rgba(0,0,0,0.2); width:290px; height:170px;padding-left:10px; background-color:green; opacity:0.5; z-index:100;">

                     <h4 style="color:white;">
                     Select report type
                         </h4>
                         <hr />                         
                         <asp:DropDownList ID="reportType" runat="server" CssClass="customSelect">
                             <asp:ListItem>Daily Report</asp:ListItem>
                             <asp:ListItem>Public Report</asp:ListItem>
                         </asp:DropDownList>    
                               <asp:Button ID="printBill" runat="server" Text="Calculate" class="customButton" style=" position:relative; 
                                    display:block; top: -2px; left: 152px;" onclick="printBill_Click" />
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
    <script type="text/jscript">
        jQuery(document).ready(function ($) {
            $('#datetimepicker1').datetimepicker({
                format: 'dd/MM/yyyy hh:mm:ss',
                pick12HourFormat: true
            });         

            $('.clicker').hover(function () {
                $('.clicker').css('font-size', 'large');
                $(this).css('font-size', 'x-large');
            });
            $('.clicker').click(function () {
                var offset = $(this).offset();
                $("#printOptions").hide();
                $("#printOptions").fadeIn("drop");
                //$("#printOptions").offset({ top: offset.top - 4, left: offset.left + 145 });
            });
        });
           </script>    

    </div>
    </form>
</body>
</html>

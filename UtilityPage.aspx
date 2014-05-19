<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UtilityPage.aspx.cs" Inherits="UtilityPage" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="images/zenatix_logo_title.png" type="image/x-icon">
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css">

<!-- Latest compiled and minified JavaScript -->
<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
    <style type="text/css">
       
         body
        {
           margin:8px;
        }
        table
        {
            border-spacing: 2px;
            border-collapse: initial;
        }
        footer
        {
            -webkit-box-sizing: initial;
        }
        .upperboard
        {
            padding-left:15px;
            text-align:left;
            
        }
        .tabclass
        {
            text-transform:capitalize;
            font-family:Arial;
            width:930px;
            text-align:left;
            padding-left:40px;
        }
            .tabclass > table
            {
                width:900px;
                padding-left:20px;
            }
        .chartclass
        {
            width:930px;
            height:100%;
        }
        td
        {
            width:400px;
        }
    </style>
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
    
      <script type="text/javascript">
        
          var plottype = <%=new JavaScriptSerializer().Serialize(plottype) %>;
          if(plottype == "kwhr")
          {
              var timeSeries=<%=new JavaScriptSerializer().Serialize(names) %>;
     
              var en=<%=new JavaScriptSerializer().Serialize(prices) %>;

              var colorOptions=['#000A00','#200200','#FAEBD7','#7FFFD4','#8A2BE2','#D2691E','#6495ED','#556B2F','#8FBC8F','#FF1493','#DAA520','#800080',];
              var colors = [];
              //timeSeries = timeSeries.filter(function(e){return e});
              for(var i=0;i<timeSeries.length;i++)
              {
                  if(timeSeries[i]!=null)
                  {
                      for(var b=0;b<timeSeries[i].length;b++)
                      {
                          colors.push(colorOptions[i]);
                          timeSeries[i][b] = timeSeries[i][b].slice(-4);
                      }
                  }
              }
              en = en.filter(function(e){return e});
          
              jQuery(document).ready(function ($) {
            

                  $('#container_kwhr').highcharts({
                      chart: {
                          type: 'column',
                          marginRight: 30,
                          marginBottom: 50,
              
                      },
                      title: {
                          text: 'Electricity Consumption',
                          x: -20 //center
                      },
                      subtitle: {
                          text: '',
                          x: -20
                      },
                      xAxis: {
             
                          categories:[{
                              name: 'January',
                              categories: timeSeries[0]                      
                          },{
                              name: 'February',
                              categories: timeSeries[1]
                          },{
                              name: 'March',
                              categories: timeSeries[2]
                          },{
                              name: 'April',
                              categories: timeSeries[3]
                          },{
                              name: 'May',
                              categories: timeSeries[4]
                          },{
                              name: 'June',
                              categories: timeSeries[5]
                          },{
                              name: 'July',
                              categories: timeSeries[6]
                          },{
                              name: 'August',
                              categories: timeSeries[7]
                          },{
                              name: 'September',
                              categories: timeSeries[8]
                          },{
                              name: 'October',
                              categories: timeSeries[9]
                          },{
                              name: 'November',
                              categories: timeSeries[10]
                          },
                          {
                              name: 'December',
                              categories: timeSeries[11]
                          }]
                      },
                      yAxis: {
                          title: {
                    
                              text: 'Energy (KWHrs)'
                          },
                          plotLines: [{
                              value: 0,
                              width: 1
                          }]
                      },
                      tooltip: {
                          valueSuffix: ' KWHr'
                      },
                      legend: {
                          enabled: false,
                          layout: 'horizontal',
                          align: 'right',
                          verticalAlign: 'top',
                          x: -10,
                          y: 100,
                          borderWidth: 0
                      },
                      plotOptions: {
                         
                          column:{
                              dataLabels: {
                                  enabled: false,
                                  color:['#000000','#AAAAAA']
                              },
                              grouping: true,
                              groupPadding: 0,
                              colorByPoint: true,
                              colors:colors
                          }
                      },
                     
                      credits:false,
                      series: [{
                          data: en[0].concat(en[1]).concat(en[2]).concat(en[3]).concat(en[4]).concat(en[5]).concat(en[6]).concat(en[7]).concat(en[8]).concat(en[9]).concat(en[10]).concat(en[11])
                      }]
                  });
              });
   
          }
          else if(plottype=="rs")
          {
              var timeSeries=<%=new JavaScriptSerializer().Serialize(months) %>;
     
              var en=<%=new JavaScriptSerializer().Serialize(price) %>;
              //timeSeries = timeSeries.filter(function(e){return e});
          
              en = en.filter(function(e){return e});
          
              jQuery(document).ready(function ($) {
            

                  $('#container_kwhr').highcharts({
                      chart: {
                          type: 'column',
                          marginRight: 30,
                          marginBottom: 70,
              
                      },
                      title: {
                          text: 'Electricity Bill',
                          x: -20 //center
                      },
                      subtitle: {
                          text: '',
                          x: -20
                      },
                      xAxis: {
             
                          categories:[{
                              categories: timeSeries                    
                         
                          }]
                      },
                      yAxis: {
                          title: {
                    
                              text: 'Bill (Rs)'
                          },
                          plotLines: [{
                              value: 0,
                              width: 1
                          }]
                      },
                      tooltip: {
                          valueSuffix: ' Rs'
                      },
                      legend: {
                          enabled: false,
                          layout: 'horizontal',
                          align: 'right',
                          verticalAlign: 'top',
                          x: -10,
                          y: 100,
                          borderWidth: 0
                      },
                      plotOptions: {                          
                          column:{
                              dataLabels: {
                                  enabled: true,
                                  color:'black'
                              },
                              grouping: true,
                              groupPadding: 0,
                              colorByPoint: true
                          }
                      },
                      colors: [
                            
                                '#B5CA92'
                      ],
                      credits:false,
                      series: [{
                          name: 'Bill Price',
                          data: en
                      }]
                  });
              });
   
          }
          else
          {

          }
    
    </script>
     <link rel="stylesheet" type="text/css" href="scripts/DefaultZ.css" />
    <title>Utility Page</title>
</head>
<body>

    <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/2.2.4/modules/exporting.js"></script>
    <script src="scripts/grouped-categories.js"></script>
    <form id="form1" runat="server" class="body">
           <table class="top-row">
            <tr>
                <td rowspan="2">
                    <a href="index.html">
                        <img src="images/zenatix_logo.png" alt="Zenetix" class="top-logo" style="margin-left: 0px;" /></a>
                </td>
                <td style="text-align: right;">
                    <input type="search" name="Search" class="search-input" placeholder="Search">
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 46%; max-width: 100%;">
                    <ul class="top-menu">
                        <li><a href="index.html">Home</a></li>
                        <li><a href="how_it_works.html">How it Works</a></li>
                        <li><a href="how_it_helps.html">How it Helps</a></li>
                        <li><a href="about_us.html">About Us</a></li>
                        <li><a href="contact_us.html">Contact Us</a></li>
                    </ul>
                </td>
            </tr>
        </table>
        <hr />
         
        <div class="upperboard">
            <span class="blue-heading" style="">Plot Your Bill Details</span><br />
        <asp:DropDownList ID="utilityList" runat="server" class="custom-textbox" Height="29px" AutoPostBack="True" OnSelectedIndexChanged="utilityList_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="none">Select Utility</asp:ListItem>
            <asp:ListItem Value="bses_delhi">BSES Delhi</asp:ListItem>
            <asp:ListItem Value="hbvn">Haryana Bijli Vitran Nigam</asp:ListItem>
            <asp:ListItem Value="spanco">SPANCO Nagpur</asp:ListItem>
            <asp:ListItem Value="tangedco">TANGEDCO TamilNadu</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="locality" runat="server" class="custom-textbox" style="height:29px" Visible="false">
        
            </asp:DropDownList>
        <asp:TextBox ID="cust_no" runat="server" class="custom-textbox" style="height:29px" placeholder="  Enter Customer ID"></asp:TextBox>

            &nbsp;&nbsp;
        <asp:Button ID="find_cust" runat="server" OnClick="find_cust_Click" Text="Find" class="custom-button" style="float:none;" />
       <br />
        </div>
        <br /> <hr /><br />
        <span class="green-heading" style="float:left;padding-left:20px;">Your Details: </span><br />
    <div id="tableContainer" runat="server" class="tabclass"></div>
        <br />
        <hr /><br />
        <div class="btn-group" style="float:left;padding-left:20px;">
            
            <asp:Button runat="server" ID="prev_bill" class="btn btn-default" Text="Previous Bill" OnClick="prev_bill_Click" />
            <asp:Button runat="server" ID="prev_consum" class="btn btn-default" Text="Previous Consumption" OnClick="prev_consum_Click"/>
        </div>
        <br />
    <div id="container_kwhr" class="chartclass" runat="server"></div>
    <br />
    </form>
     <footer>Copyright © 2014 Zenatix. All rights reserved.</footer>
</body>
</html>

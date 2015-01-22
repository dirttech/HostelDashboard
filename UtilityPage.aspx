﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UtilityPage.aspx.cs" Inherits="UtilityPage" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="shortcut icon" href="images/zenatix_logo_title.png" type="image/x-icon">

<script src="Scripts/jquery-1.4.1.min.js"></script>
<link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css">
<script src="http://netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
    <style type="text/css">
        .top-row
        {
            visibility:hidden;
            height:0px;
            display:none;
        }
        footer
        {
            visibility:hidden;
        }
         body
        {
             background-color:#4e5d6c !important;
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
            width:100%;
            text-align:left;
        }
            .tabclass > table
            {
                width:100%;
                padding-left:20px;
            }
        .chartclass
        {
            width:95%;
            height:100%;
        }
        td
        {
            width:400px;
        }
    </style>
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
    
`      <script type="text/javascript">
        
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
    <form id="form1" runat="server" class="container" style="background-color:white;">
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
         
        <div class="upperboard">
            <!--<span class="blue-heading" style="">Plot Your Bill Details</span><br />-->
        <asp:DropDownList ID="utilityList" runat="server" class="custom-textbox" Height="29px" AutoPostBack="True" OnSelectedIndexChanged="utilityList_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="none">Select Utility</asp:ListItem>
            <asp:ListItem Value="bses_delhi">BSES Delhi</asp:ListItem>
            <asp:ListItem Value="hbvn">Haryana Bijli Vitran Nigam</asp:ListItem>
            <asp:ListItem Value="spanco">SPANCO Nagpur</asp:ListItem>
            <asp:ListItem Value="best_mumbai">BEST Mumbai</asp:ListItem>
            <asp:ListItem Value="mahavitran">Mahavitran Maharashtra</asp:ListItem>
            <asp:ListItem Value="tangedco">TANGEDCO TamilNadu</asp:ListItem>
            </asp:DropDownList>
            <select id="mahavitranList" runat="server" class="custom-textbox" style="height:29px" Visible="false">
                    <option value="0000" selected="selected">Select Locality</option>
                    <option value="0019">0019 VASAI RD. URBAN S/DN</option>
	                <option value="0027">0027 VASAI RD. EAST S/DN.</option>
	                <option value="0035">0035 NALASOPARA WEST S/DN</option>
	                <option value="0043">0043 VIRAR EAST S/DN.</option>
	                <option value="0051">0051 THANE (AG)</option>
	                <option value="0078">0078 BHIWANDI II (AG)</option>
	                <option value="0233">0233 CHOPDA I S/DN</option>
	                <option value="0235">0235 ERANDOL S/DIVN.</option>
	                <option value="0240">0240 CHALISGAON-U</option>
	                <option value="0250">0250 NASIK067 BHP</option>
	                <option value="0251">0251 NASIK URBAN DN. I</option>
	                <option value="0306">0306 ACHALPUR CAMP S/DN.</option>
	                <option value="0311">0311 PANVEL</option>
	                <option value="0329">0329 PANVEL I (BHINGARI)</option>
	                <option value="0330">0330 GADCHANDUR S/DN.</option>
	                <option value="0337">0337 URAN</option>
	                <option value="0340">0340 WADSA S/DN.</option>
	                <option value="0343">0343 ALLAPALLI S/DN.</option>
	                <option value="0345">0345 KALAOMBOLI SUB DIVN</option>
	                <option value="0353">0353 NARKHED S DIV</option>
	                <option value="0354">0354 MOHAPPA (R) S/DN.</option>
	                <option value="0356">0356 RAMTEKE S/DN.</option>
	                <option value="0357">0357 UMRER S/DN.</option>
	                <option value="0358">0358 BHIVAPURA S/DN.</option>
	                <option value="0359">0359 MOUDA S/DN.</option>
	                <option value="0360">0360 KANHAN S/DN.</option>
	                <option value="0361">0361 KUHI (R) S/DN.</option>
	                <option value="0363">0363 KALMESHWAR S/DN.</option>
	                <option value="0364">0364 KHAPARKHEDA S/DN.</option>
	                <option value="0365">0365 SAONER</option>
	                <option value="0366">0366 KHAPA S/DN.</option>
	                <option value="0367">0367 NAGPUR (R) CIRCLE</option>
	                <option value="0371">0371 WARDHA S/DN. R-I</option>
	                <option value="0372">0372 WARDHA S/DN. R-II</option>
	                <option value="0373">0373 HINGHANGHAT S/DN.</option>
	                <option value="0377">0377 ASHTI SUB DN</option>
	                <option value="0379">0379 KARANJA SUB DN</option>
	                <option value="0380">0380 KHARANGANA (R) S/DN.</option>
	                <option value="0383">0383 MOHADI S/DN.</option>
	                <option value="0384">0384 BHANDARA S/DN.</option>
	                <option value="0385">0385 LAKHANDUR S/DN.</option>
	                <option value="0386">0386 PAONI S/DN.</option>
	                <option value="0388">0388 AMGAON S/DN.</option>
	                <option value="0389">0389 DEORI S/DN.</option>
	                <option value="0390">0390 GONDIA (R) S/DN.</option>
	                <option value="0391">0391 SAKOLI S/DN.</option>
	                <option value="0392">0392 TIRORA S/DN.</option>
	                <option value="0400">0400 NASIK URBAN DIVISION</option>
	                <option value="0418">0418 KALWAN S/DIV</option>
	                <option value="0426">0426 MALEGAON (U) S/DN I</option>
	                <option value="0434">0434 DABHADI (R) SUBDN</option>
	                <option value="0442">0442 LASALGAON (R) S/DIV.</option>
	                <option value="0451">0451 NASIK (R) SUBDN</option>
	                <option value="0467">0467 CC O DHULE</option>
	                <option value="0469">0469 OZAR (R) SUBDN</option>
	                <option value="0477">0477 SINNAR-II (R) SUBDN</option>
	                <option value="0485">0485 MALEGAON CC(O)</option>
	                <option value="0493">0493 CCO (R) SDN NSKRD</option>
	                <option value="0515">0515 DHULE (U-I)</option>
	                <option value="0523">0523 SAKRI S/DN</option>
	                <option value="0531">0531 PARSIONI S/DN.</option>
	                <option value="0540">0540 SHAHADA - II S/DN</option>
	                <option value="0546">0546 NAVAPUR S/DN.</option>
	                <option value="0558">0558 SHINDKHEDA</option>
	                <option value="0560">0560 JALALKHEDA</option>
	                <option value="0561">0561 SAWARGAON S/DN.</option>
	                <option value="0562">0562 BUTIBORI S/DN.</option>
	                <option value="0565">0565 MORGAON ARJUNI S/DN.</option>
	                <option value="0566">0566 NARDANA</option>
	                <option value="0574">0574 DHULE (R)</option>
	                <option value="0580">0580 CHALISGAON R-III</option>
	                <option value="0582">0582 PIMPALNER</option>
	                <option value="0583">0583 GONDPIPRI S/DN.</option>
	                <option value="0590">0590 NAGAR DEOLA S/DN.</option>
	                <option value="0591">0591 NANDURBAR-U</option>
	                <option value="0601">0601 CHANDWAD DIVISION</option>
	                <option value="0602">0602 MANMAD RURAL DIVISIO</option>
	                <option value="0603">0603 MALEGAON URBAN DIVIS</option>
	                <option value="0604">0604 NASIK RURAL DIVISION</option>
	                <option value="0612">0612 BHUSAWAL (E)</option>
	                <option value="0621">0621 BHUSAWAL U-I S/DN.</option>
	                <option value="0639">0639 YAWAL I</option>
	                <option value="0647">0647 YAWAL II SUBDN.</option>
	                <option value="0655">0655 RAVER S/DIVISION.</option>
	                <option value="0663">0663 SAVADA SUB DIVISION.</option>
	                <option value="0671">0671 EDLABAD</option>
	                <option value="0680">0680 JAMNER</option>
	                <option value="0698">0698 BODWAD</option>
	                <option value="0710">0710 AHMEDNAGAR URBAN DIV</option>
	                <option value="0744">0744 KOPARGAON (U) S/DIV.</option>
	                <option value="0787">0787 SANGAMNER (U) S/DIV.</option>
	                <option value="0795">0795 KOPARGAON (R)</option>
	                <option value="0817">0817 HADAPSAR</option>
	                <option value="0825">0825 PIMPRI (R)</option>
	                <option value="0833">0833 SHIRUR</option>
	                <option value="0841">0841 BHOR</option>
	                <option value="0850">0850 MULSHI</option>
	                <option value="0868">0868 NASARAPUR</option>
	                <option value="0876">0876 URALIKANCHAN</option>
	                <option value="0884">0884 LONAVALA</option>
	                <option value="0892">0892 TALEGAON</option>
	                <option value="0914">0914 SATARA (U)</option>
	                <option value="0922">0922 KARAD URBAN</option>
	                <option value="0931">0931 SATARA</option>
	                <option value="0949">0949 KOREGAON</option>
	                <option value="0957">0957 WAI</option>
	                <option value="0965">0965 MEDHA</option>
	                <option value="0973">0973 PACHGANI</option>
	                <option value="0974">0974 SHINDKHED S/DN.</option>
	                <option value="0981">0981 WATHAR</option>
	                <option value="0990">0990 KARAD -I S/DN</option>
	                <option value="1112">1112 KOLHAPUR URBAN DIVIS</option>
	                <option value="1121">1121 GADHINGLAJ</option>
	                <option value="1139">1139 NESARI</option>
	                <option value="1147">1147 CHANDGAD</option>
	                <option value="1155">1155 AJARA</option>
	                <option value="1163">1163 HATKANANGALE</option>
	                <option value="1171">1171 JAYSINGPUR</option>
	                <option value="1180">1180 KURUNDWAD</option>
	                <option value="1198">1198 VADGAON</option>
	                <option value="1210">1210 SANGLI URBAN DIVISIO</option>
	                <option value="1228">1228 MIRAJ urban</option>
	                <option value="1236">1236 MADHAVNAGAR</option>
	                <option value="1244">1244 MIRAJ RURAL</option>
	                <option value="1252">1252 VISHRAMBAG</option>
	                <option value="1261">1261 TASGAON (I)</option>
	                <option value="1279">1279 TASGAON (II)</option>
	                <option value="1287">1287 JATH</option>
	                <option value="1295">1295 K.MAHANKAL</option>
	                <option value="1317">1317 SOLAPUR URBAN  DIVIS</option>
	                <option value="1325">1325 SOLAPUR R-I S/DN.</option>
	                <option value="1333">1333 SOLAPUR R-II S/DN.</option>
	                <option value="1341">1341 AKKALKOT S/DN.</option>
	                <option value="1350">1350 MOHOL S/DN.</option>
	                <option value="1368">1368 KURDUWADI S/DN.</option>
	                <option value="1376">1376 KARMALA S/DN.</option>
	                <option value="1384">1384 BARSHI (U) S/DN.</option>
	                <option value="1392">1392 BARSHI (R) S/DN.</option>
	                <option value="1406">1406 KHAMGAON</option>
	                <option value="1503">1503 AKOLA URBAN DIVISION</option>
	                <option value="1511">1511 WASHIM</option>
	                <option value="1520">1520 RISOD S/DN.</option>
	                <option value="1538">1538 MANGRULPEER</option>
	                <option value="1546">1546 KARANJA</option>
	                <option value="1554">1554 MANORA DN.</option>
	                <option value="1562">1562 MALEGAON S DN.</option>
	                <option value="1619">1619 AMARAVATI URBAN DIVS</option>
	                <option value="1627">1627 ANJANGAON SUB-DN</option>
	                <option value="1635">1635 DARYAPUR SUB-DN</option>
	                <option value="1643">1643 CHANDUR BAZAR</option>
	                <option value="1651">1651 SHENDURGANAGHAT S/DN</option>
	                <option value="1660">1660 MORSHI I S/DN</option>
	                <option value="1678">1678 BADNERA SUB-DN.</option>
	                <option value="1686">1686 NANDGAON KH SUB-DN</option>
	                <option value="1694">1694 TIWASA SUB-DN.</option>
	                <option value="1708">1708 YAVATMAL S/DN</option>
	                <option value="1716">1716 WANI S DIV</option>
	                <option value="1724">1724 PANDHARKAWADA</option>
	                <option value="1732">1732 PUSAD SUB-DN</option>
	                <option value="1741">1741 UMERKHED S/DN.</option>
	                <option value="1759">1759 DIGRAS S/DN.</option>
	                <option value="1767">1767 DHANKI S/DN</option>
	                <option value="1813">1813 WARDHA URBAN S/DN</option>
	                <option value="1830">1830 HINGANGHAT (U) S/DN</option>
	                <option value="1848">1848 ARVI URBAN S/DN</option>
	                <option value="1856">1856 DEOLI RURAL S/DN</option>
	                <option value="1864">1864 SAMUDRAPUR RURAL SDN</option>
	                <option value="1881">1881 PULGAON  URBAN S/DN</option>
	                <option value="1902">1902 CT METER NGP (U) DN.</option>
	                <option value="1911">1911 KAMTEE URBAN  SDN</option>
	                <option value="1929">1929 BHANDARA URBAN S/DN</option>
	                <option value="1937">1937 GONDIA URBAN S/DN</option>
	                <option value="1945">1945 KATOL RURAL S/DN</option>
	                <option value="1953">1953 kondhali RURAL s/dn</option>
	                <option value="1996">1996 CT METER NGP (U) DN.</option>
	                <option value="2020">2020 TUMSAR S/DN</option>
	                <option value="2119">2119 CHANDRAPUR I (U)S/DN</option>
	                <option value="2127">2127 BALLARSHA URBAN SDN</option>
	                <option value="2160">2160 CHANDRAPUR-II(U)SDN</option>
	                <option value="2178">2178 WARORA (U) S/DN</option>
	                <option value="2186">2186 BHADRAVATI URBAN SDN</option>
	                <option value="2216">2216 AURANGABAD URBAN I D</option>
	                <option value="2224">2224 GANGAPUR (R) S/DN</option>
	                <option value="2232">2232 PAITHAN  (R) S/D</option>
	                <option value="2241">2241 AURANGABAD  R-I S/D</option>
	                <option value="2267">2267 AURANGBAD R-II S/D</option>
	                <option value="2275">2275 KHULATABAD (R) S/D</option>
	                <option value="2291">2291 AURANGABAD(U)-AG</option>
	                <option value="2313">2313 PARBHANI (U) S/D</option>
	                <option value="2321">2321 PARBHANI (R) S/DN</option>
	                <option value="2330">2330 GANGAKHED (R) S/DN</option>
	                <option value="2348">2348 PATHRI (R) S-DN</option>
	                <option value="2356">2356 SAILU   (R)  S/D</option>
	                <option value="2364">2364 BASMAT (R) S/DN.</option>
	                <option value="2372">2372 JINTUR (R) S/DN</option>
	                <option value="2381">2381 HINGOLI (R) S/DN</option>
	                <option value="2399">2399 KALAMNURI (R) S/DN</option>
	                <option value="2411">2411 NANDED   (U) S/DN-I</option>
	                <option value="2429">2429 NANDED RURAL-I S/DN</option>
	                <option value="2437">2437 NANDED RURAL II S/DN</option>
	                <option value="2445">2445 NAIGAON (R) S/DN</option>
	                <option value="2453">2453 LOHA (R) S/DN</option>
	                <option value="2461">2461 KANDHAR (R) S/DN</option>
	                <option value="2470">2470 DHARMABAD (R) S/DN</option>
	                <option value="2488">2488 DEGLOOR (R) S/DN</option>
	                <option value="2496">2496 MUKHED (R) S/DN</option>
	                <option value="2500">2500 KOLHAPUR CIRCLE</option>
	                <option value="2511">2511 MA</option>
	                <option value="2518">2518 BEED     (U) S/D</option>
	                <option value="2526">2526 PATODA (R) S/DN</option>
	                <option value="2534">2534 GEORAI (R) S/DN</option>
	                <option value="2542">2542 BEED RURAL S/DN</option>
	                <option value="2551">2551 ASHTI (R) S/DN</option>
	                <option value="2569">2569 AMBEJOGAI</option>
	                <option value="2577">2577 KAIJ (R) S/DN</option>
	                <option value="2585">2585 PARALI (R) S-DN</option>
	                <option value="2593">2593 MAJALGAON S/DN.</option>
	                <option value="2615">2615 OSMANABAD (R) S/DN</option>
	                <option value="2623">2623 TER (R) S/DN</option>
	                <option value="2631">2631 TULJAPUR (R) S/DN</option>
	                <option value="2640">2640 BHOOM (R) S/DN</option>
	                <option value="2642">2642 CHANDRAPUR CIRCLE</option>
	                <option value="2655">2655 GADCHIROLI CIRCLE</option>
	                <option value="2658">2658 PARANDA (R) S/DN</option>
	                <option value="2665">2665 BHANDARA CIRCLE</option>
	                <option value="2666">2666 KALAM (R)S/DN</option>
	                <option value="2674">2674 OMERGA (R) S/DN</option>
	                <option value="2680">2680 LATUR CIRCLE</option>
	                <option value="2712">2712 JALNA    (U) S/D</option>
	                <option value="2721">2721 JALNA (R) S/DN</option>
	                <option value="2739">2739 BADNAPUR (R) S/DN</option>
	                <option value="2747">2747 AMBAD (R) S/DN</option>
	                <option value="2755">2755 PARTUR (R) S/DN</option>
	                <option value="2763">2763 BHOKARDHAN (R) S/DN</option>
	                <option value="2801">2801 AKOLA (R) SUB-DN</option>
	                <option value="2810">2810 AKOT SUB DN</option>
	                <option value="2828">2828 TELHARA SUB-DN.</option>
	                <option value="2836">2836 MURTIZAPUR SUB-DN</option>
	                <option value="2844">2844 BALAPUR SUB-DN</option>
	                <option value="2852">2852 BULDANA SUB-DN</option>
	                <option value="2861">2861 CHIKHLI</option>
	                <option value="2879">2879 D RAJA SUB-DN</option>
	                <option value="2887">2887 MALKAPUR S/DN.</option>
	                <option value="2895">2895 MOTALA S. DN.</option>
	                <option value="2917">2917 KHAMGAON(R) S/DN</option>
	                <option value="2925">2925 NANDURA S/DN.</option>
	                <option value="2933">2933 JALGAON JALMOD SDIV</option>
	                <option value="2941">2941 MEHEKAR</option>
	                <option value="2984">2984 AMRAVATI (R) SUB-DN</option>
	                <option value="2992">2992 DHAMANGAON SUB-DN</option>
	                <option value="3000">3000 CHANDUR RLY SUB-DN.</option>
	                <option value="3018">3018 PIMPRI DIVISION</option>
	                <option value="3026">3026 SHIVAJI NAGAR DIVISI</option>
	                <option value="3034">3034 BUNDGARDEN DIVISION</option>
	                <option value="3041">3041 NASIK URBAN DN. II</option>
	                <option value="3042">3042 PADMAVATI DIVISION</option>
	                <option value="3051">3051 RASTAPETH DIVISION</option>
	                <option value="3069">3069 KOTHRUD DIVISION</option>
	                <option value="3077">3077 ACHALPUR CITY SUB-DN</option>
	                <option value="3085">3085 DHARNI S/DN.</option>
	                <option value="3093">3093 WARUD (I)</option>
	                <option value="3107">3107 WARUD II S/DN</option>
	                <option value="3115">3115 PIMPRI DN. (AG)</option>
	                <option value="3123">3123 SHIVAJINAGAR DN.(AG)</option>
	                <option value="3131">3131 BAND GARDEN DN. (AG)</option>
	                <option value="3140">3140 PADMAVATI DN. (AG)</option>
	                <option value="3150">3150 AKOLA URBAN DIVISION</option>
	                <option value="3151">3151 WASHIM DIVISION</option>
	                <option value="3158">3158 RASTA PETH DN. (AG)</option>
	                <option value="3161">3161 AMARAVATI URBAN DIVS</option>
	                <option value="3166">3166 KALAMB S/DN.</option>
	                <option value="3170">3170 YAVATMAL DIVSION</option>
	                <option value="3174">3174 YAVATMAL (R) SUB-DN.</option>
	                <option value="3181">3181 WARDHA DIVSION</option>
	                <option value="3183">3183 ARVI DIVISION</option>
	                <option value="3184">3184 HINGANGHAT DIVSION</option>
	                <option value="3191">3191 DHARWAH S/DN.</option>
	                <option value="3204">3204 NER S/DN.</option>
	                <option value="3221">3221 AURANGABAD URBAN I D</option>
	                <option value="3222">3222 AURANGABAD URBAN II.</option>
	                <option value="3224">3224 AURANGABAD RURAL I D</option>
	                <option value="3226">3226 KANNAD DIVISION</option>
	                <option value="3231">3231 PARBHANI DIVSION</option>
	                <option value="3251">3251 BEED DIVSION</option>
	                <option value="3271">3271 JALNA DIVISION I</option>
	                <option value="3273">3273 JALNA DIVISION II</option>
	                <option value="3310">3310 PARVATI DIVISION</option>
	                <option value="3311">3311 PARVATI DN. AG/CT</option>
	                <option value="3312">3312 NAGAR DIVISION</option>
	                <option value="3399">3399 KALYAN (EAST) DIVISI</option>
	                <option value="3544">3544 PANVEL RURAL DIVISIO</option>
	                <option value="3553">3553 THANE DIVISION-I</option>
	                <option value="3554">3554 MULUND DIVISION</option>
	                <option value="3555">3555 BHANDUP URBAN DIVISI</option>
	                <option value="3558">3558 MULUND DIVISION</option>
	                <option value="3559">3559 THANE DIVISION (IP)</option>
	                <option value="3560">3560 BHANDUP URBAN DIVISI</option>
	                <option value="3571">3571 AHMEDNAGAR RURAL DIV</option>
	                <option value="3573">3573 SANGEMNER DIVISION</option>
	                <option value="3574">3574 KARJAT (R) DIVISION</option>
	                <option value="3581">3581 DHULE RURAL DIVISION</option>
	                <option value="3582">3582 DHULE URBAN DIVISION</option>
	                <option value="3583">3583 DONDAICHA DIVISION</option>
	                <option value="3584">3584 SHAHADA DIVISION</option>
	                <option value="3585">3585 NANDURBAR (O) DIVISI</option>
	                <option value="3591">3591 SAVADA DIVISION</option>
	                <option value="3595">3595 PACHORA DIVISION</option>
	                <option value="3596">3596 CHALISGAON DIVISION</option>
	                <option value="3605">3605 SATANA DIVISION</option>
	                <option value="3611">3611 AKOLA CONSTRUCTION D</option>
	                <option value="3613">3613 BULDHANA DIVISION</option>
	                <option value="3614">3614 KHAMGAON CONSTRUCTIO</option>
	                <option value="3615">3615 MALKAPUR DIVISION</option>
	                <option value="3622">3622 AMARAVATI CONSTRUCTI</option>
	                <option value="3623">3623 ACHALPUR DIVISION</option>
	                <option value="3624">3624 MURSHI DIVSION</option>
	                <option value="3631">3631 PUSAD DIVSION</option>
	                <option value="3632">3632 PANDHARKAWADA DN.</option>
	                <option value="3643">3643 GADCHIROLI DIVISION</option>
	                <option value="3644">3644 BRAMHAPURI DIVISION</option>
	                <option value="3645">3645 ALLAPALLI DIVISION</option>
	                <option value="3651">3651 BHANDARA DIVSION</option>
	                <option value="3652">3652 GONDIA DIVISION</option>
	                <option value="3653">3653 KATOL DIVISION</option>
	                <option value="3654">3654 NAGPUR I DIVISION</option>
	                <option value="3655">3655 NAGPUR II DIVISION</option>
	                <option value="3656">3656 SAKOLI O DIVISION</option>
	                <option value="3657">3657 DEORI DIVISION</option>
	                <option value="3681">3681 AMBEJOGAI DIVISION</option>
	                <option value="3694">3694 HINGOLI DIVSION</option>
	                <option value="3808">3808 SELU RURAL S/DN</option>
	                <option value="3905">3905 NAGPUR (U) S/DN.I</option>
	                <option value="3956">3956 HINGANA S/DN</option>
	                <option value="4006">4006 DOMBIVALI URBAN DIVS</option>
	                <option value="4014">4014 KALYAN URBAN DIVISIO</option>
	                <option value="4016">4016 KOLHAPUR U.(E) S/DN.</option>
	                <option value="4017">4017 KOLHAPUR U. CENTRAL</option>
	                <option value="4018">4018 KOLHAPUR U.(W) S/DN.</option>
	                <option value="4019">4019 KOLHAPUR U.(N) S/DN.</option>
	                <option value="4021">4021 KOLHAPUR U.MARKET Y.</option>
	                <option value="4022">4022 ULHASNAGAR DIVISION </option>
	                <option value="4031">4031 ULHASNAGAR DIVISION </option>
	                <option value="4049">4049 WASHI DIVISION</option>
	                <option value="4057">4057 WASHI IND.</option>
	                <option value="4065">4065 WAGLE ESTATE IND.</option>
	                <option value="4066">4066 VISHRAMBAG NORTH ZN.</option>
	                <option value="4067">4067 SANGLIWADI WEST ZONE</option>
	                <option value="4068">4068 CENTRAL ZONE,SANGLI</option>
	                <option value="4069">4069 SOUTH ZONE,SANGLI</option>
	                <option value="4073">4073 KALYAN CC O S/DN</option>
	                <option value="4086">4086 SOLAPUR (A) S/DN.</option>
	                <option value="4087">4087 SOLAPUR (B) S/DN.</option>
	                <option value="4088">4088 SOLAPUR (C) S/DN.</option>
	                <option value="4089">4089 SOLAPUR (D) S/DN.</option>
	                <option value="4090">4090 WAGLE ESTATE DIVISIO</option>
	                <option value="4127">4127 WASHI O S/DN.</option>
	                <option value="4129">4129 GOREGAON</option>
	                <option value="4130">4130 MAHAD</option>
	                <option value="4131">4131 MHASALA</option>
	                <option value="4132">4132 MURUD</option>
	                <option value="4133">4133 PALI</option>
	                <option value="4134">4134 POLADPUR</option>
	                <option value="4135">4135 ROHA</option>
	                <option value="4136">4136 SHRIWARDHAN</option>
	                <option value="4137">4137 ALIBAG-I</option>
	                <option value="4138">4138 ALIBAG-II</option>
	                <option value="4139">4139 KARJAT(PEN CIRCLE)</option>
	                <option value="4140">4140 KHOPOLI</option>
	                <option value="4141">4141 PANVEL-II</option>
	                <option value="4142">4142 PEN</option>
	                <option value="4143">4143 BHIWANDI I S/DN.</option>
	                <option value="4144">4144 BHIWANDI II S/DN.</option>
	                <option value="4145">4145 BHIWANDI III S/DN.</option>
	                <option value="4146">4146 BHIWANDI IV S/DN.</option>
	                <option value="4147">4147 BHIWANDI V S/DN.</option>
	                <option value="4148">4148 BHIWANDI VI S/DN.</option>
	                <option value="4151">4151 KALYAN (E) S/DN.I</option>
	                <option value="4158">4158 WADA S/DN.</option>
	                <option value="4159">4159 BOISAR (R) S/DN.</option>
	                <option value="4160">4160 DAHANU S/DN.</option>
	                <option value="4161">4161 JAWHAR S/DN.</option>
	                <option value="4162">4162 PALGHAR S/DN.</option>
	                <option value="4163">4163 MIDC(BOISAR) S/DN.</option>
	                <option value="4164">4164 SAFALA S/DN.</option>
	                <option value="4165">4165 TALASARI S/DN.</option>
	                <option value="4166">4166 DOMBIVALI EAST S/DN.</option>
	                <option value="4167">4167 DOMBIVALI WEST S/DN.</option>
	                <option value="4168">4168 KALYAN I S/DN.</option>
	                <option value="4169">4169 KALYAN (E) S/DN.II</option>
	                <option value="4170">4170 ULHASNAGAR I S/DN.</option>
	                <option value="4171">4171 ULHASNAGAR II S/DN.</option>
	                <option value="4172">4172 ULHASNAGAR III S/DN.</option>
	                <option value="4173">4173 ULHASNAGAR IV S/DN.</option>
	                <option value="4174">4174 ULHASNAGAR  V S/DN.</option>
	                <option value="4175">4175 AMBERNATH(WEST)S/DN.</option>
	                <option value="4176">4176 CHIPLUN (U) S/DN.</option>
	                <option value="4177">4177 CHIPLUN (R) S/DN.</option>
	                <option value="4178">4178 DAPOLI I S/DN.</option>
	                <option value="4179">4179 GUGHAGAR S/DN.</option>
	                <option value="4180">4180 KHED S/DN.</option>
	                <option value="4181">4181 MANDANGAD S/DN.</option>
	                <option value="4182">4182 VENGURLA S/DN.</option>
	                <option value="4183">4183 DEOGAD S/DN.</option>
	                <option value="4184">4184 KANKAVALI S/DN.</option>
	                <option value="4185">4185 KUDAL I S/DN.</option>
	                <option value="4186">4186 MALWAN S/DN.</option>
	                <option value="4187">4187 SAWANTWADI (U) S/DN.</option>
	                <option value="4188">4188 SAWANTWADI (R) S/DN.</option>
	                <option value="4189">4189 DEORUKH S/DN.</option>
	                <option value="4190">4190 RATNAGIRI R-II S/DN.</option>
	                <option value="4191">4191 RAJAPUR I S/DN.</option>
	                <option value="4192">4192 RATNAGIRI R-I S/DN.</option>
	                <option value="4193">4193 RATANAGIRI (U)</option>
	                <option value="4200">4200 AHMEDNAGAR U-IIS/DN.</option>
	                <option value="4204">4204 AHMEDNAGAR U-I S/DN.</option>
	                <option value="4236">4236 JALGAON U-I S/DN.</option>
	                <option value="4237">4237 JALGAON U-II S/DN.</option>
	                <option value="4250">4250 CITY S/DN.</option>
	                <option value="4251">4251 NASIK RD (U) S/DN.</option>
	                <option value="4252">4252 PANCHAVATI S/DN.</option>
	                <option value="4253">4253 SATPUR S/DN.</option>
	                <option value="4261">4261 KALWA S/DN.</option>
	                <option value="4264">4264 MALEGAON (U) S/DN I</option>
	                <option value="4265">4265 MALEGAON (U) S/DN II</option>
	                <option value="4275">4275 AKOLA U-I S/DN</option>
	                <option value="4295">4295 AMRAVATI U. I S/DN.</option>
	                <option value="4296">4296 AMRAVATI U. II S/DN.</option>
	                <option value="4297">4297 AMRAVATI U. IIIS/DN.</option>
	                <option value="4310">4310 BHOSARI DN. (AG)</option>
	                <option value="4316">4316 KOTHRUD DN. (AG)</option>
	                <option value="4327">4327 CHANDRAPUR S/DN.</option>
	                <option value="4328">4328 RAJURA S/DN.</option>
	                <option value="4329">4329 GONDPIPRI S/DN.</option>
	                <option value="4330">4330 GADCHANDUR S/DN.</option>
	                <option value="4331">4331 BRAMHAPURI S/DN.</option>
	                <option value="4332">4332 BHIWANDI URBAN II DI</option>
	                <option value="4334">4334 SIRONCHA S/DN.</option>
	                <option value="4335">4335 MUL S/DN.</option>
	                <option value="4336">4336 NAGBHID S/DN.</option>
	                <option value="4337">4337 CHIMUR</option>
	                <option value="4338">4338 GADCHIROLI S/DN.</option>
	                <option value="4339">4339 ARMORI S/DN.</option>
	                <option value="4340">4340 WADSA S/DN.</option>
	                <option value="4341">4341 VASAI RD. URBAN S/DN</option>
	                <option value="4342">4342 KULKHEDA S/DN.</option>
	                <option value="4343">4343 ALLAPALLI S/DN.</option>
	                <option value="4344">4344 CHAMORSHI S/DN.</option>
	                <option value="4345">4345 ETAPALLI S/DN.</option>
	                <option value="4346">4346 SINDEWAHI S/DN.</option>
	                <option value="4359">4359 VASAI RD. EAST S/DN.</option>
	                <option value="4364">4364 KHAPARKHEDA S/DN.</option>
	                <option value="4367">4367 BHIWANDI URBAN I DIV</option>
	                <option value="4371">4371 WARDHA S/DN. R-I</option>
	                <option value="4372">4372 WARDHA S/DN. R-II</option>
	                <option value="4373">4373 HINGHANGHAT S/DN.</option>
	                <option value="4375">4375 NALASOPARA WEST S/DN</option>
	                <option value="4377">4377 ASHTI SUB DN</option>
	                <option value="4379">4379 KARANJA SUB DN</option>
	                <option value="4380">4380 KHARANGANA (R) S/DN.</option>
	                <option value="4383">4383 KALYAN URBAN DIVISIO</option>
	                <option value="4391">4391 MUMBRA S/DN</option>
	                <option value="4394">4394 A-BAD POWER HOUSE</option>
	                <option value="4395">4395 CHIKALDHANA S/DN.</option>
	                <option value="4396">4396 KRANTICHOWK S/DN.</option>
	                <option value="4405">4405 BADLAPUR (E) S/DN.</option>
	                <option value="4445">4445 NANDED (U) S/DN-II</option>
	                <option value="4448">4448 SHAHAPUR</option>
	                <option value="4456">4456 MURBAD</option>
	                <option value="4464">4464 VIRAR WEST S/DN.</option>
	                <option value="4532">4532 VAIBHAVWADI S/DN.</option>
	                <option value="4540">4540 KISAN NAGAR</option>
	                <option value="4541">4541 KOLSHETH URBAN S/DN.</option>
	                <option value="4542">4542 WAGLE ESTATE S/DN.</option>
	                <option value="4551">4551 LONAR S/DN.</option>
	                <option value="4570">4570 AKOLA U-II S/DN</option>
	                <option value="4572">4572 LANJA S/DN.</option>
	                <option value="4577">4577 KALYAN (E) S/DN.III</option>
	                <option value="4578">4578 CHAWANI S/DN.</option>
	                <option value="4579">4579 SOLAPUR (E) S/DN.</option>
	                <option value="4591">4591 LOKMANYA NAGAR</option>
	                <option value="4592">4592 AKOLA U-III S/DN</option>
	                <option value="4593">4593 DAPODI SUB-DN.</option>
	                <option value="4594">4594 KHERALWADI SUB-DN.</option>
	                <option value="4595">4595 AKURDI SUB-DN.</option>
	                <option value="4596">4596 BHOSARI I SUB-DN.</option>
	                <option value="4597">4597 SHIVAJINAGAR S/DN.</option>
	                <option value="4598">4598 GANESH KHIND SUB-DN.</option>
	                <option value="4599">4599 AUNDH SUB-DN.</option>
	                <option value="4600">4600 KHADKI SUB-DN.</option>
	                <option value="4601">4601 WADIA SUB-DN.</option>
	                <option value="4602">4602 VISHRANTWADI SUB-DN.</option>
	                <option value="4603">4603 HADAPSAR SUB-DN.</option>
	                <option value="4604">4604 NAGAR ROAD SUB-DN.</option>
	                <option value="4605">4605 SWARGATE SUB-DN.</option>
	                <option value="4606">4606 MARKET YARD SUB-DN.</option>
	                <option value="4607">4607 FIRE BRIGADE SUB-DN.</option>
	                <option value="4608">4608 RASTA PETH SUB-DN.</option>
	                <option value="4609">4609 KASBA SUB-DN.</option>
	                <option value="4610">4610 ST. MARRY SUB-DN.</option>
	                <option value="4611">4611 DECCAN GYMAKHANA S/D</option>
	                <option value="4612">4612 PESHWE PARK SUB-DN.</option>
	                <option value="4613">4613 KOTHRUD SUB-DN.</option>
	                <option value="4614">4614 WARGE SUB-DN.</option>
	                <option value="4615">4615 BHOSARI II SUB-DN.</option>
	                <option value="4635">4635 CHINCHWAD SUB-DN.</option>
	                <option value="4636">4636 SANGHVI SUB-DN.</option>
	                <option value="4637">4637 DHANKAWADI SUB-DN.</option>
	                <option value="4640">4640 LATUR (U) S/D(SOUTH)</option>
	                <option value="4641">4641 AIROLI S/DN.</option>
	                <option value="4642">4642 NERUL S/DN.</option>
	                <option value="4643">4643 SHIL SUB DIVISION</option>
	                <option value="4645">4645 BHIWANDI VII S/DN.</option>
	                <option value="4646">4646 DHANORA S/DN.</option>
	                <option value="4652">4652 CBD BELAPUR S/DN.</option>
	                <option value="4655">4655 KOPRI S/DN</option>
	                <option value="4656">4656 JALGAON DN. 20HP CT</option>
	                <option value="4657">4657 BHUSAVAL UCR DN.20HP</option>
	                <option value="4658">4658 DHARANGAON DN 20HP</option>
	                <option value="4659">4659 NANDED (U) S/D 20HP</option>
	                <option value="4660">4660 JALNA (U) S/DN 20HP</option>
	                <option value="4661">4661 JALNA (R) S/DN 20HP</option>
	                <option value="4662">4662 BHOKARDHAN (R) 20HP</option>
	                <option value="4663">4663 JAFARABAD(R) 20HP</option>
	                <option value="4664">4664 BADNAPUR (R) 20HP</option>
	                <option value="4665">4665 AMBAD (R) S/DN 20HP</option>
	                <option value="4666">4666 PARTUR (R) S/DN 20HP</option>
	                <option value="4667">4667 NAGAR URBAN DN. 20HP</option>
	                <option value="4668">4668 DHAD S/DN.</option>
	                <option value="4669">4669 AMBAD S/DN.</option>
	                <option value="4670">4670 DWARKA S/DN.</option>
	                <option value="4671">4671 DEOLALI S/DN.</option>
	                <option value="4672">4672 WALUNJ S/DN.</option>
	                <option value="4673">4673 SHAHGANJ S/DN.</option>
	                <option value="4674">4674 A-BAD CIDCO S/DN.</option>
	                <option value="4675">4675 GHARGHED S/DN.</option>
	                <option value="4676">4676 ALANDI SUB-DN.</option>
	                <option value="4677">4677 WADGAON SUB-DN.</option>
	                <option value="4678">4678 CIVIL LINE S/DN.</option>
	                <option value="4679">4679 MRS S/DN.</option>
	                <option value="4680">4680 LASHKARIBAG S/DN.</option>
	                <option value="4681">4681 SHANKAR NAGAR S/DN.</option>
	                <option value="4682">4682 REGENT S/DN.</option>
	                <option value="4683">4683 TRIMURTI NAGAR S/DN.</option>
	                <option value="4684">4684 MANEWADA S/DN.</option>
	                <option value="4685">4685 TULSHIBAG S/DN.</option>
	                <option value="4686">4686 NANDANWAN S/DN.</option>
	                <option value="4687">4687 ITWARI S/DN.</option>
	                <option value="4688">4688 WARDHAMAN S/DN.</option>
	                <option value="4689">4689 BINAKI S/DN.</option>
	                <option value="4690">4690 MIDC - I S/DN.</option>
	                <option value="4691">4691 MIDC - II S/DN.</option>
	                <option value="4692">4692 MANDAI SUB-DN.</option>
	                <option value="4696">4696 KALYAN U. IV S/DN.</option>
	                <option value="4697">4697 VASAI RD. WEST S/DN.</option>
	                <option value="4698">4698 VASAI RD. AG/ST.S/DN</option>
	                <option value="4699">4699 SHIKRAPUR S/DN.</option>
	                <option value="4700">4700 CHAKAN S/DN.</option>
	                <option value="4701">4701 KEDGAON S/DN.</option>
	                <option value="4702">4702 GHANSAWANGI S/DN.</option>
	                <option value="4703">4703 SARVODAY S/DN.</option>
	                <option value="4704">4704 PACH RASTA S/DN.</option>
	                <option value="4705">4705 NEELAM NAGAR S/DN.</option>
	                <option value="4706">4706 VIRAR EAST S/DN.</option>
	                <option value="4707">4707 VIRAR (E) AG/ST.S/DN</option>
	                <option value="4708">4708 NALASOPARA EAST S/DN</option>
	                <option value="4709">4709 NALASOPARA(E)IP.S/DN</option>
	                <option value="4710">4710 ICHALKARNJI A S/DN.</option>
	                <option value="4711">4711 ICHALKARNJI B S/DN.</option>
	                <option value="4713">4713 BHUSAWAL U-II S/DN.</option>
	                <option value="4714">4714 GHODEGAON SUB-DN.</option>
	                <option value="4715">4715 PANDHARPUR R-II S/DN</option>
	                <option value="4716">4716 AKLUJ II SUB-DN.</option>
	                <option value="4717">4717 SAWARDA S/DN.</option>
	                <option value="4718">4718 KILLARI S/DN.</option>
	                <option value="4719">4719 LAKNI S/DN.</option>
	                <option value="4720">4720 DOMBIVALI WEST IISDN</option>
	                <option value="4721">4721 SANKH S/DN.</option>
	                <option value="4722">4722 SOMESHWAR SUB-DN.</option>
	                <option value="4723">4723 WALCHANDNAGAR SUB-DN</option>
	                <option value="4724">4724 NANDED (U) MIDC SDN.</option>
	                <option value="4726">4726 VIKAS COMPLEX S/DN.</option>
	                <option value="4727">4727 POWER HOUSE S/DN.</option>
	                <option value="4728">4728 GADKARI S/DN.</option>
	                <option value="4729">4729 IP/COM.VIKAS COMPLEX</option>
	                <option value="4730">4730 IP/COM.POWER HOUSE</option>
	                <option value="4731">4731 IP/COM.GADKARI S/DN.</option>
	                <option value="4732">4732 ISHWAR NAGAR S/DN</option>
	                <option value="4733">4733 BHANDUP (E) S/DN</option>
	                <option value="4734">4734 PANNALAL S/DN</option>
	                <option value="4738">4738 IP/COM SARVODAY S/DN</option>
	                <option value="4739">4739 IP/COM PACHRASTA SDN</option>
	                <option value="4740">4740 IP/CO NEELAM NGR SDN</option>
	                <option value="4741">4741 OSMANABAD (U) S/DN</option>
	                <option value="4742">4742 UDGIR URBAN S/DN.</option>
	                <option value="4743">4743 VAIJAPUR-II S/DN.</option>
	                <option value="4744">4744 DHARUR SUB-DN.</option>
	                <option value="4745">4745 HADAPSAR (I) SUB-DN.</option>
	                <option value="4746">4746 WADGAON SHERI SUB-DN</option>
	                <option value="4747">4747 BHUSARI COLONY S/DN.</option>
	                <option value="4748">4748 (U)S/DN.III MALEGAON</option>
	                <option value="4749">4749 GANGAPUR N.(U) S/DN.</option>
	                <option value="4750">4750 HIMAYATNAGAR S/DN</option>
	                <option value="4751">4751 KALYAN SUB D/DN. V</option>
	                <option value="4752">4752 PALM BEACH S/DN.</option>
	                <option value="4753">4753 KOPARKHAIRENE S/DN.</option>
	                <option value="4754">4754 AMBERNATH(EAST)S/DN.</option>
	                <option value="4755">4755 BADLAPUR (W) S/DN.</option>
	                <option value="4756">4756 BARSHI TAKLI SUB-DN</option>
	                <option value="4757">4757 JEUR SUB-DIVISION</option>
	                <option value="4758">4758 LOHARA S/DN</option>
	                <option value="4759">4759 BELWANDI S/DN.</option>
	                <option value="4760">4760 MORSHI II S/DN.</option>
	                <option value="4761">4761 DAPOLI II S/DN.</option>
	                <option value="4762">4762 LOTE(MIDC) S/DN.</option>
	                <option value="4763">4763 RAJAPUR II S/DN.</option>
	                <option value="4764">4764 SANGAMESHWAR S/DN.</option>
	                <option value="4765">4765 KUDAL II S/DN.</option>
	                <option value="4766">4766 ACHARA S/DN.</option>
	                <option value="4767">4767 ACHALPUR II S/DN.</option>
	                <option value="4768">4768 DOMBIVALI(W) S/DN-IV</option>
	                <option value="4769">4769 WADGAON MAVAL S/DN.</option>
	                <option value="4770">4770 ALE PHATA S/DN.</option>
	                <option value="4771">4771 KALWAN S/DIV</option>
	                <option value="4772">4772 NASIK RURAL DIVISION</option>
	                <option value="4773">4773 RAVER S/DIVISION.</option>
	                <option value="4774">4774 AKOLA URBAN DIVISION</option>
	                <option value="4775">4775 YAVATMAL DIVSION</option>
	                <option value="4776">4776 CT METER NGP (U) DN.</option>
	                <option value="4777">4777 KAMTEE URBAN  SDN</option>
	                <option value="4778">4778 BHANDARA URBAN S/DN</option>
	                <option value="4779">4779 PIMPRI DIVISION</option>
	                <option value="4780">4780 WARUD II S/DN</option>
	                <option value="4781">4781 NER S/DN.</option>
	                <option value="4782">4782 KOTHRUD DN. (AG)</option>
	                <option value="4783">4783 AHMEDPUR</option>
	                <option value="4784">4784 KADAMWADI</option>
	                <option value="4785">4785 VITA I S/DN.</option>
	                <option value="4786">4786 MANGALVEDHA</option>
	                <option value="4787">4787 NANDGAON</option>
	                <option value="4788">4788 MIRAJ RURAL-II S/DN.</option>
	                <option value="4789">4789 SAVLAJ S/DN.</option>
	                <option value="4790">4790 BHATKULI S/DN</option>
	                <option value="4791">4791 MUDKHED S/DN</option>
	                <option value="4792">4792 BILOLI S/DN</option>
	                <option value="4793">4793 MAHUR S/DN</option>
	                <option value="4794">4794 UMRI S/DN</option>
	                <option value="4795">4795 KHARGHAR S/DN</option>
	                <option value="4796">4796 MANTHA S/DN</option>
	                <option value="4797">4797 SENGAON SDN</option>
	                <option value="4798">4798 AUNDHA NAGNATH S/DN</option>
	                <option value="4799">4799 SONPETH S/DN</option>
	                <option value="4800">4800 PALAM S/DN</option>
	                <option value="4801">4801 MANWAT S/DN</option>
	                <option value="4802">4802 SHIRUR SDN</option>
	                <option value="4803">4803 PHULAMBRI S/DN</option>
	                <option value="4804">4804 TALA S/DN</option>
	                <option value="4805">4805 MANGAON S/DN</option>
	                <option value="4806">4806 MADHA S/DN</option>
	                <option value="4807">4807 GAGANBAWADA S/DN</option>
	                <option value="4808">4808 MALSHIRAS S/DN</option>
	                <option value="4809">4809 SANGRAMPUR S/DN</option>
	                <option value="4810">4810 GOREGAON S/DN</option>
	                <option value="4811">4811 SADAK ARJUNI S/DN</option>
	                <option value="4812">4812 SALEKASA S/DN</option>
	                <option value="4813">4813 KHALAPUR S/DN</option>
	                <option value="4814">4814 PATUR S/DN</option>
	                <option value="4815">4815 BABHULGAON S/DN</option>
	                <option value="4816">4816 RALEGAON S/DN</option>
	                <option value="4817">4817 MAREGAON S/DN</option>
	                <option value="4818">4818 ZARI-JAMANI S/DN</option>
	                <option value="4819">4819 KHANDALA S/DN</option>
	                <option value="4820">4820 WASHI S/DN</option>
	                <option value="4821">4821 Tembhurni S/DN</option>
	                <option value="4822">4822 Devani S/DN</option>
	                <option value="4823">4823 Shirur Anantpal S/DN</option>
	                <option value="4824">4824 MOKHADA S/DN</option>
	                <option value="4825">4825 VIKRAMGAD S/DN</option>
	                <option value="4826">4826 Korchi S/DN</option>
	                <option value="4827">4827 Mulchera S/DN</option>
	                <option value="4828">4828 Bhamaragad S/DN</option>
	                <option value="4829">4829 Surgana S/DN</option>
	                <option value="4830">4830 Khatav S/DN</option>
	                <option value="4831">4831 Rahimatpur S/DN</option>
	                <option value="4832">4832 SAOLI S/DN</option>
	                <option value="4833">4833 POMBHURNA S/DN</option>
	                <option value="4834">4834 JIWATI S/DN</option>
	                <option value="4835">4835 MAHABALESHWAR S/DN</option>
	                <option value="4836">4836 Khanapur S/DN</option>
	                <option value="4837">4837 Akkalkuwa S/DN</option>
	                <option value="4838">4838 Dhadgaon S/DN</option>
	                <option value="4839">4839 SHRIRAMPUR S/DN</option>
	                <option value="4840">4840 BABHALESHWAR S/DN</option>
	                <option value="4841">4841 RAHURI FACTORY S/DN</option>
	                <option value="4842">4842 RAHURI S/DN</option>
	                <option value="4843">4843 BELAPUR S/DN</option>
	                <option value="4844">4844 M/S CITY CORPORATION</option>
	                <option value="4845">4845 M/S LAVASA CORPORATI</option>
	                <option value="4846">4846 M/S SAHARA INDIA COM</option>
	                <option value="4847">4847 CHIKHALDARA S/DN</option>
	                <option value="4848">4848 Jalgaon Energy (P) L</option>
	                <option value="4849">4849 M/S  ICC Reality - I</option>
	                <option value="4850">4850 M/S  ICC Reality -II</option>
	                <option value="4851">4851 GANESHKHIND (U) CIRC</option>
	                <option value="4852">4852 RASTAPETH (U) CIRCLE</option>
	                <option value="4853">4853 PUNE (R) CIRCLE</option>
	                <option value="4854">4854 RAMNATH CITY S/DN</option>
	                <option value="4855">4855 MANJRI STUD FIRM Pvt</option>
	                <option value="4856">4856 MAGARPATTA TOWNSHIP</option>
	                <option value="4857">4857 VAMONA DEV.PVT.LTD.</option>
	                <option value="4860">4860 NANDED CITY DEV&amp;LTD</option>
	                <option value="4861">4861 M/s Lodha Dwellers P</option>
	                <option value="4862">4862 EON Hadpsar P.Ltd.</option>
	                <option value="4863">4863 Raheja (BLDG No.1)</option>
	                <option value="4864">4864 Raheja (BLDG No.3)</option>
	                <option value="4865">4865 Raheja (BLDG No. 6)</option>
	                <option value="4867">4867 ACHOLE SUB DIVISION</option>
	                <option value="4870">4870 Simtool DF</option>
	                <option value="4871">4871 HDIL DF</option>
	                <option value="4872">4872 Subhedar Sdn.</option>
	                <option value="4873">4873 Pishor Sdn.</option>
	                <option value="5118">5118 LATUR   (U)  S/D</option>
	                <option value="5126">5126 LATUR (R) S/D</option>
	                <option value="5134">5134 MURURD (R) S/DN</option>
	                <option value="5142">5142 UDGIR(R) S/DN</option>
	                <option value="5151">5151 AHMEDPUR</option>
	                <option value="5169">5169 CHAKUR (R) S/DN</option>
	                <option value="5177">5177 NILANGA (R) S/DN</option>
	                <option value="5185">5185 AUSA (R) S/DN</option>
	                <option value="5193">5193 SHIRUR (R) S/DN</option>
	                <option value="5258">5258 GHANTAJI S/DN.</option>
	                <option value="5266">5266 MAHAGAON S/DN.</option>
	                <option value="5274">5274 ARNI S/DN.</option>
	                <option value="5282">5282 SHEGAON S/DN.</option>
	                <option value="5291">5291 YEOLA-R</option>
	                <option value="5401">5401 IGATPURI (R) SUBDN</option>
	                <option value="5410">5410 DINDORI (R) SUBDN</option>
	                <option value="5428">5428 NIPHAD</option>
	                <option value="5436">5436 NAMPUR</option>
	                <option value="5444">5444 MANMAD</option>
	                <option value="5452">5452 SINNAR-I</option>
	                <option value="5461">5461 CHANDWAD</option>
	                <option value="5479">5479 SATANA</option>
	                <option value="5487">5487 PIMPALGAON</option>
	                <option value="5495">5495 PEINTH</option>
	                <option value="5517">5517 NANDURBAR-R1</option>
	                <option value="5525">5525 DONDAICHA</option>
	                <option value="5533">5533 SHAHADA-I</option>
	                <option value="5541">5541 TALODA</option>
	                <option value="5550">5550 SHIRPUR-I</option>
	                <option value="5568">5568 DEOPUR</option>
	                <option value="5576">5576 SHIRPUR-II</option>
	                <option value="5584">5584 NANDURBAR-R2</option>
	                <option value="5606">5606 VARANGAON S/DN</option>
	                <option value="5614">5614 DHARANGAON DIVISION.</option>
	                <option value="5622">5622 AMALNER (U) S/DIVN.</option>
	                <option value="5631">5631 TALEGAON S/DN</option>
	                <option value="5649">5649 RENAPUR (R) S/DN</option>
	                <option value="5657">5657 AMALNER-II</option>
	                <option value="5665">5665 JALGAON-R</option>
	                <option value="5673">5673 JALGAON URBAN DIVISI</option>
	                <option value="5681">5681 JAFARABAD(R) S/DN</option>
	                <option value="5690">5690 NASHIRABAD</option>
	                <option value="5711">5711 JAMKHED</option>
	                <option value="5720">5720 KARJAT(NAGAR CIRCLE)</option>
	                <option value="5738">5738 NEWASA</option>
	                <option value="5746">5746 PATHARDI</option>
	                <option value="5754">5754 SHEVGAON</option>
	                <option value="5762">5762 SHRIGONDA</option>
	                <option value="5771">5771 RAHATA</option>
	                <option value="5789">5789 AKOLE</option>
	                <option value="5797">5797 SANGAMNER - II</option>
	                <option value="5801">5801 SASWAD</option>
	                <option value="5819">5819 MANCHAR</option>
	                <option value="5827">5827 RAJGURUNAGAR</option>
	                <option value="5835">5835 NARAYANGAON</option>
	                <option value="5843">5843 JUNNAR</option>
	                <option value="5851">5851 BARAMATI (U)</option>
	                <option value="5860">5860 BARAMATI (R)</option>
	                <option value="5878">5878 NIRA</option>
	                <option value="5886">5886 DOUND</option>
	                <option value="5894">5894 INDAPUR</option>
	                <option value="5916">5916 KARAD-II S/DN.</option>
	                <option value="5924">5924 VADUJ</option>
	                <option value="5932">5932 UMBRAJ S/DN.</option>
	                <option value="5941">5941 MALHAR PETH S/DN</option>
	                <option value="5959">5959 PATAN S/DN.</option>
	                <option value="5967">5967 PHALTAN</option>
	                <option value="5975">5975 PHALTAN (U)</option>
	                <option value="5983">5983 LONAND</option>
	                <option value="5991">5991 DAHIWADI</option>
	                <option value="6131">6131 PANHALA</option>
	                <option value="6149">6149 KALE</option>
	                <option value="6157">6157 KODOLI</option>
	                <option value="6165">6165 PARITE</option>
	                <option value="6173">6173 KADAMWADI</option>
	                <option value="6181">6181 PHULEWADI</option>
	                <option value="6190">6190 MALKAPUR</option>
	                <option value="6211">6211 ISLAMPUR (I)</option>
	                <option value="6220">6220 ISLAMPUR (II)</option>
	                <option value="6238">6238 SHIRALA</option>
	                <option value="6246">6246 ASTHA</option>
	                <option value="6254">6254 VITA I S/DN.</option>
	                <option value="6262">6262 VITA II S/DN.</option>
	                <option value="6271">6271 KIRLOSKARWADI S/DN.</option>
	                <option value="6289">6289 ATPADI S/DN.</option>
	                <option value="6319">6319 PANDHARPUR (U)</option>
	                <option value="6327">6327 PANDHARPUR R-I S/DN</option>
	                <option value="6335">6335 AKLUJ I SUB-DN.</option>
	                <option value="6343">6343 SANGOLA</option>
	                <option value="6351">6351 MANGALVEDHA</option>
	                <option value="6360">6360 NATEPUTE</option>
	                <option value="6416">6416 DEOLA</option>
	                <option value="6424">6424 MALEGAON (RSD)</option>
	                <option value="6441">6441 YEOLA (U) S/DN</option>
	                <option value="6459">6459 NANDGAON</option>
	                <option value="6602">6602 WARDHA CIRCLE</option>
	                <option value="6718">6718 PARNER NGR RSD (I)</option>
	                <option value="6726">6726 AHMEDNAGAR (R)-II</option>
	                <option value="6734">6734 GHODEGAON</option>
	                <option value="6742">6742 RAJUR</option>
	                <option value="6912">6912 AUNDH</option>
	                <option value="7111">7111 HUPARI</option>
	                <option value="7129">7129 KAGAL</option>
	                <option value="7137">7137 GARGOTI</option>
	                <option value="7145">7145 RADHANAGARI</option>
	                <option value="7153">7153 MURGUD</option>
	                <option value="7218">7218 VAIJAPUR (R) S/DN</option>
	                <option value="7226">7226 KANNAD (R) S/DN</option>
	                <option value="7234">7234 SILLOD(R) S/DN</option>
	                <option value="7242">7242 AJANTHA(R) S/SN</option>
	                <option value="7323">7323 PURNA (R) S/DN</option>
	                <option value="7412">7412 BHOKAR (R) S/DN</option>
	                <option value="7421">7421 KINWAT (R) S/DN</option>
	                <option value="7439">7439 HADGAON (R) S/DN</option>
	                <option value="7901">7901 NAGPUR CIVIL LINES D</option>
	                <option value="7919">7919 NAGPUR CONGRESS NAGA</option>
	                <option value="7927">7927 NAGPUR MAHAL DIVISIO</option>
	                <option value="7935">7935 NAGPUR GANDHIBAUG DI</option>
	                <option value="7943">7943 NAGPUR CIVIL LINES D</option>
	                <option value="7951">7951 NAGPUR CONGRESS NAGA</option>
	                <option value="7960">7960 C DN. CT METER</option>
	                <option value="7978">7978 D DN. CT METER</option>
	                <option value="7986">7986 MIDC METER NAGPUR</option>
	                <option value="7994">7994 MIDC METER NAGPUR</option>
	                <option value="8125">8125 BHADGAON</option>
	                <option value="8133">8133 PAROLA</option>
	                <option value="8141">8141 CHALISGAON-R-I</option>
	                <option value="8150">8150 CHALISGAON-R-II</option>
	                <option value="8168">8168 PACHORA-II</option>
	                <option value="8176">8176 PAHUR</option>
	                <option value="8184">8184 PACHORA-I</option>
	                <option value="8192">8192 CHOPDA-II S/DN</option>
	                <option value="8214">8214 SANGLI URBAN DIVISIO</option>
	                <option value="9016">9016 AKOLA CIRCLE</option>
	                <option value="9024">9024 AMARAVATI CIRCLE</option>
	                <option value="9032">9032 YAVATMAL CIRCLE</option>
	                <option value="9041">9041 BULDHANA CIRCLE</option>
	                <option value="9077">9077 BHOSARI DIVISION</option>
	                <option value="9113">9113 ICHALKARANJI DIVISIO</option>
	                <option value="9121">9121 ICHALKARANJI(RURAL)</option>
	                <option value="9130">9130 KOLHAPUR (U) AG.</option>
	                <option value="9148">9148 SHIROL</option>
	                <option value="9517">9517 ULHASNAGAR I(IND)</option>
	                <option value="9525">9525 ULHASNAGAR II(IND)</option>
	                <option value="9533">9533 DOMBIVALI (IND)</option>
            </select>
            <asp:DropDownList ID="locality" runat="server" class="custom-textbox" style="height:29px" Visible="false">
        
            </asp:DropDownList>
        <asp:TextBox ID="cust_no" runat="server" class="custom-textbox" style="height:29px" placeholder="  Enter Customer ID"></asp:TextBox>

            
        <asp:Button ID="find_cust" runat="server" OnClick="find_cust_Click" Text="Find" class="btn btn-info" style="float:none;border-radius:0px;height:28px;width:75px;padding-top:3px;margin-top:-2px;font-weight:bolder;" />
       <br />
        </div>
        <br /> <hr /><br />
        <div id="wrongId" runat="server" class="alert alert-danger" visible="false" style="border-radius:0;">
                <strong>Oh snap!</strong> Data not available. Please check your customer number.
        </div>
<span id="panelDown" runat="server" style="z-index:1;" visible="false">
        <span class="green-heading" style="float:left;padding-left:20px;">Your Details: </span><br />
    <div id="tableContainer" runat="server" class="tabclass"></div>
        <br />
        <hr /><br />
        <div class="btn-group" style="float:left;padding-left:20px;z-index:10;">            
            <asp:Button runat="server" ID="prev_consum" class="btn btn-default" Text="Previous Consumption" OnClick="prev_consum_Click"/>
            <asp:Button runat="server" ID="prev_bill" class="btn btn-default" Text="Previous Bills" OnClick="prev_bill_Click" />          
        </div>
        <br />
        <div id="container_kwhr" class="chartclass" runat="server"></div><br />
            <div id="sorry" runat="server" class="alert alert-danger" visible="false" style="border-radius:0;">
                <strong>Oh snap!</strong> Data not available. Please try some other option.
            </div>
        <br />
</span>
    </form>
     <footer>Copyright © 2014 Zenatix. All rights reserved.</footer>
</body>
</html>

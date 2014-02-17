<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnergySavingTips.aspx.cs" Inherits="Energy_Tips" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/Default.css" />
<link rel="Stylesheet" type="text/css" media="screen" href="../Scripts/wheather/weather.css" /> 
<link rel="shortcut icon" href="../images/dashboard_icon.png" />
 
    <script type="text/jscript" src="../Scripts/jquery-1.4.1.min.js"></script>
  
    
    <title>Energy Saving Tips</title>
  <style type="text/css">
      .contra
      {
          width:1024px;
          margin:0 auto;
      }
 h1
 {
     font-size:x-large;
     font-weight:normal;
    font-family:Verdana;
 }
.tipsUL
{
   display:none;   
   background-color:White;
   width:100%;
}
.tips
{
   cursor:pointer;   
   background-color:#f6f0f0;
   margin-top:10px;
}
.tips-icons
{
  height:150px;   
}
.opcl
{
  height:30px;
  width:30px;   
  
}

</style>
    <style type="text/css">
      
          a
      {
          font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
          text-decoration:none;
      }
        a:hover
        {
             text-decoration:none;
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
     <script type="text/jscript">

         jQuery(document).ready(function ($) {

             $('.tips').click(function () {

                 if (!$(this).find('table').is(':visible')) {
                     $(this).find("table").fadeIn('slow', function () {
                         // Animation complete.
                     });
                 }
                 else {

                     $(this).find("table").fadeOut('slow', function () {
                         // Animation complete.
                     });
                 }

             });

             $('.tipsUL').click(function ($) {
                 $('.tipsUL').toggle('slow', function () {
                     // Animation complete.
                 });;
             });



         });


</script>

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
     
    <script type="text/javascript">

        $(document).ready(function () {
            $('#options').click(function () {

                $('#optionsDiv').toggle("slow");
            });

        });

    </script>

  
<br />
   
<div class="contra">
<h1 style="color:#0d96c5;">Energy Saving Tips</h1>
<p>Wondering about ways to reduce your energy consumption? Try some of these energy-saving ideas!</p>
<div class="tips">
<h1>Lights</h1>
<table  class="tipsUL"><tr><td>
<ul type="disc">
<li>Turning Lights off when not in use
</li>
<li>Use T5 fluorescent tube light fittings.
</li>
<li>LEDs are the most effective lights and their life
is not impacted by frequency of switching on
and off.</li>
<li>Use task lighting; instead of brightly lighting an
entire room, focus the light where you need it</li>

</ul>
</td>
<td><img src="../images/tips-icons/cflF.png" alt="" class="tips-icons"/></td>
</tr></table>
</div>
<%--<img alt="" src="images/tips-icons/plusF.png" class="opcl"/><img alt="" src="images/tips-icons/minusF.png" class="opcl"/>--%>
<div class="tips">
<h1>Television</h1>
<table class="tipsUL"><tr><td>
<ul type="disc" >
<li>LCDs and LEDs are better than CRT and Plasma.
Rear projector Televisions are the most efficient
ones.
</li>
<li>Select size of the television according to the
room size
</li>
<li>Set Appropriate Picture settings to save
electricity.</li>
<li>Prefer a non HD TV as it consumes less energy
than a HD TV.</li>

</ul>
</td>
<td><img src="../images/tips-icons/tvF.png" alt="" class="tips-icons"/></td>
</tr></table>
</div>
<div class="tips">
<h1>Refrigerator</h1>
<table class="tipsUL"><tr><td>
<ul type="disc" >
<li>Regularly defrost manual-defrost refrigerators and
freezers; frost build up increases the amount of energy
needed to keep the motor running.
</li>
<li>Don't keep your refrigerator or freezer too cold.
</li>
<li>Make sure your refrigerator door seals are airtight</li>
<li>Cover liquids and wrap foods stored in the refrigerator.</li>
<li>Do not open the doors of the refrigerators frequently</li>
<li>Avoid putting hot or warm food straight into the fridge.</li>

</ul>
</td>
<td><img src="../images/tips-icons/fridgeF.png" alt="" /></td>
</tr></table>
</div>
<div class="tips">
<h1>Washing Machine</h1>
<table class="tipsUL"><tr><td>
<ul type="disc" >
<li>A front loading washing machine is always more efficient
than a top load washing machine.
</li>
<li>Always try to use washing machine with full load, as the
electricity used is same even if you under-load the
washing machine.
</li>
<li>Buy the right detergent and use the right amount</li>
<li>Never leave washing machine in standby mode.</li>

</ul>
</td>
<td><img src="../images/tips-icons/washing-machineF.png" alt="" class="tips-icons"/></td>
</tr></table>
</div>
<div class="tips">
<h1>Microwave Oven</h1>
<table class="tipsUL"><tr><td>
<ul type="disc" >
<li>Consumes 50 % less energy than conventional electric
/ gas stoves.
</li>
<li>Do not bake large food items.
</li>
<li>Unless you're baking breads or pastries, you may not
even need to preheat.</li>
<li>Don't open the oven door too often to check food
condition as each opening leads to a temperature
drop of 25°C.</li>
</ul>
</td>
<td><img src="../images/tips-icons/ovenF.png" alt="" class="tips-icons"/></td>
</tr></table>

</div>
<div class="tips">
<h1>Air Conditioner</h1>
<table class="tipsUL"><tr><td>
<ul type="disc" >
<li>Prefer air conditioner having automatic temperature cut off.
</li>
<li>Leave enough space between your air conditioner and the walls to allow better air
circulation.
</li>
<li>Set your thermostat as high as comfortably possible in the summer. The less
difference between the indoor and outdoor temperatures, the lower will be energy
consumption. Temperature setting of close to 28oC can reduce your energy
consumption of AC by almost half.</li>
<li>Don't place lamps or TV sets near your air- conditioning thermostat.</li>

</ul>
</td>
<td><img src="../images/tips-icons/airConF.png" alt="" class="tips-icons"/></td>
</tr></table>
</div>
<div class="tips">
<h1>Computers</h1>
<table class="tipsUL"><tr><td>
<ul type="disc">
<li>Enabling power management features on
the computer.
</li>
<li>Use a laptop rather than a desktop. Using
a laptop can save about 50% or more of
electricity.
</li>
<li>If you just need net surfing, use a tablet
(low powered processors that consume
less electricity).</li>
<li>Use Low power chips like Intel Atom that
provides same performance with less
power consumption.</li>

</ul>
</td>
<td><img src="../images/tips-icons/computerF.png" alt="" class="tips-icons"/></td>
</tr></table>
</div>
<div class="tips">
<h1>Mixers</h1>
<table class="tipsUL"><tr><td>
<ul type="disc">
<li>Avoid dry grinding in your food processors (mixers and grinders) as it takes longer time than
liquid grinding.
</li>
</ul>
</td>
<td><img src="../images/tips-icons/mixerF.png" alt="" class="tips-icons"/></td>
</tr></table>
</div> 
</div>
    </form>
</body>
</html>

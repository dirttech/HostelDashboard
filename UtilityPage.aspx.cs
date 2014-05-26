using System;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class UtilityPage : System.Web.UI.Page
{
    public string[] months;
    public decimal[] price;
    public string ip = "http://192.168.1.38:4999/";
    
    
    public List<string>[] names = new List<string>[12];
    public List<decimal>[] prices = new List<decimal>[12];
    public static string plottype="kwhr";
    public static string plotted = "";

    string[] allMonths = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

    public string[] jan; public string[] feb; public string[] mar; public string[] apr; public string[] may; public string[] jun;
    public string[] jul; public string[] aug; public string[] sep; public string[] oct; public string[] nov; public string[] dec;

    public decimal[] janPrice; public decimal[] febPrice; public decimal[] marPrice; public decimal[] aprPrice; public decimal[] mayPrice; public decimal[] junPrice;
    public decimal[] julPrice; public decimal[] augPrice; public decimal[] sepPrice; public decimal[] octPrice; public decimal[] novPrice; public decimal[] decPrice;


    protected void Page_Load(object sender, EventArgs e)
    {
       //ip = "http://localhost:5000/";

    }
    protected void find_cust_Click(object sender, EventArgs e)
    {
        Plotter();
    }
    protected void Plotter()
    {
        if (utilityList.SelectedValue == "bses_delhi")
        {
            getBSESPage(cust_no.Text);
        }
        else if (utilityList.SelectedValue == "hbvn")
        {
            getHBVNPage(cust_no.Text);
        }
        else if (utilityList.SelectedValue == "spanco")
        {
            getSpancoNagpur(cust_no.Text, locality.SelectedValue);
        }
        else if (utilityList.SelectedValue == "tangedco")
        {
            getTANGEDCO(cust_no.Text);
        }
        else if (utilityList.SelectedValue == "best_mumbai")
        {
            getBESTPage(cust_no.Text);
        }
        else if (utilityList.SelectedValue == "mahavitran")
        {
            getMahavitranPage(cust_no.Text, mahavitranList.Value);
        }
        else
        {

        }
        validateVisibility();
    }

    private void getBSESPage(string cust_id)
    {
        try
        {
            string sUrl = ip+"BSESDelhi?cust_no=" + cust_id;
            
            HttpWebRequest req = WebRequest.Create(sUrl) as HttpWebRequest;

            req.Proxy = WebRequest.GetSystemWebProxy();
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 2;

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string stringData = "";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(stringData);

            req.ContentLength = data.Length;

            Stream os = req.GetRequestStream();
            os.Write(data, 0, data.Length);
            os.Close();

            HttpWebResponse response = req.GetResponse() as HttpWebResponse;
    
            Stream objStream = req.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            var jss = new JavaScriptSerializer();
            string sline = objReader.ReadLine();
            var f1 = jss.Deserialize<dynamic>(sline);

            var name = f1["Name"].ToString().ToLower();
            var district = f1["District"];
            var sanctionedLoad = f1["Sanctioned Load"];
            var placeType = f1["Place Type"];
            var address = f1["Address"].ToString().ToLower();
            var circle = f1["Circle"].ToString().ToLower();

            string tableData = "Name: " + name + "#" + "Circle: " + circle + "#" + "Category: " + placeType + "#" + "Sanctioned Load: " + sanctionedLoad + "#" + "Address: " + address;
            generateTable(tableData);
            try
            {
                var readings = f1["BillReadings"];

                months = new string[readings.Length];
                price = new decimal[readings.Length];

                for (int i = 0; i < readings.Length; i++)
                {
                    var f2 = readings[i];
                    months[i] = f2[0];
                    price[i] = f2[1];
                }
                plotted = "rs";
            }
            catch (Exception exp)
            {

            }
        }
        catch (Exception f)
        {
            panelDown.Visible = false;
            wrongId.Visible = true;
        }
       
    }

    private void getHBVNPage(string cust_id)
    {
        try
        {
            string sUrl = ip+"HaryanaBijliVitranNigam?cust_no=" + cust_id;
            HttpWebRequest req = WebRequest.Create(sUrl) as HttpWebRequest;

            req.Proxy = WebRequest.GetSystemWebProxy();
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 2;

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string stringData = "";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(stringData);

            req.ContentLength = data.Length;

            Stream os = req.GetRequestStream();
            os.Write(data, 0, data.Length);
            os.Close();

            HttpWebResponse response = req.GetResponse() as HttpWebResponse;

            Stream objStream = req.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            var jss = new JavaScriptSerializer();
            string sline = objReader.ReadLine();
            var f1 = jss.Deserialize<dynamic>(sline);

            var name = f1["Name"].ToString().ToLower();
            var city = f1["City"].ToString().ToLower();
            var sanctionedLoad = f1["Load Sanctioned"];
            var placeType = f1["PlaceType"].ToString().ToLower();
            var billCategory = f1["BillCategory"];
            var loadType = f1["Load Type"];

            string tableData = "Name: " + name + "#" + "City: " + city + "#" + "Category: " + placeType + "#" + "Sanctioned Load: " + sanctionedLoad + "#" + "Load Type: " + loadType + "#" + "Bill Category: " + billCategory;
            generateTable(tableData);
            try
            {
                var readings = f1["EnergyReadings"];
                var bill_readings = f1["BillReadings"];

                
                if (plottype == "kwhr")
                {
                    plotted = "kwhr";
                    months = new string[readings.Length];
                    price = new decimal[readings.Length];

                    for (int i = 0; i < readings.Length; i++)
                    {
                        var f2 = readings[i];
                        months[i] = f2[0];
                        price[i] = f2[1];
                    }
                    clubMonths(months, price);
                }
                else
                {
                    plotted = "rs";
                    months = new string[bill_readings.Length];
                    price = new decimal[bill_readings.Length];

                    for (int i = 0; i < bill_readings.Length; i++)
                    {
                        var f2 = bill_readings[i];
                        months[i] = f2[0];
                        price[i] = f2[1];
                    }
                }
            }
            catch (Exception exp)
            {

            }
            //months = sortedPairs.Select(x => x.Value).ToArray();
            //r price = sortedPairs.Select(x => x.Keys).ToArray();
        }
        catch (Exception f)
        {
            panelDown.Visible = false;
            wrongId.Visible = true;
        }

    }

    private void getTANGEDCO(string cust_id)
    {
        try
        {
            string sUrl = ip+"TANGEDCO?cust_no=" + cust_id;
            
            HttpWebRequest req = WebRequest.Create(sUrl) as HttpWebRequest;

            req.Proxy = WebRequest.GetSystemWebProxy();
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 2;

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string stringData = "";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(stringData);

            req.ContentLength = data.Length;

            Stream os = req.GetRequestStream();
            os.Write(data, 0, data.Length);
            os.Close();

            HttpWebResponse response = req.GetResponse() as HttpWebResponse;

            Stream objStream = req.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            var jss = new JavaScriptSerializer();
            string sline = objReader.ReadLine();
            var f1 = jss.Deserialize<dynamic>(sline);

            var name = f1["Name"].ToString().ToLower();
            var address = f1["Address"].ToString().ToLower();
            var circle = f1["Circle"].ToString().ToLower();

            string tableData = "Name: " + name + "#" + "Circle: " + circle + "#" + "Address: " + address;
            generateTable(tableData);
            try
            {
                var readings = f1["BillReadings"];

                months = new string[readings.Length];
                price = new decimal[readings.Length];

                for (int i = 0; i < readings.Length; i++)
                {
                    var f2 = readings[i];
                    months[i] = f2[0];
                    price[i] = f2[1];
                }
                plotted = "rs";
            }
            catch (Exception exp)
            {

            }
        }
        catch (Exception f)
        {
            panelDown.Visible = false;
            wrongId.Visible = true;
        }
    }

    private void getBESTPage(string cust_id)
    {
        try
        {
            string sUrl = ip + "BestMumbai?cust_no=" + cust_id;

            HttpWebRequest req = WebRequest.Create(sUrl) as HttpWebRequest;

            req.Proxy = WebRequest.GetSystemWebProxy();
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 2;

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string stringData = "";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(stringData);

            req.ContentLength = data.Length;

            Stream os = req.GetRequestStream();
            os.Write(data, 0, data.Length);
            os.Close();

            HttpWebResponse response = req.GetResponse() as HttpWebResponse;

            Stream objStream = req.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            var jss = new JavaScriptSerializer();
            string sline = objReader.ReadLine();
            var f1 = jss.Deserialize<dynamic>(sline);

            var name = f1["Name"].ToString().ToLower();
            var address = f1["Address"].ToString().ToLower();
            string tableData = "Name: " + name + "#" + "Address: " + address;
            generateTable(tableData);
            try
            {
                var readings = f1["BillReadings"];

                months = new string[readings.Length];
                price = new decimal[readings.Length];

                for (int i = 0; i < readings.Length; i++)
                {
                    var f2 = readings[i];
                    months[i] = f2[0];
                    months[i] = months[i].Remove(months[i].Length - 12);
                    price[i] = f2[1];
                }
                plotted = "rs";
            }
            catch (Exception exp)
            {

            }
        }
        catch (Exception f)
        {
            panelDown.Visible = false;
            wrongId.Visible = true;
        }
    }

    private void getSpancoNagpur(string cust_id, string local)
    {
        try
        {
            string sUrl = ip + "SpancoNagpur?cust_no=" + cust_id+"&locality="+local;

            HttpWebRequest req = WebRequest.Create(sUrl) as HttpWebRequest;

            req.Proxy = WebRequest.GetSystemWebProxy();
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 2;

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string stringData = "";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(stringData);

            req.ContentLength = data.Length;

            Stream os = req.GetRequestStream();
            os.Write(data, 0, data.Length);
            os.Close();

            HttpWebResponse response = req.GetResponse() as HttpWebResponse;

            Stream objStream = req.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            var jss = new JavaScriptSerializer();
            string sline = objReader.ReadLine();
            var f1 = jss.Deserialize<dynamic>(sline);

            var name = f1["Name"].ToString().ToLower();
            var address = f1["Address"].ToString().ToLower();
            var loadSanctioned = f1["Sanctioned Load"].ToString().ToLower();
            var category = f1["Place Type"].ToString().ToLower();

            string tableData = "Name: " + name + "#" + "Sanctioned Load: " + loadSanctioned + "#" + "Address: " + address + "#" + "Category: " + category;
            generateTable(tableData);
            try
            {
                plotted = "kwhr";
                var readings = f1["EnergyReadings"];

                months = new string[readings.Length];
                price = new decimal[readings.Length];

                for (int i = 0; i < readings.Length; i++)
                {
                    var f2 = readings[i];
                    months[i] = f2[0];
                    price[i] = f2[1];
                }
                clubMonths(months, price);
            }
            catch (Exception exp)
            {

            }
        }
        catch (Exception f)
        {
            panelDown.Visible = false;
            wrongId.Visible = true;
        }
    }

    private void getMahavitranPage(string cust_id, string local)
    {
        try
        {
            string sUrl = ip + "MahaVitran?cust_no=" + cust_id + "&locality=" +local;
            HttpWebRequest req = WebRequest.Create(sUrl) as HttpWebRequest;

            req.Proxy = WebRequest.GetSystemWebProxy();
            req.AllowAutoRedirect = true;
            req.MaximumAutomaticRedirections = 2;

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string stringData = "";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(stringData);

            req.ContentLength = data.Length;

            Stream os = req.GetRequestStream();
            os.Write(data, 0, data.Length);
            os.Close();

            HttpWebResponse response = req.GetResponse() as HttpWebResponse;

            Stream objStream = req.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            var jss = new JavaScriptSerializer();
            string sline = objReader.ReadLine();
            var f1 = jss.Deserialize<dynamic>(sline);

            var name = f1["Name"].ToString().ToLower();
            var address = f1["Address"].ToString().ToLower();
            

            string tableData = "Name: " + name + "#" + "Address: " + address;
            generateTable(tableData);
            try
            {
                var readings = f1["EnergyReadings"];
                var bill_readings = f1["BillReadings"];


                if (plottype == "kwhr")
                {
                    plotted = "kwhr";
                    months = new string[readings.Length];
                    price = new decimal[readings.Length];

                    for (int i = 0; i < readings.Length; i++)
                    {
                        var f2 = readings[i];
                        months[i] = f2[0];
                        price[i] = f2[1];
                    }
                    clubMonths(months, price);
                }
                else
                {
                    plotted = "rs";
                    months = new string[bill_readings.Length];
                    price = new decimal[bill_readings.Length];

                    for (int i = 0; i < bill_readings.Length; i++)
                    {
                        var f2 = bill_readings[i];
                        months[i] = f2[0];
                        price[i] = f2[1];
                    }
                }
            }
            catch (Exception exp)
            {

            }
        }
        catch (Exception f)
        {
            panelDown.Visible = false;
            wrongId.Visible = true;
        }

    }

    private void generateTable(string tableData)
    {
        if (tableData != null)
        {
            panelDown.Visible = true;
            wrongId.Visible = false;
            var data = tableData.Split('#').ToArray();
            int len = data.Length;

            HtmlTable tab = new HtmlTable();
            for (int i = 0; i < len; i = i + 2)
            {
                HtmlTableRow row = new HtmlTableRow();

                HtmlTableCell cell1 = new HtmlTableCell();
                cell1.InnerText = data[i];
                row.Cells.Add(cell1);
                if (i + 1 < len)
                {
                    HtmlTableCell cell2 = new HtmlTableCell();
                    cell2.InnerText = data[i + 1];
                    row.Cells.Add(cell2);
                }

                tab.Rows.Add(row);
            }
            tableContainer.Controls.Add(tab);
        }
        else
        {
            panelDown.Visible = false;
            wrongId.Visible = true;
        }
    }

    private void clubMonths(string[] months, decimal[] price)
    {
        //var sortedPairs = months.Select((x, i) => new { Value = x, Keys = price[i] }).OrderBy(x => x.Value).ThenBy(x => x.Keys).ToArray();

        if (months != null && price != null)
        {
            for (int i = 0; i < months.Length; i++)
            {
                int a = 0, done=0;                
                while (done==0)
                {
                    if(months[i].Contains(allMonths[a]))
                    {
                        if (names[a] == null && prices[a] == null)
                        {
                            names[a] = new List<string>();
                            prices[a] = new List<decimal>();
                        }
                        names[a].Add(months[i]);
                        prices[a].Add(price[i]);
                        done = 1;
                    }
                    else
                    {
                        a=a+1;
                    }                    
                }
            }
            if (names[0] != null)
            {
                jan = names[0].ToArray();
            }
            if (names[0] != null) { feb = names[1].ToArray(); } mar = names[2].ToArray(); apr = names[3].ToArray(); may = names[4].ToArray(); jun = names[5].ToArray();
            jul = names[6].ToArray(); aug = names[7].ToArray(); sep = names[8].ToArray(); oct = names[9].ToArray(); nov = names[10].ToArray(); dec = names[11].ToArray();

            janPrice = prices[0].ToArray(); febPrice = prices[1].ToArray(); marPrice = prices[2].ToArray(); aprPrice = prices[3].ToArray(); mayPrice = prices[4].ToArray();
            junPrice = prices[5].ToArray(); julPrice = prices[6].ToArray(); augPrice = prices[7].ToArray(); sepPrice = prices[8].ToArray(); octPrice = prices[9].ToArray();
            novPrice = prices[10].ToArray(); decPrice = prices[11].ToArray();

            
        }
       
    }

    protected void PlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void prev_consum_Click(object sender, EventArgs e)
    {
        plottype = "kwhr"; Plotter();
    }
    protected void prev_bill_Click(object sender, EventArgs e)
    {
        plottype = "rs"; Plotter();
    }
    protected void fillTANGEDCO()
    {
        
        locality.Items.Clear();
        locality.Items.Add(new ListItem("Select Locality"));
        locality.Items.Add(new ListItem("01-Chennai-North"));
        locality.Items.Add(new ListItem("02-Villupuram"));
        locality.Items.Add(new ListItem("03-Coimbatore"));
        locality.Items.Add(new ListItem("04-Erode"));
        locality.Items.Add(new ListItem("05-Madurai"));
        locality.Items.Add(new ListItem("06-Trichy"));
        locality.Items.Add(new ListItem("07-Tirunelvel"));
        locality.Items.Add(new ListItem("08-Vellore"));
        locality.Items.Add(new ListItem("09-chennai-South"));
        locality.Items[0].Selected = true;

    }
    protected void utilityList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (utilityList.SelectedValue == "bses_delhi")
        {
            locality.Visible = false;
            mahavitranList.Visible = false;    
        }
        else if (utilityList.SelectedValue == "hbvn")
        {
            locality.Visible = false;
            mahavitranList.Visible = false;    
        }
        else if (utilityList.SelectedValue == "spanco")
        {
            locality.Visible = false;
            mahavitranList.Visible = false;    
        }
        else if (utilityList.SelectedValue == "best_mumbai")
        {
            locality.Visible = false;
            mahavitranList.Visible = false;    
        }
        else if (utilityList.SelectedValue == "tangedco")
        {
            locality.Visible = true;
            mahavitranList.Visible = false;     
            fillTANGEDCO();
        }
        else if (utilityList.SelectedValue == "mahavitran")
        {
            locality.Visible = false;
            mahavitranList.Visible = true;           
        }
        else
        {
            locality.Visible = false;
            mahavitranList.Visible = false;    
        }
        panelDown.Visible = false;
        wrongId.Visible = false;
    }

    protected void validateVisibility()
    {
        if (months==null)
        {
            container_kwhr.Visible = false;
            sorry.Visible = true;
        }
        else
        {
            if (plottype == plotted)
            {
                container_kwhr.Visible = true;
                sorry.Visible = false;
            }
            else
            {
                container_kwhr.Visible = false;
                if (plottype == "rs")
                {
                    sorry.InnerHtml = "Bills not available. Try <b>Previous Consumption</b> option.";
                }
                else if (plottype == "kwhr")
                {
                    sorry.InnerHtml = "Previous consumption not available. Try <b>Previous Bills</b> option.";
                }
                else
                {
                    sorry.InnerHtml = "<strong>Oh snap!</strong> Data not available. Please try some other option.";
                }
                sorry.Visible = true;
            }
        }
    }
}
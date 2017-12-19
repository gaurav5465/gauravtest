using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Drawing;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (expenseview.SelectedItem.Text == "Daily")
        {
            secondrow.Visible = true;
            thirdrow.Visible = false;
            fourthrow.Visible = false;
            fifthrow.Visible = false;
            dateofexpense.Text = "";

        }
        else if (expenseview.SelectedItem.Text == "-Select-")
        {
            secondrow.Visible = false;
            thirdrow.Visible = false;
            fourthrow.Visible = false;
            fifthrow.Visible = false;
        }

        else if (expenseview.SelectedItem.Text == "Monthly")
        {

            secondrow.Visible = false;
            thirdrow.Visible = true;
            fourthrow.Visible = false;
            fifthrow.Visible = false;
            monthlist.SelectedItem.Text = "-Select-";
        }
        else if (expenseview.SelectedItem.Text == "Group")
        {

            secondrow.Visible = false;
            thirdrow.Visible = false;
            fourthrow.Visible = true;
            fifthrow.Visible = true;
        }

    }

    protected void logoutevent(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript' >alertMX('Invalid Username/Password!');</script>");
        Response.Redirect("Default.aspx");

    }
    //code to display chart for expensed made in a day

    private void GetData()
    {
        HtmlGenericControl datadivcontrol = new HtmlGenericControl("div");
        datadivcontrol.ID = "datadiv";
        datadivcontrol.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl datadivcontrolnight = new HtmlGenericControl("div");
        datadivcontrolnight.ID = "datadivnight";
        //datadivcontrolnight.Attributes["style"] = "width:50%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontrolnight.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl datadivcontroleve = new HtmlGenericControl("div");
        datadivcontroleve.ID = "datadiveve";
        //datadivcontroleve.Attributes["style"] = "overflow: auto; width:100%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontroleve.Attributes["style"] = "height:200px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl datadivcontrolafter = new HtmlGenericControl("div");
        datadivcontrolafter.ID = "datadiafter";
        //datadivcontrolafter.Attributes["style"] = "overflow: auto; width:50%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontrolafter.Attributes["style"] = "height:300px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl datadivcontrolmor = new HtmlGenericControl("div");
        datadivcontrolmor.ID = "datadivmor";
        //datadivcontrolmor.Attributes["style"] = "overflow: auto; width:50%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontrolmor.Attributes["style"] = "height:200px;width:100px;margin-top:10px;margin-left:20px;float:left";

        HtmlGenericControl datadivcontrolmortotal = new HtmlGenericControl("div");
        datadivcontrolmortotal.ID = "datadivmortotal";
        //datadivcontrolmor.Attributes["style"] = "overflow: auto; width:50%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontrolmortotal.Attributes["style"] = "height:200px;width:100px;margin-top:10px;margin-left:10px;float:left;clear:both";

        HtmlGenericControl datadivcontrolafttotal = new HtmlGenericControl("div");
        datadivcontrolafttotal.ID = "datadivafttotal";
        //datadivcontrolmor.Attributes["style"] = "overflow: auto; width:50%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontrolafttotal.Attributes["style"] = "height:200px;width:100px;margin-top:10px;margin-left:20px;float:left";

        HtmlGenericControl datadivcontrolevetotal = new HtmlGenericControl("div");
        datadivcontrolevetotal.ID = "datadivevetotal";
        //datadivcontrolmor.Attributes["style"] = "overflow: auto; width:50%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontrolevetotal.Attributes["style"] = "height:200px;width:100px;margin-top:10px;margin-left:20px;float:left";

        HtmlGenericControl datadivcontrolnightotal = new HtmlGenericControl("div");
        datadivcontrolnightotal.ID = "datadivnighttotal";
        //datadivcontrolmor.Attributes["style"] = "overflow: auto; width:50%;margin-top:10px;margin-left:10px;float:right;";
        datadivcontrolnightotal.Attributes["style"] = "height:200px;width:100px;margin-top:10px;margin-left:20px;float:left";

        //code for displaying data in html table
        try
        {

            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder htmlTable = new StringBuilder();
            StringBuilder newhtmlTable = new StringBuilder();
            StringBuilder morhtmlTable = new StringBuilder();
            StringBuilder evehtmlTable = new StringBuilder();
            StringBuilder afthtmlTable = new StringBuilder();
            StringBuilder nighthtmlTable = new StringBuilder();
            StringBuilder mortotalthtmlTable = new StringBuilder();
            StringBuilder afttotalthtmlTable = new StringBuilder();
            StringBuilder evetotalthtmlTable = new StringBuilder();
            StringBuilder nighttotalthtmlTable = new StringBuilder();

            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("select Product_Name, PriceOfItem from(select mydatabase.dateofpurchase.DateofPurchase, mydatabase.product.Product_Name, mydatabase.dateofpurchase.PriceOfItem from dateofpurchase, product where dateofpurchase.Product_Id = mydatabase.product.Product_Id) as myquery  where DateofPurchase =?dateofexpense"))
                //using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) from mydatabase.dateofpurchase where DateofPurchase =?dateofexpense"))
                {

                    cmd.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                    cmd.CommandType = CommandType.Text;
                    //newcmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    //newcmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();

                    htmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                    htmlTable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Product Name</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");

                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            htmlTable.Append("<tr>");

                            htmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["Product_Name"] + "</td>");
                            htmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceOfItem"] + "</td>");

                            htmlTable.Append("</tr>");
                        }
                        htmlTable.Append("</table>");

                        producttable.Controls.Add(datadivcontrol);

                        datadivcontrol.Controls.Add(new Literal
                        {
                            Text = htmlTable.ToString()
                        });

                        productreader.Close();
                        productreader.Dispose();

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                    using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) from mydatabase.dateofpurchase where DateofPurchase =?dateofexpense"))
                    {

                        newcmd.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        newcmd.CommandType = CommandType.Text;

                        newcmd.Connection = con;

                        MySqlDataReader newproductreader = newcmd.ExecuteReader();

                        newhtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        newhtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Total Expenditure</th></tr>");

                        if (newproductreader.HasRows)
                        {
                            if (newproductreader.Read())
                            {
                                newhtmlTable.Append("<tr>");

                                newhtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + newproductreader["sum(PriceofItem)"] + "</td>");

                                newhtmlTable.Append("</tr>");
                            }
                            newhtmlTable.Append("</table>");

                            productotal.Controls.Add(datadivcontroltwo);

                            datadivcontroltwo.Controls.Add(new Literal
                            {
                                Text = newhtmlTable.ToString()
                            });

                            newproductreader.Close();
                            newproductreader.Dispose();
                        }

                    }

                    //code for find the items purcased in morning in a particular date
                    using (MySqlCommand cmdmor = new MySqlCommand("SELECT count(PurchaseTime) from mydatabase.timeofpurchase where DateofPurchase =?dateofexpense and PurchaseTime='Morning'"))
                    {

                        cmdmor.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdmor.CommandType = CommandType.Text;

                        cmdmor.Connection = con;

                        MySqlDataReader morproductreader = cmdmor.ExecuteReader();

                        morhtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        morhtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Morning</th></tr>");

                        if (morproductreader.HasRows)
                        {
                            if (morproductreader.Read())
                            {
                                morhtmlTable.Append("<tr>");

                                morhtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morproductreader["count(PurchaseTime)"] + "</td>");

                                morhtmlTable.Append("</tr>");
                            }
                            morhtmlTable.Append("</table>");

                            morPlaceHolder.Controls.Add(datadivcontrolmor);

                            datadivcontrolmor.Controls.Add(new Literal
                            {
                                Text = morhtmlTable.ToString()
                            });

                            morproductreader.Close();
                            morproductreader.Dispose();
                        }

                    }

                    //code for find the items purchased in afternoon on a particular date
                    using (MySqlCommand cmdafter = new MySqlCommand("SELECT count(PurchaseTime) from mydatabase.timeofpurchase where DateofPurchase =?dateofexpense and PurchaseTime='Afternoon'"))
                    {

                        cmdafter.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdafter.CommandType = CommandType.Text;

                        cmdafter.Connection = con;

                        MySqlDataReader afterproductreader = cmdafter.ExecuteReader();

                        afthtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        afthtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Afternoon</th></tr>");

                        if (afterproductreader.HasRows)
                        {
                            if (afterproductreader.Read())
                            {
                                afthtmlTable.Append("<tr>");

                                afthtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + afterproductreader["count(PurchaseTime)"] + "</td>");

                                afthtmlTable.Append("</tr>");
                            }
                            afthtmlTable.Append("</table>");

                            afterPlaceHolder.Controls.Add(datadivcontrolafter);

                            datadivcontrolafter.Controls.Add(new Literal
                            {
                                Text = afthtmlTable.ToString()
                            });

                            afterproductreader.Close();
                            afterproductreader.Dispose();
                        }

                    }

                    //code for find the items purchased in evening on a particular date
                    using (MySqlCommand cmdeve = new MySqlCommand("SELECT count(PurchaseTime) from mydatabase.timeofpurchase where DateofPurchase =?dateofexpense and PurchaseTime='Evening'"))
                    {

                        cmdeve.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdeve.CommandType = CommandType.Text;

                        cmdeve.Connection = con;

                        MySqlDataReader eveproductreader = cmdeve.ExecuteReader();

                        evehtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        evehtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Evening</th></tr>");

                        if (eveproductreader.HasRows)
                        {
                            if (eveproductreader.Read())
                            {
                                evehtmlTable.Append("<tr>");

                                evehtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + eveproductreader["count(PurchaseTime)"] + "</td>");

                                evehtmlTable.Append("</tr>");
                            }
                            evehtmlTable.Append("</table>");

                            evePlaceHolder.Controls.Add(datadivcontroleve);

                            datadivcontroleve.Controls.Add(new Literal
                            {
                                Text = evehtmlTable.ToString()
                            });

                            eveproductreader.Close();
                            eveproductreader.Dispose();
                        }

                    }

                    //code for find the items purchased in night on a particular date
                    using (MySqlCommand cmdnight = new MySqlCommand("SELECT count(PurchaseTime) from mydatabase.timeofpurchase where DateofPurchase =?dateofexpense and PurchaseTime='Night'"))
                    {

                        cmdnight.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdnight.CommandType = CommandType.Text;

                        cmdnight.Connection = con;

                        MySqlDataReader nightproductreader = cmdnight.ExecuteReader();

                        nighthtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        nighthtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Night</th></tr>");

                        if (nightproductreader.HasRows)
                        {
                            if (nightproductreader.Read())
                            {
                                nighthtmlTable.Append("<tr>");

                                nighthtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + nightproductreader["count(PurchaseTime)"] + "</td>");

                                nighthtmlTable.Append("</tr>");
                            }
                            nighthtmlTable.Append("</table>");

                            nightPlaceHolder.Controls.Add(datadivcontrolnight);

                            datadivcontrolnight.Controls.Add(new Literal
                            {
                                Text = nighthtmlTable.ToString()
                            });

                            nightproductreader.Close();
                            nightproductreader.Dispose();
                        }

                    }

                    //code for getting data of morning expenditure of a particular date

                    using (MySqlCommand cmdmortotal = new MySqlCommand("select sum(PriceOfItem) from mydatabase.timeofpurchase,mydatabase.dateofpurchase where mydatabase.timeofpurchase.DateofPurchase=?dateofexpense and mydatabase.dateofpurchase.DateofPurchase=?dateofexpense and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and mydatabase.timeofpurchase.PurchaseTime='Morning'"))
                    {

                        cmdmortotal.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdmortotal.CommandType = CommandType.Text;

                        cmdmortotal.Connection = con;

                        MySqlDataReader morproductreadertotal = cmdmortotal.ExecuteReader();

                        mortotalthtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        mortotalthtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Morning Expenditure</th></tr>");

                        if (morproductreadertotal.HasRows)
                        {
                            if (morproductreadertotal.Read())
                            {
                                mortotalthtmlTable.Append("<tr>");

                                mortotalthtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morproductreadertotal["sum(PriceOfItem)"] + "</td>");

                                mortotalthtmlTable.Append("</tr>");
                            }
                            mortotalthtmlTable.Append("</table>");

                            mortotalPlaceHolder.Controls.Add(datadivcontrolmortotal);

                            datadivcontrolmortotal.Controls.Add(new Literal
                            {
                                Text = mortotalthtmlTable.ToString()
                            });

                            morproductreadertotal.Close();
                            morproductreadertotal.Dispose();
                        }

                    }

                    //code for getting data of afternoon expenditure of a particular date

                    using (MySqlCommand cmdafttotal = new MySqlCommand("select sum(PriceOfItem) from mydatabase.timeofpurchase,mydatabase.dateofpurchase where mydatabase.timeofpurchase.DateofPurchase=?dateofexpense and mydatabase.dateofpurchase.DateofPurchase=?dateofexpense and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and mydatabase.timeofpurchase.PurchaseTime='Afternoon'"))
                    {

                        cmdafttotal.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdafttotal.CommandType = CommandType.Text;

                        cmdafttotal.Connection = con;

                        MySqlDataReader aftproductreadertotal = cmdafttotal.ExecuteReader();

                        afttotalthtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        afttotalthtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Afternoon Expenditure</th></tr>");

                        if (aftproductreadertotal.HasRows)
                        {
                            if (aftproductreadertotal.Read())
                            {
                                afttotalthtmlTable.Append("<tr>");

                                afttotalthtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + aftproductreadertotal["sum(PriceOfItem)"] + "</td>");

                                afttotalthtmlTable.Append("</tr>");
                            }
                            afttotalthtmlTable.Append("</table>");

                            afttotalPlaceHolder.Controls.Add(datadivcontrolafttotal);

                            datadivcontrolafttotal.Controls.Add(new Literal
                            {
                                Text = afttotalthtmlTable.ToString()
                            });

                            aftproductreadertotal.Close();
                            aftproductreadertotal.Dispose();
                        }

                    }

                    //code for getting data of evening expenditure of a particular date

                    using (MySqlCommand cmdevetotal = new MySqlCommand("select sum(PriceOfItem) from mydatabase.timeofpurchase,mydatabase.dateofpurchase where mydatabase.timeofpurchase.DateofPurchase=?dateofexpense and mydatabase.dateofpurchase.DateofPurchase=?dateofexpense and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and mydatabase.timeofpurchase.PurchaseTime='Evening'"))
                    {

                        cmdevetotal.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdevetotal.CommandType = CommandType.Text;

                        cmdevetotal.Connection = con;

                        MySqlDataReader eveproductreadertotal = cmdevetotal.ExecuteReader();

                        evetotalthtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        evetotalthtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Evening Expenditure</th></tr>");

                        if (eveproductreadertotal.HasRows)
                        {
                            if (eveproductreadertotal.Read())
                            {
                                evetotalthtmlTable.Append("<tr>");

                                evetotalthtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + eveproductreadertotal["sum(PriceOfItem)"] + "</td>");

                                evetotalthtmlTable.Append("</tr>");
                            }
                            evetotalthtmlTable.Append("</table>");

                            evetotalPlaceHolder.Controls.Add(datadivcontrolevetotal);

                            datadivcontrolevetotal.Controls.Add(new Literal
                            {
                                Text = evetotalthtmlTable.ToString()
                            });

                            eveproductreadertotal.Close();
                            eveproductreadertotal.Dispose();
                        }

                    }

                    //code for getting data of night expenditure of a particular date

                    using (MySqlCommand cmdnightotal = new MySqlCommand("select sum(PriceOfItem) from mydatabase.timeofpurchase,mydatabase.dateofpurchase where mydatabase.timeofpurchase.DateofPurchase=?dateofexpense and mydatabase.dateofpurchase.DateofPurchase=?dateofexpense and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and mydatabase.timeofpurchase.PurchaseTime='Night'"))
                    {

                        cmdnightotal.Parameters.AddWithValue("?dateofexpense", dateofexpense.Text);

                        cmdnightotal.CommandType = CommandType.Text;

                        cmdnightotal.Connection = con;

                        MySqlDataReader nightproductreadertotal = cmdnightotal.ExecuteReader();

                        nighttotalthtmlTable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        nighttotalthtmlTable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Night Expenditure</th></tr>");

                        if (nightproductreadertotal.HasRows)
                        {
                            if (nightproductreadertotal.Read())
                            {
                                nighttotalthtmlTable.Append("<tr>");

                                nighttotalthtmlTable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + nightproductreadertotal["sum(PriceOfItem)"] + "</td>");

                                nighttotalthtmlTable.Append("</tr>");
                            }
                            nighttotalthtmlTable.Append("</table>");

                            nighttotalPlaceHolder.Controls.Add(datadivcontrolnightotal);

                            datadivcontrolnightotal.Controls.Add(new Literal
                            {
                                Text = nighttotalthtmlTable.ToString()
                            });

                            nightproductreadertotal.Close();
                            nightproductreadertotal.Dispose();
                        }

                    }

                }
            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }

    protected void sumbt_Click(object sender, EventArgs e)
    {
        GetData();

    }

    protected void sumbt_person(object sender, EventArgs e)
    {
        string person_name = personlist.SelectedItem.Text;
        if (person_name == "Divya")
        {

            getdivyadata();
        }
        else if (person_name == "Jyotsna")
        {
            getjyotsnadata();
        }
        else if (person_name == "Keshvam")
        {
            getkeshvamdata();
        }
        else if (person_name == "Mitali")
        {
            getmitalidata();
        }
        else if (person_name == "Monika")
        {
            getmonikadata();
        }
        else if (person_name == "Navdeep")
        {
            getnavdeepdata();
        }
        else if (person_name == "Neeraj")
        {
            getneerajdata();
        }
        else if (person_name == "Seema")
        {
            getseemadata();
        }

    }

    private void getdivyadata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.divya"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.divya"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;
                            //con.Open();
                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }
    private void getjyotsnadata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.jyotsna"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.jyotsna"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;
                            //con.Open();
                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }
    private void getkeshvamdata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.keshvam"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.keshvam"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;
                            //con.Open();
                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }
    private void getmitalidata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.mitali"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.mitali"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;

                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }
    private void getmonikadata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.monika"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.monika"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;

                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }
    private void getnavdeepdata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.navdeep"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.navdeep"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;
                            //con.Open();
                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }
    private void getneerajdata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.neeraj"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.neeraj"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;
                            //con.Open();
                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }
    private void getseemadata()
    {

        HtmlGenericControl datadivcontroltwo = new HtmlGenericControl("div");
        datadivcontroltwo.ID = "datadivtwo";
        datadivcontroltwo.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

        HtmlGenericControl persondiv = new HtmlGenericControl("div");
        persondiv.ID = "persondiv";
        persondiv.Attributes["style"] = "margin-top:10px;margin-left:10px;float:left;clear:right";

        try
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            StringBuilder persontable = new StringBuilder();
            StringBuilder persontotal = new StringBuilder();
            using (MySqlConnection con = new MySqlConnection(connstring))

            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT date_format(DateofExpense,'%W,%d %M %Y'),NameofItem,PriceofItem FROM mydatabase.seema"))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    MySqlDataReader productreader = cmd.ExecuteReader();
                    persontable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                    persontable.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Date of Expense</th><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Name of Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item(Rs.)</th></tr>");
                    if (productreader.HasRows)
                    {
                        while (productreader.Read())
                        {
                            persontable.Append("<tr>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["date_format(DateofExpense,'%W,%d %M %Y')"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["NameofItem"] + "</td>");
                            persontable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + productreader["PriceofItem"] + "</td>");
                            persontable.Append("</tr>");
                        }
                        persontable.Append("</table>");
                        persondataholder.Controls.Add(persondiv);
                        persondiv.Controls.Add(new Literal
                        {
                            Text = persontable.ToString()
                        });
                        productreader.Close();
                        productreader.Dispose();

                        //    new code for finding the total of person
                        using (MySqlCommand newcmd = new MySqlCommand("select sum(PriceofItem) FROM mydatabase.seema"))
                        {
                            newcmd.CommandType = CommandType.Text;
                            newcmd.Connection = con;
                            //con.Open();
                            MySqlDataReader newproductreader = newcmd.ExecuteReader();
                            persontotal.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                            persontotal.Append("<tr><th style=' border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white;'>Balance Amount(Rs.)</th></tr>");
                            if (newproductreader.HasRows)
                            {
                                while (newproductreader.Read())
                                {
                                    persontotal.Append("<tr>");

                                    persontotal.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color:#D3D3D3'>" + newproductreader["sum(PriceOfItem)"] + "</td>");
                                    persontotal.Append("</tr>");
                                }

                                persontotal.Append("</table>");
                                persontotalholder.Controls.Add(datadivcontroltwo);
                                datadivcontroltwo.Controls.Add(new Literal
                                {
                                    Text = persontotal.ToString()
                                });
                                newproductreader.Close();
                                newproductreader.Dispose();

                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                        resetpersonlist();
                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
                    }

                }

            }
        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0} Exception caught.", ex);
        }

    }

    protected void sumbt_month(object sender, EventArgs e)
    {
        //string monthnumber = DateTime.Now.Month.ToString();
        DateTime now = DateTime.Now;
        int monthnumber = now.Month;

        //    if ((monthlist.SelectedIndex < 8) || (monthlist.SelectedIndex > monthnumber)) original code
        if (monthlist.SelectedIndex < 3) //new code for just checking
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
            ClientScript.RegisterStartupScript(Page.GetType(), "nodatatodisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");
        }

        else
        {

            try
            {
                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();

                StringBuilder monthdatatable = new StringBuilder();
                HtmlGenericControl datadivmonth = new HtmlGenericControl("div");
                datadivmonth.ID = "datadivmonth";
                datadivmonth.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left;overflow:hidden";

                StringBuilder moritemstable = new StringBuilder();
                HtmlGenericControl moritemdiv = new HtmlGenericControl("div");
                moritemdiv.ID = "moreitemsdiv";
                moritemdiv.Attributes["style"] = "height:10px;width:100px;margin-top:10px;margin-left:10px;float:left";

                StringBuilder aftitemstable = new StringBuilder();
                HtmlGenericControl aftitemdiv = new HtmlGenericControl("div");
                aftitemdiv.ID = "afteritemsdiv";
                aftitemdiv.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

                StringBuilder eveitemstable = new StringBuilder();
                HtmlGenericControl eveitemdiv = new HtmlGenericControl("div");
                eveitemdiv.ID = "eveningitemsdiv";
                eveitemdiv.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

                StringBuilder nightitemstable = new StringBuilder();
                HtmlGenericControl nightitemdiv = new HtmlGenericControl("div");
                nightitemdiv.ID = "nightitemsdiv";
                nightitemdiv.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

                StringBuilder totalitemstable = new StringBuilder();
                HtmlGenericControl totalitemsdiv = new HtmlGenericControl("div");
                totalitemsdiv.ID = "totalmonthitemsdiv";
                totalitemsdiv.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;float:left";

                StringBuilder monmortable = new StringBuilder();
                HtmlGenericControl monmordiv = new HtmlGenericControl("div");
                monmordiv.ID = "monthmorexpensediv";
                monmordiv.Attributes["style"] = "height:100px;width:100px;margin-top:10px;margin-left:10px;margin-right:10px;float:left";

                StringBuilder monafttable = new StringBuilder();
                HtmlGenericControl monaftdiv = new HtmlGenericControl("div");
                monaftdiv.ID = "monthaftexpensediv";
                monaftdiv.Attributes["style"] = "height:150px;width:100px;margin-top:10px;margin-left:10px;margin-right:10px;float:left";

                StringBuilder monevetable = new StringBuilder();
                HtmlGenericControl monevediv = new HtmlGenericControl("div");
                monevediv.ID = "montheveexpensediv";
                monevediv.Attributes["style"] = "height:100px;width:100px;margin-top:15px;margin-below:15px;margin-left:10px;float:left;margin-right:10px";

                StringBuilder monnighttable = new StringBuilder();
                HtmlGenericControl monnightdiv = new HtmlGenericControl("div");
                monnightdiv.ID = "monthnightexpensediv";
                monnightdiv.Attributes["style"] = "height:100px;width:100px;margin-top:15px;margin-left:10px;float:left;margin-right:10px";

                StringBuilder healthttable = new StringBuilder();
                HtmlGenericControl healthdiv = new HtmlGenericControl("div");
                healthdiv.ID = "healthexpensediv";
                healthdiv.Attributes["style"] = "height:100px;width:100px;margin-top:15px;float:left;margin-right:10px";

                StringBuilder needttable = new StringBuilder();
                HtmlGenericControl needdiv = new HtmlGenericControl("div");
                needdiv.ID = "needexpensediv";
                needdiv.Attributes["style"] = "height:100px;width:100px;margin-top:15px;margin-left:10px;float:left;clear:right";

                StringBuilder mostexpensetable = new StringBuilder();
                HtmlGenericControl mostexpensedaydiv = new HtmlGenericControl("div");
                mostexpensedaydiv.ID = "monmostexpensediv";
                mostexpensedaydiv.Attributes["style"] = "height:300px;width:200px;margin-top:200px;margin-left:10px;margin-below:15px;float:left;margin-right:10px;overflow:auto;";

                StringBuilder leastexpensetable = new StringBuilder();
                HtmlGenericControl leastexpensedaydiv = new HtmlGenericControl("div");
                leastexpensedaydiv.ID = "monleastexpensediv";
                leastexpensedaydiv.Attributes["style"] = "height:300px;width:200px;margin-top:200px;margin-below:10px;margin-left:10px;float:left;clear:right;overflow:auto;";

                using (MySqlConnection con = new MySqlConnection(connstring))
                {
                    //code for getting expenditure of the full month
                    using (MySqlCommand cmdmonth = new MySqlCommand("SELECT sum(PriceOfItem) FROM mydatabase.dateofpurchase where month(DateofPurchase) = ?listvalue;"))
                    {

                        cmdmonth.Parameters.AddWithValue("?listvalue", monthlist.SelectedIndex);

                        cmdmonth.CommandType = CommandType.Text;

                        cmdmonth.Connection = con;

                        con.Open();

                        MySqlDataReader monthtotal = cmdmonth.ExecuteReader();
                        monthdatatable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");

                        monthdatatable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Month Expenditure</th></tr>");

                        if (monthtotal.HasRows)
                        {
                            if (monthtotal.Read())
                            {
                                monthdatatable.Append("<tr>");

                                monthdatatable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + monthtotal["sum(PriceOfItem)"] + "</td>");

                                monthdatatable.Append("</tr>");
                            }
                            monthdatatable.Append("</table>");

                            monthlyplaceholder.Controls.Add(datadivmonth);

                            datadivmonth.Controls.Add(new Literal
                            {
                                Text = monthdatatable.ToString()
                            });

                            monthtotal.Close();
                            monthtotal.Dispose();
                            cmdmonth.Dispose();
                            con.Close();
                            con.Dispose();

                            //code for getting the no of items purchased in the morning of a particular month
                            using (MySqlConnection newcon = new MySqlConnection(connstring))
                            {
                                using (MySqlCommand morcmd = new MySqlCommand("SELECT count(*) FROM mydatabase.timeofpurchase where month(dateofpurchase)= ?listvalue and PurchaseTime=?timeofpurchase;"))
                                {
                                    morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                    morcmd.Parameters.AddWithValue("?timeofpurchase", "Morning");
                                    morcmd.CommandType = CommandType.Text;
                                    morcmd.Connection = newcon;
                                    newcon.Open();
                                    MySqlDataReader morreader = morcmd.ExecuteReader();
                                    moritemstable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                    moritemstable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Morning</th></tr>");
                                    if (morreader.HasRows)
                                    {
                                        if (morreader.Read())
                                        {
                                            moritemstable.Append("<tr>");

                                            moritemstable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["count(*)"] + "</td>");

                                            moritemstable.Append("</tr>");
                                        }
                                        moritemstable.Append("</table>");

                                        totalmoritemsmonth.Controls.Add(moritemdiv);

                                        moritemdiv.Controls.Add(new Literal
                                        {
                                            Text = moritemstable.ToString()
                                        });

                                        morreader.Close();
                                        morreader.Dispose();
                                        morcmd.Dispose();
                                        newcon.Close();
                                        newcon.Dispose();

                                    }
                                }

                                //code for finding the no of items purchased  in the afternoon in a particular month
                                using (MySqlConnection con2 = new MySqlConnection(connstring))
                                {
                                    using (MySqlCommand morcmd = new MySqlCommand("SELECT count(*) FROM mydatabase.timeofpurchase where month(dateofpurchase)= ?listvalue and PurchaseTime=?timeofpurchase;"))
                                    {
                                        morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                        morcmd.Parameters.AddWithValue("?timeofpurchase", "Afternoon");
                                        morcmd.CommandType = CommandType.Text;
                                        morcmd.Connection = con2;
                                        con2.Open();
                                        MySqlDataReader morreader = morcmd.ExecuteReader();
                                        aftitemstable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                        aftitemstable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Afternoon</th></tr>");
                                        if (morreader.HasRows)
                                        {
                                            if (morreader.Read())
                                            {
                                                aftitemstable.Append("<tr>");

                                                aftitemstable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["count(*)"] + "</td>");

                                                aftitemstable.Append("</tr>");
                                            }
                                            aftitemstable.Append("</table>");

                                            afttotalitemsmonth.Controls.Add(aftitemdiv);

                                            aftitemdiv.Controls.Add(new Literal
                                            {
                                                Text = aftitemstable.ToString()
                                            });

                                            morreader.Close();
                                            morreader.Dispose();
                                            morcmd.Dispose();
                                            con2.Close();
                                            con2.Dispose();
                                        }
                                    }
                                }

                                //code for finding the no of items purchased  in the evening in a particular month
                                using (MySqlConnection con3 = new MySqlConnection(connstring))
                                {
                                    using (MySqlCommand morcmd = new MySqlCommand("SELECT count(*) FROM mydatabase.timeofpurchase where month(dateofpurchase)= ?listvalue and PurchaseTime=?timeofpurchase;"))
                                    {
                                        morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                        morcmd.Parameters.AddWithValue("?timeofpurchase", "Evening");
                                        morcmd.CommandType = CommandType.Text;
                                        morcmd.Connection = con3;
                                        con3.Open();
                                        MySqlDataReader morreader = morcmd.ExecuteReader();
                                        eveitemstable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                        eveitemstable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Evening</th></tr>");
                                        if (morreader.HasRows)
                                        {
                                            if (morreader.Read())
                                            {
                                                eveitemstable.Append("<tr>");

                                                eveitemstable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["count(*)"] + "</td>");

                                                eveitemstable.Append("</tr>");
                                            }
                                            eveitemstable.Append("</table>");

                                            evetotalitemsmonth.Controls.Add(eveitemdiv);

                                            eveitemdiv.Controls.Add(new Literal
                                            {
                                                Text = eveitemstable.ToString()
                                            });

                                            morreader.Close();
                                            morreader.Dispose();
                                            morcmd.Dispose();
                                            con3.Close();
                                            con3.Dispose();
                                        }
                                    }
                                }

                                //code for finding the total no of items purchased at night during a particular month

                                using (MySqlConnection con4 = new MySqlConnection(connstring))
                                {
                                    using (MySqlCommand morcmd = new MySqlCommand("SELECT count(*) FROM mydatabase.timeofpurchase where month(dateofpurchase)= ?listvalue and PurchaseTime=?timeofpurchase;"))
                                    {
                                        morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                        morcmd.Parameters.AddWithValue("?timeofpurchase", "Night");
                                        morcmd.CommandType = CommandType.Text;
                                        morcmd.Connection = con4;
                                        con4.Open();
                                        MySqlDataReader morreader = morcmd.ExecuteReader();
                                        nightitemstable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                        nightitemstable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Items Purchased in Night</th></tr>");
                                        if (morreader.HasRows)
                                        {
                                            if (morreader.Read())
                                            {
                                                nightitemstable.Append("<tr>");

                                                nightitemstable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["count(*)"] + "</td>");

                                                nightitemstable.Append("</tr>");
                                            }
                                            nightitemstable.Append("</table>");

                                            nighttotalitemsmonth.Controls.Add(nightitemdiv);

                                            nightitemdiv.Controls.Add(new Literal
                                            {
                                                Text = nightitemstable.ToString()
                                            });

                                            morreader.Close();
                                            morreader.Dispose();
                                            morcmd.Dispose();
                                            con4.Close();
                                            con4.Dispose();
                                        }
                                    }
                                }

                                //code for finding the total no of items purchased  during a particular month

                                using (MySqlConnection con5 = new MySqlConnection(connstring))
                                {
                                    using (MySqlCommand morcmd = new MySqlCommand("SELECT count(*) FROM mydatabase.timeofpurchase where month(DateofPurchase)=?listvalue;"))
                                    {
                                        morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);

                                        morcmd.CommandType = CommandType.Text;
                                        morcmd.Connection = con5;
                                        con5.Open();
                                        MySqlDataReader morreader = morcmd.ExecuteReader();
                                        totalitemstable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                        totalitemstable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Total items purchased</th></tr>");
                                        if (morreader.HasRows)
                                        {
                                            if (morreader.Read())
                                            {
                                                totalitemstable.Append("<tr>");

                                                totalitemstable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["count(*)"] + "</td>");

                                                totalitemstable.Append("</tr>");
                                            }
                                            totalitemstable.Append("</table>");

                                            totalitemsmonth.Controls.Add(totalitemsdiv);

                                            totalitemsdiv.Controls.Add(new Literal
                                            {
                                                Text = totalitemstable.ToString()
                                            });

                                            morreader.Close();
                                            morreader.Dispose();
                                            morcmd.Dispose();
                                            con5.Close();
                                            con5.Dispose();
                                        }
                                    }
                                }

                                //code for finding the total morning expenditure  during a particular month

                                using (MySqlConnection con6 = new MySqlConnection(connstring))
                                {
                                    using (MySqlCommand morcmd = new MySqlCommand("select sum(PriceOfItem) from mydatabase.dateofpurchase,mydatabase.timeofpurchase where mydatabase.timeofpurchase.Transaction_Id=mydatabase.dateofpurchase.Transaction_Id and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and month(mydatabase.dateofpurchase.DateofPurchase)=?listvalue and mydatabase.timeofpurchase.PurchaseTime=?time;"))
                                    {
                                        morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                        morcmd.Parameters.AddWithValue("?time", "Morning");
                                        morcmd.CommandType = CommandType.Text;
                                        morcmd.Connection = con6;
                                        con6.Open();
                                        MySqlDataReader morreader = morcmd.ExecuteReader();
                                        monmortable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                        monmortable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Total Morning Expenditure</th></tr>");
                                        if (morreader.HasRows)
                                        {
                                            if (morreader.Read())
                                            {
                                                monmortable.Append("<tr>");

                                                monmortable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["sum(PriceOfItem)"] + "</td>");

                                                monmortable.Append("</tr>");
                                            }
                                            monmortable.Append("</table>");

                                            totatmorningmonexpense.Controls.Add(monmordiv);

                                            monmordiv.Controls.Add(new Literal
                                            {
                                                Text = monmortable.ToString()
                                            });

                                            morreader.Close();
                                            morreader.Dispose();
                                            morcmd.Dispose();
                                            con6.Close();
                                            con6.Dispose();
                                        }
                                    }

                                    //code for finding the total afternoon expenditure  during a particular month

                                    using (MySqlConnection con7 = new MySqlConnection(connstring))
                                    {
                                        using (MySqlCommand morcmd = new MySqlCommand("select sum(PriceOfItem) from mydatabase.dateofpurchase,mydatabase.timeofpurchase where mydatabase.timeofpurchase.Transaction_Id=mydatabase.dateofpurchase.Transaction_Id and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and month(mydatabase.dateofpurchase.DateofPurchase)=?listvalue and mydatabase.timeofpurchase.PurchaseTime=?time;"))
                                        {
                                            morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                            morcmd.Parameters.AddWithValue("?time", "Afternoon");
                                            morcmd.CommandType = CommandType.Text;
                                            morcmd.Connection = con7;
                                            con7.Open();
                                            MySqlDataReader morreader = morcmd.ExecuteReader();
                                            monafttable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                            monafttable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Total Afternoon Expenditure</th></tr>");
                                            if (morreader.HasRows)
                                            {
                                                if (morreader.Read())
                                                {
                                                    monafttable.Append("<tr>");

                                                    monafttable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["sum(PriceOfItem)"] + "</td>");

                                                    monafttable.Append("</tr>");
                                                }
                                                monafttable.Append("</table>");

                                                totalaftmonexpense.Controls.Add(monaftdiv);

                                                monaftdiv.Controls.Add(new Literal
                                                {
                                                    Text = monafttable.ToString()
                                                });

                                                morreader.Close();
                                                morreader.Dispose();
                                                morcmd.Dispose();
                                                con7.Close();
                                                con7.Dispose();
                                            }
                                        }

                                        //code for finding the total evening expenditure  during a particular month

                                        using (MySqlConnection con8 = new MySqlConnection(connstring))
                                        {
                                            using (MySqlCommand morcmd = new MySqlCommand("select sum(PriceOfItem) from mydatabase.dateofpurchase,mydatabase.timeofpurchase where mydatabase.timeofpurchase.Transaction_Id=mydatabase.dateofpurchase.Transaction_Id and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and month(mydatabase.dateofpurchase.DateofPurchase)=?listvalue and mydatabase.timeofpurchase.PurchaseTime=?time;"))
                                            {
                                                morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                                morcmd.Parameters.AddWithValue("?time", "Evening");

                                                morcmd.CommandType = CommandType.Text;
                                                morcmd.Connection = con8;
                                                con8.Open();
                                                MySqlDataReader morreader = morcmd.ExecuteReader();
                                                monevetable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                                monevetable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Total Evening Expenditure</th></tr>");
                                                if (morreader.HasRows)
                                                {
                                                    if (morreader.Read())
                                                    {
                                                        monevetable.Append("<tr>");

                                                        monevetable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["sum(PriceOfItem)"] + "</td>");

                                                        monevetable.Append("</tr>");
                                                    }
                                                    monevetable.Append("</table>");

                                                    totaleveningmonexpense.Controls.Add(monevediv);

                                                    monevediv.Controls.Add(new Literal
                                                    {
                                                        Text = monevetable.ToString()
                                                    });

                                                    morreader.Close();
                                                    morreader.Dispose();
                                                    morcmd.Dispose();
                                                    con8.Close();
                                                    con8.Dispose();
                                                }
                                            }

                                            //code for finding the total night expenditure  during a particular month

                                            using (MySqlConnection con9 = new MySqlConnection(connstring))
                                            {
                                                using (MySqlCommand morcmd = new MySqlCommand("select sum(PriceOfItem) from mydatabase.dateofpurchase,mydatabase.timeofpurchase where mydatabase.timeofpurchase.Transaction_Id=mydatabase.dateofpurchase.Transaction_Id and mydatabase.timeofpurchase.Product_Id=mydatabase.dateofpurchase.Product_Id and month(mydatabase.dateofpurchase.DateofPurchase)=?listvalue and mydatabase.timeofpurchase.PurchaseTime=?time;"))
                                                {
                                                    morcmd.Parameters.AddWithValue("?time", "Night");
                                                    morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);

                                                    morcmd.CommandType = CommandType.Text;
                                                    morcmd.Connection = con9;
                                                    con9.Open();
                                                    MySqlDataReader morreader = morcmd.ExecuteReader();
                                                    monnighttable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                                    monnighttable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Total Night Expenditure</th></tr>");
                                                    if (morreader.HasRows)
                                                    {
                                                        if (morreader.Read())
                                                        {
                                                            monnighttable.Append("<tr>");

                                                            monnighttable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["sum(PriceOfItem)"] + "</td>");

                                                            monnighttable.Append("</tr>");
                                                        }
                                                        monnighttable.Append("</table>");

                                                        totalnightmonexpense.Controls.Add(monnightdiv);

                                                        monnightdiv.Controls.Add(new Literal
                                                        {
                                                            Text = monnighttable.ToString()
                                                        });

                                                        morreader.Close();
                                                        morreader.Dispose();
                                                        morcmd.Dispose();
                                                        con9.Close();
                                                        con9.Dispose();
                                                    }
                                                }

                                                //code to find the expense done on the health
                                                using (MySqlConnection healthcon = new MySqlConnection(connstring))
                                                {
                                                    using (MySqlCommand healthcmd = new MySqlCommand("select sum(PriceOfItem) from mydatabase.dateofpurchase,mydatabase.requirement where mydatabase.requirement.Transaction_Id=mydatabase.dateofpurchase.Transaction_Id and mydatabase.requirement.Product_Id=mydatabase.dateofpurchase.Product_Id and month(mydatabase.dateofpurchase.DateofPurchase)=?listvalue and mydatabase.requirement.Requirement=?requirement;"))
                                                    {
                                                        healthcmd.Parameters.AddWithValue("?requirement", "Health");
                                                        healthcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);

                                                        healthcmd.CommandType = CommandType.Text;
                                                        healthcmd.Connection = healthcon;
                                                        healthcon.Open();
                                                        MySqlDataReader healthreader = healthcmd.ExecuteReader();
                                                        healthttable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                                        healthttable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Health Expense</th></tr>");
                                                        if (healthreader.HasRows)
                                                        {
                                                            if (healthreader.Read())
                                                            {
                                                                healthttable.Append("<tr>");

                                                                healthttable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + healthreader["sum(PriceOfItem)"] + "</td>");

                                                                healthttable.Append("</tr>");
                                                            }
                                                            healthttable.Append("</table>");

                                                            healthmonexpense.Controls.Add(healthdiv);

                                                            healthdiv.Controls.Add(new Literal
                                                            {
                                                                Text = healthttable.ToString()
                                                            });

                                                            healthreader.Close();
                                                            healthreader.Dispose();
                                                            healthcmd.Dispose();
                                                            healthcon.Close();
                                                            healthcon.Dispose();
                                                        }
                                                    }

                                                    //code for finding expense done on need for a month
                                                    using (MySqlConnection needcon = new MySqlConnection(connstring))
                                                    {
                                                        using (MySqlCommand needcmd = new MySqlCommand("select sum(PriceOfItem) from mydatabase.dateofpurchase,mydatabase.requirement where mydatabase.requirement.Transaction_Id=mydatabase.dateofpurchase.Transaction_Id and mydatabase.requirement.Product_Id=mydatabase.dateofpurchase.Product_Id and month(mydatabase.dateofpurchase.DateofPurchase)=?listvalue and mydatabase.requirement.Requirement=?requirement;"))
                                                        {
                                                            needcmd.Parameters.AddWithValue("?requirement", "Need");
                                                            needcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);

                                                            needcmd.CommandType = CommandType.Text;
                                                            needcmd.Connection = needcon;
                                                            needcon.Open();
                                                            MySqlDataReader needreader = needcmd.ExecuteReader();
                                                            needttable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                                            needttable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Needy Expense</th></tr>");
                                                            if (needreader.HasRows)
                                                            {
                                                                if (needreader.Read())
                                                                {
                                                                    needttable.Append("<tr>");

                                                                    needttable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + needreader["sum(PriceOfItem)"] + "</td>");

                                                                    needttable.Append("</tr>");
                                                                }
                                                                needttable.Append("</table>");

                                                                needmonexpense.Controls.Add(needdiv);

                                                                needdiv.Controls.Add(new Literal
                                                                {
                                                                    Text = needttable.ToString()
                                                                });

                                                                needreader.Close();
                                                                needreader.Dispose();
                                                                needcmd.Dispose();
                                                                needcon.Close();
                                                                needcon.Dispose();
                                                            }
                                                        }

                                                        //code for finding the details of the most expensive day
                                                        using (MySqlConnection con11 = new MySqlConnection(connstring))
                                                        {
                                                            List<string> itemmaxnames = new List<string>();
                                                            MySqlCommand maxitemdate = new MySqlCommand("SELECT Product_Name FROM mydatabase.product where Product_Id in (select Product_Id from mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue and PriceOfItem in (SELECT max(PriceOfItem) FROM mydatabase.dateofpurchase where month(DateofPurchase) = ?listvalue)) ;");
                                                            maxitemdate.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                                            maxitemdate.CommandType = CommandType.Text;
                                                            maxitemdate.Connection = con11;
                                                            con11.Open();
                                                            MySqlDataReader namereader = maxitemdate.ExecuteReader();
                                                            while (namereader.Read())
                                                            {

                                                                // for each row read a string and add it in
                                                                itemmaxnames.Add(namereader.GetString(0));
                                                            }
                                                            namereader.Close();
                                                            namereader.Dispose();
                                                            maxitemdate.Dispose();
                                                            con11.Close();
                                                            con11.Dispose();

                                                            MySqlCommand pricemaxitemcmd = new MySqlCommand("SELECT max(PriceOfItem) FROM mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue; ");
                                                            pricemaxitemcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                                            pricemaxitemcmd.CommandType = CommandType.Text;
                                                            pricemaxitemcmd.Connection = con11;
                                                            con11.Open();

                                                            Int32 maxitemcmd = !Convert.IsDBNull(pricemaxitemcmd.ExecuteScalar()) ? Convert.ToInt32(pricemaxitemcmd.ExecuteScalar()) : 0;
                                                            //  Int32 maxitemcmd = Convert.ToInt32(pricemaxitemcmd.ExecuteScalar());
                                                            pricemaxitemcmd.Dispose();

                                                            con11.Close();
                                                            con11.Dispose();

                                                            //command to find the date of the most expensive day
                                                            using (MySqlCommand morcmd = new MySqlCommand("SELECT date_format(DateofPurchase,'%W,%d %M %Y') FROM mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue and Product_Id in (select Product_Id from mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue and PriceOfItem in (SELECT max(PriceOfItem) FROM mydatabase.dateofpurchase where month(DateofPurchase) = ?listvalue)) ; "))
                                                            {
                                                                //  morcmd.Parameters.AddWithValue("?time", "Night");
                                                                morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);

                                                                morcmd.CommandType = CommandType.Text;
                                                                morcmd.Connection = con11;
                                                                con11.Open();

                                                                MySqlDataReader morreader = morcmd.ExecuteReader();
                                                                mostexpensetable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                                                mostexpensetable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align:left;background-color: #4CAF50;color: white; margin-top:5px;align:center;width:100px'>Most Expensive Days</th></tr>");
                                                                //  leastexpensetable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Date</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Name of the Item</th><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>Price of the Item</th></tr>");
                                                                if (morreader.HasRows)
                                                                {
                                                                    string dateofmaxexpen = "Date";
                                                                    string namofmaxitem = "Name of Item";
                                                                    string priceofmaxitem = "Price Of Item";
                                                                    mostexpensetable.Append("<td style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>" + dateofmaxexpen + "</td>");
                                                                    while (morreader.Read())
                                                                    {

                                                                        mostexpensetable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["date_format(DateofPurchase,'%W,%d %M %Y')"] + "</td>");

                                                                    }
                                                                    mostexpensetable.Append("<tr>");
                                                                    mostexpensetable.Append("<td style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>" + namofmaxitem + "</td>");
                                                                    for (int i = 0; i < itemmaxnames.Count; i++)
                                                                    {

                                                                        mostexpensetable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + itemmaxnames[i] + "</td>");

                                                                    }
                                                                    mostexpensetable.Append("</tr>");
                                                                    mostexpensetable.Append("<td style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>" + priceofmaxitem + "</td>");

                                                                    for (int i = 0; i < itemmaxnames.Count; i++)
                                                                    {
                                                                        mostexpensetable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + maxitemcmd + "</td>");

                                                                    }

                                                                }

                                                                mostexpensetable.Append("</table>");

                                                                mostexpensiveday.Controls.Add(mostexpensedaydiv);

                                                                mostexpensedaydiv.Controls.Add(new Literal
                                                                {
                                                                    Text = mostexpensetable.ToString()
                                                                });

                                                                morreader.Close();
                                                                morreader.Dispose();
                                                                morcmd.Dispose();
                                                                con11.Close();
                                                                con11.Dispose();
                                                            }

                                                        }

                                                        //code for finding the details of the least expensive day

                                                        using (MySqlConnection con11 = new MySqlConnection(connstring))
                                                        {
                                                            List<string> itemnames = new List<string>();
                                                            MySqlCommand leastitemdate = new MySqlCommand("SELECT Product_Name FROM mydatabase.product where Product_Id in (select Product_Id from mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue and PriceOfItem in (SELECT min(PriceOfItem) FROM mydatabase.dateofpurchase where month(DateofPurchase) = ?listvalue)); ");
                                                            leastitemdate.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                                            leastitemdate.CommandType = CommandType.Text;
                                                            leastitemdate.Connection = con11;
                                                            con11.Open();
                                                            MySqlDataReader namereader = leastitemdate.ExecuteReader();
                                                            while (namereader.Read())
                                                            {

                                                                itemnames.Add(namereader.GetString(0));
                                                            }
                                                            namereader.Close();
                                                            namereader.Dispose();
                                                            leastitemdate.Dispose();
                                                            con11.Close();
                                                            con11.Dispose();

                                                            MySqlCommand priceleastitemcmd = new MySqlCommand("SELECT min(PriceOfItem) FROM mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue; ");
                                                            priceleastitemcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);
                                                            priceleastitemcmd.CommandType = CommandType.Text;
                                                            priceleastitemcmd.Connection = con11;
                                                            con11.Open();

                                                            Int32 lowestitemcmd = !Convert.IsDBNull(priceleastitemcmd.ExecuteScalar()) ? Convert.ToInt32(priceleastitemcmd.ExecuteScalar()) : 0;
                                                            //      Int32 lowestitemcmd = Convert.ToInt32(priceleastitemcmd.ExecuteScalar());
                                                            priceleastitemcmd.Dispose();
                                                            con11.Close();
                                                            con11.Dispose();

                                                            //command to find the date of the least expensive day
                                                            using (MySqlCommand morcmd = new MySqlCommand("SELECT date_format(DateofPurchase,'%W,%d %M %Y') FROM mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue and Product_Id in (select Product_Id from mydatabase.dateofpurchase where month(DateofPurchase)=?listvalue and PriceOfItem in (SELECT min(PriceOfItem) FROM mydatabase.dateofpurchase where month(DateofPurchase) = ?listvalue)) ; "))
                                                            {
                                                                //  morcmd.Parameters.AddWithValue("?time", "Night");
                                                                morcmd.Parameters.AddWithValue("?listvalue", monthlist.SelectedValue);

                                                                morcmd.CommandType = CommandType.Text;
                                                                morcmd.Connection = con11;
                                                                con11.Open();

                                                                MySqlDataReader morreader = morcmd.ExecuteReader();
                                                                leastexpensetable.Append("<table style='font-family:Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse:collapse;'>");
                                                                leastexpensetable.Append("<tr><th style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align:left;background-color: #4CAF50;color: white; margin-top:5px;align:center;width:100px'>Least Expensive Days</th></tr>");

                                                                if (morreader.HasRows)
                                                                {
                                                                    string dateofexpen = "Date";
                                                                    string namofitem = "Name of Item";
                                                                    string pricofitem = "Price Of Item";
                                                                    leastexpensetable.Append("<td style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>" + dateofexpen + "</td>");
                                                                    while (morreader.Read())
                                                                    {

                                                                        leastexpensetable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + morreader["date_format(DateofPurchase,'%W,%d %M %Y')"] + "</td>");

                                                                    }
                                                                    leastexpensetable.Append("<tr>");
                                                                    leastexpensetable.Append("<td style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>" + namofitem + "</td>");

                                                                    for (int i = 0; i < itemnames.Count; i++)
                                                                    {

                                                                        leastexpensetable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + itemnames[i] + "</td>");

                                                                    }
                                                                    leastexpensetable.Append("</tr>");
                                                                    leastexpensetable.Append("<td style='border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #4CAF50;color: white; margin-top:5px;'>" + pricofitem + "</td>");

                                                                    for (int p = 0; p < itemnames.Count; p++)
                                                                    {
                                                                        leastexpensetable.Append("<td style=' border: 1px solid #ddd;padding: 8px;background-color: 	#D3D3D3'>" + lowestitemcmd + "</td>");

                                                                    }

                                                                }

                                                                leastexpensetable.Append("</table>");

                                                                leastexpensiveday.Controls.Add(leastexpensedaydiv);

                                                                leastexpensedaydiv.Controls.Add(new Literal
                                                                {
                                                                    Text = leastexpensetable.ToString()
                                                                });

                                                                morreader.Close();
                                                                morreader.Dispose();
                                                                morcmd.Dispose();
                                                                con11.Close();
                                                                con11.Dispose();
                                                            }

                                                        }

                                                    }
                                                    if (!(monthtotal.Read()))
                                                    {

                                                        ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                                                        ClientScript.RegisterStartupScript(Page.GetType(), "nodatatomonthdisplay", "<script language='javascript' >alertMX('No Records to display!');</script>");

                                                    }

                                                }

                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //  }
            //  }

            catch (MySqlException ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }

        }

    }

    protected void paymentreceived(object sender, EventArgs e)
    {

        try
        {

            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            using (MySqlConnection con = new MySqlConnection(connstring))
            {
                string name = personlist.SelectedItem.Text;
                if (name == "Divya")
                {

                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.divya"))

                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }
                else if (name == "Jyotsna")
                {
                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.Jyotsna"))

                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }
                else if (name == "Keshvam")
                {
                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.Keshvam"))

                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }
                else if (name == "Mitali")
                {
                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.Mitali"))

                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }
                else if (name == "Monika")
                {
                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.Monika"))

                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }
                else if (name == "Navdeep")
                {
                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.Navdeep"))

                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }
                else if (name == "Neeraj")
                {
                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.Neeraj"))

                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }
                else if (name == "Seema")
                {
                    using (MySqlCommand cmd = new MySqlCommand("truncate mydatabase.Seema"))

                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader data = cmd.ExecuteReader();

                        con.Close();
                        cmd.Dispose();

                        con.Dispose();
                    }
                }

                ClientScript.RegisterStartupScript(Page.GetType(), "validationdata", "<script language='javascript' >disableall();</script>");
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data has been Succesfully Submitted!');window.location.replace('Group.aspx');</script>");
                ClientScript.RegisterStartupScript(Page.GetType(), "datasubmitted", "<script language='javascript' >alertMX('Records Cleared successfuly!');</script>");
                resetfields();
            }

        }

        catch (MySqlException ex)
        {
            Console.WriteLine("{0}+Mysql exceptions", ex);
        }

    }

    protected void resetfields()
    {

        foreach (ListItem item in personlist.Items)
        {

            if (item.Selected) item.Selected = false;
        }
    }

    protected void resetpersonlist()
    {
        personlist.SelectedValue = "";
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;



public partial class Self : System.Web.UI.Page
{
    MySql.Data.MySqlClient.MySqlConnection con;
    MySql.Data.MySqlClient.MySqlCommand cmd;
    string querystrone;
    string querystrtwo;
    string querystrthree;
    string querystr;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {

                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
                using (MySqlConnection con = new MySqlConnection(connString))
                {

                    using (MySqlCommand cmd = new MySqlCommand("SELECT Product_Id, Product_Name FROM mydatabase.product order by Product_Name"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        Itemlist.DataSource = cmd.ExecuteReader();
                        Itemlist.DataTextField = "Product_Name";
                        Itemlist.DataValueField = "Product_Id";
                        Itemlist.DataBind();
                        con.Close();

                    }
                }
                Itemlist.Items.Insert(0, new ListItem("--Select Item--", "0"));
                Itemlist.Items.Add(new ListItem("New Item", "-1"));

            }


            catch (MySqlException ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

        }

    }

    protected void logoutevent(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Default.aspx");

    }

    protected void sumbt_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && additem.Enabled == false)
        {
            selfdatainsertion();

        }

    }

    private void selfdatainsertion()
    {
        MySqlConnection sc = null;
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            sc = new MySql.Data.MySqlClient.MySqlConnection(connString);
            DateTime dt = Convert.ToDateTime(dateofexpense.Text);
            string st = dt.ToString("yyyy-MM-dd");
            sc.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem) values(@Product_Id,@DateofPurchase,@PriceOfItem)", sc);
            cmd.Parameters.AddWithValue("@Product_Id", Itemlist.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@DateofPurchase", st);
            cmd.Parameters.AddWithValue("@PriceOfItem", priceofitem.Text);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            MySqlCommand cmdone = new MySqlCommand("INSERT INTO mydatabase.requirement(Product_Id,Requirement,DateofPurchase) values(@Product_Id,@Requirement,@DateofPurchase)", sc);
            cmdone.Parameters.AddWithValue("@Product_Id", Itemlist.SelectedItem.Value);
            cmdone.Parameters.AddWithValue("@Requirement", requirementlist.SelectedItem);
            cmdone.Parameters.AddWithValue("@DateofPurchase", st);
            cmdone.ExecuteNonQuery();
            cmdone.Dispose();

            MySqlCommand cmdtwo = new MySqlCommand("INSERT INTO mydatabase.timeofpurchase(Product_Id,PurchaseTime,DateofPurchase) values(@Product_Id,@PurchaseTime,@DateofPurchase)", sc);
            cmdtwo.Parameters.AddWithValue("@Product_Id", Itemlist.SelectedItem.Value);
            cmdtwo.Parameters.AddWithValue("@PurchaseTime", purchaseList.SelectedItem);
            cmdtwo.Parameters.AddWithValue("@DateofPurchase", st);
            cmdtwo.ExecuteNonQuery();
            cmdtwo.Dispose();

            ClientScript.RegisterStartupScript(Page.GetType(), "datadone", "<script language='javascript' >alert('Data Submitted successfully!');window.location.replace('Self.aspx');</script>");

        }

        catch (MySqlException e)
        {
            Console.WriteLine("{0} Exception caught.", e);
        }

        finally // clean up
        {
            if (cmd != null)
                cmd.Dispose();
            if (sc != null)
                sc.Close();
        }

    }

    protected void itemsubmit(object sender, EventArgs e)
    {
        insertnewitem();
    }

    private void insertnewitem()
    {
        MySqlConnection sc = null;
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            sc = new MySql.Data.MySqlClient.MySqlConnection(connString);
            sc.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM mydatabase.product", sc);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            Int32 newcount = count + 1;
            MySqlCommand newcmd = new MySqlCommand("INSERT INTO mydatabase.product(Product_Id,Product_Name) values(@Product_Id,@Product_Name)", sc);
            newcmd.Parameters.AddWithValue("@Product_Id", newcount);
            newcmd.Parameters.AddWithValue("@Product_Name", itemTextBox.Text);
            newcmd.ExecuteNonQuery();
            newcmd.Dispose();
            sc.Close();
            Itemlist.SelectedIndex = 0;
            itemTextBox.Text = "";
            itemTextBox.Enabled = false;
            additem.Enabled = false;
            ClientScript.RegisterStartupScript(Page.GetType(), "datasubmitted", "<script language='javascript' >itemalertMX('New Item added');</script>");
            dateofexpense.Enabled = true;
            priceofitem.Enabled = true;
            requirementlist.Enabled = true;
            purchaseList.Enabled = true;
        }
        catch (MySqlException e)
        {
            Console.WriteLine("{0} Exception caught.", e);
        }
        finally
        {
            if (cmd != null)
                cmd.Dispose();
            if (sc != null)
                sc.Close();
        }
    }

    private void disableinputs()
    {
        dateofexpense.Enabled = false;
        priceofitem.Enabled = false;
        requirementlist.Enabled = false;
        purchaseList.Enabled = false;
        sumbt.Enabled = false;
    }

    private void enableinputs()
    {
        dateofexpense.Enabled = true;
        priceofitem.Enabled = true;
        requirementlist.Enabled = true;
        purchaseList.Enabled = true;
        sumbt.Enabled = true;
    }
    private void enablenewiteminputs()
    {
        itemTextBox.Enabled = true;
        itemTextBox.Focus();
        itemTextBox.Style.Add("border-color", "black");
        additem.Enabled = true;
        additem.Attributes.Add("class", "button");
        // Itemlist.Enabled = false;
    }


    private void disalbenewiteminputs()
    {

        Itemlist.Enabled = true;
        additem.Enabled = false;
        itemTextBox.Text = "";
        itemTextBox.Style.Add("border-color", "none");
        itemTextBox.Enabled = false;
        dateofexpense.Enabled = true;
        priceofitem.Enabled = true;
        requirementlist.Enabled = true;
        purchaseList.Enabled = true;
        sumbt.Enabled = true;
    }

    private void clearinputs()
    {
        dateofexpense.Text = "";
        priceofitem.Text = "";
        requirementlist.SelectedIndex = -1;
        purchaseList.SelectedIndex = -1;
    }

    protected void Itemlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Check = Itemlist.SelectedItem.Text;
        if (Check == "New Item")
        {
            clearinputs();
            enablenewiteminputs();
            disableinputs();
        }
        else
        {
            disalbenewiteminputs();
        }
    }
}








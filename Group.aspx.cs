using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;

public partial class Group : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



    }
    protected void logoutevent(object sender, EventArgs e)
    {
        //original code
        //FormsAuthentication.SignOut();
        //Session.Remove("uname");
        //Session.RemoveAll();
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Default.aspx");



    }

    protected void sumbt_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            groupdatainsertion();
        }

    }

    private void groupdatainsertion()
    {
        int count = 0;
        for (int i = 0; i < personsinvolved.Items.Count; i++)
        {
            if (personsinvolved.Items[i].Selected == true)
            {
                count = count + 1;
            }
        }

        int price;
        price = Convert.ToInt32(priceofitem.Text);
        int newprice = price / count;
        //datelabel.Text = "Selectd Item =" + count.ToString();
        //priceofitemlabel.Text = "price to be distributed =" + newprice.ToString();



        string selecteditem = "";

        foreach (ListItem item in personsinvolved.Items)
        {
            if (item.Selected)
            {
                selecteditem += item.Text + ",";

            }
        }
        //nameofitemlabel.Text = "selected persons =" + selecteditem;
        MySqlConnection sc = null;
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            sc = new MySql.Data.MySqlClient.MySqlConnection(connString);
            DateTime dt = Convert.ToDateTime(dateofexpense.Text);
            string st = dt.ToString("yyyy-MM-dd");
            sc.Open();

            if (selecteditem.Contains("Divya"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.divya(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (selecteditem.Contains("Jyotsna"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.jyotsna(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (selecteditem.Contains("Keshvam"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.keshvam(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (selecteditem.Contains("Mitali"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.mitali(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (selecteditem.Contains("Monika"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.monika(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (selecteditem.Contains("Navdeep"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.navdeep(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (selecteditem.Contains("Neeraj"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.neeraj(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (selecteditem.Contains("Seema"))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mydatabase.seema(DateofExpense,NameofItem,PriceofItem) values(@DateofExpense,@NameofItem,@PriceOfItem)", sc);
                /*      string query1 = "INSERT INTO mydatabase.dateofpurchase(Product_Id,DateofPurchase,PriceOfItem)" + " VALUES(@Product_Id,@DateofPurchase,@PriceOfItem)"; */
                cmd.Parameters.AddWithValue("@DateofExpense", st);
                cmd.Parameters.AddWithValue("@NameofItem", nameofitem.Text);
                cmd.Parameters.AddWithValue("@PriceofItem", newprice);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            ClientScript.RegisterStartupScript(Page.GetType(), "validationdata", "<script language='javascript' >disableall();</script>");
            //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data has been Succesfully Submitted!');window.location.replace('Group.aspx');</script>");
            ClientScript.RegisterStartupScript(Page.GetType(), "datasubmitted", "<script language='javascript' >alertMX('Data has been Succesfully Submitted!');</script>");
            //  ClientScript.RegisterStartupScript(Page.GetType(), "validationdata", "<script language='javascript' >resetall();</script>");
            resetfields();

        }
        catch (MySqlException e)
        {
            Console.WriteLine("{0} Exception caught.", e);
        }





    }

    protected void resetfields()
    {
        dateofexpense.Text = string.Empty;
        nameofitem.Text = string.Empty;
        priceofitem.Text = string.Empty;
        foreach (ListItem item in personsinvolved.Items)
        {

            if (item.Selected)
                item.Selected = false;
        }
    }
}















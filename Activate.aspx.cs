using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Security;

public partial class Default2 : System.Web.UI.Page
{
    MySql.Data.MySqlClient.MySqlConnection con;
    MySql.Data.MySqlClient.MySqlCommand cmd;
    MySql.Data.MySqlClient.MySqlDataReader reader;
    string querystr;
    string name;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void active_code_Click(object sender, EventArgs e)
    {
        activateuser();
    }

    private void activateuser()
    {

        try
        {


            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            con = new MySql.Data.MySqlClient.MySqlConnection(connString);
            con.Open();
            querystr = "";
            querystr = "SELECT * FROM mydatabase.activation_table WHERE Email_id=?email AND Activation_Code=?code";
            cmd = new MySqlCommand(querystr, con);
            cmd.Parameters.AddWithValue("?email", email.Text);
            cmd.Parameters.AddWithValue("?code", code.Text);
            reader = cmd.ExecuteReader();
            name = "";
            while (reader.HasRows && reader.Read())
            {
                name = reader.GetString(reader.GetOrdinal("Activation_Code"));
            }
            if (reader.HasRows)
            {
                Session["uname"] = name;
                Response.BufferOutput = true;
                Response.Redirect("Registration.aspx", false);
            }
            else
            {
                email.Text = "";
                code.Text = "";
                ClientScript.RegisterStartupScript(Page.GetType(), "disablevalidation", "<script language='javascript' >disableall();</script>");
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript' >alertMX('Invalid Email / Activation Code!');</script>");
            }

            reader.Close();
            con.Close();
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





















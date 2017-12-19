using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Web.UI.HtmlControls;


public partial class _Default : System.Web.UI.Page
{
    MySql.Data.MySqlClient.MySqlConnection con;
    MySql.Data.MySqlClient.MySqlCommand cmd;
    MySql.Data.MySqlClient.MySqlDataReader reader;
    string querystr;
    //string name;


    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Title = "Save Money";
        //HtmlMeta tag = new HtmlMeta();
        //tag.Name = "Description";
        //tag.Content = "Save money for financial crisis";
        //Header.Controls.Add(tag);

        //HtmlMeta keywordtag = new HtmlMeta();
        //keywordtag.Name = "keywords";
        //keywordtag.Content = "Money,Saving,Crisis";
        //Header.Controls.Add(keywordtag);






        this.theForm.Focus();
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        //Response.Cache.SetNoStore();
        visitorlabel.Text = Application["NoOfVisitors"].ToString();
        if (!IsPostBack)
        {
            if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            {
                username.Text = Request.Cookies["UserName"].Value;
                password.Attributes["value"] = Request.Cookies["Password"].Value;
            }
        }

        //ClientScript.RegisterStartupScript(Page.GetType(), "validationuser", "<script language='javascript' >myfunction();</script>");




    }

    //code for calling of this method from dynamic div to enable controls in page

    [System.Web.Services.WebMethod]
    public void EnableForm(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Enabled = true;
            if (ctrl is Button)
                ((Button)ctrl).Enabled = true;
            //else if (ctrl is DropDownList)
            //    ((DropDownList)ctrl).Enabled = false;
            else if (ctrl is CheckBox)
                ((CheckBox)ctrl).Enabled = true;
            else if (ctrl is HtmlAnchor)
            {
                HtmlAnchor a = ctrl as HtmlAnchor;
                a.Disabled = false;
            }
            //else if (ctrl is HtmlInputButton)
            //    ((HtmlInputButton)ctrl).Disabled = true;
            //else if (ctrl is HtmlInputText)
            //    ((HtmlInputText)ctrl).Disabled = true;
            //else if (ctrl is HtmlSelect)
            //    ((HtmlSelect)ctrl).Disabled = true;
            //else if (ctrl is HtmlInputCheckBox)
            //    ((HtmlInputCheckBox)ctrl).Disabled = true;
            //else if (ctrl is HtmlInputRadioButton)
            //    ((HtmlInputRadioButton)ctrl).Disabled = true;

            EnableForm(ctrl.Controls);
        }
    }



    protected void submitEventMethod(object sender, EventArgs e)
    {
        //userlogin(); old code
        LoginWithPasswordHashFunction();

    }
    //new code for hash login with password

    private void LoginWithPasswordHashFunction()
    {
        List<string> salthashList = null;
        List<string> namesList = null;
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            con = new MySql.Data.MySqlClient.MySqlConnection(connString);
            con.Open();
            querystr = "SELECT slowHashSalt, Username FROM mydatabase.registertable WHERE Username=?uname";
            cmd = new MySqlCommand(querystr, con);
            cmd.Parameters.AddWithValue("?uname", username.Text);
            reader = cmd.ExecuteReader();
            string name = "";
            if (reader.HasRows && reader.Read())
            {



                if (salthashList == null)
                {
                    salthashList = new List<string>();
                    namesList = new List<string>();
                }

                string saltHashes = reader.GetString(reader.GetOrdinal("slowHashSalt"));
                salthashList.Add(saltHashes);

                name = reader.GetString(reader.GetOrdinal("Username"));
                namesList.Add(name);

            }




            reader.Close();




            if (salthashList != null)
            {
                for (int i = 0; i < salthashList.Count; i++)
                {
                    querystr = "";
                    bool validuser = PasswordStorage.VerifyPassword(password.Text, salthashList[i]);

                    {

                        if (validuser == true)
                        {

                            if (name == "hardeepsinghnegi")
                            {
                                //Session["uname"] = name; old code
                                Session["uname"] = name;

                                Response.BufferOutput = true;
                                Response.Redirect("Login.aspx", false);
                            }

                            else
                            {
                                Session["uname"] = name;
                                Response.BufferOutput = true;
                                Response.Redirect("Guest.aspx", false);
                            }
                        }
                        else
                        {
                            username.Text = "";
                            if (!(chkBoxRememberMe.Checked))
                            {
                                chkBoxRememberMe.Checked = false;
                            }

                            ClientScript.RegisterStartupScript(Page.GetType(), "validationuser", "<script language='javascript' >alertMX('Invalid Username/Password!');</script>");

                            //ClientScript.RegisterStartupScript(Page.GetType(), "validationuser", "myfunctionclick()",true);
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validationuser", "<script language='javascript' >myfunction();</script>");
                            //new code for ui dialog
                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "myfunctionclick();",true);
                            //usererror.Text = "Invalid Username/Password!";
                        }

                        con.Close();
                    }
                }

                if (chkBoxRememberMe.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies["UserName"].Value = username.Text.Trim();
                Response.Cookies["Password"].Value = password.Text.Trim();
            }
            else
            {
                username.Text = "";
                if ((chkBoxRememberMe.Checked == true))
                {
                    chkBoxRememberMe.Checked = false;
                }
                //DisableForm(Page.Controls); 

                ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");
                ClientScript.RegisterStartupScript(Page.GetType(), "validationuser", "<script language='javascript' >alertMX('Invalid Username/Password !');</script>");
                //ClientScript.RegisterStartupScript(Page.GetType(), "validationuser2", "<script language='javascript' >disableall();</script>");

                //DisableControls();

            }


        }





        catch (Exception ex)
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
    //new code for disabling controls
    /*  public void DisableForm(ControlCollection ctrls)
      {
          foreach (Control ctrl in ctrls)
          {
              if (ctrl is TextBox)
                  ((TextBox)ctrl).Enabled = false;
              if (ctrl is Button)
                  ((Button)ctrl).Enabled = false;
              //else if (ctrl is DropDownList)
              //    ((DropDownList)ctrl).Enabled = false;
              else if (ctrl is CheckBox)
                  ((CheckBox)ctrl).Enabled = false;
              else if (ctrl is HtmlAnchor)
              {
                  HtmlAnchor a = ctrl as HtmlAnchor;
                  a.Disabled = true;
              }
              //else if (ctrl is HtmlInputButton)
              //    ((HtmlInputButton)ctrl).Disabled = true;
              //else if (ctrl is HtmlInputText)
              //    ((HtmlInputText)ctrl).Disabled = true;
              //else if (ctrl is HtmlSelect)
              //    ((HtmlSelect)ctrl).Disabled = true;
              //else if (ctrl is HtmlInputCheckBox)
              //    ((HtmlInputCheckBox)ctrl).Disabled = true;
              //else if (ctrl is HtmlInputRadioButton)
              //    ((HtmlInputRadioButton)ctrl).Disabled = true;

              DisableForm(ctrl.Controls);
          }
      }

  */
}

/*    private void userlogin()

      {
          try

          {

              string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
              con = new MySql.Data.MySqlClient.MySqlConnection(connString);
              con.Open();
              querystr = "";
              //querystr = "SELECT * FROM mydatabase.registertable WHERE Username='" + username.Text + "' AND Password='" + password.Text + "'";
              querystr = "SELECT * FROM mydatabase.registertable WHERE Username=?uname AND Password=?pword";
              cmd = new MySqlCommand(querystr, con);
              cmd.Parameters.AddWithValue("?uname", username.Text);
              cmd.Parameters.AddWithValue("?pword", password.Text);
              reader = cmd.ExecuteReader();
              name = "";
              while (reader.HasRows && reader.Read())
              {
                  name = reader.GetString(reader.GetOrdinal("Username"));
              }
              if (reader.HasRows)
              {
                  if (name == "hardeep")
                  {
                      Session["uname"] = name;
                      Response.BufferOutput = true;
                      Response.Redirect("Login.aspx", false);
                  }
                  else
                  {
                      Session["uname"] = name;
                      Response.BufferOutput = true;
                      Response.Redirect("Guest.aspx", false);
                  }


              }
              else
              {
                  //valSum.HeaderText = "Invalid Username/Password!";
                  ////passReq.ErrorMessage = "Invalid Password!";
                  ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript' >alertMX('Invalid Username/Password!');</script>");
                  //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Record is not updated');", true);

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





  }*/

//new code
class InvalidHashException : Exception
{
    public InvalidHashException() { }
    public InvalidHashException(string message)
        : base(message) { }
    public InvalidHashException(string message, Exception inner)
        : base(message, inner) { }
}

class CannotPerformOperationException : Exception
{
    public CannotPerformOperationException() { }
    public CannotPerformOperationException(string message)
        : base(message) { }
    public CannotPerformOperationException(string message, Exception inner)
        : base(message, inner) { }
}

class PasswordStorage
{
    // These constants may be changed without breaking existing hashes.
    public const int SALT_BYTES = 24;
    public const int HASH_BYTES = 18;
    public const int PBKDF2_ITERATIONS = 64000;

    // These constants define the encoding and may not be changed.
    public const int HASH_SECTIONS = 5;
    public const int HASH_ALGORITHM_INDEX = 0;
    public const int ITERATION_INDEX = 1;
    public const int HASH_SIZE_INDEX = 2;
    public const int SALT_INDEX = 3;
    public const int PBKDF2_INDEX = 4;

    public static string CreateHash(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[SALT_BYTES];
        try
        {
            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(salt);
            }
        }
        catch (CryptographicException ex)
        {
            throw new CannotPerformOperationException(
                "Random number generator not available.",
                ex
            );
        }
        catch (ArgumentNullException ex)
        {
            throw new CannotPerformOperationException(
                "Invalid argument given to random number generator.",
                ex
            );
        }

        byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);

        // format: algorithm:iterations:hashSize:salt:hash
        String parts = "sha1:" +
            PBKDF2_ITERATIONS +
            ":" +
            hash.Length +
            ":" +
            Convert.ToBase64String(salt) +
            ":" +
            Convert.ToBase64String(hash);
        return parts;
    }

    public static bool VerifyPassword(string password, string goodHash)
    {
        char[] delimiter = { ':' };
        string[] split = goodHash.Split(delimiter);

        if (split.Length != HASH_SECTIONS)
        {
            throw new InvalidHashException(
                "Fields are missing from the password hash."
            );
        }

        // We only support SHA1 with C#.
        if (split[HASH_ALGORITHM_INDEX] != "sha1")
        {
            throw new CannotPerformOperationException(
                "Unsupported hash type."
            );
        }

        int iterations = 0;
        try
        {
            iterations = Int32.Parse(split[ITERATION_INDEX]);
        }
        catch (ArgumentNullException ex)
        {
            throw new CannotPerformOperationException(
                "Invalid argument given to Int32.Parse",
                ex
            );
        }
        catch (FormatException ex)
        {
            throw new InvalidHashException(
                "Could not parse the iteration count as an integer.",
                ex
            );
        }
        catch (OverflowException ex)
        {
            throw new InvalidHashException(
                "The iteration count is too large to be represented.",
                ex
            );
        }

        if (iterations < 1)
        {
            throw new InvalidHashException(
                "Invalid number of iterations. Must be >= 1."
            );
        }

        byte[] salt = null;
        try
        {
            salt = Convert.FromBase64String(split[SALT_INDEX]);
        }
        catch (ArgumentNullException ex)
        {
            throw new CannotPerformOperationException(
                "Invalid argument given to Convert.FromBase64String",
                ex
            );
        }
        catch (FormatException ex)
        {
            throw new InvalidHashException(
                "Base64 decoding of salt failed.",
                ex
            );
        }

        byte[] hash = null;
        try
        {
            hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
        }
        catch (ArgumentNullException ex)
        {
            throw new CannotPerformOperationException(
                "Invalid argument given to Convert.FromBase64String",
                ex
            );
        }
        catch (FormatException ex)
        {
            throw new InvalidHashException(
                "Base64 decoding of pbkdf2 output failed.",
                ex
            );
        }

        int storedHashSize = 0;
        try
        {
            storedHashSize = Int32.Parse(split[HASH_SIZE_INDEX]);
        }
        catch (ArgumentNullException ex)
        {
            throw new CannotPerformOperationException(
                "Invalid argument given to Int32.Parse",
                ex
            );
        }
        catch (FormatException ex)
        {
            throw new InvalidHashException(
                "Could not parse the hash size as an integer.",
                ex
            );
        }
        catch (OverflowException ex)
        {
            throw new InvalidHashException(
                "The hash size is too large to be represented.",
                ex
            );
        }

        if (storedHashSize != hash.Length)
        {
            throw new InvalidHashException(
                "Hash length doesn't match stored hash length."
            );
        }

        byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
        return SlowEquals(hash, testHash);
    }

    private static bool SlowEquals(byte[] a, byte[] b)
    {
        uint diff = (uint)a.Length ^ (uint)b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
        {
            diff |= (uint)(a[i] ^ b[i]);
        }
        return diff == 0;
    }

    private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
    {
        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
        {
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}






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
using System.Text;
using System.Security.Cryptography;


public partial class Registration : System.Web.UI.Page
{

    MySql.Data.MySqlClient.MySqlConnection con;
    MySql.Data.MySqlClient.MySqlCommand cmd;
    string querystr;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void sumbt_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //registerUser(); before modifying code
            registerUserWithSlowHash();//new code 
        }

    }
    //new code from crackstation

    private void registerUserWithSlowHash()
    {
        try
        {

            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
            con = new MySql.Data.MySqlClient.MySqlConnection(connString);
            con.Open();
            querystr = "";

            querystr = "INSERT INTO mydatabase.registertable(Username,DOB,Mobile,slowHashSalt)" +
               "VALUES(?username,?datebirth,?mob,?slowhashsalt)";
            cmd = new MySqlCommand(querystr, con);
            cmd.Parameters.AddWithValue("?username", Username.Text);
            cmd.Parameters.AddWithValue("?datebirth", dob.Text);
            cmd.Parameters.AddWithValue("?mob", mob.Text);

            string saltHashReturned = PasswordStorage.CreateHash(passwd.Text);
            int commaIndex = saltHashReturned.IndexOf(":");
            string extractedString = saltHashReturned.Substring(0, commaIndex);
            commaIndex = saltHashReturned.IndexOf(":");
            extractedString = saltHashReturned.Substring(commaIndex + 1);
            commaIndex = extractedString.IndexOf(":");
            string salt = extractedString.Substring(0, commaIndex);

            commaIndex = extractedString.IndexOf(":");
            extractedString = extractedString.Substring(commaIndex + 1);
            string hash = extractedString;
            //from the first : to the second : is the salt
            //from the second : to the end is the hash
            cmd.Parameters.AddWithValue("?slowhashsalt", saltHashReturned);
            cmd.ExecuteReader();
            con.Close();
            //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('You have been Succesfully Registered! Click O.K to navigate to Homepage.');window.location.replace('Default.aspx');</script>");
            cmd.Dispose();
            clearfields();
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript' >alertMX('Registered Succesfully! Click OK');</script>");
            //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript' >myalert('Test', 'This is a test modal dialog');</script>");
        }
        catch (MySqlException reg)
        {
            Console.WriteLine("{0}+MySql Exceptions", reg);
        }
        finally
        {
            if (!(con == null))
            {
                con.Dispose();
            }
        }

    }

    private void clearfields()
    {
        Username.Text = "";
        dob.Text = "";
        mob.Text = "";

    }

    /*   private void registerUser()
          {
              string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MywebConnection"].ToString();
              con = new MySql.Data.MySqlClient.MySqlConnection(connString);
              con.Open();
              querystr = "";
              DateTime dt = Convert.ToDateTime(dob.Text);
              string st = dt.ToString("yyyy-MM-dd");

              querystr = "INSERT INTO mydatabase.registertable(Username,DOB,Mobile,Password)" +
                  "VALUES(?username,?datebirth,?mob,?password)";

              //querystr = "INSERT INTO mydatabase.registertable(Username,DOB,Mobile,Password)"+
              //    "VALUES('" + Username.Text + "','" + dob.Text + "','" + mob.Text + "','" + passwd.Text + "')";
              cmd = new MySqlCommand(querystr, con);
              cmd.Parameters.AddWithValue("?username", Username.Text);
              cmd.Parameters.AddWithValue("?datebirth", dob.Text);
              cmd.Parameters.AddWithValue("?mob", mob.Text);
              cmd.Parameters.AddWithValue("?password", passwd.Text);
              cmd.ExecuteReader();
              con.Close();
              ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>dialog('You have been Succesfully Registered! Click O.K to navigate to Homepage.');window.location.replace('Default.aspx');</script>");
          }

              */

    //code for SHA algorithm
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
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class AddToDB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void DataBtn_Click(object sender, EventArgs e)
    {
        //the database data we wish to enter
        //StreamReader langfile = new StreamReader("C:\\Users\\Zimmy\\Documents\\Snippit\\Languages\\" + TextBox1.Text);
        StreamReader langfile = new StreamReader(Directory.GetCurrentDirectory() + @"\Languages\" + TextBox1.Text);

        //Establish an SQL connection
        string connectString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Zimmy\Documents\Snippit\App_Data\Database.mdf;Integrated Security=True;User Instance=True;Asynchronous Processing=true";
        SqlConnection conn = new SqlConnection(connectString);
        conn.Open();

        string command;                 //the string contating the SQL command we wish to execute
        SqlCommand insert;              //the SQL command that will be executed

        string[] cols = new string[4];  //holds each column value for each row of data

        Regex splitter = new Regex(@"(\t)");    //each column of data is separated by a tab
        string line;                            //a line of text that is read in
        
        while((line = langfile.ReadLine()) != null)
        {
            cols = splitter.Split(line);    //split line up by tabs (NOTE: This makes each tab into a token as well.
                                            //                              Must figure out a way around this.

            //Insert the row of data into the database
            command = "INSERT INTO " + DropDownList2.SelectedValue + " VALUES ('" + cols[0] + "', '" + cols[2] + "', '" + cols[4] + "','" + cols[6] + "')";
            insert = new SqlCommand(command, conn);
            insert.ExecuteNonQuery();
            
        }

        //close the SQL connection
        conn.Close();

        //close the StreamReader
        langfile.Close();

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
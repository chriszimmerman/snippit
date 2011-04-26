using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


public partial class CodePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request.QueryString["code"]; //gets the unique url to fetch the code from the database

        //connect to the database and fetch the code out of the database
        string connectString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Zimmy\Documents\Snippit\App_Data\Database.mdf;Integrated Security=True;User Instance=True;Asynchronous Processing=true";
        SqlConnection conn = new SqlConnection(connectString);
        SqlDataReader codeRetriever = null;

        conn.Open();
        SqlCommand getCode = new SqlCommand("select * from Code where url='" + url + "'", conn);
        codeRetriever = getCode.ExecuteReader();

        //if we found a code entry in the database, print it out. Otherwise, say that the file's not found
        if (codeRetriever.HasRows)
        {
            codeRetriever.Read();

            //set the code to a string variable and print it to the page
            string theCode = codeRetriever[3].ToString();
            theCode.Replace(" ", "&nbsp;");
            string fcode = "";


            string[] tokens = System.Text.RegularExpressions.Regex.Split(theCode, "\n");

            fcode += "<table cellspacing=\"0\"> " +
                    " <caption>Code:</caption>" +
                    "<colgroup id=\"numcol\" span=\"1\" ></colgroup>";

            for (int i = 0; i < tokens.Length; i++)
            {
                fcode += codeline(i, tokens[i]);
            }

            fcode += "</table>";

            fcode = "<pre>" + fcode + "</pre>";
            
            Label1.Text = fcode;
        }
        else
        {
            Label1.Text = "No file found.";
        }

        conn.Close();
    }


    public string codeline(int i, string line)
    {
        if ((i % 2) == 0)
        {
            return "<tr> <td><div>" + i +
                    "</div></td><td><div id=\"evenline\"> &nbsp;" +
                    line + "</div></td> </tr>";
        }
        else
            return "<tr> <td><div>" + i +
                    "</div></td><td><div id=\"oddline\"> &nbsp;" +
                    line + "</div></td> </tr>";
    }

}
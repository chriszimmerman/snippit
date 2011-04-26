using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

public partial class Home : System.Web.UI.Page
{
    public String Link
    {
        get
        {
            return Link;
        }
        set
        {
            Link = value;
        }

    }

    public int Lang
    {
        get
        {
            return DropDownList1.SelectedIndex;
        }
        set
        {
            value = DropDownList1.SelectedIndex;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {}

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lang = DropDownList1.SelectedIndex;
    }

    protected void CodeButton_Click1(object sender, EventArgs e)
    {   
        string url = genURL();                                          //randomly generated URL string
        string code = _codeText.Text;                                   //the code entered into the text box
        string formatted;                                               //the code after it has been formatteds

        if (DropDownList1.SelectedValue == "none")
        {
            formatted = code;
        }
        else
        {
            formatted = format(code, DropDownList1.SelectedValue);
        }

        //Establish an SQL connection
        string connectString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Zimmy\Documents\Snippit\App_Data\Database.mdf;Integrated Security=True;User Instance=True;Asynchronous Processing=true";
        SqlConnection conn = new SqlConnection(connectString);
        conn.Open();

        SqlParameter urlP = new SqlParameter();
        SqlParameter sourceP = new SqlParameter();
        SqlParameter languageP = new SqlParameter();
        SqlParameter formattedP = new SqlParameter();

        urlP.ParameterName = "@url";
        urlP.Value = url;

        sourceP.ParameterName = "@source";
        sourceP.Value = code;

        languageP.ParameterName = "@language";
        languageP.Value = DropDownList1.SelectedValue;

        formattedP.ParameterName = "@formatted";
        formattedP.Value = formatted;

        //Insert the url, code, and language into the database
        string command = "INSERT INTO Code (url, source, language, formatted) VALUES (@url, @source, @language, @formatted)";
        SqlCommand insert = new SqlCommand(command, conn);
        insert.Parameters.Add(urlP);
        insert.Parameters.Add(sourceP);
        insert.Parameters.Add(languageP);
        insert.Parameters.Add(formattedP);
        insert.ExecuteNonQuery();
        
        //close the SQL connection
        conn.Close();

        string dest = "CodePage.aspx?";
        dest += "code=" + url;
        Response.Redirect(dest);  //uncomment this when tokens + format works
    }

    //Generate a unique url
    public string genURL()
    {
        Random num = new Random((int)DateTime.Now.Ticks);                           //used to generate random numbers
        int length = 10;                                                            //length of the url string
        
        string _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";     //possible characters in the string
        char[] aurl = new char[length];                                             //character array that will hold the url

        //generate x characters to make a random url
        for (int i = 0; i < length; i++)
        {
            aurl[i] = _chars[num.Next(_chars.Length)];
        }

        return new string(aurl);
    }

    //return formatted input code for the appropriate language
    public string format(string input, string language)
    {
        //read language-specific tokens/keywords and HTML tags from the file we want
        StreamReader langfile = new StreamReader("C:\\Users\\Zimmy\\Documents\\Snippit\\Languages\\" + language + ".txt");

        //sorted lists containing each keyword or token to format and their corresponding HTML tags
        SortedList<string, string> openers = new SortedList<string, string>();
        SortedList<string, string> closers = new SortedList<string, string>();

        string[] cols = new string[3];  //holds each value for each row of data

        char[] delimiters = {'\t'};             //each value is separated by tab
        string line;                            //a line of text that is read in

        //read in each line, placing the keyword/token and
        //it's opening or closing HTML tag into the appropriate SortedList
        while((line = langfile.ReadLine()) != null)
        {
            cols = line.Split(delimiters);
            openers.Add(cols[0], cols[1]);
            closers.Add(cols[0], cols[2]);
        }

        //close the StreamReader
        langfile.Close();

        //read language-specific tokens/keywords and HTML tags from the file we want
        StreamReader delfile = new StreamReader("C:\\Users\\Zimmy\\Documents\\Snippit\\Languages\\" + language + "dels.txt");

        string del;                            //delimiter that is read in
        string dels = @"";                     //list of delimiters that breaks the code into tokens

        //get each delimiter from the delimiter file and add it to the list
        del = delfile.ReadLine();
        dels += "(" + del.ToString() + ")";
        while ((del = delfile.ReadLine()) != null)
        {
            dels += "|(" + del.ToString() + ")";
        }

        //close the StreamReader
        delfile.Close();

        string separators = @"(\"")|( )|(&)|(%)|($)|(#)|(!)|(\-)|(//)|(/\*)|(\')|(\*\/)|(\()|(\))|(\*)|(,)|(\.)|(:)|(;)|(\?)|(@)|(\[)|(\])|(^)|(`)|(\{)|(\|)|(\})|(~)|(\+)|(<)|(=)|(\n)|(\t)|(>)";

        //string[] tokens = System.Text.RegularExpressions.Regex.Split(input, separators);
        string[] tokens = System.Text.RegularExpressions.Regex.Split(input, dels);

        string fcode = "";

        foreach (string token in tokens)
        {
            if (openers.ContainsKey(token))
            {

                fcode += openers.ElementAt(openers.IndexOfKey(token)).Value + token
                        + closers.ElementAt(closers.IndexOfKey(token)).Value;
            }
            else
            {
                fcode += token;
            }
        }

        return fcode;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

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
    {

    }



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lang = DropDownList1.SelectedIndex;
    }

    protected void CodeButton_Click1(object sender, EventArgs e)
    {   
       // _codeText.Text.Replace('\'','\'');
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
        
        //Insert the url, code, and language into the database
        string command = "INSERT INTO Code (url, source, language, formatted) VALUES ('" + url + "', '" + code + "', '" + DropDownList1.SelectedItem + "','" + formatted + "')";
        SqlCommand insert = new SqlCommand(command, conn);
        insert.ExecuteNonQuery();
        
        //close the SQL connection
        conn.Close();

        /*string dest = "CodePage.aspx?";
        dest += "code=" + url;
        Response.Redirect(dest);*/ //uncomment this when tokens + format works
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
        SortedList<string, string> symbols = new SortedList<string, string>();

        string connectString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Zimmy\Documents\Snippit\App_Data\Database.mdf;Integrated Security=True;User Instance=True;Asynchronous Processing=true";
        SqlConnection conn = new SqlConnection(connectString);
        SqlDataReader tokenRetriever = null;
        conn.Open();
        SqlCommand getTokens = new SqlCommand("select * from Tokens where language='"+ DropDownList1.SelectedValue + "'", conn);
        tokenRetriever = getTokens.ExecuteReader();

        string seps = @"";

        tokenRetriever.Read();
        seps += "(" + tokenRetriever[1].ToString() + ")";
        while (tokenRetriever.Read())
        {
            seps += "|(" + tokenRetriever[1].ToString() + ")";
        }

        //string separators = @"(\*\/)|(\()|(\))|(\*)|(,)|(\.)|(:)|(;)|(\?)|(@)|(\[)|(\])|(^)|(`)|(\{)|(\|)|(\})|(~)|(\+)|(<)|(=)|(\n)|(\t)|(>)";
        string separators = @"(\"")|( )|(&)|(%)|($)|(#)|(!)|(\-)|(//)|(/\*)|(\')";

        string[] tokens = System.Text.RegularExpressions.Regex.Split(input, separators);

        _codeText.Text = "";
        foreach (string token in tokens)
        {
            _codeText.Text += token + "\n";
        }

        _codeText.Text += seps;
        
       /* string openers = @"";
        string closers = @"";

        _codeText.Text = separators;*/

        return input;
    }

    /*    
    public string format_java(string input)
    {
        string seps = @"(\t)|(\n)|(\+)|(-)|(\*)|(/)|(\()|(\))|(\})|(\{)|( )|(;)|(\[)|(\])";
        string[] tokens = System.Text.RegularExpressions.Regex.Split(input, seps);

        string temp;
        string output = "";

        SortedSet<string> keywords = new SortedSet<string>();
        keywords.Add("abstract");
        keywords.Add("assert");
        keywords.Add("boolean");
        keywords.Add("break");
        keywords.Add("byte");
        keywords.Add("case");
        keywords.Add("catch");
        keywords.Add("char");
        keywords.Add("class");
        keywords.Add("const");
        keywords.Add("continue");
        keywords.Add("default");
        keywords.Add("do");
        keywords.Add("double");
        keywords.Add("else");
        keywords.Add("enum");
        keywords.Add("extends");
        keywords.Add("final");
        keywords.Add("finally");
        keywords.Add("float");
        keywords.Add("for");
        keywords.Add("goto");
        keywords.Add("if");
        keywords.Add("implements");
        keywords.Add("import");
        keywords.Add("instanceof");
        keywords.Add("int");
        keywords.Add("interface");
        keywords.Add("long");
        keywords.Add("native");
        keywords.Add("new");
        keywords.Add("package");
        keywords.Add("private");
        keywords.Add("protected");
        keywords.Add("public");
        keywords.Add("return");
        keywords.Add("short");
        keywords.Add("static");
        keywords.Add("strictfp");
        keywords.Add("super");
        keywords.Add("switch");
        keywords.Add("synchronized");
        keywords.Add("this");
        keywords.Add("throw");
        keywords.Add("throws");
        keywords.Add("transient");
        keywords.Add("try");
        keywords.Add("void");
        keywords.Add("volatile");
        keywords.Add("while");

        SortedSet<string> containers = new SortedSet<string>();
        containers.Add(")");
        containers.Add("(");
        containers.Add("}");
        containers.Add("{");
        containers.Add("]");
        containers.Add("[");

        foreach (string token in tokens)
        {
            temp = token;

            if (containers.Contains(temp))
            {
                temp = "<font color=\"red\">" + temp + "</font>";
            }
            if (keywords.Contains(temp))
            {
                temp = "<b><font color=\"blue\">" + temp + "</font></b>";
            }

            output = String.Concat(output, temp);
        }

        return output;
    }*/

}
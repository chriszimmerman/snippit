using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CodePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.PreviousPage != null)
        {
            string code = PreviousPage.Name;
            int lang = PreviousPage.Lang;
            code = format(code, lang);
            Label1.Text = "<pre>" + code + "<pre>";
        }
    }

    public string format(string input, int language)
    {
        switch(language)
        {
            case 0:
                input = format_reg(input);
                break;
            case 1:
                input = format_java(input);
                break;
        }
        return input;
    }

    public string format_reg(string input)
    {
        input = input.Replace(Environment.NewLine, "<br/>");
        input = input.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

        return input;
    }

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
    }

}
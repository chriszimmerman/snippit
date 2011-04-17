using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CodeHandler : System.Web.UI.UserControl
{
    String formattedText = String.Empty;

    public String Text
    {
        get
        {
            return CodeText.Text;
        }
        set
        {
            CodeText.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CodeButton_Click(object sender, EventArgs e)
    {

    }
    protected void CodeText_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
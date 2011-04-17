using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    public String Name
    {
        get
        {
            return _codeText.Text;
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


    protected void CodeButton_Click(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lang = DropDownList1.SelectedIndex;
    }
}
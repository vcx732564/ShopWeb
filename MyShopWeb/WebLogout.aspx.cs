using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebLogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Account"] != null)
            {
                Session.Remove("Account");
                Response.Redirect("IndexFrame.aspx",false);
            }


        }
        catch (Exception ex)
        {

            throw new Exception("登出時發生錯誤" + ex.Message);
        }
        
    }
}
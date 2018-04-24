using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class WebTop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        try
        {
            Table TopTable = new Table();
            TableRow tr = new TableRow();
            TableCell HomeTc = new TableCell();

            

            if (Session["Account"] == null)
            {
                //Response.Write("<script>window.open('Default.aspx','MainPage')</script>");
            }
            else
            {
                //Response.Write("<script>window.open('index.html','MainPage')</script>");
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("<Table>");
            sb.Append("     <tr><td><input type='Image' id='HomeImg' src='Images/HomeIcon.png'  onclick='GoHomePage();' ></input></td>");
            sb.Append("         <td><div>");

            sb.Append("                  <ul><li><a src='#'>會員專區</a>");
            sb.Append("                      <ul>");
            sb.Append("                         <li><a src='#'>會員資料修改</a><li>");
            sb.Append("                         <li><a src='#'>購買歷程</a><li>");
            sb.Append("                         <li><a src='#'>購買歷程</a><li>");
            sb.Append("                      </ul>");
            sb.Append("                      </li>");
            sb.Append("                  </ul>");

            sb.Append("             </div>");
            sb.Append("         </td>");
            sb.Append("</tr></Table>");

            Response.Write(sb.ToString());

        }
        catch (Exception ex)
        {
            
            throw new Exception("身分驗證時發生錯誤");
        }
       
        */
    }
}
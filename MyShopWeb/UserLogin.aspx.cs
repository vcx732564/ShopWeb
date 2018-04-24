using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data.SqlClient;
using System.Data;

public partial class UserLogin : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            

            //抓值
            string cAccount = Request["Account"];
            string cPassword = Request["Password"];

            clsConn clsConn = new clsConn();
            SqlConnection Conn = clsConn.GetConnection();

            RPaWorkLibrary.Encryption RPa_EN = new RPaWorkLibrary.Encryption();
            //這段要寫認證機制Start--------------------------------
            string sAccount = Request["Account"];
            string sPassword = Request["Password"];

            string sCheckPsw = "SELECT cPassword FROM DEF_UserBook WHERE cAccount = @Account";
            SqlCommand sc_CheckUser = new SqlCommand(sCheckPsw, Conn);
            sc_CheckUser.Parameters.Add(new SqlParameter("@Account", SqlDbType.NVarChar));

            sc_CheckUser.Parameters["@Account"].Value = sAccount;

            SqlDataReader sdr_CheckUser;
            sdr_CheckUser = sc_CheckUser.ExecuteReader();
            DataTable dt_CheckUser = new DataTable();
            dt_CheckUser.Load(sdr_CheckUser);


            if (dt_CheckUser.Rows.Count <= 0)
            {
                Response.Write("<script>alert('帳號或密碼輸入錯誤');</script>");
                Response.Write("<script>location.href='Default.aspx';</script>");
                Response.End();
            }

            string sEncryptionPws = RPa_EN.EnCodeString(sPassword);

            if (dt_CheckUser.Rows[0]["cPassword"].ToString() != sEncryptionPws)
            {
                Response.Write("<script>alert('帳號或密碼輸入錯誤');</script>");
                Response.Write("<script>location.href='Default.aspx';</script>");
                Response.End();
            }

            //認證中.... 
            //這段要寫認證機制End----------------------------------
            Session["Account"] = cAccount;
            Response.Write("<script>window.parent.location.reload();</script>");

        }
        catch (Exception ex)
        {

            throw new Exception("帳號驗證時發生錯誤 - " + ex.Message);
        }



    }
}
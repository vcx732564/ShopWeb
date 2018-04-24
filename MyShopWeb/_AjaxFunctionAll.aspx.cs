using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

public partial class _AjaxFunctionAll : System.Web.UI.Page
{
    clsConn clsConn = new clsConn();
    protected void Page_Load(object sender, EventArgs e)
    {
        string sFunctionName = Request["FunctionName"];
        SqlConnection Conn = clsConn.GetConnection();
        RPaWorkLibrary.Encryption RPa_EN = new RPaWorkLibrary.Encryption();

        switch (sFunctionName)
        {
            case "AccountApply":

                string sInputAccount = Request["CheckValue"];
                string sSql = "SELECT cAccount FROM DEF_UserBook WHERE cAccount = @Account";

                SqlCommand sc = new SqlCommand(sSql, Conn);

                sc.Parameters.Add(new SqlParameter("@Account", SqlDbType.NVarChar));

                sc.Parameters["@Account"].Value = sInputAccount;

                SqlDataReader sdr;
                sdr = sc.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(sdr);

                if (dt.Rows.Count <= 0)
                {
                    Response.Write("OK");
                }
                else
                {
                    Response.Write("此帳號已重複");
                }

                sc.Dispose();
                sdr.Dispose();
                dt.Dispose();

                break;


            case "CheckUser":

                string sAccount = Request["CheckAcc"];
                string sPasswoed = Request["CheckPsw"];

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
                    Response.Write("帳號或密碼輸入錯誤");
                    break;
                }

                string sEncryptionPws = RPa_EN.EnCodeString(sPasswoed);

                if (dt_CheckUser.Rows[0]["cPassword"].ToString() != sEncryptionPws)
                {
                    Response.Write("帳號或密碼輸入錯誤");
                    break;
                }

                Response.Write("OK");

                break;

            default:
                Response.Write("錯誤的參數名稱");

                break;

        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    //主要的table
    Table tblMain = new Table();

    clsConn clsConn = new clsConn();

    protected void Page_Load(object sender, EventArgs e)
    {

        Ajax.Utility.RegisterTypeForAjax(typeof(_Default));



        /*
        if (IsPostBack != true)
        {
            tblMain.CssClass = "tbl";
            tblMain.Caption = "帳號登入";
            DataTable dt = GetFieldDefine();
            BuildField(dt);
            //form1.Action = "UserLogin.aspx";
        }
        */
    }




    /*Ajax 測試
    [Ajax.AjaxMethod]
    public string getmyinfo()
    {

        return "YesYouCan";
    }*/

    #region --取得欄位定義(已無使用)--
    /// <summary>
    /// 取的欄位定義
    /// </summary>
    /// <returns></returns>
    private DataTable GetFieldDefine()
    {
        //要回傳的Datatable
        DataTable dtRetrunData = new DataTable();
        //New 連線Class出來

        SqlConnection Conn = clsConn.GetConnection();
        SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM DEF_AllField WHERE cPage='Login_001'", Conn);

        sqlDA.Fill(dtRetrunData);
        form1.Controls.Add(tblMain);
        return dtRetrunData;
    }
    #endregion

    #region --利用製作登入表格(已無使用)--
    /*
    private Boolean BuildField(DataTable dt)
    {


        foreach (DataRow dr in dt.Rows)
        {
            //資料庫撈的一些設定
            string cFieldType = dr["cFieldType"].ToString().Trim();
            string cFieldEng = dr["cFieldName_Eng"].ToString().Trim();
            string cFieldChi = dr["cFieldName_Chi"].ToString().Trim();
            string cFieldDefaultValue = dr["cDefaultValue"].ToString().Trim();
            string cFieldDisable = dr["cDisable"].ToString().Trim();
            int mWidth = int.Parse(dr["mWidth"].ToString().Trim());
            int mHeight = int.Parse(dr["mHeight"].ToString().Trim());

            //欄位標題
            TableRow trTitle = new TableRow();
            TableCell tcTitle = new TableCell();
            tcTitle.Text = cFieldChi;
            trTitle.Controls.Add(tcTitle);
            tblMain.Controls.Add(trTitle);

            //輸入框
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();



            //判斷型態去做動作
            switch (cFieldType)
            {
                case "Textbox":
                    TextBox tb = new TextBox();
                    tb.ID = cFieldEng;
                    tb.Width = mWidth;
                    tb.Height = mHeight;
                    tb.CssClass = "Texboxset";
                    

                    switch (cFieldEng){
                        case "Password":
                        tb.TextMode = TextBoxMode.Password;
                        break;

                        case "Account":
                        //清除預設文字
                        tb.Attributes.Add("onClick", "AccountClick(this)");
                        //清除預設文字 但如果有User輸入的文字就不清除
                        tb.Attributes.Add("onBlur", "AccountClick(this)");
                        break;


                    }

                    //判斷是否有預設值
                    if (cFieldDefaultValue != "")
                    {
                        tb.Text = cFieldDefaultValue;
                    }

                 

                    tc.Controls.Add(tb);
                    tr.Controls.Add(tc);
                    tblMain.Controls.Add(tr);
                    break;

                default:

                    break;
            }

        }

        Button SubmitButton = new Button();
        SubmitButton.Attributes.Add("onClick", "GoSubmit()");
        SubmitButton.Text = "登入";
        SubmitButton.CssClass = "btn";


        TableCell tcBtn = new TableCell();
        TableRow trBtn = new TableRow();



        tcBtn.Controls.Add(SubmitButton);
        trBtn.Controls.Add(tcBtn);
        tblMain.Controls.Add(trBtn);

        Button registerbtn = new Button();
        registerbtn.Attributes.Add("onClick", "ToRegister()");
        registerbtn.Text = "註冊";
        registerbtn.CssClass = "btn";



        TableCell tcBtn2 = new TableCell();
        TableRow trBtn2 = new TableRow();



        tcBtn2.Controls.Add(registerbtn);
        trBtn2.Controls.Add(tcBtn2);
        tblMain.Controls.Add(trBtn2);
        
        return true;

    }

    */
    #endregion
}
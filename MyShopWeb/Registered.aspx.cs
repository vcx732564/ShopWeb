using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Text;


public partial class Registered : System.Web.UI.Page
{
    clsConn clsConn = new clsConn();
    //主要的table
    Table tblMain = new Table();
    //裝欄位的容器Fieldset
    Panel PanelRegistered = new Panel();
    SqlConnection Conn;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Conn = clsConn.GetConnection();
         
            if (IsPostBack != true)
            {

                //table的Css
                tblMain.CssClass = "tbl";
                form1.Action = "Registered.aspx";
                //取得欄位定義的DataTable
                DataTable dt = GetFieldDefine();
                //組合欄位
                BuildField(dt);

                AddControls();
            }
            else
            {

                InsertUserInfo();
                RegisterMsg();

            }


        }
        catch (Exception ex)
        {
            throw new Exception("網頁建置時發生錯誤 - " + ex.Message);
        }
        finally
        {
            //關閉連線
            clsConn.CloseConnectionObject(Conn);
        }


    }

    private bool RegisterMsg()
    {
        RPaWorkLibrary.ActionClass AC = new RPaWorkLibrary.ActionClass();

        string SendEmailAddress = Request["Email"];
        string EmailTitle = "【RPa Shop】註冊信箱認證信";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("親愛的 " + Request["RealName"] + "：");
        sb.AppendLine("感謝您的註冊！請點選以下網址來進行信箱認證，若您沒註冊請忽略此信。");
        sb.AppendLine("http://localhost:1514/testtest/EmailApprove.aspx?");
        sb.AppendLine("　");
        sb.AppendLine("　");
        sb.AppendLine("================================================================");
        sb.AppendLine("RPa Show 感謝您");
        sb.AppendLine("連絡電話：0987654321");
        sb.AppendLine("連絡信箱：vcx732564@yahoo.com.tw");

        HtmlGenericControl Msgdiv = new HtmlGenericControl("div");
        Msgdiv.Attributes.Add("class", "ForRegiestedMsgDiv");

        Msgdiv.InnerText = "阿你不就很膩駭";


        form1.Controls.Add(Msgdiv);
        return true;
    }

    #region --註冊相關處理--
    /// <summary>
    /// 送出註冊資訊後的值做處理
    /// </summary>
    /// <returns></returns>
    private bool InsertUserInfo()
    {
        RPaWorkLibrary.Encryption EN = new RPaWorkLibrary.Encryption();
        

        SortedList<string, string> slUserInfo = new SortedList<string, string>();
        slUserInfo.Add("Account", Request["Account"]);
        slUserInfo.Add("Password", EN.EnCodeString(Request["Password"]));
        slUserInfo.Add("RealName", Request["RealName"]);
        slUserInfo.Add("Email", Request["Email"]);
        slUserInfo.Add("MobilePhone", Request["MobilePhone"]);
        slUserInfo.Add("PasswordCheck", Request["PasswordCheck"]);
        slUserInfo.Add("Sex", Request["Sex"]);
        slUserInfo.Add("Address", Request["Address"]);

        //string InsertFields = "";
        //string InsertValues = ""; 

        //for (int i = 0; i <= slUserInfo.Keys.Count - 1; i++)
        //{
        //    InsertFields += slUserInfo.Keys[i]
        //}
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@"INSERT [dbo].[DEF_UserBook](
                        cAccount,cPassword,cRealName,cPowerLevel,mMoney,cEmail,cAttestation,
                        cMobilePhone,cAddress,mSex,dRegisteredTime
                        )
                        Values(
                        @cAccount,@cPassword,@cRealName,@cPowerLevel,@mMoney,@cEmail,@cAttestation,
                        @cMobilePhone,@cAddress,@mSex,@dRegisteredTime
                        )
        ");
        SqlCommand InsertCom = new SqlCommand(sb.ToString(), Conn);

        InsertCom.Parameters.Add(new SqlParameter("@cAccount", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@cPassword", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@cRealName", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@cPowerLevel", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@mMoney", SqlDbType.Int));
        InsertCom.Parameters.Add(new SqlParameter("@cEmail", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@cAttestation", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@cMobilePhone", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@cAddress", SqlDbType.NVarChar));
        InsertCom.Parameters.Add(new SqlParameter("@mSex", SqlDbType.Int));
        InsertCom.Parameters.Add(new SqlParameter("@dRegisteredTime", SqlDbType.DateTime));

        InsertCom.Parameters["@cAccount"].Value = slUserInfo["Account"];
        InsertCom.Parameters["@cPassword"].Value = slUserInfo["Password"];
        InsertCom.Parameters["@cRealName"].Value = slUserInfo["RealName"];
        InsertCom.Parameters["@cPowerLevel"].Value = "Customer";
        InsertCom.Parameters["@mMoney"].Value = 0;
        InsertCom.Parameters["@cEmail"].Value = slUserInfo["Email"];
        InsertCom.Parameters["@cAttestation"].Value = slUserInfo["Account"] + "_" + Guid.NewGuid().ToString();
        InsertCom.Parameters["@cMobilePhone"].Value = slUserInfo["MobilePhone"];
        InsertCom.Parameters["@cAddress"].Value = slUserInfo["Address"];
        InsertCom.Parameters["@mSex"].Value = slUserInfo["Sex"];
        InsertCom.Parameters["@dRegisteredTime"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //InsertCom.ExecuteNonQuery();

        return true;
    }

    #endregion

    #region --增加按鈕到網頁上的function--
    private string AddControls()
    {
        try
        {
            //註冊按鈕
            Button registerbtn = new Button();
            registerbtn.Text = "註冊";
            registerbtn.CssClass = "btn";
            TableCell tcBtn = new TableCell();
            TableRow trBtn = new TableRow();
            //registerbtn.Attributes.Add("onclick", "CheckSubmit(); return false;");
            tcBtn.Controls.Add(registerbtn);
            trBtn.Controls.Add(tcBtn);
            tblMain.Controls.Add(trBtn);


            //回登入頁按鈕
            Button CancelBack = new Button();
            CancelBack.Text = "取消回登入頁";
            CancelBack.CssClass = "btn";
            TableCell tcBtn2 = new TableCell();
            TableRow trBtn2 = new TableRow();
            //加上Return false 是為了取消postback
            CancelBack.Attributes.Add("onclick", "GoBackLogin(); return false;");
            tcBtn2.Controls.Add(CancelBack);
            trBtn2.Controls.Add(tcBtn2);
            tblMain.Controls.Add(trBtn2);


            return "";
        }
        catch (Exception ex)
        {
            throw new Exception("設定網頁按鈕元件時發生錯誤 - " + ex.Message);
        }

    }
    #endregion

    #region --取得欄位定義--
    /// <summary>
    /// 取的欄位定義
    /// </summary>
    /// <returns></returns>
    private DataTable GetFieldDefine()
    {
        try
        {
            //要回傳的Datatable
            DataTable dtRetrunData = new DataTable();
            //New 連線Class出來
            
            SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM DEF_AllField WHERE cPage='Reg_001' order by mSort", Conn);


            PanelRegistered.GroupingText = "會員註冊";
            form1.Controls.Add(PanelRegistered);

            sqlDA.Fill(dtRetrunData);

            PanelRegistered.Controls.Add(tblMain);
            return dtRetrunData;
        }
        catch (Exception ex)
        {

            throw new Exception("取得欄位定義發生錯誤：" + ex.Message);
        }



    }
    #endregion

    #region --利用製作登入表格--


    private Boolean BuildField(DataTable dt)
    {
        try
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

                //輸入框
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();

                Label lb = new Label();
                lb.Text = cFieldChi + "：";
                tc.Controls.Add(lb);


                //判斷型態去做動作
                switch (cFieldType)
                {

                    case "Textbox":
                        //新增一個textbox
                        TextBox tb = new TextBox();
                        //資料庫設定的Id 
                        tb.ID = cFieldEng;
                        //資料庫設定的長寬
                        tb.Width = mWidth;
                        tb.Height = mHeight;
                        //設定textbox Css
                        tb.CssClass = "Textboxset";

                        //各個註冊的textbox 專門的屬性設定
                        switch (cFieldEng)
                        {

                            //密碼
                            case "Password":
                                tb.TextMode = TextBoxMode.Password;
                                tb.Attributes.Add("placeholder", "請輸入20字內的密碼");
                                break;

                            //密碼再確認
                            case "PasswordCheck":
                                tb.TextMode = TextBoxMode.Password;
                                tb.Attributes.Add("placeholder", "再次輸入密碼");
                                break;

                            //帳號
                            case "Account":
                                tb.Attributes.Add("placeholder", "請輸入20字內的帳號");
                                //只能輸入英數字
                                tb.Attributes.Add("onkeyup", @"value=value.replace(/[\W]/g,'')");
                                //複製的字只能有英數字
                                tb.Attributes.Add("onbeforepaste", @"clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))");
                                break;

                            //姓名
                            case "RealName":
                                tb.Attributes.Add("placeholder", "請輸入姓名");
                                break;

                            //電子信箱
                            case "Email":
                                tb.Attributes.Add("placeholder", "請輸入完整電子信箱xxx@xxxx.xxx.tw");
                                break;

                            //手機
                            case "MobilePhone":
                                tb.Attributes.Add("placeholder", "請輸入手機號碼10碼");
                                //只能輸入數字
                                tb.Attributes.Add("onkeyup", @"value=value.replace(/[^\d]/g,'')"); 
                                //複製的字只能數字
                                tb.Attributes.Add("onbeforepaste", @"clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))");
                                break;

                        }
                        

                        //判斷是否有預設值
                        if (cFieldDefaultValue != "")
                        {
                            tb.Text = cFieldDefaultValue;
                        }
                        //即時檢查
                        tb.Attributes.Add("onblur", "CheckUserInput('" + cFieldEng + "',this)");

                        //tc的Id設定(為了擊時檢查 能在這個tc理面再加上錯誤提示文字)
                        tc.ID = "tc_"+ cFieldEng;
                        tc.Controls.Add(tb);
                        //增加一個顯示的物件html物件
                        //System.Web.UI.HtmlControls.HtmlGenericControl
                        HtmlGenericControl ErrMsgSpan = new HtmlGenericControl("ErrMsgSpan");
                        ErrMsgSpan.ID = "ErrMsg_" + cFieldEng ;
                        ErrMsgSpan.InnerText = "";
                        ErrMsgSpan.Attributes.Add("style", "font-size:14;color:red;padding-left:15px;float:none;transition-duration: 0.4s;-webkit-transition-duration: 0.4s;");


                        tc.Controls.Add(ErrMsgSpan);
                        tr.Controls.Add(tc);
                        tblMain.Controls.Add(tr);
                        break;

                    case "Radio":
                        //單選清單
                        RadioButtonList rbl = new RadioButtonList();
                        //改成跟標題同行 
                        rbl.RepeatLayout = RepeatLayout.Flow;
                        //水平顯示
                        rbl.RepeatDirection = RepeatDirection.Horizontal;
                        //多選清單項目 先不New
                        ListItem Li;
                        //單選清單物件Id
                        rbl.ID = cFieldEng;
                        
                        //開始組單選清單項目 並加進多選清單裡面
                        if (cFieldDefaultValue != "")
                        {
                            //把1|2|3 轉成陣列
                            string[] arrSelect = cFieldDefaultValue.Split('|');
                            foreach (string Item in arrSelect)
                            {
                                string[] arrItemValue = Item.Split('-');

                                Li = new ListItem();
                                Li.Value = arrItemValue[0];
                                Li.Text = arrItemValue[1];
                                rbl.Items.Add(Li);

                            }
                            //單選清單固定預設第一個
                            rbl.SelectedIndex = 0;

                            //tc的Id設定(為了擊時檢查 能在這個tc理面再加上錯誤提示文字)
                            tc.ID = "tc_" + cFieldEng;
                            tc.Controls.Add(rbl);
                            tr.Controls.Add(tc);
                            tblMain.Controls.Add(tr);
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("欄位組合時發生錯誤：" + ex.Message);
        }

        return true;

    }


    #endregion




}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class TestGridView : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (IsPostBack != true)
            {
                SetGV();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void SetGV()
    {
        //建立連線
        string ConnStr = "Data Source=TFB2008R2-DEV,3301;Initial Catalog=CEN;User ID=orbit;Password=orbit";
        SqlConnection Conn = new SqlConnection(ConnStr);
        Conn.Open();

        //建立DataSet
        DataSet ds = new DataSet();
        SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT top 210 mId,mStaffId,cAccount,cPassword,cBranchId,mLevel,mCountOfPasswordError FROM LST_Staff", Conn);
        sqlDA.Fill(ds);

        //放進ViewState 不會因為PostBack消失 但會消耗網頁直行速度
        ViewState["ds"] = ds;

        //Css設定
        //DataGridView.HeaderStyle.CssClass = "sdf";

        //讓其他的PageIndex是否顯示 
        DataGridView.AllowPaging = true;

        DataGridView.PagerSettings.Mode = PagerButtons.NumericFirstLast;
        DataGridView.PagerSettings.Position = PagerPosition.TopAndBottom;
        DataGridView.PagerStyle.BackColor = System.Drawing.Color.Gray;
        //隔行換色
        //DataGridView.AlternatingRowStyle.BackColor = System.Drawing.Color.LightBlue;
        
        DataGridView.PageIndex = 0;
        ViewState["NowPage"] = 0;
        DataGridView.PageSize = 10;
        
        DataGridView.DataSource = ds;
        DataGridView.DataBind();
        
    }


    protected void PrvePageBtn_Click(object sender, EventArgs e)
    {
        int mNowPage = int.Parse(ViewState["NowPage"].ToString()) -1;
        if (mNowPage != -1) {
            ViewState["NowPage"] = mNowPage.ToString();
            DataGridView.PageIndex = mNowPage;
            DataGridView.DataSource = ViewState["ds"];
            DataGridView.DataBind();
        }
        
    }


    protected void NextPageBtn_Click(object sender, EventArgs e)
    {
        int mNowPage = int.Parse(ViewState["NowPage"].ToString()) + 1;
        if (mNowPage <= DataGridView.PageCount -1) {
            ViewState["NowPage"] = mNowPage.ToString();
            DataGridView.PageIndex = mNowPage;
            DataGridView.DataSource = ViewState["ds"];
            DataGridView.DataBind();
        }
        
    }


    protected void DataGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataGridView.PageIndex = e.NewPageIndex;
        DataGridView.DataSource = ViewState["ds"];
        DataGridView.DataBind();
    }
 
    

    /// <summary>
    /// 加上這段才能把html標籤的東西轉成server物件 沒加的話會Error
    /// 說該物件不是run server
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        ;
    }

}
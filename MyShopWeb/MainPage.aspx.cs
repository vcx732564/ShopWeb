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
using System.IO;

public partial class MainPage : System.Web.UI.Page
{
    clsConn clsConn = new clsConn();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsCallback != true)
            {
                
                SearchData();
                BuildCommodityList();

            }

        }
        catch (Exception ex)
        {
            throw new Exception("載入網頁主頁面發生問題 - " + ex.Message);
        }
    }

    protected void SearchData()
    {


        try
        {
            //要回傳的Datatable
            DataTable dtRetrunData = new DataTable();

            //New 連線Class出來
            SqlConnection Conn = clsConn.GetConnection();
            SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM [dbo].[DEF_TitleImages] order by mSort", Conn);

            sqlDA.Fill(dtRetrunData);
            //開始建立圖片幻燈片架構(用CSS來控制成幻燈片效果)
            BuildImage(dtRetrunData);


            Conn.Dispose();
            dtRetrunData.Dispose();
            sqlDA.Dispose();

        }
        catch (Exception ex)
        {
            throw new Exception("查詢時發生錯誤 - " + ex.Message);
        }


    }

    /// <summary>
    /// 建立幻燈片架構 (建完架構後 用CSS來形成幻燈片效果)
    /// 都用html的 img標籤放圖片跟一些幻燈片控制碼
    /// </summary>
    /// <param name="dt"></param>
    protected void BuildImage(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            //商品名稱
            string cName = dr["cName"].ToString().Trim();
            //商品說明
            string cMemo = dr["cMemo"].ToString().Trim();
            //特價前價格(會有刪除線)
            string cBeforePrice = dr["mBeforePrice"].ToString().Trim();
            //特價後價格
            string cDiscountsPrice = dr["mDiscountsPrice"].ToString().Trim();
            //圖片url
            string cImagesUrl = dr["cImagesUrl"].ToString().Trim();
            //商品或活動代碼
            string cActivityKey = dr["cActivityKey"].ToString().Trim();

            //價錢前面加NT
            if (cBeforePrice != "")
            {
                cBeforePrice = "NT " + cBeforePrice;
            }

            //價錢前面加NT
            if (cDiscountsPrice != "")
            {
                cDiscountsPrice = "NT " + cDiscountsPrice;
            }

            //建立html標籤 VerifyRenderingInServerForm 這個function要有
            //要不然沒辦法宣告成server物件
            HtmlGenericControl Img = new HtmlGenericControl("Img");
            Img.Attributes.Add("data-cycle-title", cName);
            Img.Attributes.Add("data-cycle-desc", cMemo);
            Img.Attributes.Add("data-cycle-beforeprice", cBeforePrice);
            Img.Attributes.Add("data-cycle-discounts", cDiscountsPrice);
            Img.Attributes.Add("src", cImagesUrl);
            Img.Attributes.Add("onclick", "GoPage('" + cName + "')");
            SlideshowDiv.Controls.Add(Img);
        }



    }

    protected void BuildCommodityList()
    {
        try
        {
            string CommoditySql = " SELECT * FROM All_Commodity ";
            SqlConnection Conn = clsConn.GetConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDA = new SqlDataAdapter(CommoditySql, Conn);
            sqlDA.Fill(dt);



            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                HtmlGenericControl ul = new HtmlGenericControl("ul");
                HtmlGenericControl li_Image = new HtmlGenericControl("li");
                HtmlGenericControl li_Title = new HtmlGenericControl("li");
                HtmlGenericControl li_Price = new HtmlGenericControl("li");
                HtmlGenericControl li_Note = new HtmlGenericControl("li");
                HtmlGenericControl Commodity_Images = new HtmlGenericControl("Img");
                HtmlGenericControl Link_a = new HtmlGenericControl("a");

                ul.Attributes.Add("class", "CommodityList_ul");
                li_Image.Attributes.Add("class", "CommodityList_li_Image");
                li_Title.Attributes.Add("class", "CommodityList_li_Title");
                li_Price.Attributes.Add("class", "CommodityList_li_Price");
                li_Note.Attributes.Add("class", "CommodityList_li_Note");


                string ImageUrl = (Request.Url.AbsoluteUri).Replace(System.IO.Path.GetFileName(Request.PhysicalPath), "");
                Commodity_Images.Attributes.Add("src", ImageUrl + @"Images/CommodityImage/Small/" + dt.Rows[RowIndex]["cListImageFileName"].ToString());
                
                StringBuilder generatedHtml = new StringBuilder();
                string ImageHtmlText = "";
                using (var htmlStringWriter = new StringWriter(generatedHtml))
                {
                    using (var htmlTextWriter = new HtmlTextWriter(htmlStringWriter))
                    {
                        Commodity_Images.RenderControl(htmlTextWriter);
                        ImageHtmlText = generatedHtml.ToString();
                    }
                }
                Link_a.InnerHtml = ImageHtmlText;
                Link_a.Attributes.Add("href", "#");
                li_Title.InnerHtml = dt.Rows[RowIndex]["cChName"].ToString();
                li_Price.InnerHtml ="NT." + dt.Rows[RowIndex]["mPrice"].ToString();
                li_Note.InnerHtml = dt.Rows[RowIndex]["cCommodity_Info"].ToString();

                //Link_a.Controls.Add(li_Image);
                li_Image.Controls.Add(Link_a);
                ul.Controls.Add(li_Image);
                ul.Controls.Add(li_Title);
                ul.Controls.Add(li_Price);
                ul.Controls.Add(li_Note);
                GoodsList.Controls.Add(ul);


            }
        }
        catch (Exception ex)
        {

            throw new Exception("商品清單建立失敗!失敗原因:" + ex.Message);
        }

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
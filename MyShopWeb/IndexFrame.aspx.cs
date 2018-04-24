using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
public partial class IndexFrame : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] WebTopTitle = { "回首頁", "登入狀態", "會員專區", "購物車" };


        /*開始建立階層清單 這邊只建立出ul li的樹狀結構
          第一層1
          第一層2
                 第二層
                 第二層
                 第二層 第三層
                 第二層
          第一層3
          藉由CSS變成
          -------------------------------------
          第一層1  第一層2  第一層3
          -------------------------------------
                  -第二層-
                  -第二層-
                  -第二層--第三層-
                  -第二層-
         
         PS:ul視為一層
         */
        foreach (string Title in WebTopTitle){
            //第一層
            HtmlGenericControl ul = new HtmlGenericControl("ul");
            HtmlGenericControl li = new HtmlGenericControl("li");
            
            switch (Title)
            {
                case "回首頁":
                    
                    ImageButton ImgBtn = new ImageButton();
                    ImgBtn.ImageUrl = "Images/HomeIcon.png";
                    ImgBtn.Attributes.Add("onclick", "GohomePage();return false;");
                    //ImgBtn.Attributes.Add("runat", "server");
                    ImgBtn.Width = 50;
                    LeftMenu.Controls.Add(ImgBtn);
                    break;
                    


                case "會員專區":
                    HtmlGenericControl HyperLinkA = new HtmlGenericControl("a");
                    HyperLinkA.InnerText=Title;
                    HyperLinkA.Attributes.Add("href", "#");
                    li.Controls.Add(HyperLinkA);

                    //第二層
                    HtmlGenericControl FatherUl = new HtmlGenericControl("ul");
                    

                    SortedList<string, string> UserFunctionList = new SortedList<string, string>();
                    UserFunctionList.Add("修改會員資料", "UserFunction/ModifyUserInfo.aspx");
                    UserFunctionList.Add("查詢購買紀錄", "UserFunction/boughtList.aspx");
                    


                    foreach (KeyValuePair<string, string> condition in UserFunctionList)
                    {
                        HtmlGenericControl ChildLi = new HtmlGenericControl("li");
                        HtmlGenericControl HyperLinkAForDrowList = new HtmlGenericControl("a");
                        HyperLinkAForDrowList.Attributes.Add("href", condition.Value.ToString());
                        HyperLinkAForDrowList.Attributes.Add("target", "MainPageIfrmae");
                        HyperLinkAForDrowList.InnerText = condition.Key.ToString();
                        ChildLi.Controls.Add(HyperLinkAForDrowList);

                        if (condition.Key.ToString() == "修改會員資料")
                        {
                            //第三層
                            HtmlGenericControl testul = new HtmlGenericControl("ul");
                            
                            for (int i = 0; i <= 2; i++)
                            {
                                HtmlGenericControl testli = new HtmlGenericControl("li");
                                HtmlGenericControl testa = new HtmlGenericControl("a");
                                testa.Attributes.Add("href", condition.Value.ToString());
                                testa.Attributes.Add("target", "MainPageIfrmae");
                                testa.InnerText = "TEST" + i.ToString();
                                testli.Controls.Add(testa);
                                testul.Controls.Add(testli);
                            }
                            ChildLi.Controls.Add(testul);
                        }
                        FatherUl.Controls.Add(ChildLi); 
                    }

                    li.Controls.Add(FatherUl);
                    ul.Controls.Add(li);
                    LeftMenu.Controls.Add(ul);
                    break;
                    
                case "購物車":
                    //用來算購物車數量
                    Session["CarGoodsCount"] = 0 ;
                    string CarCount = Title + "(" + Session["CarGoodsCount"] + ")";
                    HtmlGenericControl HyperLinkCar = new HtmlGenericControl("a");
                    HyperLinkCar.ID = "ShoppingCar";
                    HyperLinkCar.InnerText = CarCount;
                    HyperLinkCar.Attributes.Add("href", "#");
                    li.Controls.Add(HyperLinkCar);


                    ul.Controls.Add(li);

                    RightMenu.Controls.Add(ul);
                    break;

                case "登入狀態":

                    string IsLoginAccount = "";

                    if (Session["Account"] != null)
                    {
                        IsLoginAccount = Session["Account"] as string;
                        HtmlGenericControl LoginLi = new HtmlGenericControl("a");
                        LoginLi.InnerText = IsLoginAccount;
                        LoginLi.Attributes.Add("href", "#");
                        //第二層
                        HtmlGenericControl FatherUl_ToUserAction = new HtmlGenericControl("ul");
                        HtmlGenericControl ChildLi = new HtmlGenericControl("li");
                        HtmlGenericControl HyperLinkAForDrowList = new HtmlGenericControl("a");
                        HyperLinkAForDrowList.Attributes.Add("href", "WebLogout.aspx");
                        //HyperLinkAForDrowList.Attributes.Add("target", "MainPage");
                        HyperLinkAForDrowList.InnerText = "登出";
                        ChildLi.Controls.Add(HyperLinkAForDrowList);
                        FatherUl_ToUserAction.Controls.Add(ChildLi);

                        li.Controls.Add(LoginLi);
                        li.Controls.Add(FatherUl_ToUserAction);
                        ul.Controls.Add(li);
                        RightMenu.Controls.Add(ul);

                    }else{

                        //沒登入的話 文字訊息改為以登入
                        IsLoginAccount = "請登入";
                        HtmlGenericControl LoginALink = new HtmlGenericControl("a");
                        LoginALink.InnerText = IsLoginAccount;
                        LoginALink.Attributes.Add("href", "Default.aspx");
                        LoginALink.Attributes.Add("target", "MainPageIfrmae");
                        li.Controls.Add(LoginALink);

                    }
                    
                    ul.Controls.Add(li);
                    RightMenu.Controls.Add(ul);
                    break;

                default:

                    break;

            }
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
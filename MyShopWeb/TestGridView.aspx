<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestGridView.aspx.cs" Inherits="TestGridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="Script/jquery-1.12.2.min.js"></script>
    <title></title>
    <script type="text/javascript">
        /*$(function () {
            $("[id*=GridView1] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });*/

        
        //把 ASP.NET內鍵的上下十頁兩個的文字"..." 轉成上下十頁
        $(document).ready(function () {

            //算出 還有沒有最終頁
            var Count = 0;
            $("a").each(function () {
                if ($(this).text() == '>>' ) {
                    Count = Count + 1;
                }
            });

            //先找出所有連結 在網頁上的所有<a>元素
            $("a").each(function () {
                //如果文字是三個點 "..." 才做這個動作
                if (this.innerText.indexOf('...') > -1) {

                    //因為沒定ASP.NET不會定連結的ID 只能自己抓判別字 __javascript("GridViewName","Page$N")
                    //Page$N 為第幾頁連結 從這邊下手
                    var GoTenPage = $(this).attr('href').split("Page$")[1];
                    //上十頁 一般來說會是10的倍數 如果頁數不足的話會是最大頁數-10 下十頁除以10會於1
                    if (parseInt(GoTenPage.replace("')", "")) % 10 == 0 || parseInt(GoTenPage.replace("')", "")) % 10 > 1) {
                        this.innerText = "上十頁";
                        //如果最終頁按下去只剩一頁 那上一頁的指定Page會是餘數=1
                        //ex :  << ... 41     "..."的連結會是上十頁的第31 如果還是判斷餘數是>1 會變成
                        //      << 下十頁 41   所以如果沒有最終頁 頁數又剩1 就要判斷成上十頁
                    } else if (parseInt(GoTenPage.replace("')", "")) % 10 == 1 && Count == 0) {
                        this.innerText = "上十頁";
                    }else {
                        this.innerText = "下十頁";
                    }
                };
            });
        });
        

    </script>
    <style type="text/css">
    

    th {
        background-color: pink;
    }

    tr:hover
    {
        background-color: lightgreen;
    }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--<div>
    <asp:button id="PrvePageBtn" text="上一頁" runat="server" OnClick="PrvePageBtn_Click" />
    <asp:button id="NextPageBtn" text="下一頁" runat="server" OnClick="NextPageBtn_Click" />
    </div>-->
     <asp:Gridview id="DataGridView" Width="100%" PageSize="3"  align="center"  
         runat="server" OnPageIndexChanging="DataGridView_PageIndexChanging" >

         
     </asp:Gridview>

    </form>
    <!-- 自訂換頁 放在GridView裡面
         <PagerTemplate>
             <asp:label runat="server">asdfsdf </asp:label>
         </PagerTemplate>
    -->

</body>
</html>

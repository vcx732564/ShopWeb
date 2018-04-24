<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Script/jquery-1.12.2.min.js"></script>
    <script src="Script/jquery.cycle2.js"></script>
    <link href="Script/CSS/Slideshow.css" rel="stylesheet" />

    <title></title>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $('#cycle-slideshow').cycle({
                fx: 'fadeOut',  //特效           speed:  7500,
                timeout: 200,
                random: 1,
                speed: 150,
                
            });
        });

        function GoPage(txt) {
            //alert(txt);
            //window.open('default.aspx', 'MainPage');
            //document.location.href = 'default.aspx';
            //var c = window.parent.document.getElementById("ShoppingCar");
            //c.innerText = '購物車(1)';
        }

     </script>
    <style>
        * {
            font-family:'Microsoft JhengHei';
        }
    </style>

</head>
    
<body>

    

    <div id="SlideshowDiv" class="cycle-slideshow" 
         data-cycle-fx="fadeOut" 
         data-cycle-caption-plugin="caption2"
         data-cycle-loader="wait"
         data-cycle-overlay-template="<div class=divprice><span class=beforeprice>&nbsp;{{beforeprice}}</span><br>{{discounts}} </div> {{title}}<br>{{desc}}"
         runat="server"
        >

        <!--顯示圓點 可點選跳至指定頁 -->
        <div class="cycle-pager" runat="server"></div>

        <!-- 這段加上去會顯示頁數 例：1/4
        <div class="cycle-caption"></div>
        -->

        <!--顯示黑框文字區域 -->
        <div class="cycle-overlay" runat="server"></div>

    </div>

    <div id="GoodsList" runat="server" class="GoodsListCss">

    </div>

</body>
</html>

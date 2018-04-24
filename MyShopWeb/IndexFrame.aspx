<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndexFrame.aspx.cs" Inherits="IndexFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Script/CSS/WebTopCss.css?a=3" rel="stylesheet" />
    <title></title>

    <script type="text/javascript">
        //導回首頁 Home鍵
        function GohomePage() {
            window.open('MainPage.aspx', 'MainPageIfrmae');
        }

        //讓MainPage的Iframe 能夠因應換頁而自動調整高度
        function resizeIframe(obj) {

            obj.style.height = document.getElementById("MainPageIfrmae").contentDocument.documentElement.scrollHeight + 'px';
        }

    </script>
    <style>
         /*iframe主框架CSS設定*/
        .main {
            padding-top: 50px;
            width: 100%;
            border: 0px;

        }

        /*iframe底部框架的CSS設定*/
        .foot {
            width: 100%;
            border: 0px;
            height: 200px;
            margin-top: -4px;
            margin-bottom: -4px;
        }



    </style>


</head>
<body>


    <div id="LoginTopDiv" class="LoginTopCss" runat="server">
        <div id="LoginTopDivLeft" class="LoginTopLeftCss" runat="server">
            <div id="LeftMenu" class="MenuCss" runat="server">
            </div>
        </div>
        <div id="LoginTopDivRight" class="LoginTopRightCss" runat="server">
            <div id="RightMenu" class="MenuCss" runat="server">
            </div>
        </div>
    </div>
    <div id="MainPageDiv" runat="server" >
        <iframe id="MainPageIfrmae"  name="MainPageIfrmae" class="main" src="MainPage.aspx" onload="resizeIframe(this)"></iframe>
    </div>
    <div id="FooterDiv" runat="server">
        <iframe id="WebFoot" name="WebFoot" class="foot" src="WebBottom.aspx"></iframe>
    </div>
</body>
</html>

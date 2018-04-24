<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="Script/jquery-1.12.2.min.js"></script>
    <script src="Script/jquery.cycle2.js"></script>
    <title></title>

        <script type="text/javascript">

            function go() {
                alert('asda');
            }

        </script>
    <style>
      

        /* set border-box so that percents can be used for width, padding, etc (personal preference) */
        .cycle-slideshow, .cycle-slideshow * { 
            -webkit-box-sizing: border-box; 
            -moz-box-sizing: border-box; 
            box-sizing: border-box; 

        }

        .cycle-slideshow { 
            width: 100%;
            height:400px; 
            min-width: 200px; 
            max-width:1000px; 
            margin: 10px auto; 
            padding: 0; 
            position: relative;
        }

        /* slideshow images (for most of the demos, these are the actual "slides") */
        .cycle-slideshow img { 
            /* 
            some of these styles will be set by the plugin (by default) but setting them here
            helps avoid flash-of-unstyled-content
            */
            position: absolute; 
            top: 0; 
            left: 0;
            width: 100%; 
            padding: 0; 
            display: block;
            height:400px;

        }

        .cycle-slideshow img:hover {
            cursor:pointer;

        }


        /* in case script does not load */
        .cycle-slideshow img:first-child {
            position: static; 
            z-index: 100;
        }

        /* 張數css */
        .cycle-caption { 
            position: absolute; 
            color: white; 
            bottom: 15px; 
            right: 15px; 
            z-index: 700; 

        }

        /*下面的黑框+文字的css*/
        .cycle-overlay { 
            font-family: tahoma, arial;
            position: absolute; 
            bottom: 0; 
            width: 100%; 
            
            /*重疊效果 數字越大圖層越前面*/
            z-index: 600;
            background: black; 
            color: white; 
            padding:15px;
            
            height:100px;
            /*不透明度*/
            opacity: 0.7;
            font-size:25px;

             /*動作時間(IE不支援)*/
            transition-duration: 0.4s;
            -webkit-transition-duration: 0.4s;
        }

    



        /* pager */
        .cycle-pager { 
            text-align: right; 
            width: 100%; 
            z-index: 500; 
            position: absolute; 
            top: 10px; 
            overflow: hidden;
            opacity: 0.3;
            padding-right:15px;
        }

        .cycle-pager span { 
            font-family: arial; 
            font-size: 50px; 
            width: 16px; 
            height: 16px; 
            display: inline-block; 
            color: #ddd; 
            cursor: pointer; 
        }
        .cycle-pager span.cycle-pager-active { 
            color: #D69746;

        }
        .cycle-pager > * { 
            cursor: pointer;

        }


        .divprice{

            float:right;
        }

        .beforeprice{
            font-size:14px;
            text-decoration:line-through;
        }

    </style>

</head>
<body>
    <div id="SlideshowDiv" class="cycle-slideshow" 
         data-cycle-fx="scrollHorz" 
         data-cycle-caption-plugin="caption2"
         data-cycle-loader="wait" 
         data-cycle-timeout="1000"

         data-cycle-overlay-template="<div class=divprice><span class=beforeprice>&nbsp;{{beforeprice}}</span><br>{{discounts}} </div> {{title}}<br>{{  }}"
        runat="server"
        >
    <div class="cycle-pager" runat="server"></div>
    <!--<div class="cycle-caption"></div>-->
    <div class="cycle-overlay" runat="server"></div>


    <img src="C:\Users\Benson\Desktop\testimage\1.jpg" data-cycle-title="Spring" data-cycle-desc="a" data-cycle-beforeprice="NT 2000" data-cycle-discounts="NT 1000" runat="server" onclick="go()" />
    <img src="C:\Users\Benson\Desktop\testimage\2.jpg" data-cycle-title="Redwoods" data-cycle-desc="b" runat="server"/>
    <img src="C:\Users\Benson\Desktop\testimage\3.jpg" data-cycle-title="Angel Island" data-cycle-desc="c" runat="server"/>
    <img src="C:\Users\Benson\Desktop\testimage\4.jpg" data-cycle-title="Raquette Lake" data-cycle-desc="d" runat="server"/>
    


</div>
</body>
</html>

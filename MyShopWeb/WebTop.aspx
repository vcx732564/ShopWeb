<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebTop.aspx.cs" Inherits="WebTop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            margin: 0px;
            padding: 0px;
            overflow: hidden;
            background-color: #44c767;
        }

        input[type=image] {
            width: 45px;
            height: 45px;
        }

        table {
            padding-left: 20px;
        }

        div {
            background-color: green;
        }

            div a {
                text-decoration: none;
                color: white;
                font-size: 20px;
                padding: 15px;
                display: inline-block;
            }

        ul {
            display: inline;
            margin: 0;
            padding: 0;
        }

            ul li {
                display: inline-block;
            }

                ul li:hover {
                    background: #555;
                }

                    ul li:hover ul {
                        display: block;
                    }

                ul li ul {
                    position: absolute;
                    width: 200px;
                    display: none;
                }

                    ul li ul li {
                        background: #555;
                        display: block;
                    }

                        ul li ul li a {
                            display: block !important;
                        }

                        ul li ul li:hover {
                            background: #666;
                        }
    </style>

    <script type="text/javascript">
        function GoHomePage() {

            window.open('index.html', 'MainPageIfrmae');

        }


    </script>

</head>

<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>

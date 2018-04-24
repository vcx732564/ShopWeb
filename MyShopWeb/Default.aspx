<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="X-UA-Compatible" content="IE=11" /> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Script/jquery-1.12.2.min.js"></script>
    <title></title>
    <style>
        body {
            font-family:Microsoft JhengHei;
            background-image:url("Images/shopmall.jpg");
            background-size: cover;
            overflow:hidden;
        }

        .Sp{
            text-align: center;
            color:white;
            font-size:40px;

        }
        .divset {
            padding: 50px 110px 80px 0px;
            
            margin: 0 auto;
            margin-bottom:100px;
            position:relative; 
            text-align:center;
            position:static !important;
            position:absolute;
            top:100%; 
            width:300px;
            border-radius: 7px;
            -webkit-border-radius: 7px;

            /*半透明色*/
            /*hsla(hue 值, saturation 值, lightness 值, alpha 值)*/
            /*hue        360 →紅色， 60 →黃色， 120 →綠色， 240 →藍色*/
            /*saturation 0% ~ 100% 的百分比值， 0% →灰色調， 100% →最大飽和度。所以 0% 時，不論 hue 值是多少，都會以灰階單色呈現 */
            /*lightness  0% ~ 100% 的百分比值， 0% →最暗 (暗黑)， 100% →最亮 (亮白)， 50% →正常亮度。 50% 以上漸漸增加白色， 50% 以下漸漸增加黑色。*/
            /*alpha      0.0 ~ 1.0 允許小數一位的數值。*/
            background-color:hsla(0, 0%, 15%, 0.9) ;


            
            
        }

        .tbl {
            border-collapse: collapse;
            border: 0px solid black;
        }



        td {
            font-size: 25px;
            width: 300px;
            padding-left: 30px;
            padding-right: 30px;
            padding-bottom: 4px;
            padding-top: 4px;
            text-align: left;
        }

        
        input[type=text],input[type=password]{
            background-repeat: no-repeat;
            padding-left: 40px;
            padding: 12px 12px 12px 40px;
            
            font-size: 18px;
            color:white;
            width:300px;
            height:20px;
            text-align:left;

            border-left:0px;
            border-top:0px;
            border-right:0px;
            /*透明背景*/
            background-color:transparent;
            /*不能輸入中文 IE限定語法 
                ime-mode:disabled;*/
           
            -webkit-transition: 0.5s;
            transition: 0.5s;

        }
        .Accountset {
            background-image: url("Images/UserIcon.png");
        }

        .Passwordset {
            background-image: url("Images/PasswordIcon.png");      
        }


        input:focus{
           border-bottom-color:#44c767;
            
        }

        .btn {
            width: 350px;
            font-size: 16px;
            background-color: #44c767;
            border: 0px;
            /*圓邊
            FireFox=-moz-border-radius: 5px;
            Chrome=-webkit-border-radius: 5px;*/
            border-radius: 7px;
            -webkit-border-radius: 7px;
            display: inline-block;
            /*滑鼠鼠標變化*/
            cursor: pointer;
            color: #ffffff;
            /*物件間隔*/
            padding: 8px 76px;
            /*動作時間(IE不支援)*/
            transition-duration: 0.4s;
            -webkit-transition-duration: 0.4s;
        }

            /*Mouse over*/
        .btn:hover {
                background-color: #199010;
        }

        .MsgDivSet{
            height:30px;
            color:red;
            font-size:16px;
            font-family:Microsoft JhengHei;
            /*動作時間(IE不支援)*/
            transition-duration: 0.4s;
            -webkit-transition-duration: 0.4s;
            padding:10px 10px 0px 10px;
        }

    </style>
    <script type="text/javascript">

        /*Ajax 測試
        function testj() {
            var a = document.getElementById('yyyy');
            var myajaxtext = _Default.getmyinfo();

            a.value = myajaxtext.value;

        };*/
        function GoSubmit() {
            var objAccount = document.getElementById('Account');
            var objPassword = document.getElementById('Password');
            var Massage = document.getElementById('MessageDiv');

            if (objAccount.value == '' || objPassword.value == '') {
                Massage.innerText = '帳號或密碼不得為空值';
                return false;
            }

            var Result = CheckUser(objAccount.value, objPassword.value);
            if (Result != "") {
                Massage.innerHTML = Result;
                return false;
            }

            
            
            form1.action = "UserLogin.aspx";
            form1.submit();

        }

        function ToRegister() {
            location.href = 'Registered.aspx';
            //form1.action = "Registered.aspx";
        }


        function CheckUser(InputAccount,InputPsw) {
            var ReturnString = "";
            $.ajax({
                //傳送頁面的方式 Get or Post
                type: "POST",
                //要求資料的網頁
                url: "_AjaxFunctionAll.aspx",
                //傳送給要求網頁的參數
                data: "FunctionName=" + 'CheckUser'  + "&CheckAcc=" + InputAccount + "&CheckPsw=" + InputPsw,
                //定義回傳格式
                dataType: "text",
                //非同步處理 這要加很重要 要不然回傳的Result都是undefined
                async: false,
                //成功時做的事情
                success: function (Result) {
                    if (Result == "OK") {
                        ReturnString = "";
                    } else {
                        ReturnString = Result;
                    }

                },
                //失敗時做的事情
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('使用Ajax時發生錯誤 - ' + xhr.responseText);
                    ReturnString = xhr.responseText;
                }
            });
            return ReturnString;
        }
    </script>
</head>
<body>
    <div class="divset">
        <form id="form1" runat="server" >
            <table class="tbl" id ="table1" >
                <tr>
                    <td>
                        <div class="Sp"><span>Login</span></div>
                    </td>
                </tr>
                

                <tr>
                    <td>
                        <input type="text" id="Account" name="Account" class="Accountset"   placeholder="帳號"/>
                    </td>
                </tr>

                <tr>
                    <td>
                        <input type="password" id="Password"  name="Password"  class="Passwordset" placeholder="密碼"/>
                    </td>
                </tr>
                <tr>
                    <td>
                      <div id="MessageDiv" class="MsgDivSet">

                      </div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <input type="button" onclick="GoSubmit()" value="登入" class="btn"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="button" onclick="ToRegister()" value="註冊" class="btn" />
                    </td>
                </tr>


            </table>

        </form>
        
    </div>
    <!-- Ajax 測試
    <input type="text" id="yyyy" value="sdfdf" />
        <input type="button" value="testwdfwefgergerg" onclick="testj()" />
        -->

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registered.aspx.cs" Inherits="Registered" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title></title>
    <script src="Script/jquery-1.12.2.min.js"></script>
    <script src="Script/jquery.corner.js"></script>
    <link href="Script/CSS/RegisteredCss.css?a=5" rel="stylesheet" />
    <script type="text/javascript">

        //回主頁按鈕
        function GoBackLogin() {
            location.href = "../Default.aspx";
        }

        //檢查使用者輸入的值 
        function CheckUserInput(CheckTypeId, obj) {
            var cErrorMsg = "";
            var cFieldValue = obj.value;
            var Msg = document.getElementById('ErrMsg_' + CheckTypeId);


            //全部欄位都要檢查的
            if (cFieldValue.trim() == '') {

                ErrorShow(Msg, '此欄位不可為空值');
                return false;

            }


            //按照欄位名稱做個別檢查
            switch (CheckTypeId) {
                case 'Account':
                    //檢查是否重複
                    cErrorMsg = CheckDoubleAccountApply(cFieldValue);
                    if (cErrorMsg != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }

                    //檢查特殊符號
                    cErrorMsg = CheckInjection(cFieldValue);
                    if (cErrorMsg != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }

                    //檢查長度
                    cErrorMsg = CheckStringLength(8, 20, cFieldValue);
                    if (cErrorMsg != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }
                    break;

                case 'Password':
                    //檢查長度
                    cErrorMsg = CheckStringLength(8, 20, cFieldValue);
                    if (cErrorMsg != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }

                    //檢查特殊符號
                    cErrorMsg = CheckInjection(cFieldValue);
                    if (cErrorMsg != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }
                    break;
                case 'PasswordCheck':
                    //檢查是否相同
                    var Psw = $('#Password').val();
                    if (Psw != cFieldValue) {
                        cErrorMsg = '與密碼不一致';
                        ErrorShow(Msg, cErrorMsg);
                        return false;

                    } else {

                        Msg.style.color = "lightgreen";
                        Msg.innerHTML = "密碼相同";
                        return true;
                    }

                    break;
                case 'RealName':
                    //檢查長度
                    cErrorMsg = CheckStringLength(1, 20, cFieldValue);
                    if (cErrorMsg != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }
                    break;
                case 'Email':
                    //檢查電子信箱格是
                    cErrorMsg = CheckEmail(cFieldValue);
                    if (cErrorMsg != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }

                    break;
                case 'MobilePhone':
                    //檢查只能數字
                    if (isNaN(cFieldValue) == true) {
                        cErrorMsg = '輸入含非數字字元';
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }
                    //檢查長度
                    if (cFieldValue.length != 10) {
                        cErrorMsg = '請輸入10碼電話';
                        ErrorShow(Msg, cErrorMsg);
                        return false;
                    }
                    //檢查特殊符號
                    if (CheckInjection(cFieldValue) != '') {
                        ErrorShow(Msg, cErrorMsg);
                        return false;

                    }
                    break;

            }



            Msg.style.color = "lightgreen";
            Msg.innerHTML = "可以使用！";
            return true;

        }

        //顯示錯誤資訊(即時) 只能套用在這個WEB 
        function ErrorShow(objMsg, cErrorMsg) {
            objMsg.style.color = "red";
            objMsg.innerHTML = cErrorMsg;
            return false;
        }


        //檢查特殊符號
        function CheckInjection(Value) {
            var ReturnString = "";

            var InjectionString = "~|!|@|#|$|%|^|&|*|(|)|+|,|/|'|`|;|?";
            var ArrCheckInjection = InjectionString.split('|');
            for (i = 0; i <= ArrCheckInjection.length - 1 ; i++) {
                if (Value.indexOf(ArrCheckInjection[i]) >= 0) {

                    ReturnString = '輸入值含不合法字元';

                }
            }

            return ReturnString;
        }

        //確定是否輸入的字數在設定區間
        function CheckStringLength(MinLength, MaxLength, Value) {
            var ReturnString = "";
            if (Value.length < MinLength) {
                ReturnString = '需至少輸入' + MinLength + '個字';
            }

            if (Value.length > MaxLength) {
                ReturnString = '最多輸入' + MaxLength + '個字';
            }

            return ReturnString;

        }

        //檢查電子信箱格式
        function CheckEmail(InputEmail) {
            ReturnString = "";
            //Javascript Email格式檢查代碼
            var emailRegxp = /[\w-]+@([\w-]+\.)+[\w-]+/;
            if (emailRegxp.test(InputEmail) != true) {
                ReturnString = "E-mail格式錯誤";
            }
            return ReturnString;
        }

        function CheckDoubleAccountApply(InputAccount) {
            var ReturnString = "";
            $.ajax({
                //傳送頁面的方式 Get or Post
                type: "POST",
                //要求資料的網頁
                url: "_AjaxFunctionAll.aspx",
                //傳送給要求網頁的參數
                data: "CheckValue=" + InputAccount + "&FunctionName=" + 'AccountApply',
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
    <div id="Div_Registered" class="divset" runat="server">

        <form id="form1" runat="server">
        </form>

    </div>
</body>
</html>

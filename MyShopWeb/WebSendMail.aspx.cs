using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//用到的
using System.Net.Mail;
using System.Reflection;
using System.Collections.Specialized;
using System.Net;

public partial class WebSendMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            string SendEmailAddress = Request["CustomEmail"];
            //string SendEmailAddress = "vcx732564@hotmail.com";
            SendMail(SendEmailAddress);
        }
        catch (Exception ex)
        {
            throw new Exception(" 寄送認證信時發生錯誤 - " + ex.Message);
        }


    }


    private bool SendMail(string Email)
    {

        //寄件人
        //string MailFrom = "benson_zhen@orbit.com.tw";
        string MailFrom = "autosendemail01@gmail.com";
        //建立MailMessage物件
        System.Net.Mail.MailMessage mms = new System.Net.Mail.MailMessage();
        //指定一位寄信人MailAddress
        mms.From = new MailAddress(MailFrom);
        //信件主旨
        mms.Subject = "測試用";
        //信件內容
        mms.Body = "你好 你毫棒喔!!";
        //信件內容 是否採用Html格式
        mms.IsBodyHtml = false;

        mms.To.Add(new MailAddress(Email));

        //加入信件的副本(們)address
        //mms.CC.Add(new MailAddress(Ccs[i].Trim()));
        //加入信件的夾帶檔案
        //mms.Attachments.Add(file);

        /*
                    outlook.com   smtp.live.com port:25           (沒試過)
                    yahoo         smtp.mail.yahoo.com.tw port:465 (一直會失敗= =)
                    Gmail         smtp.gmail.com  587             (目前會成功的)
        */
        //using (SmtpClient client = new SmtpClient("msa.hinet.net",  25))//或公司、客戶的smtp_server
        using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))//或公司、客戶的smtp_server
        {


            //網友阿尼尼:http://www.dotblogs.com.tw/kkc123/archive/2012/06/26/73076.aspx
            //※公司內部不用認證,寄到外部信箱要特別認證 Account & Password

            //自家公司MIS:
            //※要看smtp server的設定呀~

            //結論...
            //※程式在客戶那邊執行的話，問客戶，程式在自家公司執行的話，問自家公司MIS，最準確XD
            //寄信帳密
            client.Credentials = new NetworkCredential(MailFrom, "c58q9rpbxw");

            //Gmial 的 smtp 使用 SSL
            client.EnableSsl = true;

            client.Send(mms);//寄出一封信
        }

        //釋放每個附件，才不會Lock住
        if (mms.Attachments != null && mms.Attachments.Count > 0)
        {
            for (int i = 0; i < mms.Attachments.Count; i++)
            {
                mms.Attachments[i].Dispose();
                mms.Attachments[i] = null;
            }
        }


        return true;
    }
}
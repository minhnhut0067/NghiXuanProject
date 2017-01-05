using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;

public partial class Admin_QuenMatKhau : System.Web.UI.Page
{
    WebCNPMDataContext st = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void butXacNhan_Click(object sender, EventArgs e)
    {
        try
        {
            var matkhau = st.st_QuenMatKhau(txtUsername.Text, txtEmail.Text);
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential("smileangelstk61@gmail.com", "LOVEtobeLOVE");
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            MailMessage omail = new MailMessage();
            foreach (var item in matkhau)
            {
                omail.From = new MailAddress("smileangelstk61@gmail.com", "Admin Gà Bông");
                omail.To.Add(txtEmail.Text);
                omail.Subject = "Mật khẩu quản trị";
                omail.Body = "Mật khẩu của bạn là: " + item.password.Trim();
                omail.Priority = MailPriority.High;
                omail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                omail.ReplyTo = new MailAddress(txtEmail.Text);
                lblThongBao.Text = "Mật khẩu được gửi thành công";
                SmtpServer.Send(omail);
            }

        }
        catch (Exception)
        {

            lblThongBao.Text = "Sai Email hoặc maatk khẩu";
        }
        
    }
}


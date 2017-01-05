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

public partial class Admin_DoiMatKhau : System.Web.UI.Page
{
    WebCNPMDataContext db = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Dangnhap"] != null) && (Session.Contents["TrangThai"].ToString() == "DaDangNhap"))
        {
            var tt = from c in db.Accounts where c.Username == Session["Dangnhap"].ToString() select new { c.Username };

            foreach (var item in tt)
            {
                txtUserName.Text = item.Username.Trim();

            }
        }
        else
            if ((Session.Contents["TrangThai"].ToString() == "ChuaDangNhap") && (Session["Dangnhap"] == null))
            {
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            }
    }
    protected void btDoiMK_Click(object sender, EventArgs e)
    {
        Account ac = db.Accounts.SingleOrDefault(c => c.Username == txtUserName.Text && c.MemberID == c.Member.MemberID && c.Password == txtPasswordcu.Text.Trim());
        if (txtnhappassmoi.Text == txtpassmoi.Text)
        {
            ac.Password = txtpassmoi.Text;
            //db.SubmitChanges();

            db.SubmitChanges();
            lblThongbao.Text = "Bạn đổi mật khẩu thành công";
            Response.Redirect("ThongTinCaNhan.aspx");
        }
        else
            lblThongbao.Text = "Bạn nhập lại mật khẩu mới không đúng";


        //Account ac = db.Accounts.SingleOrDefault(c => c.Username == txtUserName.Text && c.MemberID == c.Member.MemberID && c.Password == txtPasswordcu.Text.Trim());
        //if (txtnhappassmoi.Text == txtpassmoi.Text)
        //{
        //    ac.Password = txtpassmoi.Text;
        //    db.SubmitChanges();
        //    lblThongbao.Text = "Bạn đổi mật khẩu thành công";
        //}
        //else
        //    lblThongbao.Text = "Bạn nhập lại mật khẩu mới không đúng";

           
    }
}

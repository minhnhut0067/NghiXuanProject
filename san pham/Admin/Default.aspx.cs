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

public partial class Admin_Default3 : System.Web.UI.Page
{
    WebCNPMDataContext st = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session.Contents["TrangThai"].ToString() == "ChuaDangNhap")
        //{
        //    Response.Redirect("Login.aspx");
        //}
        //else
        if ((Session["Dangnhap"] != null) && (Session.Contents["TrangThai"].ToString() == "DaDangNhap"))
        {
            var tt = from c in st.Accounts where c.Username == Session["Dangnhap"].ToString() select new { c.Member.FullName };
            string html;
            foreach (var item in tt)
            {
                html = "<b>Chào bạn:&nbsp;";
                lblTTuserDN.Text = html + item.FullName.Trim().ToString();
                html = "</b>";

            }

        }
        else
            if ((Session.Contents["TrangThai"].ToString() == "ChuaDangNhap") && (Session["Dangnhap"] == null))
            {
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            }
    }
}

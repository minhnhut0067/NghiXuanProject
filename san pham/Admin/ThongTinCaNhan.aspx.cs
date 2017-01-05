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

public partial class Admin_TTCN : System.Web.UI.Page
{
    WebCNPMDataContext st = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
       if ((Session["Dangnhap"] != null) && (Session.Contents["TrangThai"].ToString() == "DaDangNhap"))
        {
            var tt = from c in st.Accounts where c.Username == Session["Dangnhap"].ToString() select new { c.Username };

            foreach (var item in tt)
            {
                lblThongtin.Text = "Thông tin cá nhân của bạn:&nbsp;" + item.Username.Trim().ToString();
            }

        }
        else
            if ((Session.Contents["TrangThai"].ToString() == "ChuaDangNhap") && (Session["Dangnhap"] == null))
            {
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            }
        if (!IsPostBack)
        {

            LoadThongTin(Session["Dangnhap"].ToString());
            //LoadThongTin(Convert.ToInt32(Session["Dangnhap"]));

        }

    }
    public void LoadThongTin(string tendn)
    {
        try
        {
            //int ma = Convert.ToInt32(Session["Dangnhap"]);
            string ten = (Session["Dangnhap"].ToString());
            //st_ThongtincanhanResult thongtin = st.st_Thongtincanhan(ten).FirstOrDefault();
            ThongtincanhanResult thongtin = st.Thongtincanhan(ten).FirstOrDefault();
            txtTenDN.Text = Session["Dangnhap"].ToString();
            txtHoten.Text = thongtin.FullName;
            txtNgaysinh.Text = thongtin.Birthday.ToString();
            txtGioiTinh.Text = thongtin.Gender;
            txtEmail.Text = thongtin.Email;
            //drGioiTinh.DataValueField = thongtin.FirstOrDefault().Member.Gender;
            txtCMTND.Text = thongtin.IdentityCard;
            txtDiaChi.Text = thongtin.Address;
            txtSDT.Text = thongtin.PhoneNumber;
            txtNgayKichhoatTK.Text = thongtin.RegistrationDate.ToString();
            txtQuyen.Text = thongtin.Decendalization;
            txtTrangThaiHD.Text = thongtin.Status;
        }
        catch (Exception)
        {
        }
    }
    protected void btCapNhatTT_Click(object sender, EventArgs e)
    {
        Account thanhvien = st.Accounts.SingleOrDefault(c => c.Username == Session["Dangnhap"].ToString() && c.MemberID==c.Member.MemberID);
        thanhvien.Member.FullName = txtHoten.Text;
        thanhvien.Member.Birthday = DateTime.Parse(txtNgaysinh.Text);
        thanhvien.Member.Address = txtDiaChi.Text;
        thanhvien.Member.Email = txtEmail.Text;
        thanhvien.Member.Gender = txtGioiTinh.Text;
        thanhvien.Member.IdentityCard = txtCMTND.Text;
        thanhvien.Member.PhoneNumber = txtSDT.Text;
        st.SubmitChanges();

    }
}

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

public partial class Admin_TaoTaiKhoan : System.Web.UI.Page
{
    WebCNPMDataContext st = new WebCNPMDataContext();
    public void Loaddrop()
    {
        var thongtin = st.st_LayThanhvien1();
        drmembename.DataSource = thongtin;
        drmembename.DataTextField = "FullName";
        drmembename.DataValueField = "MemberID";
        drmembename.DataBind();

    }
    public void LoadGrid()
    {
        var ct = from c in st.Accounts
                 select new { c.MemberID, c.Username, c.Decendalization, c.RegistrationDate, c.Status };
        grvTaiKhoan.DataSource = ct;
        grvTaiKhoan.DataBind();
    }
    public bool KTraRong()
    {
        if (txtUsename.Text == "" || txtMK.Text == "")
        { return true; }
        else { return false; }
    }
    public void Refresh1()
    {
        txtUsename.Text = ""; txtMK.Text = "";
        txtNgaytaoTK.Text = "";
        rdHoatDong.Checked = false;
        rdKhoa.Checked = false;
        chkbQuyenCM.Visible = false;
        drmembename.SelectedValue = "0";
        drQuyen.SelectedValue = "0";
    }
    public void Refresh2()
    {
        txtUsename.Text = ""; txtMK.Text = "";
        txtNgaytaoTK.Text = "";
        rdHoatDong.Checked = false;
        rdKhoa.Checked = false;
        chkbQuyenCM.Visible = false;
        drmembename.SelectedValue = "0";
        drQuyen.SelectedValue = "0";
        lblThongbao.Visible = false;
    }
    //load quyen tuong ung khi chon trong drop
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents["TrangThai"].ToString() == "ChuaDangNhap")
        {
            Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
        }
        if (Session.Contents["TrangThai"].ToString() == "DaDangNhap")
        {
            var tt = from c in st.Accounts
                     where (c.Username == Session["Dangnhap"].ToString() && c.MemberID.ToString() == Session["MemberID"].ToString())
                     select new { c.MemberID, c.Decendalization };
            foreach (var item in tt)
            {
                //if (Session["MemberID"].ToString() == item.MemberID.ToString() && item.Decendalization == "Admin")
                //if (item.Decendalization=="Admin")
                //{
                //    Response.Redirect("TaoTaiKhoan.aspx");
                //}
                if (Session["MemberID"].ToString() == item.MemberID.ToString() && item.Decendalization == "Quản lý chuyên mục")
                {
                    Response.Redirect("Default.aspx");
                }
                if (Session["MemberID"].ToString() == item.MemberID.ToString() && item.Decendalization == "Viết bài")
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        if (!IsPostBack)
        {
            rdHoatDong.Checked = true;
            Loaddrop();
            LoadGrid();
            Refresh1();
            chkbQuyenCM.Enabled = true;
            chkbQuyenCM.Visible = true;
        }
    }
    protected void lbtTaoTK_Click(object sender, EventArgs e)
    {
        if (KTraRong() == false)
        {
            CateMember1 cm = new CateMember1();
            Account ac = new Account();
            ac.MemberID = int.Parse(drmembename.SelectedValue);
            ac.Username = txtUsename.Text;
            ac.Password = txtMK.Text;

            if (drQuyen.SelectedValue == "1")
            {
                ac.Decendalization = "Admin";
            }
            if ( drQuyen.SelectedValue == "0")
            {
                ac.Decendalization = "--Chọn Quyền--";
            }
            if (drQuyen.SelectedValue == "2")
            {
                ac.Decendalization = "Quản lý chuyên mục";
            }

            ac.RegistrationDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
            if (rdHoatDong.Checked == true)
            {
                ac.Status = "Hoạt động";
            }
            else
            {
                if (rdKhoa.Checked == true)
                { ac.Status = "Khóa"; }
            }
            st.Accounts.InsertOnSubmit(ac);
            st.SubmitChanges();
            LoadGrid();
            //lblThongbao.Text = "Bạn tạo mới tài khoản thành công!!";
            Refresh1();
            drmembename.Focus();

            if (chkbQuyenCM.Items[0].Selected == true)
            {
                cm.CateTinCongNghe = 1;
            }
            else { cm.CateTinCongNghe = 0; }
            if (chkbQuyenCM.Items[1].Selected == true)
            {
                cm.CateMicrosoft = 2;
            }
            else { cm.CateMicrosoft = 0; }
            if (chkbQuyenCM.Items[2].Selected == true)
            {
                cm.CateSun = 3;
            }
            else { cm.CateSun = 0; }
            if (chkbQuyenCM.Items[3].Selected == true)
            {
                cm.CateCongNgheMoi = 4;
            }
            else { cm.CateCongNgheMoi = 0; }
            //}
            cm.MemberID = ac.MemberID;
            st.CateMember1s.InsertOnSubmit(cm);
            st.SubmitChanges();
            //LoadGrid();
            //Refresh1();
            Response.Redirect("TaoTaiKhoan.aspx");
        }
    }
    protected void grvTaiKhoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvTaiKhoan.PageIndex = e.NewPageIndex;
        LoadGrid();
        Refresh2();
        chkbQuyenCM.Enabled = true;
        chkbQuyenCM.Visible = true;


    }
    protected void grvTaiKhoan_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //try
        //{
        TextBox txtTenDN1 = (TextBox)grvTaiKhoan.Rows[e.RowIndex].FindControl("txtTenDN");
        TextBox txtQuyen1 = (TextBox)grvTaiKhoan.Rows[e.RowIndex].FindControl("txtQuyen");
        TextBox txtNgayTao1 = (TextBox)grvTaiKhoan.Rows[e.RowIndex].FindControl("txtNgayTao");
        TextBox txtTrangThai1 = (TextBox)grvTaiKhoan.Rows[e.RowIndex].FindControl("txtTrangThai");
        Label lblMa = (Label)grvTaiKhoan.Rows[e.RowIndex].FindControl("lblMa");
        Account ac = st.Accounts.SingleOrDefault(c => c.MemberID == int.Parse(lblMa.Text));
        ac.MemberID = int.Parse(lblMa.Text);
        ac.Username = txtTenDN1.Text;
        ac.Decendalization = txtQuyen1.Text;
        ac.RegistrationDate = Convert.ToDateTime((txtNgayTao1.Text).ToString());
        if ((txtTrangThai1.Text == "Hoạt động") || (txtTrangThai1.Text == "Khóa"))
        {
            ac.Status = txtTrangThai1.Text;
        }
        st.SubmitChanges();

        grvTaiKhoan.EditIndex = -1;
        LoadGrid();
        lblThongbao.Text = "Cập nhật thành công";
        Refresh1();
        Response.Redirect("TaoTaiKhoan.aspx");
        //}
        //catch (Exception)
        //{
        //}
    }
    /// <summary>
    /// Khi nhấn nút sửa trên gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grvTaiKhoan_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //lblmemberid.Visible = true;
        Label lblMa = (Label)grvTaiKhoan.Rows[e.NewEditIndex].FindControl("lblMa");
        Account ac = st.Accounts.SingleOrDefault(c => c.MemberID == int.Parse(lblMa.Text) && c.MemberID == c.Member.MemberID && c.MemberID == c.Member.CateMember1.MemberID);
        drmembename.SelectedItem.Text = ac.Member.FullName;
        txtUsename.Text = ac.Username.ToString();
        txtMK.Text = ac.Password;
        drQuyen.SelectedItem.Text = ac.Decendalization.ToString();
        if (ac.Decendalization == "Admin")
        {
            //drQuyen.SelectedValue =" 1";
            chkbQuyenCM.Enabled = true;
            chkbQuyenCM.Items[0].Selected = true;
            chkbQuyenCM.Items[1].Selected = true;
            chkbQuyenCM.Items[2].Selected = true;
            chkbQuyenCM.Items[3].Selected = true;
            chkQLBaiViet.Checked = true;
        }
        if (ac.Decendalization == "Quản lý chuyên mục")
        {
            chkbQuyenCM.Enabled = true;
            chkbQuyenCM.Visible = true;
            chkQLBaiViet.Checked = true;
            if (ac.Member.CateMember1.CateTinCongNghe == 1)
            {
                chkbQuyenCM.Items[0].Selected = true;
            }
            if (ac.Member.CateMember1.CateMicrosoft == 2)
            {
                chkbQuyenCM.Items[1].Selected = true;

            } if (ac.Member.CateMember1.CateSun == 3)
            {
                chkbQuyenCM.Items[2].Selected = true;

            } if (ac.Member.CateMember1.CateCongNgheMoi == 4)
            {
                chkbQuyenCM.Items[3].Selected = true;

            }
        }
        if (ac.Decendalization == "Viết bài")
        {
            chkbQuyenCM.Enabled = true;
            chkbQuyenCM.Visible = true;
            chkQLBaiViet.Checked = true;
            if (ac.Member.CateMember1.CateTinCongNghe == 1)
            {
                chkbQuyenCM.Items[0].Selected = true;

            }
            if (ac.Member.CateMember1.CateMicrosoft == 2)
            {
                chkbQuyenCM.Items[1].Selected = true;

            } if (ac.Member.CateMember1.CateSun == 3)
            {
                chkbQuyenCM.Items[2].Selected = true;

            } if (ac.Member.CateMember1.CateCongNgheMoi == 4)
            {
                chkbQuyenCM.Items[3].Selected = true;

            }
        }
        txtNgaytaoTK.Text = ac.RegistrationDate.ToString();
        if (ac.Status == "Hoạt động")
        {
            rdHoatDong.Checked = true;
        }
        if (ac.Status == "Khóa")
        {
            rdKhoa.Checked = true;
        }
        if (ac.Status == "")
        {
            rdHoatDong.Checked = false;
            rdKhoa.Checked = false;
        }
        grvTaiKhoan.EditIndex = e.NewEditIndex;
        LoadGrid();
        //Response.Redirect("TaoTaiKhoan.aspx");
        //lblThongbao.Text = "Cập nhật thành công";
        //lblmemberid.Visible = false;
    }
    protected void grvTaiKhoan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lblMa = (Label)grvTaiKhoan.Rows[e.RowIndex].FindControl("lblMa");
            Account ac = st.Accounts.SingleOrDefault(c => c.MemberID == int.Parse(lblMa.Text));
            st.Accounts.DeleteOnSubmit(ac);
            st.SubmitChanges();
            LoadGrid();
            Response.Redirect("TaoTaiKhoan.aspx");
        }
        catch (Exception)
        {
        }
    }
    protected void lbtLuu_Click(object sender, EventArgs e)
    {
        //Account ac = st.Accounts.SingleOrDefault(c => c.Username == txtUsename.Text && c.MemberID == int.Parse(drmembename.SelectedValue) && c.MemberID == c.Member.CateMember1.MemberID && c.MemberID == c.Member.MemberID);
        ////Account ac = st.Accounts.SingleOrDefault(c => c.Username == txtUsename.Text && c.MemberID == int.Parse(drmembename.SelectedValue));
        //CateMember1 quyen = ac.Member.
        //    //ac.Username = txtUsename.Text;
        //    //ac.Password = txtMK.Text;
        //ac.Decendalization = drQuyen.SelectedItem.Text;
        //if (drQuyen.SelectedValue == "2")
        //{
        //    ac.Decendalization = "Quản lý chuyên mục";

        //}
        //if (drQuyen.SelectedValue == "1")
        //{
        //    ac.Decendalization = "Admin";
        //}
        //if ((chkQLBaiViet.Checked == true && chkbQuyenCM.Items[0].Selected == true)
        //    || (chkQLBaiViet.Checked == true && chkbQuyenCM.Items[1].Selected == true)
        //        || (chkQLBaiViet.Checked == true && chkbQuyenCM.Items[2].Selected == true)
        //        || (chkQLBaiViet.Checked == true && chkbQuyenCM.Items[3].Selected == true))
        ////if (chkQLBaiViet.Checked==true)
        //{

        //    ac.Decendalization = "Viết bài";
        //}
        //if (rdHoatDong.Checked == true)
        //{
        //    ac.Status = "1";
        //}
        //if (rdKhoa.Checked == true)
        //{
        //    ac.Status = "0";
        //}
        ////st.SubmitChanges();
        ////ac.RegistrationDate = DateTime.Parse(txtNgaytaoTK.Text);

        //if (chkbQuyenCM.Items[0].Selected == true)
        //{
        //    //ac.Member.CateMember1.CateTinCongNghe = 1;
        //    quyen.CateTinCongNghe = 1;
        //}
        //else
        //{
        //    quyen.CateTinCongNghe = 0;

        //    //ac.Member.CateMember1.CateTinCongNghe = 0; 
        //}
        //if (chkbQuyenCM.Items[1].Selected == true)
        //{
        //    //ac.Member.CateMember1.CateMicrosoft = 2;
        //    quyen.CateMicrosoft = 2;
        //}
        //else
        //{
        //    //ac.Member.CateMember1.CateMicrosoft = 0;
        //    quyen.CateMicrosoft = 0;
        //}
        //if (chkbQuyenCM.Items[2].Selected == true)
        //{
        //    //ac.Member.CateMember1.CateSun = 3;
        //    quyen.CateSun = 3;
        //}
        //else
        //{
        //    //ac.Member.CateMember1.CateSun = 0;
        //    quyen.CateSun = 0;
        //}
        //if (chkbQuyenCM.Items[3].Selected == true)
        //{
        //    //ac.Member.CateMember1.CateCongNgheMoi = 4;
        //    quyen.CateCongNgheMoi = 4;
        //}
        //else
        //{
        //    //ac.Member.CateMember1.CateCongNgheMoi = 0;
        //    quyen.CateCongNgheMoi = 0;
        //}

        //st.SubmitChanges();
        //LoadGrid();
        //Refresh1();
        //lblThongbao.Text = "Cập nhật thành công";
        //txtUsename.Focus();
        //Response.Redirect("TaoTaiKhoan.aspx");
    }
    protected void grvTaiKhoan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Response.Redirect("TaoTaiKhoan.aspx");
        //LoadGrid();
    }
    protected void drQuyen_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drQuyen.SelectedValue == "1")
        {
            chkbQuyenCM.Enabled = true;
            chkbQuyenCM.Items[0].Selected = true;
            chkbQuyenCM.Items[1].Selected = true;
            chkbQuyenCM.Items[2].Selected = true;
            chkbQuyenCM.Items[3].Selected = true;
            chkQLBaiViet.Visible = true;
            chkQLBaiViet.Checked = true;
        }
        if (drQuyen.SelectedValue == "2")
        {
            chkbQuyenCM.Enabled = true;
            chkbQuyenCM.Items[0].Selected = false;
            chkbQuyenCM.Items[1].Selected = false;
            chkbQuyenCM.Items[2].Selected = false;
            chkbQuyenCM.Items[3].Selected = false;
            chkQLBaiViet.Visible = false;
            chkQLBaiViet.Enabled = true;
        }
        if (drQuyen.SelectedValue == "0")
        {
            chkbQuyenCM.Enabled = true;
            chkbQuyenCM.Items[0].Selected = false;
            chkbQuyenCM.Items[1].Selected = false;
            chkbQuyenCM.Items[2].Selected = false;
            chkbQuyenCM.Items[3].Selected = false;
            chkQLBaiViet.Visible = true;
            chkQLBaiViet.Checked = false;

        }
    }
}
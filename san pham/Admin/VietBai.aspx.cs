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
using System.Diagnostics;
using System.IO;

public partial class Admin_VietBai : System.Web.UI.Page
{

    WebCNPMDataContext st = new WebCNPMDataContext();
 
    public void LoadDropCha()
    {
        #region
        //var quyen = from c in st.Accounts
        //            select new { c.MemberID, c.Decendalization };
        //foreach (var item in quyen)
        //{
        //    if (item.Decendalization == "Quản lý bài viết")
        //    {
        //        drChuyenMuc.Enabled = false;
        //        var cm = from c in st.Categories select new { c.CategoryID, c.CategoryName };
        //        drchuyenmuccon.DataSource = cm;
        //        drchuyenmuccon.DataTextField = "CategoryName";
        //        drchuyenmuccon.DataValueField = "CategoryID";
        //        drchuyenmuccon.DataBind();
        //    }
        //    else
        //    {
        //        if (item.Decendalization=="Admin"||item.Decendalization=="Quản lý chuyên mục")
        //        {
        //            drChuyenMuc.Enabled = true;
        //            string ma = Session["MemberID"].ToString();
                    
        //            var ctid = st.LoadQuyen(int.Parse(ma));
        //            drChuyenMuc.DataSource = ctid;
        //            drChuyenMuc.DataTextField = "CategoryName";
        //            drChuyenMuc.DataValueField = "CategoryID";
        //            drChuyenMuc.DataBind();
        //        }
                
                
        //    }
        //}
        #endregion

        //Session["MemberID"] = account.MemberID;
        drChuyenMuc.Enabled = true;
        string ma = Session["MemberID"].ToString();
        var ctid = st.LoadQuyen(int.Parse(ma));
        drChuyenMuc.DataSource = ctid;
        drChuyenMuc.DataTextField = "CategoryName";
        drChuyenMuc.DataValueField = "CategoryID";
        drChuyenMuc.DataBind();
    }
    /// <summary>
    /// Kiểm tra các ô textbox rỗng
    /// </summary>
    /// <returns></returns>
    public bool KtraRong()
    {
        if (txtTieude.Text == "" || drChuyenMuc.Text == "" || txtMota.Text == "" || FCKNoidung.Value == "" || txtLienket.Text == "" || txtTuKhoa.Text == "")
        {
            return true;
        }
        else
        { return false; }

    }
    /// <summary>
    /// Làm rỗng các điều khiển
    /// </summary>
    public void Refresh1()
    {
        txtTieude.Text = "";
        txtMota.Text = ""; FCKNoidung.Value = "";
        txtTacgia.Text = "";
        txtLienket.Text = "";
        txtTGianviet.Text = ""; txtTuKhoa.Text = "";
        txtTieude.Focus();
        rdKhongduocphep.Checked = false;
        rdPhanhoi.Checked = false;
        lblMachuyenmuc.Text = "";
        ckDangbai.Checked = false;
        ckDuyetbai.Checked = false;
    }

    /// <summary>
    /// Lấy ảnh
    /// </summary>

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Contents["TrangThai"].ToString() == "DaDangNhap")
        {            
            var tt = from c in st.Accounts 
                     where c.Username == Session["Dangnhap"].ToString()
                     select new { c.Member.FullName, c.Decendalization };
            foreach (var item in tt)
            {
                txtTacgia.Text = item.FullName.ToString();
                if (item.Decendalization=="Admin")
                {
                    ckDangbai.Visible = true;
                    ckDuyetbai.Visible = true;
                }
                if (item.Decendalization == "Quản lý chuyên mục")
                {
                    ckDangbai.Enabled = true;
                    ckDuyetbai.Visible = true;
                }
                if (item.Decendalization == "Viết bài")
                {
                    ckDangbai.Enabled = false;
                    ckDuyetbai.Visible = true;
                }
                  
                  
            }
           
        }
        else
        {
            if (Session.Contents["TrangThai"].ToString() == "ChuaDangNhap")
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
        }
        if (!IsPostBack)
        {

            rdPhanhoi.Checked = false;
            //LoadDrop1(drChuyenmuc, 0);
            LoadDropCha();
            //Loaddrop1();
            //LoadCMucCon();
        }
    }
    protected void btGuibai_Click(object sender, EventArgs e)
    {
        int cateID;
        try
        {
            cateID = int.Parse(lblMachuyenmuc.Text);
            //ThemBaiMoi();
        }
        catch (Exception)
        {
            cateID = 1;
        }
        if (KtraRong() == false)
        {
            New tintuc = new New();
            tintuc.Title = txtTieude.Text;
            tintuc.CategoryID = int.Parse(drchuyenmuccon.SelectedValue);
            tintuc.Description = txtMota.Text;
            tintuc.Contents = FCKNoidung.Value.ToString();
            tintuc.Author = txtTacgia.Text;
            tintuc.Posttime = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
            tintuc.Keyword = txtTuKhoa.Text;
            if (upimg.HasFile)
            {
                string t = "upload/";
                tintuc.Image = t + upimg.FileName;
                string path = Server.MapPath("..\\upload") + "\\" + upimg.FileName;
                upimg.SaveAs(path);
                //tintuc.Image=t+  upimg.SaveAs( path);
                //tintuc.Image = upimg.SaveAs(path);
            }
            else { tintuc.Image = lblimg.Text; }
            tintuc.Link = txtLienket.Text;
            if (rdPhanhoi.Checked == true)
            {
                tintuc.feedback = "1";
            }
            if (rdKhongduocphep.Checked == true)
            {
                tintuc.feedback = "0";
            }
            else
            {
                lblThongBao.Text = "Bạn chưa chọn quyền phản hồi cho bạn đọc";
            }
            //tintuc.Status = "ngoancute";
            if (ckDangbai.Checked == true)
            {
                tintuc.Status = "1";
            }
            if (ckDuyetbai.Checked == true)
            {
                tintuc.Status = "0";
            }

            else
            {
                if (ckDangbai.Checked == false && ckDuyetbai.Checked == false)
                {
                    tintuc.Status = "";
                }
            }
            st.News.InsertOnSubmit(tintuc);
            st.SubmitChanges();
            lblThongBao.Text = "Bài viết đã được lưu!^.^";
            Refresh1();
            //Response.Redirect("VietBai.aspx");
            txtTieude.Focus();
        }
        else
        {
            //Response.Write("Khong duoc de trong truong thong tin bai viet"); 
            lblThongBao.Text = "Hãy điền đầy đủ thông tin còn trống!!!";
        }
    }

    protected void drChuyenmuc_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //lblMachuyenmuc.Text = drChuyenMuc.SelectedValue.ToString();
    }
    protected void btLamLai_Click(object sender, EventArgs e)
    {
        Refresh1();
        rdPhanhoi.Enabled = true;
        rdKhongduocphep.Enabled = true;
        lblThongBao.Text = "";
    }

    protected void drChuyenMuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadCMucCon();

        drchuyenmuccon.Items.Clear();
        var chuyenmuccon = st.st_LayCMcontheoCha(int.Parse(drChuyenMuc.SelectedValue));
        drchuyenmuccon.DataSource = chuyenmuccon;
        drchuyenmuccon.DataTextField = "CategoryName";
        drchuyenmuccon.DataValueField = "CategoryID";
        drchuyenmuccon.DataBind();
    }
}

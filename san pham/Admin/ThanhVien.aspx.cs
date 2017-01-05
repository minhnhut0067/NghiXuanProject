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
using System.IO;

public partial class Admin_ThanhVien : System.Web.UI.Page
{
    //SoftwareTechnologyDataContext st = new SoftwareTechnologyDataContext();
    WebCNPMDataContext st = new WebCNPMDataContext();
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
            //LoadDrop();

            //LoadDrop();
            LoadGrid();
            Refresh1();
            lblma.Visible = false;
        }
    }
    public bool KtraRong()
    {
        if (txtEmail.Text == "" || txtHoTen.Text == "" || txtNS.Text == "" ||
             txtCMTND.Text == "" || txtdiachi.Text == "")
        {
            return true;
        }
        else { return false; }
    }
    public void Refresh1()
    {
        txtEmail.Text = ""; txtHoTen.Text = ""; txtNS.Text = "";
        txtCMTND.Text = "";
        txtdiachi.Text = "";
        txtDT.Text = "";
    }
    public void LoadDrop()
    {
        ddlGioitinh.Items.Clear();
        var Gt = (from c in st.Members select new {c.Gender }).Distinct();
       
        ddlGioitinh.DataSource = Gt;
        ddlGioitinh.DataValueField = "Gender";
        ddlGioitinh.DataTextField = "Gender";
        ddlGioitinh.DataBind();
    }
    public void LoadGrid()
    {
        var mb = from c in st.Members select new { c.MemberID,c.Address, c.FullName, c.Email };
        grvThanhvien.DataSource = mb;
        grvThanhvien.DataBind();    
    }
    protected void lbtThemTV_Click(object sender, EventArgs e)
    {
        try
        {

            if (KtraRong() == false)
            {
                Member mb = new Member();
                    mb.Email = txtEmail.Text;
                    mb.FullName = txtHoTen.Text;
                    mb.Birthday = DateTime.Parse(txtNS.Text);                  
                    mb.Gender = ddlGioitinh.SelectedValue;
                    mb.IdentityCard = txtCMTND.Text;
                    mb.Address = txtdiachi.Text;
                    mb.PhoneNumber = txtDT.Text;
                    st.Members.InsertOnSubmit(mb);
                    st.SubmitChanges();
                    LoadGrid();
                    lblThongBao.Text = "Bạn thêm thành công!";
                    Refresh1();
                    txtHoTen.Focus();
            }
            else
            {
                if (KtraRong() == true)
                {
                    lblThongBao.Text = "Hãy nhập đủ dữ liệu!";
                }
            }
        }
        catch (Exception)
        {
            lblThongBao.Text = "Hãy nhập đủ dữ liệu!";
        }
    }
    #region
    //protected void grvThanhvien_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    lblma.Visible = true;
    //    //TextBox txtma1 = (TextBox)grvThanhvien.Rows[e.NewSelectedIndex].FindControl("txtMa");
    //    //Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(txtma1.Text));
   
    //    Label lblMa = (Label)grvThanhvien.Rows[e.NewSelectedIndex].FindControl("lblMa");
    //    Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(lblMa.Text));
    //    lblma.Text = cl.MemberID.ToString();
    //    txtHoTen.Text = cl.FullName.ToString();
    //    txtEmail.Text = cl.Email.ToString();
    //    txtDT.Text = cl.PhoneNumber.ToString();
    //    txtEmail.Text = cl.Email.ToString();
    //    txtdiachi.Text = cl.Address.ToString();
    //    txtNS.Text = cl.Birthday.ToString();
    //    txtCMTND.Text = cl.IdentityCard.ToString();
    //    ddlGioitinh.Text=cl.Gender.ToString();



    //}
    #endregion
    protected void grvThanhvien_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtDiaChi1 = (TextBox)grvThanhvien.Rows[e.RowIndex].FindControl("txtdiachi");
        TextBox txtHoTen1 = (TextBox)grvThanhvien.Rows[e.RowIndex].FindControl("txtHoTen");
        TextBox txtEmail1 = (TextBox)grvThanhvien.Rows[e.RowIndex].FindControl("txtmail");
        //TextBox txtma1 = (TextBox)grvThanhvien.Rows[e.RowIndex].FindControl("txtMa");
        //Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(txtma1.Text));
        Label lblMa = (Label)grvThanhvien.Rows[e.RowIndex].FindControl("lblMa");
        Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(lblMa.Text));
        //mb.Email = txtEmail.Text;
        cl.FullName = txtHoTen1.Text;
        cl.Address = txtDiaChi1.Text;
        cl.Email = txtEmail1.Text;     
        //st.Members.InsertOnSubmit(mb);
        st.SubmitChanges();
        grvThanhvien.EditIndex = -1;
        LoadGrid();
        lblThongBao.Text = "Bạn đã cập nhật thành công";
        Refresh1();
        Response.Redirect("ThanhVien.aspx");
        txtHoTen.Focus();
        //lblThongBao.Text = "Bạn thêm thành công!";
    }
    protected void grvThanhvien_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblma.Visible = true;
        //TextBox txtma1 = (TextBox)grvThanhvien.Rows[e.NewSelectedIndex].FindControl("txtMa");
        //Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(txtma1.Text));

        Label lblMa = (Label)grvThanhvien.Rows[e.NewEditIndex].FindControl("lblMa");
        Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(lblMa.Text));
        lblma.Text = cl.MemberID.ToString();
        txtHoTen.Text = cl.FullName.ToString();
        txtEmail.Text = cl.Email.ToString();
        txtDT.Text = cl.PhoneNumber.ToString();
        txtEmail.Text = cl.Email.ToString();
        txtdiachi.Text = cl.Address.ToString();
        txtNS.Text = cl.Birthday.ToString();
        txtCMTND.Text = cl.IdentityCard.ToString();
        ddlGioitinh.Text = cl.Gender.ToString();     
        grvThanhvien.EditIndex = e.NewEditIndex;
        LoadGrid();
        //Response.Redirect("ThanhVien.aspx");
    }
    protected void grvThanhvien_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //TextBox txtma1 = (TextBox)grvThanhvien.Rows[e.RowIndex].FindControl("txtMa");
        //Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(txtma1.Text));
        Label lblMa = (Label)grvThanhvien.Rows[e.RowIndex].FindControl("lblMa");
        Member cl = st.Members.SingleOrDefault(c => c.MemberID == int.Parse(lblMa.Text));
        st.Members.DeleteOnSubmit(cl);
        st.SubmitChanges();
        LoadGrid();
        txtHoTen.Focus();
    }
    protected void grvThanhvien_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvThanhvien.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void lbtTaoMoi_Click(object sender, EventArgs e)
    {
        Refresh1();
        lblThongBao.Text = "";
        
    }
    public void Layngaysinh()
    {
        var ngaysinh = from c in st.Members select new { c.Birthday};
        
    }
    protected void lbtSave_Click(object sender, EventArgs e)
    {
        try
        {
            Member mb = st.Members.SingleOrDefault(c => c.MemberID == int.Parse( lblma.Text));
            mb.MemberID = int.Parse(lblma.Text);
            mb.FullName = txtHoTen.Text;
            mb.Birthday = DateTime.Parse(txtNS.Text);
            //mb.Birthday = DateTime.Parse(txtNS.Text).ToString().Split('/');
            mb.Email = txtEmail.Text;
            mb.Address = txtdiachi.Text;
            mb.PhoneNumber = txtDT.Text;
            mb.IdentityCard = txtCMTND.Text;
            st.SubmitChanges();
            LoadGrid();
            Refresh1();
            lblThongBao.Text = "Đã cập nhật thành công";
            txtHoTen.Focus();
            lblma.Visible = false;
            Response.Redirect("ThanhVien.aspx");
        }
        catch (Exception)
        {
            
           
        }

        
    }
    protected void grvThanhvien_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadGrid();

    }
    protected void grvThanhvien_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        Response.Redirect("ThanhVien.aspx");
        txtHoTen.Focus();
    }
}
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

public partial class Admin_QuanLyFeedback : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents["TrangThai"].ToString() == "ChuaDangNhap")
            Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
        if (Session.Contents["TrangThai"].ToString() == "DaDangNhap")
        {
            var tt = from c in cn.Accounts
                     where (c.Username == Session["Dangnhap"].ToString() && c.MemberID.ToString() == Session["MemberID"].ToString())
                     select new { c.MemberID, c.Decendalization };
            foreach (var item in tt)
            {
                if (Session["MemberID"].ToString() == item.MemberID.ToString() && item.Decendalization == "Viết bài")
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        if (!IsPostBack)
        {
            LoadDropChuyenMucCha();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvFeedback.PageIndex = e.NewPageIndex;
    }
    private void LoadDropChuyenMucCha()
    {
        string ma = Session["MemberID"].ToString();
        var LoadDropCha = cn.LoadQuyen(int.Parse(ma));
        drChuyenMucCha.DataSource = LoadDropCha;
        drChuyenMucCha.DataTextField = "CategoryName";
        drChuyenMucCha.DataValueField = "CategoryID";
        drChuyenMucCha.DataBind();
    }

    protected void drChuyenMucCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        var LoadFeedback = cn.LoadFeedbackManager(int.Parse(drChuyenMucCon.SelectedValue));
        grvFeedback.DataSource = LoadFeedback;
        grvFeedback.DataBind();
    }
    protected void drChuyenMucCha_SelectedIndexChanged(object sender, EventArgs e)
    {
        drChuyenMucCon.Items.Clear();
        var chuyenmuccon = cn.st_LayCMcontheoCha(int.Parse(drChuyenMucCha.SelectedValue));
        drChuyenMucCon.DataSource = chuyenmuccon;
        drChuyenMucCon.DataTextField = "CategoryName";
        drChuyenMucCon.DataValueField = "CategoryID";
        drChuyenMucCon.DataBind();
    }
    protected void grvFeedback_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvFeedback_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int i = 1;
        string err;
        Label lblMa1 = (Label)grvFeedback.Rows[e.NewEditIndex].FindControl("lblMa");
        var fb = cn.st_UpdateStatusFb(int.Parse(lblMa1.Text), i, "Đăng bài viết thành công");
        grvFeedback.EditIndex = -1;
        var LoadFeedback = cn.LoadFeedbackManager(int.Parse(drChuyenMucCon.SelectedValue));
        grvFeedback.DataSource = LoadFeedback;
        grvFeedback.DataBind();
        lblThongBao.Text = "Đăng phản hồi thành công";
    }
    protected void grvFeedback_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblMa1 = (Label)grvFeedback.Rows[e.RowIndex].FindControl("lblMa");
        Feedback tintuc = cn.Feedbacks.SingleOrDefault(c => c.FeedbackID == (int.Parse(lblMa1.Text)));
        cn.Feedbacks.DeleteOnSubmit(tintuc);
        cn.SubmitChanges();
        var LoadFeedback = cn.LoadFeedbackManager(int.Parse(drChuyenMucCon.SelectedValue));
        grvFeedback.DataSource = LoadFeedback;
        grvFeedback.DataBind();
        lblThongBao.Text = "Phản hồi đã được xóa";
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        if (txtTuNgay.Text != "" && txtDenNgay.Text != "")
        {
            if (drChuyenMucCha.SelectedValue != "" && drChuyenMucCon.SelectedValue != ""
              && (DateTime.Parse(txtTuNgay.Text).Date) <= (DateTime.Parse(txtDenNgay.Text).Date))
            {
                var LoadFb = cn.LoadFeedbackSearch(int.Parse(drChuyenMucCon.SelectedValue), DateTime.Parse(txtTuNgay.Text), DateTime.Parse(txtDenNgay.Text));
                grvFeedback.DataSource = LoadFb;
                grvFeedback.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Ngày bắt đầu ko được lớn hơn ngày kết thúc');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa nhập đủ nội dung');", true);
        }
        
    }
}

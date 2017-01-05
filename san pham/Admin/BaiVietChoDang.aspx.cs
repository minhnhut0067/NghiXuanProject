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
using System.Linq.Expressions;

public partial class Admin_BaiChoDang : System.Web.UI.Page
{
    WebCNPMDataContext st = new WebCNPMDataContext();
    public void LoadDrop2()
    {
        //    ddlCMuc.SelectedValue = "---Chọn chuyên mục---";
        string ma = Session["MemberID"].ToString();
        var ctid = st.LoadQuyen(int.Parse(ma));
        ddlCMuc.DataSource = ctid;
        ddlCMuc.DataTextField = "CategoryName";
        ddlCMuc.DataValueField = "CategoryID";
        ddlCMuc.DataBind();

    }
    /// <summary>
    /// Phuong thuc load len gridview
    /// </summary>
    public void LoadGrid()
    {
        string ma = Session["MemberID"].ToString();
        var ctid = st.LoadQuyen(int.Parse(ma));
        //var ctid = st.st_LayMaCMTheoQuyen(int.Parse(ma));
        DataTable dt = new DataTable();
        dt.Columns.Add("NewsID");
        dt.Columns.Add("Title");
        dt.Columns.Add("Description");
        dt.Columns.Add("CategoryName");
        dt.Columns.Add("Author");
        DataRow dr;
        foreach (var item in ctid)
        {
            var tinchodang = from c in st.News
                             where c.CategoryID == c.Category.CategoryID && c.Category.ParentID == item.CategoryID && c.Status == "0"
                             select new { c.NewsID, c.Title, c.Description, c.Category.CategoryName, c.Category.ParentID, c.Posttime, c.Author };
            foreach (var item1 in tinchodang)
            {
                dr = dt.NewRow();
                dr["NewsID"] = item1.NewsID;
                dr["Title"] = item1.Title;
                dr["Description"] = item1.Description;
                dr["CategoryName"] = item1.CategoryName;
                dr["Author"] = item1.Author;
                dt.Rows.Add(dr);
            }
            grvDSbaivietchoDang.DataSource = dt;
            grvDSbaivietchoDang.DataBind();
        }
    }
    public void Refresh()
    {
        txtTuNgay.Text = "";
        txtDenNgay.Text = "";
        txtTuKhoa.Text = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            if (Session.Contents["TrangThai"].ToString() == "ChuaDangNhap")
            {
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            }
            if (Session.Contents["TrangThai"].ToString() == "DaDangNhap")
            {
                var tt = from c in st.Accounts
                         where c.Username == Session["Dangnhap"].ToString()
                         select new { c.MemberID, c.Decendalization };
                foreach (var item in tt)
                {

                    if (Session["MemberID"].ToString() == item.MemberID.ToString() && item.Decendalization == "Viết bài")
                    {
                        Response.Redirect("Default.aspx");
                    }

                }
            }
        }
        if (!IsPostBack)
        {
            LoadGrid();
            LoadDrop2();
        }
    }
    protected void ddlCMuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblMa.Text = ddlCMuc.SelectedValue;
        drchuyenmuccon.Items.Clear();
        var chuyenmuccon = st.st_LayCMcontheoCha(int.Parse(ddlCMuc.SelectedValue));
        drchuyenmuccon.DataSource = chuyenmuccon;
        drchuyenmuccon.DataTextField = "CategoryName";
        drchuyenmuccon.DataValueField = "CategoryID";
        drchuyenmuccon.DataBind();
    }

    protected void lbtTieudebaiviet_Click(object sender, EventArgs e)
    {
        TextBox txtTieudebaiviet1 = (TextBox)grvDSbaivietchoDang.FindControl("txtTieudebaiviet");
        LinkButton lbtTieudebaivie = (LinkButton)sender;
        Label lblNewsid = (Label)grvDSbaivietchoDang.FindControl("lblNewsid");
        New tintuc = st.News.SingleOrDefault(c => c.NewsID == int.Parse(lblNewsid.Text));
        LoadGrid();

    }
    protected void lbtDuyetbai_Click(object sender, EventArgs e)
    {
        //LinkButton lbtDuyetbai1 = (LinkButton)sender;
        string err = "";
        //Label lblIDnews = (Label)lbtDuyetbai1.Parent.FindControl("txtNewsid");
        //New tintuc = st.News.SingleOrDefault(c => c.NewsID == int.Parse(lblNewsid.Text));
        Label lblMa1 = (Label)grvDSbaivietchoDang.FindControl("lblMa");
        var dangtin = st.st_UpdateStatus(int.Parse(lblMa.Text), "1", err);
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string ma = Session["MemberID"].ToString();
        var ctid = st.LoadQuyen(int.Parse(ma));
        //var ctid = st.st_LayMaCMTheoQuyen(int.Parse(ma));
        DataTable dt = new DataTable();
        dt.Columns.Add("NewsID");
        dt.Columns.Add("Title");
        dt.Columns.Add("Description");
        dt.Columns.Add("CategoryName");
        dt.Columns.Add("Author");
        DataRow dr;
        var thongtin = from c in st.News
                       where c.CategoryID == c.Category.CategoryID && c.Status.ToString() == "0"
                       select new { c.NewsID, c.Title, c.Description, c.CategoryID, c.Category.CategoryName, c.Category.ParentID, c.Author, c.Keyword, c.Posttime };
        foreach (var item in ctid)
        {
            if (txtTuNgay.Text != "" && txtDenNgay.Text != "" && txtTuKhoa.Text == ""
                && ddlCMuc.SelectedValue == "" && drchuyenmuccon.SelectedValue == "")
            {
                if (((DateTime.Parse(txtTuNgay.Text).Date) <= (DateTime.Parse(txtDenNgay.Text).Date)))
                {
                    var tindadang1 = from c in thongtin
                                     where ((c.ParentID == item.CategoryID) &&
                                     (c.Posttime.Value.Date >= DateTime.Parse(txtTuNgay.Text).Date &&
                                     (c.Posttime.Value.Date) <= DateTime.Parse(txtDenNgay.Text).Date))
                                     select new { c.NewsID, c.Title, c.Description, c.CategoryName, c.Author, c.Keyword, c.Posttime };
                    foreach (var item1 in tindadang1)
                    {
                        dr = dt.NewRow();
                        dr["NewsID"] = item1.NewsID;
                        dr["Title"] = item1.Title;
                        dr["Description"] = item1.Description;
                        dr["CategoryName"] = item1.CategoryName;
                        dr["Author"] = item1.Author;
                        dt.Rows.Add(dr);
                    }
                }
            }
            if (txtTuNgay.Text != "" && txtDenNgay.Text != "" && txtTuKhoa.Text != ""
                && ddlCMuc.SelectedValue == "" && drchuyenmuccon.SelectedValue == "")
            {
                if ((DateTime.Parse(txtTuNgay.Text).Date) <= (DateTime.Parse(txtDenNgay.Text).Date))
                {
                    var tindadang3 = from c in thongtin
                                     where ((c.ParentID == item.CategoryID)
                                     && (c.Keyword.Contains(txtTuKhoa.Text.Trim())) &&
                                     ((c.Posttime.Value.Date >= DateTime.Parse(txtTuNgay.Text).Date) &&
                                (c.Posttime.Value.Date) <= DateTime.Parse(txtDenNgay.Text).Date))
                                     select new { c.NewsID, c.Title, c.Description, c.CategoryName, c.Author };
                    foreach (var item1 in tindadang3)
                    {
                        dr = dt.NewRow();
                        dr["NewsID"] = item1.NewsID;
                        dr["Title"] = item1.Title;
                        dr["Description"] = item1.Description;
                        dr["CategoryName"] = item1.CategoryName;
                        dr["Author"] = item1.Author;
                        dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                if (txtTuNgay.Text != "" && txtDenNgay.Text != "")
                {
                    if ((DateTime.Parse(txtTuNgay.Text).Date) > (DateTime.Parse(txtDenNgay.Text).Date))
                        lblThongBao.Text = "Ngày bắt đầu không được lớn hơn ngày kết thúc";
                }
            }
            if (txtTuKhoa.Text != "" && txtTuNgay.Text == "" && txtDenNgay.Text == "")
            {
                var tindadang6 = from c in thongtin
                                 where ((c.ParentID == item.CategoryID)
                                 && (c.Keyword.Contains(txtTuKhoa.Text.Trim())))
                                 select new { c.NewsID, c.Title, c.Description, c.CategoryName, c.Author };
                foreach (var item1 in tindadang6)
                {
                    dr = dt.NewRow();
                    dr["NewsID"] = item1.NewsID;
                    dr["Title"] = item1.Title;
                    dr["Description"] = item1.Description;
                    dr["CategoryName"] = item1.CategoryName;
                    dr["Author"] = item1.Author;
                    dt.Rows.Add(dr);
                }
            }
        }
        grvDSbaivietchoDang.DataSource = dt;
        grvDSbaivietchoDang.DataBind();
        Refresh();
        if (ddlCMuc.SelectedValue != "" && drchuyenmuccon.SelectedValue != ""
                        && txtTuNgay.Text == "" && txtDenNgay.Text == "")
        {
            var tindadang7 = from c in thongtin
                             where c.CategoryID.ToString() == drchuyenmuccon.SelectedValue && c.ParentID.ToString() == ddlCMuc.SelectedValue && c.CategoryName.Contains(drchuyenmuccon.SelectedItem.Text.Trim())
                             select new { c.NewsID, c.Title, c.Description, c.CategoryName, c.Author };
            foreach (var item1 in tindadang7)
            {
                dr = dt.NewRow();
                dr["NewsID"] = item1.NewsID;
                dr["Title"] = item1.Title;
                dr["Description"] = item1.Description;
                dr["CategoryName"] = item1.CategoryName;
                dr["Author"] = item1.Author;
                dt.Rows.Add(dr);
            }
            grvDSbaivietchoDang.DataSource = dt;
            grvDSbaivietchoDang.DataBind();
            Refresh();
        }
        if (txtTuNgay.Text != "" && txtDenNgay.Text != "")
        {
            if (txtTuKhoa.Text != "" && ddlCMuc.SelectedValue != "" && drchuyenmuccon.SelectedValue != ""
                && (DateTime.Parse(txtTuNgay.Text).Date) <= (DateTime.Parse(txtDenNgay.Text).Date))
            {
                var tindadang5 = from c in thongtin
                                 where ((c.ParentID.ToString() == ddlCMuc.SelectedValue)
                                 && (c.CategoryID.ToString() == drchuyenmuccon.SelectedValue) &&
                                 (c.Keyword.Contains(txtTuKhoa.Text)) &&
                                 ((c.Posttime.Value.Date >= DateTime.Parse(txtTuNgay.Text).Date) &&
                            (c.Posttime.Value.Date) <= DateTime.Parse(txtDenNgay.Text).Date))
                                 select new { c.NewsID, c.Title, c.Description, c.CategoryName, c.Author };
                foreach (var item1 in tindadang5)
                {
                    dr = dt.NewRow();
                    dr["NewsID"] = item1.NewsID;
                    dr["Title"] = item1.Title;
                    dr["Description"] = item1.Description;
                    dr["CategoryName"] = item1.CategoryName;
                    dr["Author"] = item1.Author;
                    dt.Rows.Add(dr);
                }
                grvDSbaivietchoDang.DataSource = dt;
                grvDSbaivietchoDang.DataBind();
                Refresh();
            }
        }
        if (txtTuNgay.Text == "" && txtDenNgay.Text == "")
        {
            if (txtTuKhoa.Text == "" && ddlCMuc.SelectedValue == "" && drchuyenmuccon.SelectedValue != "")
            {
                var tindadang5 = from c in thongtin
                                 where ((c.ParentID.ToString() == drchuyenmuccon.SelectedValue))


                                 select new { c.NewsID, c.Title, c.Description, c.CategoryName, c.Author };
                foreach (var item1 in tindadang5)
                {
                    dr = dt.NewRow();
                    dr["NewsID"] = item1.NewsID;
                    dr["Title"] = item1.Title;
                    dr["Description"] = item1.Description;
                    dr["CategoryName"] = item1.CategoryName;
                    dr["Author"] = item1.Author;
                    dt.Rows.Add(dr);
                }
                grvDSbaivietchoDang.DataSource = dt;
                grvDSbaivietchoDang.DataBind();
                Refresh();
            }
        }

    }
    protected void grvDSbaivietchoDang_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label lblMa1 = (Label)grvDSbaivietchoDang.Rows[e.NewEditIndex].FindControl("lblMa");
        Session["mabaiviet"] = lblMa1.Text;
        var LoadNews = st.st_DuyettinChoDang(int.Parse(lblMa1.Text));
        DataList1.DataSource = LoadNews;
        DataList1.DataBind();
    }
    protected void grvDSbaivietchoDang_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDSbaivietchoDang.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void drchuyenmuccon_SelectedIndexChanged(object sender, EventArgs e)
    {
        var tinchodang = from c in st.News
                         where c.CategoryID == int.Parse(drchuyenmuccon.SelectedValue) && c.Status == "0"
                         select new { c.NewsID, c.Title, c.Description, c.Category.CategoryName, c.Category.ParentID, c.Author };
        grvDSbaivietchoDang.DataSource = tinchodang;
        grvDSbaivietchoDang.DataBind();
    }

    protected void grvDSbaivietchoDang_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblMa1 = (Label)grvDSbaivietchoDang.Rows[e.NewSelectedIndex].FindControl("lblMa");
        var tintuc = st.st_UpdateStatus(int.Parse(lblMa1.Text), "1", "Đăng bài viết thành công");
        LoadGrid();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectMe", "alert('Bạn đăng bài thành công');", true);
    }
    protected void lbtDang_Click(object sender, EventArgs e)
    {
        var tintuc = st.st_UpdateStatus(int.Parse(Session["mabaiviet"].ToString()), "1", "Đăng bài viết thành công");

        LoadGrid();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectMe", "alert('Bạn đăng bài thành công');", true);
        Response.Redirect("BaiVietChoDang.aspx");
    }
    protected void lbSua_Click(object sender, EventArgs e)
    {
        try
        {
            var tendaydu = from c in st.Accounts
                           //where c.MemberID == int.Parse(Session["MemberID"].ToString())
                           where (c.Username == Session["Dangnhap"].ToString() && c.MemberID == int.Parse(Session["MemberID"].ToString()))
                           select new { c.Member.FullName };
            foreach (var item in tendaydu)
            {
                var tacgia = from c in st.News where c.Status == "0" select new { c.Author };
                foreach (var item1 in tacgia)
                {

                    if (item1.Author.ToString() == item.FullName.ToString())
                    {
                        var tin = st.st_thongtinbaiviet(int.Parse(Session["mabaiviet"].ToString()));
                        foreach (var item3 in tin)
                        {
                            Response.Redirect("VietBai.aspx?NewsID=" + item3.NewsID);

                        }
                    }
                }

            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectMe", "alert('Bạn đăng bài thành công');", true);
            //Response.Redirect("BaiVietChoDang.aspx");

        }

    }
}

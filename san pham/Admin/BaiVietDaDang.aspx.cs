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

public partial class Admin_Baidadang : System.Web.UI.Page
{
    WebCNPMDataContext st = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string html;
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
        if (!IsPostBack)
        {
            LoadGrid();
            LoadChuyenMuc();
            //LoadDrop1(ddlCMuc, 0);
        }
    }

    //Load chuyen muc len dropdownlist
    public void LoadChuyenMuc()
    {
        //var chuyenmuc = from c in st.Categories select new { c.CategoryID, c.CategoryName };
        string ma = Session["MemberID"].ToString();
        var ctid = st.LoadQuyen(int.Parse(ma));
        ddlCMuc.DataSource = ctid;
        ddlCMuc.DataTextField = "CategoryName";
        ddlCMuc.DataValueField = "CategoryID";
        ddlCMuc.DataBind();
    }
    //load chuyen muc
    public void LoadDropDownList(
       DropDownList ddlName,
       DataTable dtbName,
       string strTenTruongHienThi,
       string strTenTruongGiaTri)
    {
        ddlName.DataSource = dtbName;
        ddlName.DataTextField = strTenTruongHienThi;
        ddlName.DataValueField = strTenTruongGiaTri;
        ddlName.DataBind();
    }
    private void LoadDrop1(DropDownList drName, int cateID)
    {
        ListItem list = new ListItem();
        ddlCMuc.Items.Clear();
        ddlCMuc.Items.Add(list);
        //drName.Items.Clear();
        //drName.Items.Add(list);
        list.Selected = true;
        list.Value = "0";
        list.Text = "--- Chọn chuyên mục ---";
        var cl = st.st_CayChuyenMuc(cateID);
        DataTable dtb = new DataTable();
        dtb.Columns.Add("ID");
        dtb.Columns.Add("ParentID");
        dtb.Columns.Add("CategoryName");
        DataRow dr;
        foreach (var item in cl)
        {
            dr = dtb.NewRow();
            dr["ID"] = item.CategoryID;
            dr["ParentID"] = item.ParentID;
            dr["CategoryName"] = item.CategoryName;
            dtb.Rows.Add(dr);
        }

        LoadDropDownList(drName, dtb, "CategoryName", "ID");

    }
    public void Refresh()
    {
        txtTuNgay.Text = "";
        txtDenNgay.Text ="";
        txtTuKhoa.Text = "";
    }
    //Load gridview
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
            var tindadang = from c in st.News
                            where c.CategoryID == c.Category.CategoryID && c.Category.ParentID == item.CategoryID && c.Status == "1"
                            select new { c.NewsID, c.Title, c.Description, c.Category.CategoryName, c.Author };
            foreach (var item1 in tindadang)
            {
                dr = dt.NewRow();
                dr["NewsID"] = item1.NewsID;
                dr["Title"] = item1.Title;
                dr["Description"] = item1.Description;
                dr["CategoryName"] = item1.CategoryName;
                dr["Author"] = item1.Author;
                dt.Rows.Add(dr);
            }
            grvDSbaidaDang.DataSource = dt;
            grvDSbaidaDang.DataBind();
        }
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        try
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
                           where c.CategoryID == c.Category.CategoryID && c.Status.ToString() == "1"
                           select new { c.NewsID, c.Title, c.Description, c.CategoryID, c.Category.CategoryName, c.Category.ParentID, c.Author, c.Keyword, c.Posttime };
            foreach (var item in ctid)
            {
                if (txtTuNgay.Text != "" && txtDenNgay.Text != "" &&txtTuKhoa.Text==""&&ddlCMuc.SelectedValue==""&&ddlCMcon.SelectedValue=="")
                {
                    if (((DateTime.Parse(txtTuNgay.Text).Date) <= (DateTime.Parse(txtDenNgay.Text).Date)))
                    {
                        var tindadang1 = from c in thongtin
                                         where (c.ParentID == item.CategoryID &&
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
                if (txtTuNgay.Text != "" && txtDenNgay.Text != "")
                {
                    if ((DateTime.Parse(txtTuNgay.Text).Date) <= (DateTime.Parse(txtDenNgay.Text).Date)
                        && txtTuKhoa.Text != "")
                    {
                        var tindadang3 = from c in thongtin
                                         where ((c.ParentID == item.CategoryID)
                                         && (c.Keyword == txtTuKhoa.Text.Trim()) &&
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
                    //foreach (var item in ctid)
                    //{
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
            grvDSbaidaDang.DataSource = dt;
            grvDSbaidaDang.DataBind();
            Refresh();
            if (ddlCMuc.SelectedValue != "" && ddlCMcon.SelectedValue != ""
                            && txtTuNgay.Text == "" && txtDenNgay.Text == "")
            {
                var tindadang7 = from c in thongtin
                                 where c.CategoryID.ToString() == ddlCMcon.SelectedValue && c.ParentID.ToString() == ddlCMuc.SelectedValue && c.CategoryName.Contains(ddlCMcon.SelectedItem.Text.Trim())
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
                grvDSbaidaDang.DataSource = dt;
                grvDSbaidaDang.DataBind(); Refresh();
            }
            if (txtTuNgay.Text != "" && txtDenNgay.Text != "")
            {
                if (txtTuKhoa.Text != "" && ddlCMuc.SelectedValue != "" && ddlCMcon.SelectedValue != ""
                    && (DateTime.Parse(txtTuNgay.Text).Date) <= (DateTime.Parse(txtDenNgay.Text).Date))
                {
                    var tindadang5 = from c in thongtin
                                     where ((c.ParentID.ToString() == ddlCMuc.SelectedValue)
                                     && (c.CategoryID.ToString() == ddlCMcon.SelectedValue) &&
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
                    grvDSbaidaDang.DataSource = dt;
                    grvDSbaidaDang.DataBind(); Refresh();
                }
            }


        }
        catch (Exception)
        {

            lblThongBao.Text = "Bạn chưa chọn chuyên mục con";
        }

    }
    protected void grvDSbaidaDang_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDSbaidaDang.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void grvDSbaidaDang_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //TextBox txttieude = (TextBox)grvDSbaidaDang.Rows[e.RowIndex].FindControl("txtTieude");
        //Label lblMa = (Label)grvDSbaidaDang.Rows[e.RowIndex].FindControl("lblNewsid");

    }
    protected void grvDSbaidaDang_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblNewsid1 = (Label)grvDSbaidaDang.Rows[e.RowIndex].FindControl("lblNewsid");
        //var tintuc = st.sp_Xoatin(int.Parse(lblNewsid1.Text), "Xóa bài viết thành công");
        //LoadGrid();
        New tintuc = st.News.SingleOrDefault(c => c.NewsID == (int.Parse(lblNewsid1.Text)));
        st.News.DeleteOnSubmit(tintuc);
        st.SubmitChanges();
        LoadGrid();
        Response.Redirect("BaiVietDaDang.aspx");

    }
    protected void grvDSbaidaDang_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Response.Redirect("BaiVietDaDang.aspx");
    }
    protected void lbtTieude_Click(object sender, EventArgs e)
    {
        LinkButton lblTitle = (LinkButton)sender;
        Label lblidNews = (Label)lblTitle.Parent.FindControl("lblNewsid");
        Response.Redirect("<a hrf='~/ChiTiet.aspx?NewsID=" + lblidNews + @"'></a>");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "OpenModalPopup('VietBai.aspx?idNews=" + lblidNews.Text + "',1000,1000);", true);
    }


    protected void ddlCMuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCMcon.Items.Clear();
        var chuyenmuccon = st.st_LayCMcontheoCha(int.Parse(ddlCMuc.SelectedValue));
        ddlCMcon.DataSource = chuyenmuccon;
        ddlCMcon.DataTextField = "CategoryName";
        ddlCMcon.DataValueField = "CategoryID";
        ddlCMcon.DataBind();
    }
}

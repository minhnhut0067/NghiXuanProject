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

public partial class Admin_ChuyenMuc : System.Web.UI.Page
{
     WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents["TrangThai"].ToString() == "DaDangNhap")
        {
            var tt = from c in cn.Accounts
                     where (c.Username == Session["Dangnhap"].ToString() && c.MemberID.ToString() == Session["MemberID"].ToString())
                     select new { c.MemberID, c.Decendalization };
            foreach (var item in tt)
            {
                if (!IsPostBack)
                {
                    LoadDrop1(ddlChuyenMucCha, item.MemberID);
                    LoadDrop1(ddlCMucCha, item.MemberID);
                    LoadGridChuyenMuc(item.MemberID);
                }

                if (Session["MemberID"].ToString() == item.MemberID.ToString() && item.Decendalization == "Qu?n lý bài vi?t")
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        else
        {
            if (Session.Contents["TrangThai"].ToString() == "ChuaDangNhap")
                //Response.Redirect("Login.aspx");
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
        }
    }
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
       ddlName.Items.Insert(0, "-- Chọn chuyên mục --");
   }
    private void LoadDrop1(DropDownList drName, int MemberID)
    {
        ListItem list = new ListItem();
        //ddlChuyenMucCha.Items.Clear();
        //ddlChuyenMucCha.Items.Add(list);
        drName.Items.Clear();
        drName.Items.Add(list);
        list.Selected = true;
        list.Value = "0";
        list.Text = "--- Chọn chuyên mục ---";
        var cl = cn.CayChuyenMuc2(MemberID);
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
    private void LoadGridChuyenMuc(int memberid)
    {
        grvChuyenMuc.DataSource = null;
        grvChuyenMuc.DataBind();
        var a = cn.CayChuyenMuc2(memberid);
        //foreach (var item in a)
        //{
        //    ppppppp.GroupingText = "Tìm thấy <u>" +item.ToString() + "</u> chuyên mục";
        grvChuyenMuc.DataSource = a;
        grvChuyenMuc.DataBind();
        //}


    }
    protected void lbtTimKiem_Click(object sender, EventArgs e)
    {
        var ab = cn.ChuyenMuc_TruyVan(int.Parse("0" + ddlCMucCha.SelectedValue), txtTuKhoa.Text);
        grvChuyenMuc.DataSource = ab;
        grvChuyenMuc.DataBind();
        //ddlCMucCha.SelectedValue = "0";
        //txtTuKhoa.Text = "";
        txtTuKhoa.Text = "";
        ResetForm();
    }
    private void ResetForm()
    {
        
        hdSeach.Value = "";
        txtTenCMuc.Text = "";
        hdAction.Value = "";
        txtmota.Text = "";
    }
    protected void lbtThem_Click(object sender, EventArgs e)
    {
        hdAction.Value = "0";
        string error = "";
        if (txtTenCMuc.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa nhập tên chuyên mục');", true);
        }
                    else
        {
            cn.Category_Edit(int.Parse("0" + hdAction.Value), 0, int.Parse("0" + ddlChuyenMucCha.SelectedValue), txtTenCMuc.Text + "",txtmota.Text+"", ref error);
            if (error + "" != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('" + error + "');", true);
                 var bb = from c in cn.Accounts where c.Username == Session["Dangnhap"].ToString() select new {mmm= c.MemberID};
            foreach (var item in bb)
            {
                LoadGridChuyenMuc(item.mmm);
                ResetForm();
                LoadDrop1(ddlChuyenMucCha, item.mmm);
                LoadDrop1(ddlCMucCha, item.mmm);
            }
                }
        }
    }
    private void LoadChiTietChuyenMuc(int cateID)
   {
       var h = cn.CayChuyenMuc1(cateID);
       foreach (var item in h)
       {
           txtTenCMuc.Text = item.CategoryName + "";
           txtmota.Text = item.Description + "";
           ddlChuyenMucCha.SelectedValue = item.ParentID.ToString()+"";
       }
   }
    protected void lbtSua_Click(object sender, EventArgs e)
    {
        hdAction.Value = "2";
        int numcheck = 0;
        if (grvChuyenMuc.Rows.Count > 0)
        {
            for (int i = 0; i < grvChuyenMuc.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grvChuyenMuc.Rows[i].FindControl("checked");
                Label lblCateID = (Label)grvChuyenMuc.Rows[i].FindControl("lbCateID");
                if (chk.Checked == true)
                {
                    hdCategory.Value = lblCateID.Text + "";
                    numcheck++;

                }
            }
            if (numcheck > 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chỉ được chọn một Chuyên mục để sửa');", true);
            }
            else
            {
                if (numcheck == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa chọn chuyên mục nào nào');", true);
                }
                else
                {


                    LoadChiTietChuyenMuc(int.Parse("0" + hdCategory.Value));
                }
            }

        }
    }
    private void LuuCM(int cateid)
    {
        string error = "";
        if (txtTenCMuc.Text + "" == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa nhập tên cho chuyên mục');", true);
        }
        else
        {
            cn.Category_Edit(int.Parse("0" + hdAction.Value), cateid, int.Parse("0" + ddlChuyenMucCha.SelectedValue),
                txtTenCMuc.Text + "",txtmota.Text + "", ref error);
            if (error + "" != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('" + error + "');", true);
                var bb = from c in cn.Accounts where c.Username == Session["Dangnhap"].ToString() select new { mmm = c.MemberID };
                foreach (var item in bb)
                {
                    LoadGridChuyenMuc(item.mmm);
                    ResetForm();
                    LoadDrop1(ddlCMucCha, item.mmm);
                    LoadDrop1(ddlChuyenMucCha, item.mmm);
                }
                
            }
        }
    }
    protected void lbtLuu_Click(object sender, EventArgs e)
    {
        LuuCM(int.Parse("0" + hdCategory.Value));
    }
    protected void lbtXoa_Click(object sender, EventArgs e)
    {
        hdAction.Value = "1";
        string error = "";
        int numcheck = 0;
        if (grvChuyenMuc.Rows.Count > 0)
        {
            for (int i = 0; i < grvChuyenMuc.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grvChuyenMuc.Rows[i].FindControl("checked");
                Label lblcateid = (Label)grvChuyenMuc.Rows[i].FindControl("lbCateID");
                if (chk.Checked == true)
                {

                    cn.Category_Edit(int.Parse("0" + hdAction.Value), int.Parse("0" + lblcateid.Text), 0, "", "", ref error);
                    numcheck++;
                }
            }
            if (numcheck > 0)
            {
                if (error != "")
                {
                    var bb = from c in cn.Accounts where c.Username == Session["Dangnhap"].ToString() select new { mmm = c.MemberID };
                    foreach (var item in bb)
                    {
                        LoadGridChuyenMuc(item.mmm);
                        ResetForm();
                        LoadDrop1(ddlCMucCha, item.mmm);
                        LoadDrop1(ddlChuyenMucCha, item.mmm);
                        
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('" + error + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn cần chọn ít nhất một Chuyên mục để xóa');", true);

            }
        }
    }
    protected void grvChuyenMuc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvChuyenMuc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvChuyenMuc.PageIndex = e.NewPageIndex;
        var bb = from c in cn.Accounts where c.Username == Session["Dangnhap"].ToString() select new { mmm = c.MemberID };
        foreach (var item in bb)
        {
            LoadGridChuyenMuc(item.mmm);
        }
    }
}

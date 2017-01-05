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
using System.Data.SqlClient;

public partial class ChiTiet : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string idnews = Request.QueryString["NewsID"];
            if (!string.IsNullOrEmpty(idnews))
            {
                int id = Convert.ToInt32(idnews);
                var LoadTin = cn.SP_SelectTin(id);
                DataList1.DataSource = LoadTin;
                DataList1.DataBind();
                ltrChuyenMuc.Text = LoadTieuDeChuyenMuc(id);
                LoadTinHot(id);
                LoadTinMoi(id);
                LtrComment.Text = LoadFeedBack(id.ToString());
            }
            int newsid = Convert.ToInt32(idnews);
            var LoadTin1 = cn.SP_SelectTin(newsid);
            foreach (var item in LoadTin1)
            {
                this.Title = item.Title.ToString();
            }
        }
        lbThongBao.Visible = false;
    }
    private string LoadTieuDeChuyenMuc(int NewsID)
    {
        string html = "";
        var TenChuyenMucCha = cn.LoadTenChuyenMucCha(NewsID);
        var TenChuyenCon = cn.LoadTenChuyenMucCon(NewsID);
        foreach (var c1 in TenChuyenMucCha)
        {
            foreach (var c2 in TenChuyenCon)
            {
                 html += @" <div class='left'>
            <span class='top'><a href='ChuyenMucCha.aspx?CategoryID=" + c1.CategoryID.ToString() + @"'>
                " + c1.CategoryName+ @"</a>
                              <a href='ChuyenMuc.aspx?CategoryID=" + c2.CategoryID.ToString() + @"'> -->
                " + c2.CategoryName + @"</a>
             </span>
         </div>";
            }
        }
        return html;

    }
    private void LoadTinHot(int NewsId)
    {
        var c = cn.Load5TinHotCungChuyenMuc(NewsId);
        DataList2.DataSource = c;
        DataList2.DataBind();
    }
    private void LoadTinMoi(int NewsId)
    {
        var Load = cn.Load5TinMoiCungChuyenMuc(NewsId);
        DataList3.DataSource = Load;
        DataList3.DataBind();
    }
    private string LoadFeedBack(string NewsID)
    {
        string html = "";
        var feedback1 = cn.FeedBack1(int.Parse(NewsID));
        DataTable dt = new DataTable();
        for (int i = 0; i < 8; i++)
            dt.Columns.Add();
        DataRow dr;
        foreach (var item in feedback1)
        {
            dr = dt.NewRow();
            dr[0] = item.FeedbackID;
            dr[1] = item.NewsID;
            dr[2] = item.Title;
            dr[3] = item.Email;
            dr[4] = item.NameReader;
            dr[5] = item.Contents;
            dr[6] = item.Status;
            dr[7] = item.DateComment;
            dt.Rows.Add(dr);
        }
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow item in dt.Rows)
            {
                html += @"<li class='comment_odd'>
                    <div class='author'><span class='name'>" + item[4] + @"</span><span class='wrote'>&nbsp;&nbsp;Viết:</span></div>
                    <div class='submitdate'>" + item[7] + @"</div>
                    <p>" + item[5] + @"</p>
                    </li>
                    ";
            }
        }
        return html;

    }
    public void Refresh()
    {
        txtContents.Text = "";
        txtHoTen.Text = "";
        txtEmail.Text = "";
       
    }
    protected void btGuiYKien_Click(object sender, EventArgs e)
    {
        
        string idnews = Request.QueryString["NewsID"];
        if (txtEmail.Text == "" || txtHoTen.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Họ tên và Email ko được để trống');", true);
        }
        if(txtHoTen.Text!=""&&txtEmail.Text!="")
        {
            if (!string.IsNullOrEmpty(idnews))
            {
                int id = Convert.ToInt32(idnews);
                Feedback fb = new Feedback();
                fb.NewsID = id;
                fb.Email = txtEmail.Text;
                fb.NameReader = txtHoTen.Text;
                fb.Contents = txtContents.Text;
                fb.Status = int.Parse("0");
                fb.DateComment = DateTime.Parse(DateTime.Now.ToString());
                cn.Feedbacks.InsertOnSubmit(fb);
                cn.SubmitChanges();
                lbThongBao.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Cảm ơn bạn đã gửi ý bình luận');", true);
                //lbThongBao.Text = "Bạn đã gửi thành công. Xin cảm ơn!";
                Refresh();

            }
        }
       
    }
    protected void btNhapLai_Click(object sender, EventArgs e)
    {
        Refresh();
        lbThongBao.Text = "";
    }
    protected void btXemTinTheongay_Click(object sender, EventArgs e)
    {
            string idnews = Request.QueryString["NewsID"];
            if (!string.IsNullOrEmpty(idnews))
            {
                int id = Convert.ToInt32(idnews);
                Session["idNews"] = id;
            }
            Response.Redirect("SearchDay.aspx?Date="+txtNgay.Text+"");
    }
}

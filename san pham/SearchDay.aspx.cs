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

public partial class SearchDay : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string tukhoa = Request.QueryString["Date"];
             int NewsID = int.Parse(Session["idNews"].ToString());
       if (!string.IsNullOrEmpty(tukhoa))
       {
           DateTime dt=DateTime.Parse(tukhoa);
           var loadTin = cn.LoadTinTheoNgay(dt, NewsID);
           DataList4.DataSource = loadTin;
           DataList4.DataBind();
           LoadTinHot(NewsID);
           LoadTinMoi(NewsID);
        }
       ltrTieuDeChuyenMuc.Text = LoadTenChuyenMuc(NewsID);
        }
    }
    private string LoadTenChuyenMuc(int NewsID)
    {
        string html = "";
        var TenChuyenMucCha = cn.LoadTenChuyenMucCha(NewsID);
        var TenChuyenCon = cn.LoadTenChuyenMucCon(NewsID);
        foreach (var c1 in TenChuyenMucCha)
        {
            foreach (var c2 in TenChuyenCon)
            {
                html += @" <div class='left'>
            <span class='top'><a href='ChuyenMuc.aspx?CateID=" + c1.CategoryID.ToString() + @"'>
                " + c1.CategoryName + @"</a>
                              <a href='ChuyenMuc.aspx?CateID=" + c2.CategoryID.ToString() + @"'> -->
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
}

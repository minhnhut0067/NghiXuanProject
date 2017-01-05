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

public partial class ChuyenMuc : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cateID = Request.QueryString["CategoryID"];
            if (!string.IsNullOrEmpty(cateID))
            {
                int cateid = Convert.ToInt32(cateID);
                var bantin = cn.Bantinchuyenmuc(cateid);
                DataTable dt = new DataTable();
                dt.Columns.Add("NewsID");
                dt.Columns.Add("Title");
                dt.Columns.Add("Description");
                dt.Columns.Add("CategoryID");
                dt.Columns.Add("Image");
                dt.Columns.Add("Author");
                DataRow dr;
                foreach (var item1 in bantin)
                {
                    dr = dt.NewRow();
                    dr["NewsID"] = item1.NewsID;
                    dr["Title"] = item1.Title;
                    dr["Description"] = item1.Description;
                    dr["CategoryID"] = item1.CategoryID;
                    dr["Image"] = item1.Image;
                    dr["Author"] = item1.Author;
                    dt.Rows.Add(dr);
                }
                CollectionPager1.PageSize = 3;
                // Vì cái k?t qu? truy v?n ra có th? nhi?u nên phân trang s? lu?ng là 8 b?n tin trên/ trang
                CollectionPager1.DataSource = dt.DefaultView;
                // Ð? d? li?u vào phân trang này là m?t b?ng dtb chính là t? th?ng này  DataTable dtb = A.BanTinChuyenMuc(cateid);
                CollectionPager1.BindToControl = DataList4;
                // Ð?y d? li?u khi dã phân trang b?ng collectionpager vào datalist
                DataList4.DataSource = CollectionPager1.DataSourcePaged;
                int cateid1 = Convert.ToInt32(cateID);
                var b = cn.Tentheochuyenmuc(cateid1);
                var c = cn.Tenchuyenmuc(cateid1);
                foreach (var item in b)
                {
                    foreach (var item1 in c)
                    {
                        this.Title = item.CategoryName + "->" + item1.CategoryName;
                    }

                }
                ltrTieuDeChuyenMuc.Text = TieuDeChuyenMuc(cateid);
                ltrLastNews.Text = TinMoiNhat(cateid);
                ltrHotNews.Text = TinHot(cateid);
            }
            
        }
    }
    private string TieuDeChuyenMuc(int categoryID)
    {
        string html = "";

        var ab = cn.Tenchuyenmuc(categoryID);
        var ac = cn.Tentheochuyenmuc(categoryID);
        foreach (var item in ac)
        {
            foreach (var item1 in ab)
            {
                html += @" <div class='left'>
            <span class='top'><a href='ChuyenMucCha.aspx?CategoryID=" + item.CategoryID.ToString() + @"'>
                " + item.CategoryName + @"</a>
                              <a href='ChuyenMuc.aspx?CategoryID=" + item1.CategoryID.ToString() + @"'> -->
                " + item1.CategoryName + @"</a>
             </span>
         </div>";
            }

        }
        return html;
    }
    private string TinMoiNhat(int cateid)
    {
        string html = "";
        var abc = cn.motchuyenmuc(cateid);
        foreach (var item in abc)
        {
            html += @"<div>
                <a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'>
                    <img height='230' width='330' src='" + item.Image + @"'></a>
                 </div>
            <div class='head01'>
                <img src='images/ico_new.gif'>&nbsp<a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'>" + item.Title + @"</a>
            </div>
            <p>" + item.Description + @"</p>";
        }
        return html;
    }
    private string TinHot(int cateid1)
    {
        string html = "";
        var ab = cn.TinHot(cateid1);
        foreach (var item in ab)
        {
            html += @"<li><a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'
                        title='" + item.Description + @"'>" + item.Title + @"</a></li>";
        }
        return html;
    }
}

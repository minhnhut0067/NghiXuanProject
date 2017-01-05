using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrNewCongNghe.Text = LoadTinMoiBenTrai("1");
        ltrMicrosoft.Text = LoadTinMoiBenTrai("2");
        ltrSun.Text = LoadTinMoiBenTrai("3");
        ltrTinMoi.Text = LoadSlideNew();
        cateTinCongNghe.Text = LoadTieuDe("1");
        ltrTinCongNge.Text = LoadTinDau("1");
        ltrTinHot.Text = TinMoi("1");
        CateTinMicrosoft.Text=LoadTieuDe("2");
        ltrTinMicrosoft.Text = LoadTinDau("2");
        ltr3TinLienQuanmicro.Text = TinMoi("2");
        CateSunMicrosoft.Text = LoadTieuDe("3");
        LtrTinSunMicrosystem.Text = LoadTinDau("3");
        ltr3TinLienQuanSun.Text = TinMoi("3");
        CateCongNgheMoi.Text = LoadTieuDe("4");
        LtrTinCongNgheMoi.Text = LoadTinDau("4");
        ltr3TinLienQuanCongNgheMoi.Text = TinMoi("4");
        ltrQuangCao.Text = LoadQuangCao();
    }
    private string LoadTieuDe(string ma)
    {
        string html = "";
        var LoadTieuDe = from c in cn.Categories
                         where c.CategoryID ==int.Parse(ma)
                         select c;
        foreach (var item in LoadTieuDe)
        {

            html += @"<a href='ChuyenMucCha.aspx?CategoryID=" + item.CategoryID.ToString() + @"' style='color: #059BD8; text-decoration: none;'>
                        " + item.CategoryName + @"</a>";
        }
        return html;

    }
    private string TinMoi(string ma)
    {
        string html = "";
        var Load3tin = cn.LoadTinLienQuan(int.Parse(ma));
        foreach (var item in Load3tin)
        {
            
                html += "<img height='5' width='6' src='images/demo/mgod_tintuc_arrow.gif'>&nbsp;";
                html += "<a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'";
                html += "onmouseover='showtip(\"<h3>" + item.Title + "</h3>";
                html += "<img width=\\\"125\\\" height=\\\"100\\\" src=\\\"" + item.Image + "\\\" />";
                html += "<div>" + item.Description + "</div> \");' onmouseout='hidetip();'";
                html += "style='color: black;'  >" + item.Title + @"</a><br>";
        }
        return html;
    }
    private string LoadTinDau(string ma)
    {
        string html = "";
        var LoadTin = cn.LoadTieudetin(int.Parse(ma));
        foreach (var item in LoadTin)
        {
            //html += @"<p style='text-align:left;margin-top:5px;'><strong><a href='ChiTiet.aspx?idNews=" + item.NewsID.ToString() + @"'style='color: blue;'>" + item.Title + @"</a></strong></p><p style='text-align:right;margin-top:5px; align='justify'><img style='float:left;margin:0 10px 0 0;padding:4px;border:1px solid #C7C5C8;clear:left;' alt='' src='" + item.Image + @"'/>" + item.Description + @"</p>";
            html += @"<img height='100' width='100' style='float:left;margin:0 10px 0 0;padding:4px;border:1px solid #C7C5C8;clear:left;' alt='' src='" + item.Image + @"'/><p style='text-align:left;margin-top:5px;'><strong><a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'style='color: #059BD8;'>" + item.Title + @"</a></strong></p><p style='text-align:left;margin-top:5px; align='justify'>" + item.Description + @"</p><br/><br/><br/>";
        }
        return html;
    }
    private string LoadTinMoiBenTrai(string ma)
    {
        string html = "";
        var LoadTin = cn.LoadTinNewChuyenMuc(int.Parse(ma));
        foreach (var item in LoadTin)
        {
            //html += @"<p style='text-align:left;margin-top:5px;'><strong><a href='ChiTiet.aspx?idNews=" + item.NewsID.ToString() + @"'style='color: blue;'>" + item.Title + @"</a></strong></p><p style='text-align:right;margin-top:5px; align='justify'><img style='float:left;margin:0 10px 0 0;padding:4px;border:1px solid #C7C5C8;clear:left;' alt='' src='" + item.Image + @"'/>" + item.Description + @"</p>";
            html += @"<img height='100' width='100' style='float:left;margin:0 10px 0 0;padding:4px;border:1px solid #C7C5C8;clear:left;' alt='' src='" + item.Image + @"'/><p style='text-align:left;margin-top:5px;'><strong><a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'style='color: #059BD8;'>" + item.Title + @"'</a></strong><br/></p><p style='text-align:left;margin-top:5px; align='justify'>" + item.Description + @"</p><br/><br/><br/>";
        }
        return html;
    }
   
    private string LoadSlideNew()
    {
        string html = "";
        int j = 1;
        var LoadTinMoi = cn.Load5BanTinMoi();
        DataTable dt = new DataTable();
        for (int i = 0; i < 11; i++)
            dt.Columns.Add();
        DataRow dr;
        foreach (var item in LoadTinMoi)
        {
            dr = dt.NewRow();
            dr[0] = item.NewsID;
            dr[1] = item.CategoryID;
            dr[2] = item.Title;
            dr[3] = item.Description;
            dr[4] = item.Contents;
            dr[5] = item.Author;
            dr[6] = item.Posttime;
            dr[7] = item.Keyword;
            dr[8] = item.Image;
            dr[9] = item.Link;
            dr[10] = item.Status;
            dt.Rows.Add(dr);
        }
        if (dt.Rows.Count>0)
        {
            foreach (DataRow item in dt.Rows)
            {
                html +=@"<li >
                <img height='280' width='600' src='" + item[8] + @"'/>
                <div class='panel-overlay'>
                <a href='ChiTiet.aspx?NewsID=" + item[0].ToString() + @"'style='color: #059BD8;'><h2>" + item[2] + @"</h2></a>
                <p style='text-align:left;margin-top:5px;'>" + item[3] + @"<a href='ChiTiet.aspx?NewsID=" + item[0].ToString() + @"'style='color: #059BD8;'>" + "&nbsp;&nbsp;Đọc tiếp &raquo;" + @"</a></p>
                   </div></li> ";
            }
                j++;
        }
        return html;
    }
    private string LoadQuangCao()
    {
        string html = "";
        var loadQC = from c in cn.Images
                     where c.Status == int.Parse("1")
                     select c;
        DataTable dt = new DataTable();
        for (int i = 0; i < 5; i++)
            dt.Columns.Add();
        DataRow dr;
        foreach (var item in loadQC)
        {
            dr = dt.NewRow();
            dr[0] = item.ImageID;
            dr[1] = item.LinkImage;
            dr[2] = item.Description;
            dr[3] = item.Status;
            dr[4] = item.ImageAdvertising;
             dt.Rows.Add(dr);
        }
        if (dt.Rows.Count > 0)
        {
            html += @"<div  class='fl_left'><a href='"+dt.Rows[0][1].ToString()+@"'><img src='"+dt.Rows[0][4].ToString()+@"' alt=''/></a></div>
                      <div class='fl_right'><a href='"+dt.Rows[1][1].ToString()+@"'><img src='"+dt.Rows[1][4].ToString()+@"'alt=''/></a></div>
            ";
        }
        return html;
    }
}

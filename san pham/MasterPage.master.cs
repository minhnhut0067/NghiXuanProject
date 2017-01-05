using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    WebCNPMDataContext cn = new WebCNPMDataContext(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr3TinHot.Text = Load3TinHot();
        ltrQuangCao.Text = LoadQuangCao();
    }
    private string Load3TinHot()
    {

        string html = "";
        var LoadTinMoi = cn.Load3TinHot();
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
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow item in dt.Rows)
            {
                html += @"<li class='last'>
                <img height='130' width='190' src='" + item[8] + @"'/>
                <p><a href='ChiTiet.aspx?NewsID=" + item[0].ToString() + @"'style='color: #059BD8;'>" + item[2] + @"</a></p>
                 <p>" + item[3] +@"</p>
                <p class='readmore'><a href='ChiTiet.aspx?NewsID=" + item[0].ToString() + @"'style='color: #059BD8;'>" + "Đọc tiếp &raquo;" + @"</a></p>
                 </li> ";
            }
        }
        return html;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx?Keyword=" + txtSearch.Text + "");
    }
    private string LoadQuangCao()
    {
        string html = "";
        var loadQC = from c in cn.Images
                     where c.Status == int.Parse("0")
                     select c;
        foreach (var item in loadQC)
        {
            html += @"<div class='holder'>
                    <a href=" + item.LinkImage + @"><img src=" + item.ImageAdvertising + @" alt=''/></a>
                    </div>
            ";
        }
        return html;
    }
}

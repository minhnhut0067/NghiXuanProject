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

public partial class ChuyenMucCha : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
         int[] a;
         if (!IsPostBack)
         {
             string cateID = Request.QueryString["CategoryID"];
             if (!string.IsNullOrEmpty(cateID))
             {
                 int cateid = Convert.ToInt32(cateID);
                 int cateid1 = Convert.ToInt32(cateID);
                 var loadtieude = cn.Tenchuyenmuc(cateid1);
                 foreach (var tieude in loadtieude)
                 {
                     this.Title = tieude.CategoryName;
                 }
                 var dl1 = cn.idchuyenmuccon(cateid);
                 DataTable dtb = new DataTable();
                 for (int i = 0; i < 1; i++)
                 dtb.Columns.Add("CategoryID");
                 DataRow dr;
                 foreach (var item in dl1)
                 {
                     dr = dtb.NewRow();
                     dr["CategoryID"] = item.categoryID;
                     dtb.Rows.Add(dr);
                 }
                 Label lblCateID = new Label();
                 if (dtb.Rows.Count > 0)
                 {
                     a = new int[dtb.Rows.Count];
                     for (int i = 0; i < a.Length; i++)
                     {
                         lblCateID.Text = "";
                         lblCateID.Text = dtb.Rows[i]["CategoryID"] + "";
                         a[i] = int.Parse(lblCateID.Text);
                     }
                     for (int j = 0; j < a.Length; j++)
                     {
                         if (a[0] != 0)
                         {
                             var chuyenmuc = cn.LoadTinTheoChuyenMuc(a[0]);
                                     var cc = cn.Muccon(a[0]);
                                     foreach (var item in cc)
                                     {
                                         lbl1.Text = item.CategoryName.ToString();
                                         dtl1.DataSource = chuyenmuc;
                                         dtl1.DataBind();
                                     }
                                     
                             }
                         }
                     if (a[0] != 0)
                     {
                         var chuyenmuc1 = cn.LoadTinTheoChuyenMuc(a[0]);
                                 var cc1 = cn.Muccon(a[0]);
                                 foreach (var item1 in cc1)
                                 {
                                     lbl1.Text = item1.CategoryName.ToString();
                                     dtl1.DataSource = chuyenmuc1;
                                     dtl1.DataBind();
                                 }
                                
                     }
                     if (a[1] != 0)
                     {
                         var chuyenmuc2 = cn.LoadTinTheoChuyenMuc(a[1]);
                         var cc2 = cn.Muccon(a[1]);
                         foreach (var item2 in cc2)
                         {
                             lbl2.Text = item2.CategoryName.ToString();
                             dtl2.DataSource = chuyenmuc2;
                             dtl2.DataBind();
                         }
                         
                     }
                     if (a[2] != 0)
                     {
                         var chuyenmuc3 = cn.LoadTinTheoChuyenMuc(a[2]);
                         var cc3 = cn.Muccon(a[2]);
                         foreach (var item3 in cc3)
                         {
                             lbl3.Text = item3.CategoryName.ToString();
                             dtl3.DataSource = chuyenmuc3;
                             dtl3.DataBind();
                         }
                        
                     }
                     if (a[3] != 0)
                     {
                         var chuyenmuc4 = cn.LoadTinTheoChuyenMuc(a[3]);
                         var cc4 = cn.Muccon(a[3]);
                         foreach (var item4 in cc4)
                         {
                             lbl4.Text = item4.CategoryName.ToString();
                             dtl4.DataSource = chuyenmuc4;
                             dtl4.DataBind();
                         }

                     }
                     if (a[4] != 0)
                     {
                         var chuyenmuc5 = cn.LoadTinTheoChuyenMuc(a[4]);
                         var cc5 = cn.Muccon(a[4]);
                         foreach (var item5 in cc5)
                         {
                             lbl5.Text = item5.CategoryName.ToString();
                             dtl5.DataSource = chuyenmuc5;
                             dtl5.DataBind();
                         }

                     }
                     }
                 ltrTieuDeChuyenMuc.Text = LoadTieuDe(cateid.ToString());
                 ltrLastNews.Text = LoadTinDau(cateid.ToString());
                 ltrHotNews.Text = TinHot(cateid);
                 }
             //int cateid1 = Convert.ToInt32(cateID);
             //var loadtieude = cn.Tenchuyenmuc(cateid1);
             //foreach (var tieude in loadtieude)
             //{
             //    this.Title = tieude.CategoryName;
             //}
             }
         }
    private string LoadTieuDe(string ma)
    {
        string html = "";
        var LoadTieuDe = from c in cn.Categories
                         where c.CategoryID == int.Parse(ma)
                         select c;
        foreach (var item in LoadTieuDe)
        {
            html += @" <div class='left'>
            <span class='top'><a href='ChuyenMucCha.aspx?CategoryID=" + item.CategoryID.ToString() + @"'>
                " + item.CategoryName + @"</a>
             </span>
         </div>";

//            html += @"<a href='ChuyenMucCha.aspx?CateID=" + item.CategoryID.ToString() + @"' style='color: #059BD8; text-decoration: none;'>
//                        " + item.CategoryName + @"</a>";
        }
        return html;

    }
    private string LoadTinDau(string ma)
    {
        string html = "";
        var LoadTin = cn.LoadTieudetin(int.Parse(ma));
        foreach (var item in LoadTin)
        {
            html += @"<div>
                <a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'>
                    <img height='280' width='430' src='" +item.Image + @"'></a>
                 </div>
            <div class='head01'>
                <img src='images/ico_new.gif'>&nbsp<a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'>" + item.Title + @"</a>
            </div>
            <p>" + item.Description + @"</p>";

            //html += @"<img height='100' width='100' style='float:left;margin:0 10px 0 0;padding:4px;border:1px solid #C7C5C8;clear:left;' alt='' src='" + item.Image + @"'/><p style='text-align:left;margin-top:5px;'><strong><a href='ChiTiet.aspx?idNews=" + item.NewsID.ToString() + @"'style='color: #059BD8;'>" + item.Title + @"</a></strong></p><p style='text-align:left;margin-top:5px; align='justify'>" + item.Description + @"</p><br/><br/><br/>";
        }
        return html;
    }

    private string TinHot(int cateid1)
    {
        string html = "";
        var ab = cn.TinHotChuyenMucCha(cateid1);
        foreach (var item in ab)
        {
             html += @"<li><a href='ChiTiet.aspx?NewsID=" + item.NewsID.ToString() + @"'
                        title='" + item.Description + @"'>" + item.Title + @"</a></li>";
        }
        return html;
    }
    }


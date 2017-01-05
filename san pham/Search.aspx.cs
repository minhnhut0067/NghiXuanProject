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

public partial class Search : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tukhoa = Request.QueryString["Keyword"];
            if (!string.IsNullOrEmpty(tukhoa))
            {
                var search = cn.Search(tukhoa);
                DataTable dt = new DataTable();
                dt.Columns.Add("NewsID");
                dt.Columns.Add("CategoryID");
                dt.Columns.Add("Title");
                dt.Columns.Add("Description");
                dt.Columns.Add("Contents");
                dt.Columns.Add("Author");
                dt.Columns.Add("Posttime");
                dt.Columns.Add("Keyword");
                dt.Columns.Add("Image");
                dt.Columns.Add("Link");
                dt.Columns.Add("Status");
                DataRow dr;
                foreach (var item in search)
                {
                    dr = dt.NewRow();
                    dr["NewsID"] = item.NewsID;
                    dr["CategoryID"] = item.CategoryID;
                    dr["Title"] = item.Title;
                    dr["Description"] = item.Description;
                    dr["Contents"] = item.Contents;
                    dr["Author"] = item.Author;
                    dr["Posttime"] = item.Posttime;
                    dr["Keyword"] = item.Keyword;
                    dr["Image"] = item.Image;
                    dr["Link"] = item.Link;
                    dr["Status"] = item.Status;
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                   lbThongBao.Text = "--Tìm thấy <u>" + dt.Rows.Count + "</u> kết quả với từ khóa ''<b>" + tukhoa + "</b>''--";
                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                }
                ////pnSearch.GroupingText = "Tìm thấy <u>" + seach.Count() + "</u> kết quả với từ khóa ''<b>" + tukhoa + "</b>''";
                //DataList1.DataSource = seach;
                //DataList1.DataBind();
            }
        }
    }
}

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

public partial class Admin_QuanLyAnhQuangCao : System.Web.UI.Page
{
    WebCNPMDataContext cn = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
        LoadGridImage();
        }
    }
    private void LoadGridImage()
    {
        var LoadGrid = from c in cn.Images
                       select c;
        grvQuangcao.DataSource = LoadGrid;
        grvQuangcao.DataBind();
    }
    private void LoadGridUpControl(int ImageID)
    {
        var image = from c in cn.Images
                    where c.ImageID == ImageID
                    select c;
        foreach (var item in image)
        {
            if (item.Status == int.Parse("0"))
            {
                txtLink.Text = item.LinkImage + "";
                txtMoTa.Text = item.Description + "";
                rdMaster.Checked = true;

            }
            else if (item.Status == int.Parse("1"))
            {
                txtLink.Text = item.LinkImage + "";
                txtMoTa.Text = item.Description + "";
                rdDefault.Checked = true;
            }

        }
    }
    private void Refresh()
    {
        txtLink.Text = "";
        txtMoTa.Text = "";
        rdDefault.Checked = false;
        rdMaster.Checked = false;
    }
    protected void lbtThem_Click(object sender, EventArgs e)
    {
        if (txtMoTa.Text != "" && txtLink.Text != "")
        {
            Image img = new Image();
            if (rdDefault.Checked == true)
            {
                img.LinkImage = txtLink.Text;
                img.Description = txtMoTa.Text;
                img.Status = int.Parse("1");
                if (FileUpload1.HasFile)
                {
                    string t = "upload/";
                    img.ImageAdvertising = t + FileUpload1.FileName;
                    string path = Server.MapPath("..\\upload") + "\\" + FileUpload1.FileName;
                    FileUpload1.SaveAs(path);

                }
                cn.Images.InsertOnSubmit(img);
                cn.SubmitChanges();
                LoadGridImage();
                Refresh();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn thêm thành công');", true);
               
            }
            if (rdMaster.Checked == true)
            {
                img.LinkImage = txtLink.Text;
                img.Description = txtMoTa.Text;
                img.Status = int.Parse("0");
                if (FileUpload1.HasFile)
                {
                    string t = "upload/";
                    img.ImageAdvertising = t + FileUpload1.FileName;
                    string path = Server.MapPath("..\\upload") + "\\" + FileUpload1.FileName;
                    FileUpload1.SaveAs(path);
                   
                }
                cn.Images.InsertOnSubmit(img);
                cn.SubmitChanges();
                LoadGridImage();
                Refresh();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn thêm thành công');", true);
               
            }
        }
        else if (txtMoTa.Text == "" || txtLink.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn cần nhập đủ thông tin');", true);
        }
    }
    protected void lbtSua_Click(object sender, EventArgs e)
    {
        hdAction.Value = "2";
        int numcheck = 0;
        if (grvQuangcao.Rows.Count > 0)
        {
            for (int i = 0; i < grvQuangcao.Rows.Count; i++)
            {
                CheckBox ck = (CheckBox)grvQuangcao.Rows[i].FindControl("checked");
                Label lbImageID = (Label)grvQuangcao.Rows[i].FindControl("lbImageID");
                if (ck.Checked == true)
                {
                    hdImageID.Value = lbImageID.Text + "";
                    numcheck++;
                }
            }
            if (numcheck > 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chỉ được chọn một mục để sửa');", true);
            }
            else
            {
                if (numcheck == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa chọn mục nào');", true);
                }
                else
                {
                    LoadGridUpControl(int.Parse(hdImageID.Value));
                    LoadGridImage();
                }
            }
        }
    }
    protected void lbtXoa_Click(object sender, EventArgs e)
    {
        hdAction.Value = "2";
        int numcheck = 0;
        if (grvQuangcao.Rows.Count > 0)
        {
            for (int i = 0; i < grvQuangcao.Rows.Count; i++)
            {
                CheckBox ck = (CheckBox)grvQuangcao.Rows[i].FindControl("checked");
                Label lbImageID = (Label)grvQuangcao.Rows[i].FindControl("lbImageID");
                if (ck.Checked == true)
                {
                    hdImageID.Value = lbImageID.Text + "";
                    numcheck++;
                }
            }
            if (numcheck > 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chỉ được chọn một mục để sửa');", true);
            }
            else
            {
                if (numcheck == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa chọn mục nào');", true);
                }
                else
                {
                    Image img = cn.Images.SingleOrDefault(c => c.ImageID == (int.Parse(hdImageID.Value)));
                    cn.Images.DeleteOnSubmit(img);
                    cn.SubmitChanges();
                    LoadGridImage();
                }
            }
        }
    }
    protected void lbtLuu_Click(object sender, EventArgs e)
    {
        Image img = cn.Images.SingleOrDefault(c => c.ImageID == (int.Parse(hdImageID.Value)));
        if (rdDefault.Checked == true)
        {
            img.LinkImage = txtLink.Text;
            img.Description = txtMoTa.Text;
            img.Status = int.Parse("1");
            //if (FileUpload1.HasFile)
            //{
            //    string t = "upload/";
            //    img.ImageAdvertising = t + FileUpload1.FileName;
            //    string path = Server.MapPath("..\\upload") + "\\" + FileUpload1.FileName;
            //    FileUpload1.SaveAs(path);

            //}
            cn.SubmitChanges();
            LoadGridImage();
        }
        if (rdMaster.Checked == true)
        {
            img.LinkImage = txtLink.Text;
            img.Description = txtMoTa.Text;
            img.Status = int.Parse("0");
            //if (FileUpload1.HasFile)
            //{
            //    string t = "upload/";
            //    img.ImageAdvertising = t + FileUpload1.FileName;
            //    string path = Server.MapPath("..\\upload") + "\\" + FileUpload1.FileName;
            //    FileUpload1.SaveAs(path);

            //}
            cn.SubmitChanges();
            LoadGridImage();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn đã lưu thay đổi thành công chọn mục nào');", true);
    }
}

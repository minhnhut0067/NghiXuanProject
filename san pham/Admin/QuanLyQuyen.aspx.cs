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

public partial class Admin_QuanLyQuyen : System.Web.UI.Page
{
    WebCNPMDataContext st = new WebCNPMDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDSThanhVien();
        }
    }

    public void Rong()
    {
        ddlThanhVien.SelectedValue = "0";
        chkbQuyenCM.Items[0].Selected = false;
        chkbQuyenCM.Items[1].Selected = false;
        chkbQuyenCM.Items[2].Selected = false;
        chkbQuyenCM.Items[3].Selected = false;
        chkbQuyenCM.Items[3].Selected = false;
        chkbQuyenCM.Items[4].Selected = false;
        //chkQLBaiViet.Checked = false;
    }
    public void LoadDSThanhVien()
    {
        var thanhvien = st.st_LayThanhvien1();
        ddlThanhVien.DataSource = thanhvien;
        ddlThanhVien.DataTextField = "FullName";
        ddlThanhVien.DataValueField = "MemberID";
        ddlThanhVien.DataBind();
    }
    protected void ddlThanhVien_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkbQuyenCM.Items[4].Selected = true;
        var quyen = from c in st.CateMember1s
                    where c.MemberID.ToString() == ddlThanhVien.SelectedValue
                    select c;
        Account tk = st.Accounts.SingleOrDefault(c => c.MemberID.ToString() == ddlThanhVien.SelectedValue);
        //if ((tk.Decendalization == "Admin") || (tk.Decendalization == "Viết bài") || (tk.Decendalization == "Quản lý chuyên mục"))
        ////if ((tk.Decendalization == "Admin") || (tk.Decendalization == "Viết bài"))
        //{
        //    chkbQuyenCM.Items[4].Selected = true;
        //}
        foreach (var item in quyen)
        {
            if (item.CateTinCongNghe == 1)
            {
                chkbQuyenCM.Items[0].Selected = true;
            }
            else { chkbQuyenCM.Items[0].Selected = false; }
            if (item.CateMicrosoft == 2)
            {
                chkbQuyenCM.Items[1].Selected = true;
            }
            else { chkbQuyenCM.Items[1].Selected = false; }
            if (item.CateSun == 3)
            {
                chkbQuyenCM.Items[2].Selected = true;
            }
            else { chkbQuyenCM.Items[2].Selected = false; }
            if (item.CateCongNgheMoi == 4)
            {
                chkbQuyenCM.Items[3].Selected = true;
            }
            else { chkbQuyenCM.Items[3].Selected = false; }
        }
    }
    protected void btThem_Click(object sender, EventArgs e)
    {
        try
        {
            CateMember1 quyen1 = st.CateMember1s.SingleOrDefault(c => c.MemberID.ToString() == ddlThanhVien.SelectedValue);
            Account tk = st.Accounts.SingleOrDefault(c => c.MemberID.ToString() == ddlThanhVien.SelectedValue);
            if (ddlThanhVien.SelectedValue != "0")
            {
                if ((chkbQuyenCM.Items[0].Selected == false) && (chkbQuyenCM.Items[1].Selected == false) && (chkbQuyenCM.Items[2].Selected == false) && (chkbQuyenCM.Items[3].Selected == false) && (chkbQuyenCM.Items[4].Selected == false))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectMe", "alert('Bạn chưa chọn quyền');", true);

                }
                if (chkbQuyenCM.Items[0].Selected == true)
                {
                    quyen1.CateTinCongNghe = 1;

                }
                else { quyen1.CateTinCongNghe = 0; }
                if (chkbQuyenCM.Items[1].Selected == true)
                {
                    quyen1.CateMicrosoft = 2;

                }
                else { quyen1.CateMicrosoft = 0; }
                if (chkbQuyenCM.Items[2].Selected == true)
                {
                    quyen1.CateSun = 3;

                }
                else { quyen1.CateSun = 0; }
                if (chkbQuyenCM.Items[3].Selected == true)
                {
                    quyen1.CateCongNgheMoi = 4;

                }
                else { quyen1.CateCongNgheMoi = 0; }
                if ((chkbQuyenCM.Items[4].Selected == true) && (chkbQuyenCM.Items[0].Selected == false) && (chkbQuyenCM.Items[1].Selected == false) && (chkbQuyenCM.Items[2].Selected == false) && (chkbQuyenCM.Items[3].Selected == false))
                {
                    tk.Decendalization = "Viết bài";
                }
                if (chkbQuyenCM.Items[4].Selected == true)
                {
                    if ((chkbQuyenCM.Items[0].Selected == true && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == true))
                    {
                        tk.Decendalization = "Admin";
                    }

                    else
                    {
                        if ((chkbQuyenCM.Items[0].Selected == true && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == false)
                            ||((chkbQuyenCM.Items[0].Selected == true && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == false && chkbQuyenCM.Items[3].Selected == true))
                            || ((chkbQuyenCM.Items[0].Selected == true && chkbQuyenCM.Items[1].Selected == false && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == true))
                            || ((chkbQuyenCM.Items[0].Selected == false && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == true))

                            || ((chkbQuyenCM.Items[0].Selected == true && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == false && chkbQuyenCM.Items[3].Selected == false))
                            || ((chkbQuyenCM.Items[0].Selected == true && chkbQuyenCM.Items[1].Selected == false && chkbQuyenCM.Items[2].Selected == false && chkbQuyenCM.Items[3].Selected == false))
                            || ((chkbQuyenCM.Items[0].Selected == false && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == false && chkbQuyenCM.Items[3].Selected == false))
                            || ((chkbQuyenCM.Items[0].Selected == false && chkbQuyenCM.Items[1].Selected == false && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == false))
                            || ((chkbQuyenCM.Items[0].Selected == true && chkbQuyenCM.Items[1].Selected == false && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == false))
                            || ((chkbQuyenCM.Items[0].Selected == false && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == false))
                            || ((chkbQuyenCM.Items[0].Selected == false && chkbQuyenCM.Items[1].Selected == true && chkbQuyenCM.Items[2].Selected == false && chkbQuyenCM.Items[3].Selected == true))
                            || ((chkbQuyenCM.Items[0].Selected == false && chkbQuyenCM.Items[1].Selected == false && chkbQuyenCM.Items[2].Selected == true && chkbQuyenCM.Items[3].Selected == false))
                            )
                        { tk.Decendalization = "Quản lý chuyên mục"; }
                    }
                }
                //else { tk.Decendalization = "Quản lý chuyên mục"; }
                st.SubmitChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectMe", "alert('Bạn cập nhật quyền thành công');", true);
                Rong();
            }
            else
            {
                //lblThongBao.Text = "Bạn chưa chọn thành viên"; Rong(); 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectMe", "alert('Bạn chưa chọn thành viên');", true);

            }

        }
        catch (Exception)
        {
            //lblThongBao.Text = "Bạn chưa chọn thành viên";
        }
    }

}

<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true"
    CodeFile="VietBai.aspx.cs" Inherits="Admin_VietBai" Title="Untitled Page" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table style="width: 100%; border: 1px solid #6495ED;" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" align="center" class="dongtieude">
                <b>QUẢN LÝ BÀI VIẾT -&gt;&nbsp; VIẾT BÀI</b> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="theten" style="width: 150px;">
                <b>Tiêu đề:</b>
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtTieude" runat="server" Width="500px"></asp:TextBox>
                <asp:HiddenField ID="hdUsser" runat="server" Visible="False" />
            &nbsp;</td>
        </tr>
        <tr>
            <td class="theten">
                Chuyên mục cha:
            </td>
            <td class="dieukhien">
                <%--<asp:DropDownList ID=" drChuyenmuc" runat="server" Height="23px" 
                    Width="227px" AppendDataBoundItems="True"
                    AutoPostBack="false">
                    <asp:ListItem Value="0" Selected="True">-----Chọn chuyên mục-----</asp:ListItem>
                </asp:DropDownList>--%><asp:DropDownList ID="drChuyenMuc" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="drChuyenMuc_SelectedIndexChanged" Width="229px" 
                    AppendDataBoundItems="True" Height="25px">
                    <asp:ListItem Value="0" Selected="True">---Chọn chuyên mục cha---</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Label ID="lblMachuyenmuc" runat="server" Text="" Visible="true"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="theten">
                Chuyên mục con:
            </td>
            <td class="dieukhien">
                <asp:DropDownList ID="drchuyenmuccon" runat="server" Height="25px" 
                    Width="231px" AppendDataBoundItems="True">
                    <asp:ListItem Value="0" Selected="True">---Chọn chuyên mục con---</asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td class="theten">
                Ảnh đại diện:
            </td>
            <td class="dieukhien">
                <asp:FileUpload ID="upimg" runat="server" Width="216px" />
                &nbsp;
                <asp:Label ID="lblimg" runat="server" Text="cute.jpg" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="theten">
                Mô tả:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtMota" runat="server" Height="58px" TextMode="MultiLine" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="theten">
                Nội dung:
            </td>
            <td class="dieukhien">
               <%-- <FCKeditorV2:FCKeditor ID="FCKNoidung" runat="server" BasePath="../fckeditor/" Height="300px">
                </FCKeditorV2:FCKeditor>--%>
                <FCKeditorV2:FCKeditor ID="FCKNoidung" runat="server" BasePath="../fckeditor/" Height="300px">
                </FCKeditorV2:FCKeditor>
                </td>
        </tr>
        <tr>
            <td class="theten">
                Tác giả:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtTacgia" runat="server" Width="258px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="theten">
                Liên kết:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtLienket" runat="server" Width="258px"></asp:TextBox>
            </td>
        </tr>
        <%--<tr>
            <td class="theten">
                Thời gian viết:</td>
            <td class="dieukhien">
                <asp:TextBox ID="txtTGianviet" runat="server" Width="258px"></asp:TextBox>
                <cc1:CalendarExtender ID="cldThoigianviet" runat="server" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txtTGianViet">
                </cc1:CalendarExtender>
                                    </td>
        </tr>--%>
        <tr>
            <td class="theten">
                Thời gian viết:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtTGianviet" runat="server" Width="258px"></asp:TextBox>
                <%--<cc1:CalendarExtender ID="cldThoigianviet" runat="server" Enabled="true" Format="MM/dd/yyyy" TargetControlID="txtTGianViet">
                </cc1:CalendarExtender>--%>
            </td>
        </tr>
        <tr>
            <td class="theten">
                Phản hồi:
            </td>
            <td class="dieukhien">
                <asp:RadioButton ID="rdPhanhoi" GroupName="Feedback" runat="server" Text="Được phản hồi"
                    AutoPostBack="false" />
                &nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rdKhongduocphep" GroupName="Feedback" runat="server" AutoPostBack="false"
                    Text="Không được phản hồi" />
            </td>
        </tr>
        <tr>
            <td class="theten">
                Từ khóa:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtTuKhoa" runat="server" Width="258px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; border-width: 0.01px; border-color: #6495ed; border-style: solid;
                border-top: 0px; padding: 5px; border-left: 0px; border-bottom: 0px;" colspan="2"
                valign="middle">
                <div style="text-align:left">
                <asp:Label ID="lblThongBao" runat="server" Text=""  CssClass="LabelThongbao"></asp:Label>
                </div>
                
                <asp:CheckBox ID="ckDangbai" runat="server" Text="Đăng bài" />&nbsp; &nbsp;&nbsp;<asp:CheckBox
                    ID="ckDuyetbai" runat="server" Text="Duyệt bài" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btGuibai" runat="server" Text="Gửi bài viết" Font-Bold="true" OnClick="btGuibai_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btLamLai" runat="server" Text="Làm lại" Width="100px" Font-Bold="true"
                    OnClick="btLamLai_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

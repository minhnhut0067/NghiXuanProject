<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true"
    CodeFile="ThongTinCaNhan.aspx.cs" Inherits="Admin_TTCN" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <center>
        <table cellpadding="0" cellspacing="0" style="width: 80%" border="1px">
            <tr>
                <td colspan="2" align="center" class="dongtieude">
                    <b>QUẢN TRỊ HỆ THỐNG -&gt;&nbsp;THÔNG TIN CÁ NHÂN </b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblThongtin" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Tên đăng nhập
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtTenDN" ReadOnly="true" runat="server" Width="311px" MaxLength="100"></asp:TextBox>
                    <%--<br />--%>
                   <%-- <asp:Label ID="lblmaTV" runat="server" Text=""></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Họ tên
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtHoten" runat="server" Width="311px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Ngày sinh
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtNgaysinh" runat="server" MaxLength="50" Width="311px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cldNgaySinh" runat="server" Enabled="true" Format="MM/dd/yyyy"
                        TargetControlID="txtNgaySinh">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Email
                </td>
                <td class="dieukhien" align="left">
                    <asp:TextBox ID="txtEmail" runat="server" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Giới tính
                </td>
                <td class="dieukhien" align="left">
                    <%-- <asp:DropDownList ID="drGioiTinh" runat="server" Height="22px" Width="137px">
                   
                
                </asp:DropDownList>--%>
                    <asp:TextBox ID="txtGioiTinh" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Chứng minh thư nhân dân
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtCMTND" runat="server" MaxLength="30" Width="311px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Địa chỉ
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtDiaChi" runat="server" Width="311px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Số điện thoại
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtSDT" runat="server" MaxLength="30" Width="311px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Ngày kích hoạt tài khoản
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtNgayKichhoatTK" runat="server" MaxLength="50" Width="311px" ReadOnly="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="cldNgayKichhoat" runat="server" Enabled="true" Format="MM/dd/yyyy"
                        TargetControlID="txtNgayKichhoatTK">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Quyền hạn
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtQuyen" runat="server" ReadOnly="true" Width="311px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" class="theten">
                    Trạng thái hoạt động
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtTrangThaiHD" runat="server" Width="311px" ReadOnly="true" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
             <tr>
            <td colspan="2"  class="dongdieukhien" align="center">
            
                <asp:Button ID="btCapNhatTT" runat="server" Text="Cập nhật thông tin" 
                    Width="133px" Font-Bold="true" BackColor="Red" ForeColor="Yellow" 
                    Height="28px" onclick="btCapNhatTT_Click" > </asp:Button >&nbsp;&nbsp;
                    <%--<asp:Button ID="btHuy" runat="server" Text="Hủy" 
                    Width="133px" Font-Bold="true"> </asp:Button >--%>
            </td>
        </tr>
        </table>
    </center>
</asp:Content>

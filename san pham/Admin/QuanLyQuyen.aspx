<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true"
    CodeFile="QuanLyQuyen.aspx.cs" Inherits="Admin_QuanLyQuyen" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table style="border: 1px solid #6495ED" width="120px" cellpadding="0px" cellspacing="0px"
        class="style1">
        <tr>
            <td colspan="2" class="dongtieude" align="center">
                <b>QUẢN TRỊ HỆ THỐNG -&gt;&nbsp;QUẢN LÝ QUYỀN</b>
            </td>
        </tr>
        <tr>
      
        </tr>
        <tr>
            <td class="theten" style="width: 262px">
                Chọn thành viên
            </td>
            <td class="dieukhien">
                <asp:DropDownList ID="ddlThanhVien" runat="server" Height="25px" Width="265px" AutoPostBack="true"
                    onselectedindexchanged="ddlThanhVien_SelectedIndexChanged" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Value="0">---Chọn thành viên---</asp:ListItem>
                </asp:DropDownList>
        </tr>
        <tr>
            <td style="width: 180px" class="theten">
                Quyền:
            </td>
            <td class="dieukhien">
               <%--&nbsp;<asp:CheckBox ID="chkQLBaiViet" runat="server" Text="Viết bài" />--%>
               <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                &nbsp;<br />--%>
                <asp:CheckBoxList ID="chkbQuyenCM" runat="server" TextAlign="Right">
                    <asp:ListItem Value="0" >Tin công nghệ</asp:ListItem>
                    <asp:ListItem Value="1">Microsoft</asp:ListItem>
                    <asp:ListItem Value="2">SunMicrosoft</asp:ListItem>
                    <asp:ListItem Value="3">Công nghệ mới</asp:ListItem>
                    <asp:ListItem Value="3">Viết bài</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>        
        <%--<tr>
            <td colspan="2" 
                style="text-align: left; text-decoration: bold; color: Red; height: 25px;">
                <asp:Label ID="lblThongBao" runat="server" Text="" CssClass="LabelThongbao"></asp:Label>
            </td>
        </tr>--%>
        <tr>
        <td colspan="2" align="center" class="dongdieukhien" valign="middle">
            <asp:Button ID="btThem" runat="server" Text="Cập nhật quyền" 
                onclick="btThem_Click" />&nbsp;
            </td>
        </tr>
        
    </table>
</asp:Content>

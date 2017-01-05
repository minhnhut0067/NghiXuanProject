<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true" CodeFile="DoiMatKhau.aspx.cs" Inherits="Admin_DoiMatKhau" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <center>
    <table cellpadding="0" cellspacing="0" border="1px" width="400px">
                <tr>
                    <td colspan="2" style=" height: 31px; border:0px; background-color: #5f9ae0;font-size: 16px; font-weight: bold;" align="center">
                       Đổi mật khẩu
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height:22px; border-top:1px; border-left:0px; border-right:0px; " >
                    </td>
                </tr>
                <tr>
                    <td style="width:40%;" class="theten">
                        Tên đăng nhập
                    </td>
                    <td class="dieukhien" style="margin-left:1px; padding-left:1px;"  >
                        <asp:TextBox ID="txtUserName" runat="server" Width="97%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%" class="theten">
                        Mật khẩu
                        cũ</td>
                    <td  class="dieukhien">
                        <asp:TextBox ID="txtPasswordcu" runat="server" Width="97%" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="width:40%" class="theten">
                        Mật khẩu mới
                    </td>
                    <td class="dieukhien">
                        <asp:TextBox ID="txtpassmoi" runat="server" Width="97%" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td  class="theten">
                        Nhập lại mật khẩu mới</td>
                    <td  class="dieukhien">
                        <asp:TextBox ID="txtnhappassmoi" runat="server" Width="97%" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 38px; font-size:16px; border:0px; padding-top:5px; " colspan="2"   >
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btDoiMK" runat="server" Text="Đổi mật khẩu" 
                            onclick="btDoiMK_Click" Width="116px" 
                              />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       
                        <asp:Label ID="lblThongbao" runat="server" Text="" CssClass="LabelThongbao"></asp:Label>
                    &nbsp;&nbsp;
                    </td>
            
                </tr>
               <%-- <tr>
                    </td><td align="center" style="padding-left: 10px; line-height: 28px" colspan="2">
                        <asp:Label ID="lblthongbao" runat="server" Font-Bold="true" Font-Size="12" ForeColor="Red" ></asp:Label>
                        <asp:HyperLink ID="hplQuenMK" runat="server" NavigateUrl="#href=abc">Quên 
                    mật khẩu?</asp:HyperLink>
                    </td>
                </tr>--%>
            </table>
    </center>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true" CodeFile="QuanLyAnhQuangCao.aspx.cs" Inherits="Admin_QuanLyAnhQuangCao" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <table cellpadding="0" cellspacing="1px" 
        style="width: 100%; border: 1px solid blue; height: -12px;">
          <tr>
            <td colspan="2">
                <asp:HiddenField ID="hdAction" runat="server" />
                <asp:HiddenField ID="hdUser" runat="server" />
                <asp:HiddenField ID="hdImageID" runat="server" />
                <asp:HiddenField ID="hdSeach" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="dongtieude" align="center" style="height: 32px"> <b>QUẢN TRỊ HỆ THỐNG -&gt;&nbsp;QUẢN LÝ ẢNH QUẢNG CÁO</b>
                &nbsp;</td>
        </tr>
        <tr>
        <td colspan="2"></td>
        </tr>
        <tr>
            <td class="theten" style="border-top:1px;">Link liên kết:
                &nbsp;</td>
                <td class="dieukhien" style="border-top:1px;">
                    <asp:TextBox ID="txtLink" runat="server" Width="310px"></asp:TextBox>   
            </td>
        </tr>
        <tr>
            <td class="theten">
                Mô tả:</td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtMoTa" runat="server" Width="310px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr >
        <td>Chọn nơi hiển thị ảnh:</td>
                <td class="dieukhien" colspan="2">
                    <asp:RadioButton ID="rdMaster" runat="server" Text="Trang master" />
                    <asp:RadioButton ID="rdDefault" runat="server" Text="Trang Default" />
                   </td>
        </tr>
        <tr>
        <td class="theten">Ảnh đại diện</td>
        <td class="dieukhien"> 
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </td>
        </tr>
        <tr>
            <td class="dongdieukhien"  align="center" colspan="2" style="border-bottom:1px; border-right:0px; border-left:0px; border-top:0px; border-color:#33CCFF">
                <asp:LinkButton ID="lbtThem" CssClass="Link" runat="server" 
                    Font-Underline="false" onclick="lbtThem_Click" > <img src="Image/add.png" style="border:0px;" /><b>&nbsp;Thêm</b></asp:LinkButton>&nbsp;                
                <asp:LinkButton ID="lbtSua" CssClass="Link"  runat="server" 
                    Font-Underline="false" onclick="lbtSua_Click" > <img src="Image/update.png" style="border:0px;" /><b>&nbsp;Sửa</b></asp:LinkButton>&nbsp; 
                <asp:LinkButton ID="lbtXoa" CssClass="Link"  runat="server" 
                    Font-Underline="false" onclick="lbtXoa_Click" > <img src="Image/delete.png" style="border:0px;" /><b>&nbsp;Xóa</b></asp:LinkButton>&nbsp; 
               <asp:LinkButton ID="lbtLuu" CssClass="Link"  runat="server" 
                    Font-Underline="false" onclick="lbtLuu_Click" > <img src="Image/save.png" style="border:0px;" /><b>&nbsp;Lưu</b></asp:LinkButton>
                <br />
                
                
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="dongdieukhien">
                <asp:GridView ID="grvQuangcao" runat="server" AutoGenerateColumns="False" 
                    Width="100%"  PageSize="4" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="checked" runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ImageID" SortExpression="ImageID" 
                            Visible="false">
                            <ItemTemplate>
                                    <asp:Label ID="lbImageID" runat="server" Text='<%# Bind("ImageID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="LinkImage" HeaderText="Link liên kết" />
                        <asp:BoundField DataField="Description" HeaderText="Mô tả" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="ImageAdvertising" 
                            HeaderText="Link ảnh hiển thị" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        
    </table>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true" CodeFile="ChuyenMuc.aspx.cs" Inherits="Admin_ChuyenMuc" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table cellpadding="0" cellspacing="1px" 
        style="width: 100%; border: 1px solid blue; height: -12px;">
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="hdAction" runat="server" />
                <asp:HiddenField ID="hdUser" runat="server" />
                <asp:HiddenField ID="hdCategory" runat="server" />
                <asp:HiddenField ID="hdSeach" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="dongtieude" align="center" style="height: 32px"> <b>QUẢN TRỊ HỆ THỐNG -&gt;&nbsp;QUẢN LÝ CHUYÊN MỤC</b>
                &nbsp;</td>
        </tr>
        <tr>
        <td colspan="2"></td>
        </tr>
        <tr>
            <td class="theten" style="border-top:1px;">Chuyên mục cha
                &nbsp;</td>
                <td class="dieukhien" style="border-top:1px;">
                    <asp:DropDownList ID="ddlChuyenMucCha" runat="server" Height="25px" 
                        Width="236px" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="theten">
                Tên chuyên mục</td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtTenCMuc" runat="server" Width="310px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="theten">
                Mô tả</td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtmota" runat="server" MaxLength="100" Width="310px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="dongdieukhien"  align="center" colspan="2" style="border-bottom:1px; border-right:0px; border-left:0px; border-top:0px; border-color:#33CCFF">
                <asp:LinkButton ID="lbtThem" CssClass="Link" runat="server" 
                    Font-Underline="false" onclick="lbtThem_Click"> <img src="Image/add.png" style="border:0px;" /><b>&nbsp;Thêm</b></asp:LinkButton>&nbsp;                
                <asp:LinkButton ID="lbtSua" CssClass="Link"  runat="server" 
                    Font-Underline="false" onclick="lbtSua_Click"> <img src="Image/update.png" style="border:0px;" /><b>&nbsp;Sửa</b></asp:LinkButton>&nbsp; 
                <asp:LinkButton ID="lbtXoa" CssClass="Link"  runat="server" 
                    Font-Underline="false" onclick="lbtXoa_Click"> <img src="Image/delete.png" style="border:0px;" /><b>&nbsp;Xóa</b></asp:LinkButton>&nbsp; 
                <asp:LinkButton ID="lbtTimKiem" CssClass="Link"  runat="server" 
                    Font-Underline="false" onclick="lbtTimKiem_Click"> <img src="Image/seach.png" style="border:0px;" /><b>&nbsp;Tìm kiếm</b></asp:LinkButton>&nbsp; 
               <asp:LinkButton ID="lbtLuu" CssClass="Link"  runat="server" 
                    Font-Underline="false" onclick="lbtLuu_Click"> <img src="Image/save.png" style="border:0px;" /><b>&nbsp;Lưu</b></asp:LinkButton>
                <br />
                
                
            </td>
        </tr>
        <tr>
            <td  colspan="2" class="dieukhien" align="center" style="border-left:0px;">
               Chuyên mục cha &nbsp;&nbsp; 
                <asp:DropDownList ID="ddlCMucCha" runat="server" Height="22px" Width="208px">
                </asp:DropDownList>&nbsp; &nbsp;
              Từ khóa&nbsp;&nbsp; <asp:TextBox ID="txtTuKhoa" runat="server" Width="214px" MaxLength="100"></asp:TextBox>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="dongdieukhien">
                <asp:GridView ID="grvChuyenMuc" runat="server" AutoGenerateColumns="False" 
                    Width="100%" AllowPaging="True" 
                    onpageindexchanging="grvChuyenMuc_PageIndexChanging" 
                    onselectedindexchanged="grvChuyenMuc_SelectedIndexChanged" PageSize="8" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="checked" runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CategoryID" SortExpression="CategoryID" 
                            Visible="false">
                            <ItemTemplate>
                                    <asp:Label ID="lbCateID" runat="server" Text='<%# Bind("CategoryID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="CategoryID" HeaderText="Mã chuyên mục" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Tên chuyên mục" />
                        <asp:BoundField DataField="Description" HeaderText="Mô tả" 
                            SortExpression="Description" />
                        <asp:BoundField DataField="MenuLevel" HeaderText="Chuyên mục cha/con-0/1" 
                            SortExpression="MenuLevel" />
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


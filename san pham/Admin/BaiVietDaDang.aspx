<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true"
    CodeFile="BaiVietDaDang.aspx.cs" Inherits="Admin_Baidadang" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%--  <table cellpadding="0px" cellspacing="5px" style="width: 100%; border: 1px solid #blue">--%>
    <table style="border: 1px solid #6495ED" width="120px" cellpadding="0px" cellspacing="0px"
        class="style1">
        <tr>
            <td colspan="2" class="dongtieude" align="center">
                <asp:HiddenField ID="hdUser" runat="server" />
                <b>QUẢN TRỊ BÀI VIẾT -&gt;&nbsp;DANH SÁCH BÀI VIẾT ĐÃ ĐĂNG </b>
            </td>
        </tr>
        <tr>
            <td class="dongdieukhien" style="width: 50%">
                <table>
                    <tr>
                        <td>
                            Chọn chuyên mục
                        </td>
                        <%-- <asp:DropDownList ID="ddlCMuc" runat="server" Height="22px"
                    Width="203px" AutoPostBack="false" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Value="0">----Chọn chuyên mục----</asp:ListItem>
                </asp:DropDownList>--%>
                        <td>
                            <asp:DropDownList ID="ddlCMuc" runat="server" Height="24px" Width="100%" AutoPostBack="true"
                                AppendDataBoundItems="True" 
                                onselectedindexchanged="ddlCMuc_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">---Chọn chuyên mục---</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <%--</td>--%>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Chọn chuyên mục con
                        </td>
                        <td>
                            <%--<asp:DropDownList ID="drchuyenmuccon" runat="server" Height="26px" Width="100%" OnSelectedIndexChanged="ddlCMuc_SelectedIndexChanged"
                                AutoPostBack="false" AppendDataBoundItems="True">
                                <asp:ListItem Value="0" Selected="True">---Chọn chuyên mục---</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlCMcon" runat="server" Height="26px" Width="100%" AutoPostBack="false" AppendDataBoundItems="true">
                            <asp:ListItem Value="0" Selected="True">---Chọn chuyên mục---</asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                </table>
    </td>
    <td class="dongdieukhien" style="width: 50%">
       <table width="100%">
                    <tr>
                        <td >
                            Từ ngày 
                            <asp:TextBox ID="txtTuNgay" runat="server" Width="117px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cldTuNgay" Format="MM/dd/yyyy" runat="server" Enabled="true"
                                TargetControlID="txtTuNgay">
                            </cc1:CalendarExtender></td>
                            <td>đến ngày
                            <asp:TextBox ID="txtDenNgay" runat="server" Width="117px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cldDenNgay" runat="server" TargetControlID="txtDenNgay"
                                Format="MM/dd/yyyy" Enabled="true">
                            </cc1:CalendarExtender></td>
                        <%--</td>--%>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Từ khóa <sub><asp:TextBox ID="txtTuKhoa" runat="server" Width="171px" Height="22px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </sub><asp:Button
                                ID="btTimKiem" runat="server" Text="Tìm kiếm" Width="95px" 
                                OnClick="btTimKiem_Click" style="height: 26px" />
                            <%-- &nbsp;&nbsp;   <asp:Button ID="Button1" runat="server" Text="Tìm" 
                                onclick="Button1_Click" />--%>
                        </td>
                    </tr>
                </table>
    </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: left; text-decoration: bold; color: Red">
            <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center" class="dongdieukhien">
            <asp:GridView ID="grvDSbaidaDang" runat="server" AutoGenerateColumns="False" Style="width: 100%"
                AllowPaging="True" PageSize="20" BorderWidth="1px" CellPadding="4" ForeColor="#333333"
                GridLines="None" OnPageIndexChanging="grvDSbaidaDang_PageIndexChanging" OnRowDeleting="grvDSbaidaDang_RowDeleting">
                <RowStyle BackColor="#EFF3FB" BorderStyle="None" />
                <Columns>
                    <asp:TemplateField HeaderText="NewsID" SortExpression="NewsID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblNewsid" runat="server" Text='<%#Bind("NewsID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblNewsid" runat="server" Text='<%#Bind("NewsID") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STT">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtstt" runat="server" ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tiêu đề" SortExpression="Title">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtTieude" runat="server" Text='<%# Eval("Title") %>' OnClick="lbtTieude_Click"
                                Font-Underline="false" ForeColor="black"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTieude" runat="server" Text='<%#Bind("Title") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CategoryName" HeaderText="Chuyên mục" />
                    <asp:BoundField DataField="Author" HeaderText="Tên người đăng" />
                    <%--<asp:CommandField InsertText="" ShowEditButton="True" EditText="Sửa" HeaderText="Chọn" />--%>
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Xóa" />
                    <%-- <asp:TemplateField HeaderText="Sửa">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtSua" runat="server"><img src="Image/update.png" style="border:0px" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Xóa">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtXoa" runat="server"><img src="Image/delete.png" style="border:0px;" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ẩn">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtAn" runat="server" OnClientClick="return confirm ('Bạn có chắc muốn ẩn bài viết này không?')"><img src="Image/check.png" style="border:0px;" /></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAnbaiviet" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>--%>
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

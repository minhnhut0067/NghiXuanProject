<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true"
    CodeFile="BaiVietChoDang.aspx.cs" Inherits="Admin_BaiChoDang" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%-- <table cellpadding="0px" cellspacing="5px" style="width: 100%; border: 1px solid #blue">--%>
    <table style="border: 1px solid #6495ED" width="120px" cellpadding="0px" cellspacing="0px"
        class="style1">
        <tr>
            <td colspan="2" class="dongtieude" align="center">
                <asp:HiddenField ID="hdUser" runat="server" />
                <b>QUẢN TRỊ BÀI VIẾT -&gt;&nbsp;DANH SÁCH BÀI VIẾT CHỜ ĐĂNG
            </td>
        </tr>
        <tr>
            <td class="dongdieukhien" style="width: 50%">
                <table width="100%">
                    <tr>
                        <td style="width: 40%">
                            Chọn chuyên mục cha
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCMuc" runat="server" Height="25px" Width="100%" OnSelectedIndexChanged="ddlCMuc_SelectedIndexChanged"
                                AutoPostBack="true" AppendDataBoundItems="True">
                                <asp:ListItem Value="0" Selected="True">---Chọn chuyên mục---</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Chọn chuyên mục con
                        </td>
                        <td>
                            <asp:DropDownList ID="drchuyenmuccon" runat="server" Height="26px" Width="100%" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="drchuyenmuccon_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">---Chọn chuyên mục---</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMa" runat="server" Text="" CssClass="LabelThongbao"></asp:Label>
            </td>
            <td class="dongdieukhien" style="width: 50%">
                <table width="100%">
                    <tr>
                        <td>
                            Từ ngày
                            <asp:TextBox ID="txtTuNgay" runat="server" Width="117px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cldTuNgay" Format="MM/dd/yyyy" runat="server" Enabled="true"
                                TargetControlID="txtTuNgay">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            đến ngày
                            <asp:TextBox ID="txtDenNgay" runat="server" Width="117px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cldDenNgay" runat="server" TargetControlID="txtDenNgay"
                                Format="MM/dd/yyyy" Enabled="true">
                            </cc1:CalendarExtender>
                        </td>
                        <%--</td>--%>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Từ khóa <sub>
                                <asp:TextBox ID="txtTuKhoa" runat="server" Width="171px" Height="22px"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </sub>
                            <asp:Button ID="btTimKiem" runat="server" Text="Tìm kiếm" Width="95px" OnClick="btTimKiem_Click" />
                            <%-- &nbsp;&nbsp;   <asp:Button ID="Button1" runat="server" Text="Tìm" 
                                onclick="Button1_Click" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left; text-decoration: bold; color: Red">
                <asp:Label ID="lblThongBao" runat="server" Text="" CssClass="LabelThongbao"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="dongdieukhien">
                <asp:Panel ID="Panel1" runat="server" Width="760px">
                    <asp:GridView ID="grvDSbaivietchoDang" runat="server" AutoGenerateColumns="False"
                        Width="100%" OnRowEditing="grvDSbaivietchoDang_RowEditing" AllowPaging="True"
                        OnPageIndexChanging="grvDSbaivietchoDang_PageIndexChanging" PageSize="5" AutoGenerateSelectButton="True"
                        OnSelectedIndexChanging="grvDSbaivietchoDang_SelectedIndexChanging">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Mã bài viết" SortExpression="NewsID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("NewsID") %>' ID="lblMa"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("NewsID") %>' ID="lblMa"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STT">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSTT" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tiêu đề bài viết" SortExpression="Tittle">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtTieudebaiviet" runat="server" Text='<%#Eval("Title") %>' Font-Underline="false"
                                        ForeColor="Black" OnClick="lbtTieudebaiviet_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTieudebaiviet" runat="server" Text='<%Bind("Title") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="Mô tả" Visible="false" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Chuyên mục" />
                            <asp:BoundField DataField="Author" HeaderText="Tác giả" />
                            <asp:CommandField InsertText="" ShowEditButton="True" EditText="Xem bài" HeaderText="Xem bài viết"
                                ControlStyle-CssClass="link_grid">
                                <ControlStyle CssClass="link_grid" />
                            </asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
                &nbsp;
            </td>
        </tr>
        <%--<asp:AccessDataSource></asp:AccessDataSource>------------------------------------------------------------------%>
        <tr>
            <td colspan="2" align="center" class="dongdieukhien">
                <asp:Panel ID="Panel2" runat="server" Width="760px">
                    <asp:DataList ID="DataList1" runat="server">
                        <ItemTemplate>
                            <div class="Box_Button">
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("NewsID") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>&nbsp;
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Posttime") %>'></asp:Label>
                            </div>
                            <div class="tdDescription">
                                <p>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                </p>
                            </div>
                            <div class="divTextContent">
                                <p style="text-align: center">
                                    <center>
                                        <asp:Image ID="Image1" runat="server" CssClass="Anh" 
                                            Width="450px" Height="300px" 
                                            ImageUrl='<%# "BaiVietChoDang.aspx?NewsID="+Eval("Image") %>' 
                                            DescriptionUrl='<%# "BaiVietChoDang.aspx?NewsID="+Eval("NewsID") %>' />
                                    </center>
                                    <p>
                                    </p>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Contents") %>'></asp:Label>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    </p>
                                </p>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <div style="text-align:right; margin-right:10px;">
                        <asp:LinkButton ID="lbSua" runat="server" onclick="lbSua_Click" >Sửa bài viết</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lbtDang" runat="server" onclick="lbtDang_Click" >Đăng bài viết</asp:LinkButton>&nbsp;
                    <%-- <asp:Button ID="Button1" runat="server" Text="Đăng bài" onclick="Button1_Click" />--%>
                     
                    </div>
                   
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

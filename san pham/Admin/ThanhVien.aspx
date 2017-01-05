<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true"
    CodeFile="ThanhVien.aspx.cs" Inherits="Admin_ThanhVien" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table style="border: 1px solid #6495ED" width="120px" cellpadding="0px" cellspacing="0px"
        class="style1">
        <tr>
            <td colspan="2" align="center" style="font-size: 12px; padding-bottom: 4px; padding-top: 4px;
                background-color: #1e90ff; height: 26px;">
                <b>QUẢN TRỊ HỆ THỐNG -&gt;&nbsp;QUẢN LÝ THÀNH VIÊN </b>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 179px" class="theten">
                Họ tên:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtHoTen" runat="server" Width="255px"></asp:TextBox>
                &nbsp;<asp:Label ID="lblma" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 179px" class="theten">
                Ngày sinh:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtNS" runat="server" Width="195px"></asp:TextBox>
                <cc1:CalendarExtender ID="cldNgaySinh" runat="server" Enabled="true" Format="M/d/yyyy"
                    TargetControlID="txtNS">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 179px; height: 26px;" class="theten">
                Giới tính:
            </td>
            <td class="dieukhien" style="height: 26px">
                <asp:DropDownList ID="ddlGioitinh" runat="server" Height="22px" Width="201px">
                    <asp:ListItem>Nam</asp:ListItem>
                    <asp:ListItem>Nữ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 179px" class="theten">
                Email:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtEmail" runat="server" Width="255px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="vldtKTmail" runat="server" ErrorMessage="Hòm thư không đúng định dạng"
                    ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 179px" class="theten">
                Số điện thoại:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtDT" runat="server" Width="255px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 179px" class="theten">
                Số chứng minh thư nhân dân:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtCMTND" runat="server" Width="255px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 179px" class="theten">
                Địa chỉ:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtdiachi" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left" style="color: Red; font-size: 15px; font-weight: bold;
                width: 100%; height: 25px;">
                <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="dongdieukhien" valign="middle">
                <asp:LinkButton ID="lbtThemTV" runat="server" CssClass="Link" OnClick="lbtThemTV_Click">  <img src="Image/user.png" style="border:0px;"/><b>&nbsp;Thêm thành viên</b></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbtSave" runat="server" CssClass="Link" OnClick="lbtSave_Click"> <img src="Image/save.png" style="border:0px;" /><b>&nbsp;Lưu thông tin</b></asp:LinkButton>
                <asp:LinkButton ID="lbtTaoMoi" runat="server" CssClass="Link" OnClick="lbtTaoMoi_Click"> <img src="Image/New.gif" style="border:0px;" /><b>&nbsp;Tạo mới</b></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="padding: 5px;" width="76%">
                <br />
                <asp:Panel runat="server" Width="760px">
                  <%--  <asp:GridView ID="grvThanhvien" runat="server" AutoGenerateColumns="False" Width="760px"
                        DataKeyNames="MemberID" OnRowUpdating="grvThanhvien_RowUpdating" 
                        OnRowEditing="grvThanhvien_RowEditing" OnRowDeleting="grvThanhvien_RowDeleting"
                        AllowPaging="True" CellPadding="5" ForeColor="#333333" GridLines="None"
                        Height="220px" 
                        >--%>
                        <asp:GridView ID="grvThanhvien" runat="server" Width="100%" 
                        AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="grvThanhvien_PageIndexChanging" PageSize="5" OnRowDeleting="grvThanhvien_RowDeleting"
                        OnRowEditing="grvThanhvien_RowEditing" 
                        OnRowUpdating="grvThanhvien_RowUpdating" 
                        onrowcancelingedit="grvThanhvien_RowCancelingEdit1">
                       
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Mã thành viên" SortExpression="MemberID">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("MemberID") %>' ID="lblMa"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("MemberID") %>' ID="lblMa"></asp:Label></EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Địa chỉ" SortExpression="Address">
                                <ItemTemplate>
                                    <%#Eval("Address")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%#Bind("Address")%>' ID="txtdiachi"></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Họ và Tên" SortExpression="FullName">
                                <ItemTemplate>
                                    <%#Eval("FullName")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%#Bind("FullName")%>' ID="txtHoTen"></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                <ItemTemplate>
                                    <%#Eval("Email")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%#Bind("Email")%>' ID="txtmail"></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>                           
                            <asp:CommandField InsertText="" ShowEditButton="True" EditText="Sửa"
                                HeaderText="Chọn" />
                            <asp:CommandField HeaderText="Xóa" ShowDeleteButton="true" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

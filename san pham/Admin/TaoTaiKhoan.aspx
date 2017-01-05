<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true"
    CodeFile="TaoTaiKhoan.aspx.cs" Inherits="Admin_TaoTaiKhoan" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table style="border: 1px solid #6495ED" width="120px" cellpadding="0px" cellspacing="0px"
        class="style1">
        <tr>
            <td colspan="2" align="center" style="font-size: 12px; padding-bottom: 4px; padding-top: 4px;
                background-color: #1e90ff">
                <b>QUẢN TRỊ HỆ THỐNG -&gt;&nbsp;TẠO TÀI KHOẢN CHO THÀNH VIÊN </b>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 180px; height: 30px;" class="theten">
                Chọn thành viên:
            </td>
            <td class="dieukhien" style="height: 30px">
                <%--<asp:DropDownList ID="drmembename" runat="server" AutoPostBack="false" Enabled="true"
                    Width="255px" Height="23px" >
                    <asp:ListItem Value="0" Selected="True" >---Chọn thành viên----</asp:ListItem>
                </asp:DropDownList>--%>
                <asp:DropDownList ID="drmembename" runat="server" AutoPostBack="false" Width="173px"
                    Height="22px" AppendDataBoundItems="True" 
                    onselectedindexchanged="drQuyen_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">---Chọn thành viên---</asp:ListItem>
                   
                </asp:DropDownList>
                &nbsp;
                <%-- <asp:HiddenField ID="hdmemberid" runat="server" Value="MemberID" >
                    </asp:HiddenField>--%>
        </tr>
        <tr>
            <td style="width: 180px" class="theten">
                Tên đăng nhập:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtUsename" runat="server" Width="255px"></asp:TextBox>
                <asp:CustomValidator ID="ctKTussername" runat="server" 
                    ErrorMessage="CustomValidator" Visible="false"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 180px" class="theten">
                Mật khẩu:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtMK" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 180px" class="theten">
                Quyền:
            </td>
            <td class="dieukhien">
                <asp:DropDownList ID="drQuyen" runat="server" AutoPostBack="true" Width="173px"
                    Height="22px" AppendDataBoundItems="True" 
                    onselectedindexchanged="drQuyen_SelectedIndexChanged">
                    <asp:ListItem Value="0">---Chọn quyền---</asp:ListItem>
                    <asp:ListItem Value="1">Admin</asp:ListItem>
                    <asp:ListItem Value="2">Quản lý chuyên mục</asp:ListItem>
                   <%-- <asp:ListItem Value="3">Quản lý bài viết</asp:ListItem>--%>
                </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkQLBaiViet" runat="server" Text="Viết bài" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                &nbsp;<br />
                <asp:CheckBoxList ID="chkbQuyenCM" runat="server" TextAlign="Right">
                    <asp:ListItem Value="0" >Tin công nghệ</asp:ListItem>
                    <asp:ListItem Value="1">Ngôn ngữ lập trình</asp:ListItem>
                    <asp:ListItem Value="2">Cơ sở dữ liệu</asp:ListItem>
                    <asp:ListItem Value="3">Định hướng nghề nghiệp</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td style="width: 180px" class="theten">
                Ngày tạo tài khoản:
            </td>
            <td class="dieukhien">
                <asp:TextBox ID="txtNgaytaoTK" runat="server" Width="195px"></asp:TextBox>
                &nbsp;&nbsp;
                <%--<cc1:CalendarExtender ID="cldNgaytaoTK" runat="server" TargetControlID="txtNgaytaoTK" Enabled="true" Format="MM/dd/yyyy" >
                 </cc1:CalendarExtender>--%>
            </td>
        </tr>
        <tr>
            <td style="width: 180px" class="theten">
                Trạng thái:
            </td>
            <td class="dieukhien">
                <asp:RadioButton ID="rdHoatDong" runat="server" GroupName="grrdTrangthai" Text="Hoạt động" />
                &nbsp;
                <asp:RadioButton ID="rdKhoa" runat="server" GroupName="grrdTrangThai" Text="Khóa" />
            </td>
        </tr>
        <%--<tr>
           <td style="width:180px; height: 37px;" class="theten">
                Trạng thái:</td>
             <td class="dieukhien" style="height: 37px">
                 <asp:RadioButton ID="rdHoatDong" runat="server" Text="Hoạt động"/>
                &nbsp;&nbsp;&nbsp; <asp:RadioButton ID="rdKhoa" runat="server" Text="Khóa"/>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2" align="center" class="dongdieukhien" valign="middle">
                <asp:LinkButton ID="lbtTaoTK" runat="server" CssClass="Link" OnClick="lbtTaoTK_Click">  <img src="Image/user.png" style="border:0px;"/><b>&nbsp;Thêm thành viên</b></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbtLuu" runat="server" CssClass="Link" OnClick="lbtLuu_Click">  <img src="Image/save.png" style="border:0px;"/><b>&nbsp;Lưu thông tin</b></asp:LinkButton> &nbsp; &nbsp;
                 &nbsp;&nbsp;&nbsp;<br />
                <asp:Label ID="lblThongbao" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="padding: 5px;">
                <asp:Panel ID="Panel1" runat="server" Width="760px">
                    <asp:GridView ID="grvTaiKhoan" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="grvTaiKhoan_PageIndexChanging" PageSize="5" OnRowDeleting="grvTaiKhoan_RowDeleting"
                        OnRowEditing="grvTaiKhoan_RowEditing" OnRowUpdating="grvTaiKhoan_RowUpdating"
                        OnRowCancelingEdit="grvTaiKhoan_RowCancelingEdit">
                        <Columns>
                            <asp:TemplateField HeaderText="STT">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <%--<asp:TextBox ID="txtSTT" ReadOnly="true" runat="server"></asp:TextBox>--%>
                                    <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mã thành viên" SortExpression="MemberID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("MemberID") %>' ID="lblMa"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("MemberID") %>' ID="lblMa"></asp:Label></EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tên đăng nhập" SortExpression="Username">
                                <ItemTemplate>
                                    <%#Eval("Username")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" Text='<%#Bind("Username")%>' ID="txtTenDN"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quyền" SortExpression="Decendalization">
                                <ItemTemplate>
                                    <%#Eval("Decendalization")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%#Bind("Decendalization")%>' ID="txtQuyen"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày tạo" SortExpression="RegistrationDate">
                                <ItemTemplate>
                                    <%#Eval("RegistrationDate")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%#Bind("RegistrationDate")%>' ID="txtNgayTao"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trạng thái" SortExpression="Status">
                                <ItemTemplate>
                                    <%#Eval("Status")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%#Bind("Status")%>' ID="txtTrangThai"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:CommandField ShowDeleteButton="True" HeaderText="Xóa" />--%>
                            <asp:CommandField InsertText="" ShowEditButton="True" EditText="Sửa" HeaderText="Chọn" />
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Xóa" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

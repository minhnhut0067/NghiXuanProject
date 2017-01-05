<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage1.master" AutoEventWireup="true" CodeFile="QuanLyFeedback.aspx.cs" Inherits="Admin_QuanLyFeedback" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<table style="border: 1px solid #6495ED" width="120px" cellpadding="0px" cellspacing="0px"
        class="style1">
        <tr>
            <td colspan="2" class="dongtieude" align="center">
                <asp:HiddenField ID="hdUser" runat="server" />
                <b>QUẢN LÝ FEEDBACK-&gt;&nbsp;DANH SÁCH FEEDBACK CHỜ KIỂM DUYỆT
            </td>
        </tr>
        <tr>
            <td class="dongdieukhien" style=" height: 74px;">
                <table>
             <tr>
                <td> 
                 Chọn chuyên mục cha&nbsp;
                 <asp:DropDownList ID="drChuyenMucCha" runat="server" Height="21px" Width="180px"  
                        AutoPostBack="true" 
                        onselectedindexchanged="drChuyenMucCha_SelectedIndexChanged" 
                        AppendDataBoundItems="True">
                     <asp:ListItem>--Chọn chuyên mục cha--</asp:ListItem>
                 </asp:DropDownList>
                </td>
             </tr>
             <tr>
               <td>
               Chọn chuyên mục con&nbsp;
               <asp:DropDownList ID="drChuyenMucCon" runat="server" Height="21px" Width="180px"  
                       AutoPostBack="true" 
                       onselectedindexchanged="drChuyenMucCon_SelectedIndexChanged" 
                       AppendDataBoundItems="True">
                   <asp:ListItem>--Chọn chuyên mục con--</asp:ListItem>
               </asp:DropDownList>
               </td> 
             </tr>
                </table>
                 <asp:Label ID="lblMa" runat="server" Text="" CssClass="LabelThongbao"></asp:Label>
            </td>
            
            <td class="dongdieukhien" style="height: 74px">
                <table>
                    <tr>
                        <td>
                            Từ ngày&nbsp;
                            <asp:TextBox ID="txtTuNgay" runat="server" Width="117px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cldTuNgay" Format="MM/dd/yyyy" runat="server" Enabled="true" TargetControlID="txtTuNgay">
                            </cc1:CalendarExtender>
                            đến ngày
                            <asp:TextBox ID="txtDenNgay" runat="server" Width="117px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cldDenNgay" runat="server" TargetControlID="txtDenNgay" Format="MM/dd/yyyy"
                                Enabled="true">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button
                                ID="btTimKiem" runat="server" Text="Tìm kiếm" Width="95px" 
                                onclick="btTimKiem_Click"  />
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
                <asp:GridView ID="grvFeedback" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" onpageindexchanging="GridView1_PageIndexChanging" 
                    PageSize="5" onselectedindexchanged="grvFeedback_SelectedIndexChanged" 
                    onrowediting="grvFeedback_RowEditing" 
                    onrowdeleting="grvFeedback_RowDeleting">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                      <%--  <asp:TemplateField HeaderText="Mã Feedback" SortExpression="FeedbackID"></asp:TemplateField>--%>
                      <asp:TemplateField HeaderText="Mã bài viết" SortExpression="NewsID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("FeedbackID") %>' ID="lblMa"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label runat="server" Text='<%#Bind("FeedbackID") %>' ID="lblMa"></asp:Label></EditItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="Title" HeaderText="Tiêu đề bản tin" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="NameReader" HeaderText="Tên hiển thị" />
                        <asp:BoundField DataField="Contents" HeaderText="Ý kiến phản hồi" />
                        <asp:BoundField DataField="DateComment" HeaderText="Thời gian " />
                        <asp:CommandField EditText="Chọn đăng" HeaderText="Đăng Feedback" 
                            ShowEditButton="True" />
                        <asp:CommandField DeleteText="Xóa bài" EditText="" HeaderText="Xóa FeedBack" 
                            ShowDeleteButton="True" />
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


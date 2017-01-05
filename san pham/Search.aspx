<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="styles/ChuyenMuc.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <div class= "header-article">
            <asp:Literal ID="ltrTieuDeChuyenMuc" runat="server"></asp:Literal>
      </div>
<br />
  <center>
    <asp:Label ID="lbThongBao" runat="server" Text="Label"></asp:Label>
    <asp:Panel ID="pnSearch" runat="server">
        <asp:DataList ID="DataList1" runat="server" Width="1012px" >
            <ItemTemplate>
                <div class="cont09">
                    <div class="cont09im">
                         <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>">
                            <asp:Image ID="img" runat="server" ImageUrl='<%# Eval("Image") %>'/>
                        </a>
                    </div>
                    <div>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "ChiTiet.aspx?NewsID="+Eval("NewsID") %>' Text='<%# Eval("Title") %>' CssClass="Hyper"></asp:HyperLink>
                        <br />
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </div>
                </div>
            </ItemTemplate>
         </asp:DataList>
    </asp:Panel>
   </center>
</div>
</asp:Content>


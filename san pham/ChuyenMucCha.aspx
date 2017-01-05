<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChuyenMucCha.aspx.cs" Inherits="ChuyenMucCha" Title="Website tin t&#432;&#769;c công nghê&#803; phâ&#768;n mê&#768;m" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="styles/ChuyenMuc.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="styles/ChuyenMuc1.css" rel="stylesheet" type="text/css" />
    <link href="styles/ChiTietTin1.css" rel="stylesheet" type="text/css" />
    <link href="styles/Layout1.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="center">
        <table cellpadding="0" cellspacing="0"  class="bang" >
            <tr>
                <td>
                        <div class="header-article">
                            <asp:Literal ID="ltrTieuDeChuyenMuc" runat="server"></asp:Literal>
                        </div>
                        <div class="topic-left">
                            <div class="dot_Hotnews">
                                <asp:Literal ID="ltrLastNews" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="topic-right">
                            <div class="box-topic">
                                <div class="box-topic-header">
                                    &nbsp;&nbsp;&nbsp; Xem nhiều
                                </div>
                                <div class="box-topic-body">
                                    <ul>
                                        <asp:Literal ID="ltrHotNews" runat="server"></asp:Literal>
                                    </ul>
                                </div>
                                
                            </div>
                            <br class="clear" />
                        </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="Box_Button">
                    <%--<div class="container">
                    <div class="header-article">--%>
                        »&nbsp;<asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
                    <%--</div>--%>
                    </div>
                  <div class="DivCha">
                         <asp:DataList ID="dtl1" runat="server"  Height="679px" BackColor="White" 
                             BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                             ForeColor="Black" GridLines="Horizontal">
                             <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <ItemTemplate>
                                <div class="cont09">
                                    <div class="cont09im">
                                        <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>">
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image") %>' />
                                        </a>
                                    </div>
                                    <div>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "ChiTiet.aspx?NewsID="+Eval("NewsID") %>'
                                            Text='<%# Eval("Title") %>' CssClass="Hyper"></asp:HyperLink>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        <%--<br class="clear" />--%>
                                    </div>
                                </div>
                                
                            </ItemTemplate>
                            
                             <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                             <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            
                        </asp:DataList>
                        
                        <br class="clear" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                     <div class="Box_Button">
                         »&nbsp;<asp:Label ID="lbl2" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="DivCha">
                        <asp:DataList ID="dtl2" runat="server"  Height="679px">
                            <ItemTemplate>
                                <div class="cont09">
                                    <div class="cont09im">
                                        <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>">
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image") %>' />
                                        </a>
                                    </div>
                                    <div>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "ChiTiet.aspx?NewsID="+Eval("NewsID") %>'
                                            Text='<%# Eval("Title") %>' CssClass="Hyper"></asp:HyperLink>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="Box_Button">
                        »&nbsp;<asp:Label ID="lbl3" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="DivCha">
                        <asp:DataList ID="dtl3" runat="server"  Height="679px" CellPadding="3" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <ItemTemplate>
                                <div class="cont09">
                                    <div class="cont09im">
                                        <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>">
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image") %>' />
                                        </a>
                                    </div>
                                    <div>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "ChiTiet.aspx?NewsID="+Eval("NewsID") %>'
                                            Text='<%# Eval("Title") %>' CssClass="Hyper"></asp:HyperLink>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <ItemStyle ForeColor="#000066" />
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:DataList>
                    </div>
                </td>
            </tr>
                <tr>
                <td>
                     <div class="Box_Button">
                          »&nbsp;<asp:Label ID="lbl4" runat="server" Text=""></asp:Label>
                          </div>
                    <div class="DivCha">
                        <asp:DataList ID="dtl4" runat="server"  Height="679px">
                            <ItemTemplate>
                                <div class="cont09">
                                    <div class="cont09im">
                                        <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>">
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image") %>' />
                                        </a>
                                    </div>
                                    <div>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "ChiTiet.aspx?NewsID="+Eval("NewsID") %>'
                                            Text='<%# Eval("Title") %>' CssClass="Hyper"></asp:HyperLink>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                   <div class="Box_Button">
                        »&nbsp;<asp:Label ID="lbl5" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="DivCha">
                        <asp:DataList ID="dtl5" runat="server"  Height="679px">
                            <ItemTemplate>
                                <div class="cont09">
                                    <div class="cont09im">
                                        <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>">
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image") %>' />
                                        </a>
                                    </div>
                                    <div>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "ChiTiet.aspx?NewsID="+Eval("NewsID") %>'
                                            Text='<%# Eval("Title") %>' CssClass="Hyper"></asp:HyperLink>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
            </table>
            </div>
   <%-- </form>--%>
</asp:Content>




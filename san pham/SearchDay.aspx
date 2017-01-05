<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchDay.aspx.cs" Inherits="SearchDay" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="styles/ChiTietTin.css" rel="stylesheet" type="text/css" />
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <link href="styles/ChuyenMuc.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="top">
 <div id="breadcrumb">
    <ul class="header-article">
      <asp:Literal ID="ltrTieuDeChuyenMuc" runat="server"></asp:Literal>
    </ul>
  </div>
<div class="container">
    <div class="content">
        <div class="DivCha">      
  <asp:DataList ID="DataList4" runat="server" >
            <ItemTemplate>
                <div class="cont09">
                    <div class="cont09im">
                        <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>"> 
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image") %>' CssClass="img"/>
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
        <br />
        </div>
   </div>
   <div class="column">
    <div class="subnav">
        <h2 style="color:Blue">Tiêu &#273;i&#7875;m </h2>
        <ul>
         <asp:DataList ID="DataList2" runat="server">
         <ItemTemplate>
         <div>
             <asp:HyperLink ID="HyperLink1" runat="server" CssClass="a" 
                 NavigateUrl='<%# "Chitiet.aspx?NewsID="+ Eval("NewsID") %>' 
                 Text='<%# Eval("Title") %>'></asp:HyperLink>
         </div>
         </ItemTemplate>
          </asp:DataList>
          </ul>
     </div>
   <%-- <div class="column">--%>
      <div class="subnav">
      <h2 style="color:Blue">Tin m&#7899;i nh&#7845;t</h2>
      <ul>
          <asp:DataList ID="DataList3" runat="server">
          <ItemTemplate>
          <div>
           <asp:HyperLink ID="HyperLink2" runat="server" CssClass="a" 
                  NavigateUrl='<%# "Chitiet.aspx?NewsID="+ Eval("NewsID") %>' 
                  Text='<%# Eval("Title") %>'></asp:HyperLink>
          </div>
          </ItemTemplate>
          </asp:DataList>
      </ul>
      </div>
      </div>
       <%--</div>--%>
</div>
</div>
</asp:Content>


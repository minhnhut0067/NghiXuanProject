<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChuyenMuc.aspx.cs" Inherits="ChuyenMuc" Title="Untitled Page" %>

<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <link href="styles/ChiTietTin1.css" rel="stylesheet" type="text/css" />
    <link href="styles/Layout1.css" rel="stylesheet" type="text/css" />
    <link href="styles/ChuyenMuc1.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="left">
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
                                    &nbsp;&nbsp;&nbsp; Xem nhi&#7873;u
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
   <div class="DivCha">
    <%--<div id="hpage_cats">--%>
        <asp:DataList ID="DataList4" runat="server"  Height="679px" 
           EnableViewState="False">
            <ItemTemplate>
                <div class="cont09">
                    <div class="cont09im">
                    
                        <a href="ChiTiet.aspx?NewsID=<%#Eval("NewsID") %>">
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image") %>' />
                            
                        </a>
                    </div>
                    <div >
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "ChiTiet.aspx?NewsID="+Eval("NewsID") %>'
                            Text='<%# Eval("Title") %>' CssClass="Hyper"></asp:HyperLink>
                        <p>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
      <%--  </div>--%>
    </div>
        
        </td>
        </tr>
        <tr>
        <td>
        <div>
        
        <cc1:CollectionPager ID="CollectionPager1" runat="server" BackText="« Trang Tr&#432;&#7899;c"
                LabelText="Trang:" NextText="Trang Ti&#7871;p »" ResultsFormat="Hi&#7879;n t&#7841;i: {0}-{1} (of {2})">
            </cc1:CollectionPager>
        </div>
        </td>
        </tr>
</table>
      </div>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChiTiet.aspx.cs" Inherits="ChiTiet" Title="Website tin t&#432;&#769;c công nghê&#803; phâ&#768;n mê&#768;m" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<meta http-equiv="imagetoolbar" content="no" />
<link rel="stylesheet" href="styles/layout.css" type="text/css" />
<link href="styles/ChiTietTin.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="top">
<div class="wrapper">
<!-- ####################################################################################################### -->
  <div id="breadcrumb">
    <ul class="header-article">
      <asp:Literal ID="ltrChuyenMuc" runat="server"></asp:Literal>
    </ul>
  </div>
  <!-- ####################################################################################################### -->
 <%--<div class="wrapper">--%>
  <div class="container">
    <div class="content">
    <div class="DivCha">
        <asp:DataList ID="DataList1" runat="server" >
        <ItemTemplate>
        <div class="Box_Button">
        <%--<img  src="images/demo/note.gif"/>--%>
            &nbsp;<asp:Label ID="Label1" runat="server" 
                Text='<%# Eval("Title") %>'></asp:Label>&nbsp;
            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Posttime") %>'></asp:Label>
        </div>
        <div class="tdDescription">
        <p>
            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
        </p>
        </div>
        <div class="divTextContent">
        <p style="text-align:center">
           <center> <asp:Image ID="Image1" runat="server"  CssClass="Anh" 
                ImageUrl='<%# Eval("Image") %>' Width="450px" Height="300px"/>
                </center>
            <p>
            </p>
            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Contents") %>'></asp:Label>
            <p>
            </p>
        </p>
        </div>
        <div class="divTacGia">
            <asp:Label ID="Label5" runat="server" Text="Tác gi&#7843;:"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Author") %>'></asp:Label>
        </div>
        <%--<div class="divCuoiBai"></div>--%>
        </ItemTemplate>
        </asp:DataList>
        </div>
      <div id="comments">
      <h2>Ý ki&#7871;n ph&#7843;n h&#7891;i</h2>
      <ul class="commentlist">
        <asp:Literal ID="LtrComment" runat="server"></asp:Literal>
        </ul>
      </div> 
      <h2>G&#432;&#777;i y&#769; kiê&#769;n pha&#777;n hô&#768;i</h2>
      <div id="respond">
  <%-- <form  method="post" runat="server">--%>
    <asp:Label ID="Label7" runat="server" ><small>H&#7885; tên</small></asp:Label>&nbsp;
              <asp:TextBox ID="txtHoTen" runat="server"  ></asp:TextBox> 
                <asp:Label ID="Label8" runat="server" ><small>Email</small></asp:Label> &nbsp;
              <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br /><br />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Không &#273;&#432;&#7907;c &#273;&#7875; tr&#7889;ng" Display="Dynamic" ValidationGroup="error"></asp:RequiredFieldValidator>
              <asp:TextBox ID="txtContents" runat="server" Height="129px" TextMode="MultiLine" 
                  Width="627px"></asp:TextBox><br />
   <asp:Label ID="lbThongBao" runat="server"  Text="Label"></asp:Label> <br />
   <asp:Button ID="btGuiYKien" runat="server" Text="G&#432;&#777;i y&#769; kiê&#769;n" 
        onclick="btGuiYKien_Click" />&nbsp;
   <asp:Button ID="btNhapLai" runat="server" Text="Nhâ&#803;p la&#803;i" 
        onclick="btNhapLai_Click" />
      <%-- </form>--%>
      </div><br /><br />
      <div class="divCuoiBai" >
      <center>
          <asp:Label ID="Label9" runat="server" Text="Xem tin theo nga&#768;y"></asp:Label>
          <asp:TextBox ID="txtNgay" runat="server"></asp:TextBox>
          <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNgay" Enabled="true">
          </cc1:CalendarExtender>
          <asp:Button ID="btXemTinTheongay" runat="server" Text="Xem tin-->" 
              onclick="btXemTinTheongay_Click" />
          </center>
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
      <div class="column">
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
      <div id="featured">
      <asp:Literal ID="Literal2" runat="server"></asp:Literal>
      </div>
      <div class="holder">
      <asp:Literal ID="Literal3" runat="server"></asp:Literal>
      </div>
    </div>
    <br class="clear" />
  </div>
    <%--  <asp:HiddenField ID="hdf_IDNews" runat="server"  Value='<%# Eval("NewsID") %>' />--%>
</div>
</div>
</asp:Content>


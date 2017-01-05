<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Bệnh viện đa khoa Nghi Xuân" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="imagetoolbar" content="no" />
    <link rel="stylesheet" href="styles/layout.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="top">
        <div class="wrapper">
            <div class="container">
                <div class="content">
                    <div id="featured_slide">
                    <ul id="featurednews">  
                          <asp:Literal ID="ltrTinMoi" runat="server"></asp:Literal>                               
                    </ul>
                    </div>
                </div>
                <div class="column">
                    <ul class="latestnews">
                       <li>
                                     <asp:Literal ID="ltrNewCongNghe" runat="server"></asp:Literal> 
                        </li>
                        <li>
                                <asp:Literal ID="ltrMicrosoft" runat="server"></asp:Literal> 
                        </li>
                        <li class="last">
                                <asp:Literal ID="ltrSun" runat="server"></asp:Literal> 
                        </li>
                    </ul>
                </div>
                <br class="clear" />
            </div>
        </div>
        <!-- ####################################################################################################### -->
        <div class="wrapper">
            <div id="adblock">
                <asp:Literal ID="ltrQuangCao" runat="server"></asp:Literal>
                <%--<div class="fl_left">
                    <a href="http://www.asiasoft.com.vn/">
                        <img src="images/demo/asiaSoft.jpg" alt="" /></a>
                        </div>
                <div class="fl_right">
                    <a href="http://www.trananh.vn/">
                        <img src="images/demo/hp.jpg" alt="" /></a>
                   </div>--%>
                <br class="clear" />
            </div>
            <div id="hpage_cats">
                <div class="fl_left">
                    <h2>
                        <asp:Literal ID="cateTinCongNghe" runat="server"></asp:Literal>&raquo;
                    </h2>
                    <div style="width:468px; height:150px; " >
                    <asp:Literal ID="ltrTinCongNge" runat="server"></asp:Literal><br /><br />
                    </div>
                    <table>
                    <tr><td background="images/demo/newhome_tinmoi_bg.gif" align="center"></td></tr>
                    </table>
                    <div> 
                        <asp:Literal ID="ltrTinHot" runat="server"></asp:Literal>
                     </div>
                </div>
                <div class="fl_right">
                    <h2>
                        <asp:Literal ID="CateTinMicrosoft" runat="server"></asp:Literal>&raquo;
                    </h2>
                    <div style="width:468px; height:150px; "  >
                    <asp:Literal ID="ltrTinMicrosoft" runat="server"></asp:Literal><br /><br />
                    </div>
                    <%--<div style="background-image:images/demo/newhome_tinmoi_bg.gif"></div>--%>
                    <table>
                    <tr><td background="images/demo/newhome_tinmoi_bg.gif" align="center"></td></tr>
                    </table>
                    <div> 
                        <asp:Literal ID="ltr3TinLienQuanmicro" runat="server"></asp:Literal></div>
                </div>
                <br class="clear" />
                <div class="fl_left">
                    <h2>
                        <asp:Literal ID="CateSunMicrosoft" runat="server"></asp:Literal>&raquo;
                    </h2>
                    <div style="width:468px; height:150px;"  >
                    <asp:Literal ID="LtrTinSunMicrosystem" runat="server"></asp:Literal><br /><br />
                    </div>
                    <table>
                    <tr><td background="images/demo/newhome_tinmoi_bg.gif" align="center"></td></tr>
                    </table>
                    <div> 
                        <asp:Literal ID="ltr3TinLienQuanSun" runat="server"></asp:Literal></div>
                    </div>
                <div class="fl_right">
                    <h2>
                        <asp:Literal ID="CateCongNgheMoi" runat="server"></asp:Literal>&raquo;
                    </h2>
                    <div style="width:468px; height:150px; " >
                    <asp:Literal ID="LtrTinCongNgheMoi" runat="server"></asp:Literal><br /><br />
                    </div>
                    <table  >
                    <tr  ><td height="1" background="images/demo/newhome_tinmoi_bg.gif" align="center" ></td></tr>
                    </table>
                    <div> 
                        <asp:Literal ID="ltr3TinLienQuanCongNgheMoi" runat="server"></asp:Literal></div>
                </div>
                <br class="clear" />
            </div>
        </div>
    </div>
</asp:Content>

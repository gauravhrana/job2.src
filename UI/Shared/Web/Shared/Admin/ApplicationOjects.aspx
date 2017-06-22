<%@ Page Title="Application Objects" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="ApplicationOjects.aspx.cs" Inherits="Shared.UI.Web.Admin.ApplicationOjects" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
  Application Variables  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="font-weight: bold; color: Black"  border="0">
        
         <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style=" width:150px; vertical-align:top;display: inline-block;">
            <b> Keys</b>
            <div id="divObjects" runat="server" class="DetailControlBorder" style=" padding-left:5px; padding-top:5px;">
                            </div>
            </td>
            <td style=" width:850px; vertical-align:top; overflow:auto;">
            <b> Details</b>
            <div class="DetailControlBorder" style=" overflow:auto;">
                <table cellpadding="2" cellspacing="4"  style=" border-color:#3a4f63; border-width: 2px;">
                    <tr valign="top" style="height:15px;">
                        <td align="left" style="width:10%;">
                            Session Key  :
                        </td>
                        <td  style="width:90%;">
                            <div id="divObjects2" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr style="height:20px;">
                        <td align="left" style="width:10%; ">
                            Object Type  :
                        </td>
                        <td  style="width:90%;">
                            <asp:Label ID="lblObjectType" runat="server" Text=""></asp:Label>
                        </td>
                       
                    </tr>
                    <tr valign="top" style="height:20px;">
                        
                         <td align="left" style="width:12%; ">
                            Object Value :
                        </td>
                        <td align="left" style="width:88%;">
                            <div id="divValue" runat="server" style="overflow: auto">
                            </div>
                        </td>
                    </tr>
                </table>
                </div>
            </td>
            <%--<td>
                <table cellpadding="2" cellspacing="4" border="0" >
                    <tr valign="top">
                        <td width="150px">
                            Application Objects
                        </td>
                        <td width="150px">
                            Object Type
                        </td>
                        <td align="left">
                            Object Value
                        </td>
                    </tr>
                    <tr valign="top">
                        <td width="150px">
                            <div id="divObjects" runat="server">
                            </div>
                        </td>
                        <td width="150px">
                            <asp:Label ID="lblObjectType" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="left">
                            <div id="divValue" runat="server" style="overflow: auto">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>--%>
        </tr>
    </table>
</asp:Content>

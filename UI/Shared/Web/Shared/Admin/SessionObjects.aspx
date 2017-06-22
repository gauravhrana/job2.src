<%@ Page Title="Session Objects" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="SessionObjects.aspx.cs" Inherits="Shared.UI.Web.Admin.SessionObjects" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <b>Session Variables</b>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="font-weight: bold; color: Black; overflow:visible" width="100%;" border="0">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style=" width:150px; height:auto; vertical-align:top;display: inline-block;">
            <b>Keys</b>
            <div id="divObjects" runat="server" class="DetailControlBorder" style=" padding-left:5px; padding-top:5px;">
                            </div>
            </td>
            <td style=" width:850px; height:auto; vertical-align:top; overflow:auto;display: inline-block;">
                <b>Details</b>
            <div class="DetailControlBorder" style=" overflow:auto;" id="detailsPanel" runat="server">
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
        </tr>
        <tr>
            
        </tr>
    </table>
</asp:Content>

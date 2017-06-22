<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="CrossReference.aspx.cs" Inherits="ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
   <asp:Table runat="server">
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Application:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionApplication" runat="server" AppendDataBoundItems="true" 
             OnSelectedIndexChanged="drpSearchConditionApplication_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
         <asp:TableCell>         
            <asp:TextBox runat="server" Text="0" Visible="false" ID="txtSearchConditionApplicationName"/>   
        </asp:TableCell></asp:TableRow></asp:Table><table>
        <tr>
            <td>
                <table border="1" width="400">
                    <tr>
                        <td width="150">
                            Direction: </td><td>
                            <asp:DropDownList Width="300" ID="drpSelection" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                                <asp:ListItem Value="ByNotificationPublisher">NotificationPublisher To NotificationEventType</asp:ListItem><asp:ListItem Value="ByNotificationEventType">NotificationEventType To NotificationPublisher</asp:ListItem></asp:DropDownList></td></tr></table></td></tr><tr>
            <td>
                <asp:PlaceHolder ID="dynNotificationPublisher" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                NotificationPublisher: </td><td>
                                <asp:DropDownList ID="drpNotificationPublisher" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpNotificationPublisher_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfNotificationEventType" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynNotificationEventType" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                NotificationEventType: </td><td>
                                <asp:DropDownList ID="drpNotificationEventType" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpNotificationEventType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfNotificationPublisher" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>




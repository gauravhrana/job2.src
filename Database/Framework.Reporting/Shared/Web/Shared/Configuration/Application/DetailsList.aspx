<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="DetailsList.aspx.cs"
 Inherits="Shared.UI.Web.Configuration.Application.DetailsList" %>

<%@ Reference  Control="Controls/Details.ascx"  %>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
         Application : Details
    </asp:Content>

    <asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="overflow:auto;height:auto;">
    <asp:PlaceHolder ID="plcDetailsList" runat="server"></asp:PlaceHolder>
    </div>
    <div style="text-align:center;">
     <asp:LinkButton ID="btDelete" Text="Delete" OnClick="btnDelete_Click" runat="server" />
                   
    </div>
    </asp:Content>
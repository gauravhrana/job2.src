<%@ Page Title="Language - Details" MasterPageFile="~/MasterPages/Site.master" Language="C#" AutoEventWireup="true"
    CodeBehind="Details.aspx.cs" Inherits="Shared.UI.Web.Configuration.Language.Details"
    EnableEventValidation="false" %>

<%@ Register Src="~/Shared/Controls/ControlDetails.ascx" TagName="DetailsControl" TagPrefix="dc" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-sm-10"></div>
        <div class="col-sm-2">
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged" AutoPostBack="true" />
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <dc:DetailsControl ID="oDetailsControl" EntityName="Language" runat="server" />
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <asp:LinkButton ID="LinkButton1" Text="Update" OnClick="btnUpdate_Click" runat="server" CssClass="btn" />
            <asp:LinkButton ID="LinkButton2" Text="Delete" OnClick="btnDelete_Click" runat="server" CssClass="btn" />
            <asp:LinkButton ID="LinkButton3" Text="Back" OnClick="btnBack_Click" runat="server" CssClass="btn" />
            <asp:LinkButton ID="LinkButton4" runat="server" Text="Clone" OnClick="btnClone_Click" CssClass="btn" />
        </div>
    </div>

</asp:Content>



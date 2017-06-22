﻿<%@ Page Title="WorkTicket - Update" MasterPageFile="~/MasterPages/Site.master" Language="C#"
    AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket.Update"
    EnableEventValidation="false" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register TagPrefix="gnrc" TagName="GenericTool" Src="./Controls/Generic.ascx" %>
<%@ Reference Control="./Controls/Details.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="UpdateContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="col-sm-10"></div>
        <div class="col-sm-2"><asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged" AutoPostBack="true" />
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Style="color: Red;"></asp:Label>
            <asp:PlaceHolder ID="plcUpdateList" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary btn-sm"/>
            <asp:LinkButton ID="btnClone" runat="server" Text="Clone" OnClick="btnClone_Click" CssClass="btn btn-default btn-sm"/>
            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default btn-sm"/>
        </div>
    </div>    

</asp:Content>
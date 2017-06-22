<%@ Page Title="Clone" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Site.master"
    AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="ApplicationContainer.UI.Web.Question.Delete" %>

<%@ Register TagName="DetailsControl" TagPrefix="uc" Src="./Controls/Details.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-sm-10"></div>
        <div class="col-sm-2">
            <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                AutoPostBack="true" Enabled="true" Checked="true" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">                        
            Question Details                                                
            <div style="overflow: auto; height: auto;">
                <asp:PlaceHolder ID="plcDetailsList" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <div class="col-sm-12">              
            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CssClass="btn" OnClick="btnDelete_Click" />
            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click" />            
        </div>
    </div>

</asp:Content>
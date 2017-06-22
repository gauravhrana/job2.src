<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="ViewSettings.aspx.cs" Inherits="Shared.UI.Web.Admin.ViewSettings" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
       <div class="form-group">
                    <label class="col-sm-2 control-label"><asp:Label ID="lblViewName" runat="server"></asp:Label> </label>
                    <div class="col-md-10">  </div>
            </div>
   
    <asp:PlaceHolder ID="plcUS" runat="server"></asp:PlaceHolder>
</asp:Content>

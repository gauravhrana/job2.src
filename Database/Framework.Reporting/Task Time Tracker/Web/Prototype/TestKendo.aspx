<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.Master" CodeBehind="TestKendo.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.TestKendo" %>

<%@ Register Src="~/Shared/Controls/KendoTextEditor/KendoEditor.ascx" TagPrefix="uc1" TagName="KendoEditor" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   
    
</asp:Content>
<asp:Content ID="UpdateContent" ContentPlaceHolderID="MainContent" runat="server">
   <uc1:KendoEditor runat="server" id="KendoEditor" />

     <textarea id="editor2" rows="10" cols="30" style="height:140px; width:800px;" onchange="SetDescription();">
        <%= hdnDescription.Value %>
    </textarea>
    <asp:HiddenField ID="hdnDescription" runat="server" Value="kendo"   />
    
     <script type="text/javascript">
         function SetDescription() {
             alert("hello");
             var txtarea = document.getElementById("editor2");
             var hdndescription = document.getElementById('<%=hdnDescription.ClientID%>');
             hdndescription.value = txtarea.value;
             alert(txtarea.value);

         }
     </script> 
</asp:Content>

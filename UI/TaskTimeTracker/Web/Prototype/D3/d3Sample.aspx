<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="d3Sample.aspx.cs" MasterPageFile="~/MasterPages/Default.master"
    Inherits="ApplicationContainer.UI.Web.Prototype.D3.d3Sample" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
    
    <script>
        var ControlName = "~/Prototype/D3/Controls/d3SampleControl.ascx";
        $.ajax({
            type: "POST",
            url: "CommonWebMethod.aspx/AjaxGetBarCtrl",
            data: "{controlName:'" + ControlName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
             
                $('#dvSearchFilter').html(msg.d);
            }
        });

        
        $(document).ready(function () {
            $("#dvSearchFilter").kendoWindow({
                width: "1000px",
                title: "d3SampleControl",
                height: "500px"
            });
        });

    </script>
</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    
</asp:Content>

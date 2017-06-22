<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Default.master"
    CodeBehind="d3PieSample.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.d3PieSample" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
    
    <script>
        var ControlName = "~/Prototype/D3/Controls/d3PieSampleControl.ascx";
        $.ajax({
            type: "POST",
            url: "d3CommonMasterSample.aspx/AjaxGetBarCtrl",
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

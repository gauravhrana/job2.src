<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master"
    CodeBehind="d3AnimatedSample.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.d3AnimatedSample" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<%--<%@ Register TagPrefix="gnrc" TagName="d3SampleControl" Src="~/Prototype/D3/Controls/d3AnimatedSample.ascx" %>--%>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
    <script>

        $(document).ready(function () {
            d3.select("svg").remove();
            var ControlName = "~/Prototype/D3/Controls/d3AnimatedSample.ascx";
            $.ajax({
                type: "POST",
                url: "CommonWebMethod.aspx/AjaxGetBarCtrl",
                data: "{controlName:'" + ControlName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                failure: function (response) {                  
                    alert(response.d);
                },

                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    debugger;
                    alert("Status: " + textStatus); alert("Error: " + XMLHttpRequest.responseText);
                },
                success: function (msg) {
                    
                    $('#chart').html(msg.d);
                }
            });
        });



    </script>
</asp:Content>
<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
    <div id="chart">
    </div>
    
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
</asp:Content>

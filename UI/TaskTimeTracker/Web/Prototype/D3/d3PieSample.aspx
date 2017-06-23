<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master"
    CodeBehind="d3PieSample.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.d3PieSample" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    

</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">

    <script>
        $(document).ready(function () {            
            var controlName = "~/Prototype/D3/Controls/d3PieSampleControl.ascx";
            
            $.ajax({
                type: "POST",
                url: "CommonWebMethod.aspx/AjaxGetBarCtrl",                
                data: "{'controlName' :'" + controlName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {                  
                    alert(response.d);
                },

                error: function (XMLHttpRequest, textStatus, errorThrown) {                    
                    alert("Status: " + textStatus); alert("Error: " + XMLHttpRequest.responseText);
                }

            });

            function OnSuccess(msg) {               
                $('#dvSearchFilter').html(msg.d);
            }


            $("#dvSearchFilter").kendoWindow({
                width: "1000px",
                title: "d3SampleControl",
                height: "1500px"
            });
        });

    </script>

</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <div id="dvSearchFilter"></div>
</asp:Content>

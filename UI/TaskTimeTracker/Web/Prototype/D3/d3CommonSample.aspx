<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master"
    CodeBehind="d3CommonSample.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.d3CommonSample" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<%@ Register TagPrefix="gnrc" TagName="d3SampleControl" Src="~/Prototype/D3/Controls/d3SampleControl.ascx" %>
<%@ Register TagPrefix="gnrc" TagName="d3PieSampleControl" Src="~/Prototype/D3/Controls/d3PieSampleControl.ascx" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
  
</asp:Content>
<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
    <table>
        <tr>

            <td>
                <gnrc:d3SampleControl ID="mySampleControl" runat="server"  />
            </td>

        </tr>
        <tr>
            <td>
                <gnrc:d3PieSampleControl ID="myPieSampleControl" runat="server" />

            </td>
        </tr>
    </table>

</asp:Content>

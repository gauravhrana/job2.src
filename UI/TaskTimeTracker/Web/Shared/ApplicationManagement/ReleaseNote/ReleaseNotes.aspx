<%@ Page Title="ReleaseNotes" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master"
    CodeBehind="ReleaseNotes.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLog.ReleaseNotes" %>


<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>

<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/DetailsWithChildren.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>

<%@ Register TagPrefix="sc" TagName="StatChart" Src="~/Shared/ApplicationManagement/ReleaseNote/Controls/ReleaseNotesStatChart.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Shared/ApplicationManagement/ReleaseNote/Controls/ReleaseNotesSearchControl.ascx" %>

<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:searchfilter id="oSearchFilter" runat="server" />
</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">

    <asp:Panel ID="dynGridContainer" runat="server"  />

    <div>
        <asp:LinkButton ID="lnkGridStyle" runat="server" OnClick="lnkGridStyle_Click" Text="Classic" />
        <asp:Panel Style="float: right;" runat="server" ID="fontStyleContainer">
            Font Size:
            <asp:LinkButton ID="lnkfontsmall" runat="server" Style="font-size: 12px; color: Blue; font-weight: bold;" OnClick="lnkfontsmall_Click">A</asp:LinkButton>
            <asp:LinkButton ID="lnkfontmedium" runat="server" Style="font-size: 14px; color: Blue; font-weight: bold;" OnClick="lnkfontmedium_Click">A</asp:LinkButton>
            <asp:LinkButton ID="lnkfontlarger" runat="server" Style="font-size: 16px; color: Blue; font-weight: bold;" OnClick="lnkfontlarger_Click">A</asp:LinkButton>
        </asp:Panel>
    </div>

    <div id="Div1">

        <asp:PlaceHolder ID="plcGroupByHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="plcTabHolder" runat="server"></asp:PlaceHolder>

        <div style="background: lightblue; height: 50px;">
            <div class="exportmenuContainer" style="background: lightblue; float: left;">
                <asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div style="float: right;">
                <dc:ExportMenu ID="myExportMenu" runat="server" />
            </div>
        </div>
        <table border="1">
            <tr>
                <td align="left">
                    <asp:Label ID="lblSearchStatus" Text="" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                    <div id="maindiv" runat="server">

                        <asp:Repeater ID="GrdParentGrid" runat="server" OnItemDataBound="GrdParentGrid_RowDataBound">

                            <ItemTemplate>

                                <div id="itemdiv" runat="server">

                                    <table>
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <a href="Details/<%# DataBinder.Eval(Container.DataItem,"ReleaseLogId")%>"><%# DataBinder.Eval(Container.DataItem,"Name")%></a>
                                                    <asp:HiddenField ID="hdnReleaseLogId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ReleaseLogId")%>' />
                                                </b>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnInsert" runat="server" Text="Insert" PostBackUrl="~/ReleaseLogDetail/Insert/" />
                                            </td>
                                            <td align="right">
                                                <span>
                                                    <asp:LinkButton ID='chkbox' runat="server" AutoPostBack="true" Text='[X]' CommandName='<%# Eval("ReleaseLogId") %>' OnClick="chkbox_Click" ForeColor="GrayText" />
                                                </span>
                                            </td>
                                        </tr>
                                    </table>

                                    <dt>
                                        <div id="detailsGridContainer" runat="server"></div>
                                    </dt>

                                </div>
                                <br />

                            </ItemTemplate>

                        </asp:Repeater>

                    </div>
                </td>
            </tr>
        </table>
        
        <asp:PlaceHolder ID="TableReportContent" runat="server">
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="dynChart" runat="server">
            <table>
                <tr>
                    <td>
                        <sc:statchart id="oSC" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>


        <%-- <asp:PlaceHolder ID="TableReportContent" runat="server"/> 
        <dc:GroupList ID="oGroupList" runat="server" Visible="false" />
     <sg:StatGrid ID="oSG" runat="server" />                      --%>
    </div>

</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>


<%@ Page Title="Jira Summary Email" Language="C#" MasterPageFile="~/MasterPages/Schedule/Default.Master" AutoEventWireup="true"
    CodeBehind="JiraSummary.aspx.cs" Inherits="ApplicationContainer.UI.Web.BM.Scheduling.JiraSummary" %>

<%@ Register Src="~/Shared/Controls/DateRange.ascx" TagPrefix="uc1" TagName="DateRange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ControlVisibilityManager" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SearchControlItem" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading">
            Search
        </div>

        <div class="collapse in panel-body" id="pnlSearchParameters">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Person :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpPersons" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Work Date :</label>
                    <div class="col-md-10">
                        <uc1:DateRange runat="server" ID="oDateRange" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Status :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpStatus" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Priority :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpPriority" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Project :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpProject" runat="server">
                            <asp:ListItem Text="None">None</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Group By :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpGroupBy" runat="server">
                            <asp:ListItem Text="None">Person</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Sub Group By :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpSubgroupBy" runat="server">
                            <asp:ListItem Text="None">None</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label"></label>
                <div class="col-md-10">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ListControlItem" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading">
            Issues
        </div>
    <div class="panel panel-body">
        <table>
            <tr>
                <td>
                    <asp:PlaceHolder ID="contentHolder" runat="server"></asp:PlaceHolder>
                      <asp:GridView ID="gridViewJira" runat="server">
            </asp:GridView>

                </td>
            </tr>
        </table>
    </div>

</asp:Content>

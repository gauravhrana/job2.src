<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true"
    CodeBehind="EntityTestData.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Prototype.TestData.EntityTestData" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar" TagPrefix="ucSearchActionBar" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="ContentSectionName" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <div align="left">
        <b>Lists count of Test data & Audit Data per entity on the basis of Application and Database </b>
        <br />
    </div>
    <div class="row">
        <div class="col-sm-11" id="divC">
            <div class="newDivSearch" id="dvSearchFilter">
                <div class="table-bordered" style="border-color: lightblue; background: grey;">

                    <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />

                    <div id="Div1" class="searchFilterHeaderContainer show" runat="server">
                        <div id="tabs" style="border-color: greenyellow; border-width: 2px;" class="ui-tabs">

                            <div id="divTabContentContainer" runat="server" class="k-content" style="background: lightgrey;">

                                <div id="divSearchParam" class="form-horizontal">

                                    <div runat="server" id="containerRow" class="DisplayColumn1">

                                        <div class="form-group">
                                            <div class="col-sm-2 control-label">
                                                <label id="Label1" style="color: black; font-size: small;">Application: </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlApplication" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" CssClass="k-input"></asp:DropDownList>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-2 control-label">
                                                <label id="Label3" style="color: black; font-size: small;">Entity: </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="drpSystemEntity" runat="server" CssClass="k-input"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2 control-label">
                                                <label id="Label4" style="color: black; font-size: small;">Database Name: </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="drpDbName" runat="server" CssClass="k-input"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:Button ID="Button1" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-default" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">

    <table class="table table-bordered" style="background: lightcyan;">
        <tr>
            <td>
                <div id="griddiv" runat="server">
                    <div id="gridContainer" class="gridContainer">
                        <asp:GridView ID="TestAndAuditGrid" AllowPaging="false" Width="500px" AutoGenerateColumns="false"
                            runat="server" CssClass="table table-bordered table-condensed table-hover table-striped">
                            <Columns>
                                <asp:BoundField DataField="Entity Id" HeaderText="Entity Id" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Application Id" HeaderText="Application Id" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Entity Name" HeaderText="Entity Name" />
                                <asp:BoundField DataField="Test Data Count" HeaderText="Test Data Count" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderText="Audit Data Count" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# String.Format("{0:N0}",Convert.ToInt64(DataBinder.Eval
(Container.DataItem, "Audit Data Count"))) %>'
                                            ID="Label2"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
            </td>

        </tr>
    </table>

</asp:Content>

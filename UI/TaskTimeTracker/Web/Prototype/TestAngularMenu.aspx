<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="TestAngularMenu.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestAngularMenu" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchControlItem" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ListControlItem" runat="server">
    <table>
        <tr>
            <td colspan="3">
                <div ng-app>
                    <div class="navbar-collapse" ng-class="isCollapsed ? 'collapse' : 'in'">
                        <ul class="nav navbar-nav">

                            <li class="main btn" ng-repeat="item in menu" ng-mouseover="showSubMenu(item, $index)"></li>
                            <li class="dropdown" data-is-open="nOpen">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" id="R7" class="dropdown-toggle" data-toggle="dropdown" data-toggle="dropdown">Nested Drop Down<b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li class="menu-item dropdown dropdown-submenu">
                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" data-toggle="dropdown">Level 1</a>
                                        <ul class="dropdown-menu">
                                            <li class="menu-item">
                                                <a href="#">Link 1</a>
                                            </li>
                                            <li class="menu-item dropdown dropdown-submenu">
                                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" data-toggle="dropdown">Level 2</a>
                                                <ul class="dropdown-menu">
                                                    <li>
                                                        <a href="#">Link 3</a>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>

                            </li>
                        </ul>

                    </div>
                </div>

            </td>
        </tr>
    </table>
    <table>

        <%-- <tr>
            <td colspan="3">
               <div style="width: 500px; height: 500px; overflow-y: scroll;">
                    <asp:CheckBoxList ID="chkList"  RepeatColumns="2"  RepeatDirection="Vertical" RepeatLayout="Flow" runat="server">
                    </asp:CheckBoxList>
                </div>
                
            </td>
        </tr>--%>
        <tr>
            <td colspan="2"></td>
        </tr>
    </table>
</asp:Content>

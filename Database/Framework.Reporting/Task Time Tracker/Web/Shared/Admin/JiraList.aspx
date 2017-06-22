<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="JiraList.aspx.cs" 
    Inherits="Shared.UI.Web.Admin.JiraList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div class="panel panel-info">
        <div class="panel-heading">
            Search:
        </div>

        <div class="collapse in panel-body" id="pnlSearchParameters">
            <div class="form-horizontal">
                <form novalidate name="searchForm">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">User :</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpUsers" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                   <%-- <div class="form-group">
                        <div class="col-sm-2"></div>
                        <div class="col-md-2">
                            <input type="submit" class="btn btn-primary"
                                ng-click="getJIRAIssues()" value="Search" />
                        </div>
                        <div class="col-md-8"></div>
                    </div>--%>
                </form>
            </div>
        </div>
    </div>

    <div class="panel panel-info" ng-show="showJIRAList">
        <div class="panel-heading">
            Jira Issues
        </div>
        <div class="panel panel-body">

            <asp:GridView ID="gridViewJira" runat="server">
            </asp:GridView>

        </div>
    </div>





</asp:Content>

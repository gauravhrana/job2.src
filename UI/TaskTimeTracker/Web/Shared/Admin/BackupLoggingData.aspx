<%@ Page Title="Logging Backup" Language="C#" MasterPageFile="~/MasterPages/SA/Site.Master" AutoEventWireup="true"
    CodeBehind="BackupLoggingData.aspx.cs" Inherits="Shared.UI.Web.Admin.BackupLoggingData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDate.ClientID%>").datepicker({
                dateFormat: '<%= ConvertDateTimeFormat %>'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <div class="container text-center ">
        <h3>Backup Logging Data</h3>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="col-sm-5">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="panel-title">
                        <h4>Current Status</h4>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-10">
                        <%--<div>Log4Net<asp:Label ID="lblLog4NetCount" runat="server" Text=""></asp:Label></div>
                        <div>User Login<asp:Label ID="lblUserLoginCount" runat="server" Text=""></asp:Label></div>
                        <div>User Login History<asp:Label ID="lblUserLoginHistoryCount" runat="server" Text=""></asp:Label></div>--%>
                        <asp:ListBox ID="lstEntities" runat="server" Rows="10" CssClass="form-control"></asp:ListBox>
                    </div>
                    <div class="col-sm-2 text-right panel-title">
                        <asp:LinkButton ID="btnRandom"
                            runat="server"
                            CssClass="btn btn-primary"
                            OnClick="btnRefresh_Click">
                                <span aria-hidden="true" class="glyphicon glyphicon-refresh"></span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-7">
            <div class="panel panel-default">
                <div class="panel-body">
                    <p class="bg-success">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>

                    <p></p>
                    <div class="container">
                        <div class="form-group">
                            <label for="exampleInputEmail1">Date: </label>
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Entity">Entity: </label>
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstBackupEntity" runat="server" SelectionMode="Multiple" CssClass="form-control">
                                        <asp:ListItem Text="Log4Net" Selected="True">Log4Net</asp:ListItem>
                                        <asp:ListItem Text="UserLogin">UserLogin</asp:ListItem>
                                        <asp:ListItem Text="UserLoginHistory">UserLoginHistory</asp:ListItem>
                                    </asp:ListBox>
                                </div>
                                <div class="col-md-7"></div>
                            </div>
                        </div>
                        <asp:Button ID="btnGetCount" CssClass="btn btn-default" runat="server" Text="Get Count" OnClick="btnGetCount_Click" />&nbsp;
                        <asp:Button ID="btnSubmit" CssClass="btn btn-default" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                    <p></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeBehind="AdminTasks.aspx.cs" Inherits="Shared.UI.Web.Admin.AdminTasks" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>


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
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">
    <b>
        <center>Remove/Delete Unused SuperKeyDetails based on Expiration Date</center>
    </b>
    <br />

    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label ID="lblMessage" runat="server" Text=" " CssClass="col-sm-2 control-label"></asp:Label>
        </div>
    </div>
    <div id="divTabContentContainer" runat="server" class="k-content">
        <div id="divSearchParam" class="form-horizontal">
            <div runat="server" id="containerRow">
                <div class="form-group">
                    <div class="col-sm-2">
                        <label id="lblApplicationId" runat="server" class="col-sm2 control-label">Application:</label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlApplication" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-2">
                        <label id="lblDate" runat="server" class="col-sm2 control-label">Date:</label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="drpCalendarDate" runat="server" OnSelectedIndexChanged="drpCalendarDate_SelectedIndexChanged"
                            AutoPostBack="true" CssClass="form-control">
                            <asp:ListItem Value="0">Today</asp:ListItem>
                            <asp:ListItem Value="7">Last Week</asp:ListItem>
                            <asp:ListItem Value="31">Last Month</asp:ListItem>
                            <asp:ListItem Value="120">Last Quarter</asp:ListItem>
                            <asp:ListItem Value="365">Last Year</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-2">
                        <label class="col-sm-2 control-label">Expired_SuperKey_Records</label>
                    </div>
                    <div class="col-sm-10">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                        <asp:Button ID="btnDeleteSuperKey" runat="server" Text="Delete" OnClick="btnDeleteSuperKey_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <table class="table table-bordered" style="background: lightcyan;">
        <div id="griddiv" runat="server">
            <div id="gridContainer" class="gridContainer">
                <asp:GridView ID="dgvSuperKey" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-hover table-striped">
                    <Columns>
                        <asp:BoundField DataField="SuperKeyId" HeaderText="Id" ControlStyle-Width="100" HeaderStyle-BorderColor="Black" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-ForeColor="#428bca"  />
                        <asp:BoundField DataField="Name" HeaderText="Name" ControlStyle-Width="200" HeaderStyle-BorderColor="Black" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-ForeColor="#428bca" />
                        <asp:BoundField DataField="SystemEntityType" HeaderText="SystemEntityType" HeaderStyle-BorderColor="Black" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-ForeColor="#428bca"/>
                        <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-BorderColor="Black" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-ForeColor="#428bca" />
                        <asp:BoundField DataField="SortOrder" HeaderText="SortOrder" HeaderStyle-BorderColor="Black" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-ForeColor="#428bca" />
                        <asp:BoundField DataField="ExpirationDate" HeaderText="ExpirationDate" HeaderStyle-BorderColor="Black" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-ForeColor="#428bca" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </table>
</asp:Content>

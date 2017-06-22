<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register TagPrefix="un" TagName="AppUserName" Src="~/Shared/Controls/ApplicationUsername.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table>
        <tr>
            <td>
                <table class="table table-striped">

                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblApplicationUserId" Text="ApplicationUserId:"
                                    runat="server"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationUserId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUserId" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Application:</label></td>
                        <td>

                            <asp:DropDownList ID="drpApplicationIdList" runat="server" OnSelectedIndexChanged="drpApplicationIdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationId" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Application User Name:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationUserName" Text="" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUserName" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Email Address:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailAddress" Text="" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEmailAddress" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Application User Title:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationUserTitleList" runat="server"
                                OnSelectedIndexChanged="drpApplicationUserTitleList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationUserTitleId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUserTitleId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">First Name:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFirstName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Middle Name:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMiddleName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Last Name:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynLastName" runat="server" />
                        </td>
                    </tr>


                </table>
                <ui:UpdateInfo ID="UpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <a href="~/Shared/ApplicationManagement/ApplicationUser/Controls/SearchFilter.ascx">~/ApplicationManagement/ApplicationUser/Controls/SearchFilter.ascx</a>
                                        <td>
                                            <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dc:List ID="oHistoryList" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            </td>
        </tr>
    </table>
</div>

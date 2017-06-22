<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Shared/Controls/KendoTextEditor/KendoEditor.ascx" TagPrefix="uc1" TagName="KendoEditor" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtCreatedDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });

        $("#<%=txtUpdatedDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });


    });
</script>
<div id="borderdiv" runat="server">
    <table width="400">
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblFunctionalityXFunctionalityImageId" Text="FunctionalityXFunctionalityImageId:"
                                    runat="server"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityXFunctionalityImageId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityXFunctionalityImageId" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td class="ralabel">Functionality:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityList" runat="server" OnSelectedIndexChanged="drpFunctionalityList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">FunctionalityImage:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityImageList" runat="server" OnSelectedIndexChanged="drpFunctionalityImageList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityImageId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityImageId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblKeyString" Text="KeyString:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeyString" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynKeyString" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblTitle" Text="Title:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTitle" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblDescription" Text="Description:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <uc1:KendoEditor runat="server" ID="txtDescription" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblSortOrder" Text="SortOrder:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">CreatedBy:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedBy" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCreatedBy" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">CreatedDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedDate" runat="server"></asp:TextBox>

                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="dynCreatedDate" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">UpdatedBy:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUpdatedBy" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUpdatedBy" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">UpdatedDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUpdatedDate" runat="server"></asp:TextBox>

                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblUserDateTimeFormat2" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="dynUpdatedDate" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>

                </table>
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                                <table>
                                    <tr>
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
            </td>
        </tr>
    </table>
</div>

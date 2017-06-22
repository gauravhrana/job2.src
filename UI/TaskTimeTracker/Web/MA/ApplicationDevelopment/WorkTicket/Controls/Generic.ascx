<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" 
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Shared/Controls/KendoTextEditor/KendoEditor.ascx" TagPrefix="uc1" TagName="KendoEditor" %>

<div id="borderdiv" runat="server">
    <br /><br />
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblWorkTicketId" Text="EntityId:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWorkTicketId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynWorkTicketId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Application:
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationId" runat="server" Width="50%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="50%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel" valign="top">
                            Description:
                        </td>
                        <td >
                            <div id="example" class="k-content">
                                
                                <%--<CKEditor:CKEditorControl ID="txtDescription" Width="750" runat="server" Toolbar="Basic"  visible="false"
                                ToolbarBasic="Bold|Italic|-|NumberedList|BulletedList|-|Link|Unlink|Anchor|-|Outdent|Indent" />--%>
                            <%--<textarea id="txtDescription2"  runat="server" rows="10" cols="30" style="height:140px"></textarea>--%>
                                 <uc1:KendoEditor runat="server" id="editor" />
                            <%-- <script>
                             $(document).ready(function () {
                                 // create Editor from textarea HTML element with default set of tools
                                 $("#editor").kendoEditor();
                             });
                            </script>--%>
                                    
                            </div>
                                </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Sort Order:
                     
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Active Status:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpEntityList" runat="server" OnSelectedIndexChanged="drpEntity_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityyd" runat="server" />
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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleDetailInsert.ascx.cs"
    Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.Controls.ScheduleDetailInsert" %>

<link href="/Prototype/Kendo/content/shared/styles/examples-offline.css" rel="stylesheet">
<link href="/styles/kendo/kendo.common.min.css" rel="stylesheet">
<link href="/styles/kendo/kendo.rtl.min.css" rel="stylesheet">
<link href="/styles/kendo/kendo.default.min.css" rel="stylesheet">

<script src="/scripts/kendo/full/kendo.web.min.js"></script>
<script src="/Prototype/Kendo/content/shared/js/console.js"></script>
<script>
    $(document).ready(function () {
        // create TimePicker from input HTML element 
        $("input[type=text][id*=txtInsertInTime]").kendoTimePicker({ interval: "15" });
        $("input[type=text][id*=txtInsertOutTime]").kendoTimePicker({ interval: "15" });
        $("input[type=text][id*=txtEditInTime]").kendoTimePicker({ interval: "15" });
        $("input[type=text][id*=txtEditOutTime]").kendoTimePicker({ interval: "15" });
    });
</script>
<div id="borderdiv" runat="server">
    <table>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvScheduleDetails"
                    runat="server"
                    AutoGenerateColumns="false"
                    ShowFooter="true"
                    OnRowDeleting="gvScheduleDetails_RowDeleting"
                    OnRowCommand="gvScheduleDetails_RowCommand"
                    OnRowDataBound="gvScheduleDetails_RowDataBound"
                    OnRowEditing="gvScheduleDetails_Editing"
                    OnRowUpdating="gvScheduleDetails_Updating"
                    DataKeyNames="ScheduleDetailId"
                    OnRowCancelingEdit="gvScheduleDetails_CancelingEdit" GridLines="Both">
                    <Columns>

                        <asp:TemplateField HeaderText="ScheduleDetail Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblScheduleDetailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ScheduleDetailId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ScheduleId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblScheduleId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ScheduleId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activity Category" ControlStyle-Width="140" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpScheduleDetailActivityCategory" runat="server"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="In Time" ControlStyle-Width="130" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtInsertInTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InTime", "{0:t}") %>'>></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditInTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InTime", "{0:t}") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Out Time" ControlStyle-Width="130">
                            <ItemTemplate>
                                <asp:TextBox ID="txtInsertOutTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OutTime", "{0:t}") %>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditOutTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OutTime", "{0:t}") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Ticket" ControlStyle-Width="140" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWorkTicket" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WorkTicket") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Message" ControlStyle-Width="400">
                            <ItemTemplate>
                                <asp:TextBox ID="txtInsertMsg" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Message") %>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditMessage" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Message") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duration (hr)" ControlStyle-Width="50">
                            <ItemTemplate>
                                <div style="padding-right: 3px;">
                                     <asp:Label ID="lblDateDiffHrs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateDiffHrs", "{0:0.00}") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                <div style="padding-right: 3px;">
                                    <asp:Label ID="Label1" runat="server" Text='Total: '></asp:Label>
                                    <asp:Label ID="lblDateDiffHrsTotal" runat="server" Text='Test'></asp:Label>
                                </div>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedDate") %>' Visible="false"></asp:Label>
                                <asp:TextBox ID="txtInsertCreatedDate" runat="server" Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="txtEditCreatedDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreatedDate") %>' Visible="true"></asp:Label>
                            </EditItemTemplate>
                            <%-- <FooterTemplate>
                        <asp:TextBox ID="txtAddCreatedDate" runat="server" Visible="false"></asp:TextBox>
                    </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Do you want to delete?')" />
                                    <%--<asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>--%>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Add"></asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="6">

                <asp:LinkButton ID="lbtnSubmit" runat="server" CommandName="Submit" Text="Submit" OnClick="lbtnSubmit_Click" />
                <%-- <asp:LinkButton ID="lbtnSubmit" runat="server" CommandName="Submit" Text="Submit" OnClick="lbtnSubmit_Click"></asp:LinkButton>--%>
            </td>

        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
</div>

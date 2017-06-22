<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditScheduleQuestion.ascx.cs"
    Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.Controls.EditScheduleQuestion" %>

<div id="borderdiv" runat="server">
    <table>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td>

                <asp:GridView ID="gvScheduleQuestions"
                    runat="server"
                    AutoGenerateColumns="false"
                    ShowFooter="true"
                    DataKeyNames="ScheduleQuestionId"
                    OnRowDataBound="gvScheduleQuestions_RowDataBound"
                    GridLines="Both">
                    <Columns>
                        <asp:TemplateField HeaderText="ScheduleDetail Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblScheduleQuestionId" runat="server"
                                    Text='<%#DataBinder.Eval(Container.DataItem, "ScheduleQuestionId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblQuestionId" runat="server"
                                    Text='<%#DataBinder.Eval(Container.DataItem, "QuestionId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question">
                            <ItemTemplate>
                                <asp:Label ID="lblQuestionPhrase" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem, "QuestionPhrase") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Answer" ControlStyle-Width="140" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpAnswer" runat="server">
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
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


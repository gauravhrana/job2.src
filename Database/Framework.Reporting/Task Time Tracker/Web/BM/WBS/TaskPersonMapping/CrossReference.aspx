<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="CrossReference.aspx.cs" Inherits="ApplicationContainer.UI.Web.WBS.TaskPersonMapping.CrossReference" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>   
<asp:Content ID="DefaultContent" ContentPlaceHolderID="SearchControlItem" runat="server">
    <table>
        <tr>
            <td>
                <table border="1" width="450">
                    <tr>
                        <td width="150">
                            Direction:
                        </td>
                        <td>
                            <asp:DropDownList Width="155" ID="drpSelection" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                                <asp:ListItem Value="ByTask">Task To Person</asp:ListItem>
                                <asp:ListItem Value="ByPersons">Person To Task</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
            <td width="200" valign="top">Task Status Type</td>
            <td width="*">
                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="drpTaskStatusType_OnSelectedIndexChanged" runat="server" ID="drpTaskStatusType"></asp:DropDownList>
            </td>
        </tr>
        <tr valign="top">
            <td class="ralabel">
                StartDate:
            </td>
            <td>
                <asp:Calendar ID="clnStartDate" runat="server" OnSelectionChanged="clnStartDate_SelectionChanged">
                </asp:Calendar>
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox><asp:Label
                    ID="lblStartDateFormat" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynStartDate" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr valign="top">
            <td class="ralabel">
                DueDate:
            </td>
            <td>
                <asp:Calendar ID="clnDueDate" runat="server" OnSelectionChanged="clnDueDate_SelectionChanged">
                </asp:Calendar>
            </td>
            <td>
                <asp:TextBox ID="txtDueDate" runat="server" Visible="false"></asp:TextBox><asp:Label
                    ID="lblDueDateFormat" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynDueDate" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr valign="top">
            <td class="ralabel">
                CompletedDate:
            </td>
            <td>
                <asp:Calendar ID="clnCompletedDate" runat="server" OnSelectionChanged="clnCompletedDate_SelectionChanged">
                </asp:Calendar>
            </td>
            <td>
                <asp:TextBox ID="txtCompletedDate" runat="server" Visible="false"></asp:TextBox><asp:Label
                    ID="lblCompletedDateFormat" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCompletedDate" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynTask" runat="server">
                    <table border="1" width="450">
                        <tr>
                            <td width="150" valign="top">
                                TaskType
                            </td>
                            <td width="*">
                                <asp:DropDownList Width="155" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpTaskType_OnSelectedIndexChanged"
                                    ID="drpTaskType">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" valign="top">
                                Task
                            </td>
                            <td width="*">
                                <asp:DropDownList Width="155" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpTask_OnSelectedIndexChanged"
                                    ID="drpTask">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="1" width="510px">
                        <tr>
                        <td width="250px" align="center" style=" color:Black;">
                                Available Persons:
                            </td>
                             <td width="250px" align="center" style=" color:Black;">
                                Current Assigned Persons:
                            </td>
                        </tr>
                       
                        <tr>
                         <td width="250px" height="250px">
                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSourcePerson" style=" width:250px;height:250px;"></asp:ListBox>
                            </td>
                            <td width="250px" height="250px">
                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTargetPerson" style=" width:250px;height:250px;"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                         <td style=" text-align:center;">
                                <asp:Button runat="server" Text="-->" ID="btnLeftPerson" OnClick="btnLeftPerson_Click" /> 
                            </td>
                             <td style=" text-align:center;">
                            <asp:Button runat="server" Text="<--" ID="btnRightPerson" OnClick="btnRightPerson_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="right">
                                <asp:Button runat="server" Text="Save" ID="btnSavePersons" OnClick="btnSavePerson_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynPerson" runat="server" Visible="false">
                    <table border="1" width="400">
                        <tr>
                            <td width="150" valign="top">
                                Person
                            </td>
                            <td width="*">
                                <asp:DropDownList Width="155" AutoPostBack="true" OnSelectedIndexChanged="drpPerson_OnSelectedIndexChanged"
                                    runat="server" ID="drpPerson">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="1" width="300">
                     <tr>
                        <td width="250px" align="center" style=" color:Black;">
                                 All Tasks
                            </td>
                             <td width="250px" align="center" style=" color:Black;">
                                Current Task Holding the Persons:
                            </td>
                        </tr>
                       
                        <tr>
                         <td width="250px" height="250px">
                                 <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSourceTask"  style="width:250px;height:250px;"></asp:ListBox>
                            </td>
                            <td width="250px" height="250px">
                                 <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTargetTask" style=" width:250px;height:250px;"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                         <td style=" text-align:center;">
                                <asp:Button runat="server" Text="-->" ID="btnLeftTask" OnClick="btnLeftTask_Click" />
                            </td>
                             <td style=" text-align:center;">
                            <asp:Button runat="server" Text="<--" ID="btnRightTask" OnClick="btnRightTask_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="right">
                                <asp:Button runat="server" Text="Save" ID="btnSaveTask" OnClick="btnSaveTask_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCheckTask" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>



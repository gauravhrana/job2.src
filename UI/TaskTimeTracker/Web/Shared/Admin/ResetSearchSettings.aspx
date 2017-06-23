<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="ResetSearchSettings.aspx.cs" Inherits="Shared.UI.Web.Admin.ResetSearchSettings" %>

<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <style>
        label
        {
            margin-left: 5px;
            vertical-align: middle;
        }
    </style>
    <table border="0">
        <tr>
            <td colspan="2" align="left"><b>Reset : Search Settings</b></td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
               <label class="col-sm-2 control-label">Application:</label>
            <td align="left">
                <asp:DropDownList ID="ddlApplication" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <label class="col-sm-2 control-label">ApplicationUser:</label>
            <td align="left">
                <asp:DropDownList ID="drpApplicationUser" runat="server"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <label class="col-sm-2 control-label">SystemEntity:</label>
            <td align="left">
                <asp:DropDownList ID="drpSystemEntity" runat="server"></asp:DropDownList>
            </td>
            <%--<td align="left">&nbsp;&nbsp;
                <asp:LinkButton ID="lnkResetAll" runat="server" OnClick="lnkResetAll_Click">RESET ALL</asp:LinkButton>
            </td>--%>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
         <tr>
             <td align="center">
                <label></label>
            <td  align="left">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
            </td>
        </tr>
        
       
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <label class="col-sm-2 control-label">SearchCategories:</label>
                
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" style= "border-style:solid; border-width: thin;">
                <div style="width: 500px; margin-left:15px; height: 500px; overflow-y: scroll;">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinkButton ID="lnkReset" runat="server" OnClick="lnkReset_Click">RESET SEARCH CATEGORY</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <label class="col-sm-2 control-label" id="lblParamCaption" visible="false" runat="server">Parameters_that_will_Be_Affected</label>                
                <br />
                <asp:Label ID="lblParams" runat="server" />
            </td>
            <td>
                <label class="col-sm-2 control-label" id="lblParamValues" runat="server" Visible="false">Values_of_each_Parameters</label>
                <br />
                <asp:Label ID="lblParamValuesList" runat="server" />
            </td>
        </tr>
        <%--<asp:Repeater ID="SearchSettings" runat="server" OnItemCommand="SearchSettings_ItemCommand">
            <HeaderTemplate>
                <tr>
                    <td>
            </HeaderTemplate>
            <ItemTemplate>--%>


        <%--<div style=" width:30%; float:left; text-align:right;">
            <asp:Label ID="lblSearchSettingName" runat="server" Text='<%# Eval("EntityName") %>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HiddenField ID="hdnSearchSetting" runat="server" Value='<%# Eval("EntityName") + "DefaultViewSearchControl" %>' />
                </div>&nbsp;&nbsp;
            <div style=" width:10%; float:right; text-align:left;">
            <asp:LinkButton ID="lnkReset" runat="server" CommandName='<%# Eval("EntityName") + "DefaultViewSearchControl" %>'>RESET</asp:LinkButton>
                </div>
            <div style=" width:60%;"></div>--%>
        <%--<table>
            <tr>
                <td width="350px" style="text-align: right;">
                    <asp:Label ID="lblSearchSettingName" runat="server" Text='<%# Eval("EntityName") %>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HiddenField ID="hdnSearchSetting" runat="server" Value='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:LinkButton ID="lnkReset" runat="server" CommandArgument='<%# Eval("Name")  %>' CommandName="click">RESET</asp:LinkButton>
                </td>

            </tr>

        </table>
        <br />
        </ItemTemplate>
            <footertemplate></td></tr></footertemplate>
        </asp:Repeater>
    </table>--%>
    </table>
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true"
    CodeBehind="TestRepeater.aspx.cs"
    Inherits="Shared.UI.Web.Development.TestRepeater" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ControlVisibilityManager" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SearchControlItem" runat="server">

    <div class="container-fluid form-horizontal">
        <asp:Repeater ID="SearchParametersRepeater" runat="server" OnItemDataBound="SearchParametersRepeater_ItemDataBound">
            <ItemTemplate>
                <div class="col-sm-2">
                    <asp:PlaceHolder ID="plcHoverLinkLabel" runat="server" />
                </div>
                <div id="controlContainerDiv" runat="server">
                    <asp:PlaceHolder ID="plcControlHolder" runat="server" />
                </div>
                <div class="col-sm-1">
                    <asp:TextBox ID="txtbox1" runat="server" />
                    <asp:HiddenField ID="hdnfield" Value='<%# Eval("Name") %>' runat="server" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ListControlItem" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ActionContent" runat="server">
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VerticalTabChildControl.ascx.cs"
    Inherits="Shared.UI.Web.Controls.VerticalTabChildControl" EnableViewState="true" %>
<asp:Panel ID="pnlHeader" runat="server" Height="30px"  onclick="test()">
    <div style="padding: 5px; cursor: pointer; vertical-align: middle; margin-left: 10px;
        width: 80%">
        <div style="width: 60%">
            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Content/images/expand_blue.jpg" AlternateText="Show Details" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                ID="lblTitle" runat="server" Text=""></asp:Label><asp:HiddenField ID="hdnId" runat="server" />
            <asp:TextBox ID="txtIsCollapsed" runat="server" Visible="false"></asp:TextBox>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="pnlCollapsibleContent" runat="server" CssClass="collapsePanel" Height="0">
</asp:Panel>
<ajaxToolkit:CollapsiblePanelExtender ID="cpExtender" runat="Server" TargetControlID="pnlCollapsibleContent"
    ExpandControlID="pnlHeader" CollapseControlID="pnlHeader" TextLabelID="lblPanelStatus"
    ImageControlID="Image1" ExpandedText="Hide Details" CollapsedText="Show Details"
    ExpandedImage="~/Content/images/collapse_blue.jpg" CollapsedImage="~/Content/images/expand_blue.jpg"
    SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
<script type="text/javascript">

    function test() {
    }

    function ReceiveServerData(arg, context) {

    }
</script>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.Question.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>


<div class="form-horizontal">

    <div class="form-group">
        <asp:Label ID="lblQuestionId" Text="Question Id:" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="txtQuestionId"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtQuestionId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynQuestionId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtQuestion.ClientID%>">Question:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtQuestion" runat="server" CssClass="form-control"></asp:TextBox>
            <span class="help-block">.</span>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynQuestion" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=drpCategoryList.ClientID%>">Category Id:</label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpCategoryList" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpCategoryList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtCategoryId" runat="server" Visible="false"></asp:TextBox>           
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtSortOrder.ClientID%>">Sort Order:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
        </div>
    </div>

    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />

</div>

<asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label ID="lblHistory" runat="server" Text="" CssClass="col-sm-2 control-label" />
            <div class="col-sm-10">.</div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">Record History:</label>
            <div class="col-sm-10 control-label">
                <dc:List ID="oHistoryList" runat="server" />
            </div>
        </div>
    </div>
</asp:PlaceHolder>


<div id="borderdiv" runat="server">
    <table>
    </table>
</div>

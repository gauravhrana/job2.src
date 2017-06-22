<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateInfo.ascx.cs" Inherits="Shared.UI.Web.Controls.UpdateInfo" %>

<div id="updateStyle1" runat="server" visible="false">
    <div>
        <div class="row form-group" >
            <div class="col-sm-2 control-label">
                <asp:Label ID="lblUpdatedDateText" runat="server" CssClass="control-label" AssociatedControlID="lblUpdatedDate1">UpdatedDate</asp:Label>
            </div>
            <div class="col-sm-8">
                <asp:Label ID="lblUpdatedDate1" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2 control-label">
            </div>
        </div>
        <div class="row form-group" >
            <div class="col-sm-2 control-label">
                <asp:Label ID="lblUpdatedByText" runat="server" CssClass="control-label" AssociatedControlID="lblUpdatedBy1">UpdatedBy</asp:Label>
            </div>
            <div class="col-sm-8">
                <asp:Label ID="lblUpdatedBy1" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2 control-label">
            </div>
        </div>
        <div class="row form-group" >
            <div class="col-sm-2 control-label">
                <asp:Label ID="lblLastActionText" runat="server" CssClass="control-label" AssociatedControlID="lblLastAction1">LastAction</asp:Label>
            </div>
            <div class="col-sm-8">
                <asp:Label ID="lblLastAction1" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2 control-label">
            </div>
        </div>
    </div>
</div>

<div id="updateStyle2" runat="server" visible="false">
    <div>
        <div class="row form-group" >
            <label class="col-sm-2 control-label">Updated: </label>            
            <div class="col-sm-10">
                <div >
                    <asp:Label ID="lblLastAction2" runat="server"></asp:Label>
                    <asp:Label ID="lblUpdatedDate2" runat="server"></asp:Label>
                    <asp:Label ID="lblUpdatedBy2" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</div>

<div id="updateStyle3" runat="server" visible="false">
    <div class="row" >
        <div class="col-sm-2 control-label"></div>
        <div class="col-sm-10 control-label">
            <asp:Label ID="lblUpdatedBy3" runat="server"></asp:Label>                                
            &nbsp;<asp:Label ID="lblLastAction3" runat="server"></asp:Label>
            &nbsp;<asp:Label ID="Label1" Text="this record" runat="server"></asp:Label>
            &nbsp;<asp:Label ID="lblUpdatedDate3" ForeColor="Teal" Font-Size="X-Small" runat="server"></asp:Label>
        </div>        
    </div>
</div>





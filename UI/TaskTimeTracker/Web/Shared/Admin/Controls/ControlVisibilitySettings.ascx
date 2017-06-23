<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlVisibilitySettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.ControlVisibilitySettings" %>

  <div class="collapse in panel-body" id="pnlSearchParameters">
  <div class="form-horizontal">

  <div class="form-group">
        <label class="col-sm-2 control-label" for="txtBreadCrumb" >Bread Crumb Visible:</label>
        <div class="col-md-10">
            <asp:DropDownList ID="drpBreadCrumbVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </div>        
    </div>

 <div class="form-group">
        <label class="col-sm-2 control-label" for="txtSubMenu" >Sub Menu Visible:</label>
        <div class="col-md-10">
            <asp:DropDownList ID="drpSubMenuVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </div>        
    </div>

 <div class="form-group">
        <label class="col-sm-2 control-label" for="txtSearchFilter" >Search Filter Visible:</label>
        <div class="col-md-10">
           <asp:DropDownList ID="drpSearchFilterVisible" runat="server">
                <asp:ListItem Value="true">True</asp:ListItem>
                <asp:ListItem Value="false">False</asp:ListItem>
            </asp:DropDownList>
        </div>        
    </div>

    <div class="form-group">
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-md-10">
           <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                CausesValidation="true" />
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_OnClick" />
        </div>
        
    </div>

  </div>        
    </div>


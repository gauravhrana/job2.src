<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Site.Master" 
    CodeBehind="ResultCheck.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.Report.ResultCheck" %>


<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register Src="~/Shared/Controls/DateRange.ascx" TagPrefix="uc1" TagName="DateRange" %>
<%@ Register Src="~/Shared/Controls/GroupList.ascx" TagPrefix="uc1" TagName="GroupList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
    <center><b>Comparitive analayis of time spent on development efforts per application vs over all time </b></center><br />
        
<div class="form-horizontal">

    <div class="form-group">
        <label ID="lblApplicationUser" class="col-sm-2 control-label" >Application User:</label>
        <div class="col-sm-8">
        <asp:DropDownList ID="drpApplicationUser" runat="server" OnSelectedIndexChanged="drpApplicationUser_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Value="All">All</asp:ListItem>
                </asp:DropDownList>
        </div><div class="col-sm-2">
            <asp:TextBox ID="txtApplicationUserId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynApplicationUserId" runat="server" />
        </div>
    </div>
   

    <div class="form-group">
        <label ID="lblApplication" class="col-sm-2 control-label" >Application :</label>
        <div class="col-sm-8">
        <asp:DropDownList ID="drpApplicationId" runat="server" OnSelectedIndexChanged="drpApplicationId_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Value="All">All</asp:ListItem>
                </asp:DropDownList>
        </div><div class="col-sm-2">
            <asp:TextBox ID="txtApplicationId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynApplicationId" runat="server" />
        </div>
    </div>

     <div class="form-group">
        <label ID="lblExcludedItems" class="col-sm-2 control-label" >Excluded Items:</label>
        <div class="col-sm-8">
        <asp:DropDownList ID="drpExcludedList" runat="server" OnSelectedIndexChanged="drpExcludedItems_SelectedIndexChanged">                
                <asp:ListItem Enabled="true" Value="EmptyUsers">Empty Users</asp:ListItem>
                <asp:ListItem Enabled="true" Value="EmptyProjects">Empty Projects</asp:ListItem>
                </asp:DropDownList>
        </div><div class="col-sm-2">
            <asp:TextBox ID="txtExcludedItems" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynExcludedItems" runat="server" />
        </div>
    </div>
    
    <div class="form-group">
        <label ID="lblDateRange" class="col-sm-2 control-label" >Date Range:</label>
        <div class="col-sm-8">
            <asp:PlaceHolder ID="plcControlHolder" runat="server" />
        </div>
    </div>
    </div>
    <div class="form-horizontal">
        <asp:Button ID="btnSubmit" Text="Calculate" runat="server" OnClick="btnSubmit_OnClick" />
        <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_OnClick" />
    </div>
	<div class="panel">
        <asp:GridView ID="gv"  AutoGenerateColumns="false" runat="server">
			<Columns>
            <asp:BoundField DataField="ApplicationUserName" HeaderText="User"  HeaderStyle-HorizontalAlign ="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="TotalRNHrs" HeaderText="Total Release Notes Hrs "  HeaderStyle-HorizontalAlign ="Center" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="TotalScheduleHrs" HeaderText="Total Schedule WorkedHours" HeaderStyle-HorizontalAlign ="Center" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </asp:GridView>
    </div>
    <uc1:GroupList runat="server" ID="GroupList" />
</asp:Content>
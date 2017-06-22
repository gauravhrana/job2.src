<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkCategory.aspx.cs"
    Inherits="ApplicationContainer.UI.Web.BM.Scheduling.WorkCategory" MasterPageFile="~/MasterPages/Schedule/Site.master" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register Src="~/Shared/Controls/DateRange.ascx" TagPrefix="uc1" TagName="DateRange" %>
<%@ Register Src="~/Shared/Controls/GroupList.ascx" TagPrefix="uc1" TagName="GroupList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <center><b>Work Category Details </b></center>
    <br />
    <div class="form-horizontal">

    <div class="form-group">
        <label ID="lblCustomTimeCatgeory" class="col-sm-2 control-label" >Custom Time Category:</label>
        <div class="col-sm-8">
        <asp:DropDownList ID="drpCustomTimeCategory" runat="server" OnSelectedIndexChanged="drpCustomTimeCategory_SelectedIndexChanged ">
                <asp:ListItem Enabled="true" Value="All">All</asp:ListItem>
                </asp:DropDownList>
        </div><div class="col-sm-2">
            <asp:TextBox ID="txtCustomTimeCategoryId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynCustomTimeCategoryId" runat="server" />
        </div>
    </div>
    
   <div class="form-group">
        <label ID="lblApplicationUser" class="col-sm-2 control-label" >Application User:</label>
        <div class="col-sm-8">
        <asp:DropDownList ID="drpApplicationUser" runat="server" OnSelectedIndexChanged="drpApplicationUser_SelectedIndexChanged ">
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
       <label ID="lblDateRange" class="col-sm-2 control-label" >Date Range:</label>
       <div class="col-sm-8">
            <asp:PlaceHolder ID="plcControlHolder" runat="server" />
        </div>
       </div>
        
   <div class="form-group">
        <label ID="lblGroupby" class="col-sm-2 control-label" >Group By:</label>
        <div class="col-sm-8">
        <asp:DropDownList ID="drpGroupBy" runat="server" OnSelectedIndexChanged="drpGroupBy_SelectedIndexChanged ">
                <asp:ListItem Enabled="true" Value="All">All</asp:ListItem>
                </asp:DropDownList>
        </div><div class="col-sm-2">
            <asp:TextBox ID="txtGroupBy" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynGroupBy" runat="server" />
        </div>
    </div>
        
   <div class="form-group">
        <label ID="lblSubGroupBy"  class="col-sm-2 control-label" >Sub Group By:</label>
        <div class="col-sm-8">
        <asp:DropDownList ID="drpSubGroupBy" runat="server" OnSelectedIndexChanged="drpSubGroupBy_SelectedIndexChanged ">
                <asp:ListItem Enabled="true" Value="All">All</asp:ListItem>
                </asp:DropDownList>
        </div><div class="col-sm-2">
            <asp:TextBox ID="txtSubGroupBy" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynSubGroupBy" runat="server" />
        </div>
    </div>
        
                <div class="form-group">
                                <asp:Button ID="btnSubmit" Text="Calculate" runat="server" OnClick="btnSubmit_OnClick" />
                                <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_OnClick" />
                           </div>
                             <div class="form-group">

                                <asp:Label ID="txtPersonId" Visible="false" runat="server" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeek" runat="server" Font-Bold="true"></asp:Label>
                                        <br />
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Value" HeaderText="Value" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <uc1:GroupList runat="server" ID="GroupList" />
       </div>                                                     
    </div>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="2DStatistics.ascx.cs"
    Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls._2DStatistics" %>

<%@ Register Src="~/Shared/Controls/DateRange.ascx" TagPrefix="uc1" TagName="DateRange" %>

<div class="panel panel-info">
    <div class="panel-heading">
        Search:
    </div>

    <div class="collapse in panel-body" id="pnlSearchParameters">
        <div class="form-horizontal">
            <div class="form-group">
                <label class="col-sm-2 control-label">Person :</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="drpPersons" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label">Work Date :</label>
                <div class="col-md-10">
                    <uc1:DateRange runat="server" ID="oDateRange" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label">Work Category :</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="drpWorkCategory" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label">Message :</label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox>
                </div>
            </div>
             
            <div class="form-group">
                <label class="col-sm-2 control-label">Filter :</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="PersonId">Person</asp:ListItem>
                        <asp:ListItem Value="WorkDate">WorkDate</asp:ListItem>
                        <asp:ListItem Value="ScheduleDetailActivityCategoryId">ScheduleDetailActivityCategory</asp:ListItem>
                        <asp:ListItem Value="DateDiffHrs">Hours</asp:ListItem>
                        </asp:DropDownList>
                    </div></div>
            
            <div class="form-group">
                <label class="col-sm-2 control-label">Columns :</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="drpXAxis" runat="server">
                        <asp:ListItem Value="PersonId">Person</asp:ListItem>
                        <asp:ListItem Value="WorkDate">WorkDate</asp:ListItem>
                        <asp:ListItem Value="ScheduleDetailActivityCategoryId">ScheduleDetailActivityCategory</asp:ListItem>
                        <asp:ListItem Value="DateDiffHrs">Hours</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label">Rows :</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="drpYAxis" runat="server">
                        <asp:ListItem Value="ScheduleDetailActivityCategoryId">ScheduleDetailActivityCategory</asp:ListItem>
                        <asp:ListItem Value="PersonId">Person</asp:ListItem>
                        <asp:ListItem Value="WorkDate">WorkDate</asp:ListItem>
                        <asp:ListItem Value="DateDiffHrs">Hours</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label">Measure :</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="drpZAxis" runat="server">
                        <asp:ListItem Value="DateDiffHrs">Hours</asp:ListItem>
                        <asp:ListItem Value="ScheduleDetailActivityCategory">Category</asp:ListItem>
                        <asp:ListItem Value="Person">Person</asp:ListItem>
                        <asp:ListItem Value="WorkDate">Work Date</asp:ListItem>
                    </asp:DropDownList>
                     <asp:DropDownList ID="drpFunction" runat="server">
                        <asp:ListItem Value="Sum">Sum</asp:ListItem>
                        <asp:ListItem Value="Average">Average</asp:ListItem>
                        <asp:ListItem Value="Count">Count</asp:ListItem>
                        <asp:ListItem Value="Min">Min</asp:ListItem>
                        <asp:ListItem Value="Max">Max</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
               
            <div class="form-group">
                <label class="col-sm-2 control-label">Show Aggeregate :</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="drpShowAggeregate" runat="server">
                        <asp:ListItem Value="None">None</asp:ListItem>
                        <asp:ListItem Value="Both">Both</asp:ListItem>
                        <asp:ListItem Value="xAxis">X Axis</asp:ListItem>
                        <asp:ListItem Value="yAxis">Y Axis</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label"></label>
                <div class="col-md-10">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </div>
</div>


<div class="panel panel-info" ng-show="showJIRAList">
    <div class="panel-heading">
        Jira Issues
    </div>
    <div class="panel panel-body">

        <asp:PlaceHolder ID="contentHolder" runat="server"></asp:PlaceHolder>

    </div>
</div>


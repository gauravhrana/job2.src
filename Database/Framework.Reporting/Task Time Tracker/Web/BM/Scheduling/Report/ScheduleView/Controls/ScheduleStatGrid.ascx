<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleStatGrid.ascx.cs" Inherits="ApplicationContainer.UI.Web.BM.Scheduling.Report.ScheduleView.Controls.ScheduleStatGrid" %>
<!--Bootstrap -->
<link href="/Content/bootstrap.css" rel="stylesheet" type="text/css" />
<!--Bootstrap Theme -->
<link href="/Content/bootstrap-theme.css" rel="stylesheet" type="text/css" />

<%--<div id="StatsDiv" runat="server">
    <div class="table" id="DivStats" style="width: 1000px; border-collapse: collapse">
        <div style="width: 1060px;" id="Header" class="row head">
            <div class="cell">Schedule</div>
            <div class="cell">Total</div>
            <div class="cell">Average</div>
            <div class="cell">Median</div>
            <div class="cell">Count</div>
            <div class="cell">Max</div>
            <div class="cell">Min</div>
        </div>
    </div>
</div>--%>
<div id="StatsDiv11" runat="server">
    <table>
        <tr class="rowActive">
            <th class="text-center">
                <asp:Label ID="lblHeader" runat="server"></asp:Label></th>
            <th class="text-center">Total</th>
            <th class="text-center">Total(%)</th>
            <th class="text-center">Average</th>
            <th class="text-center">Median</th>
            <th class="text-center">Count</th>
            <th class="text-center">Count(%)</th>
            <th class="text-center">Max</th>
            <th class="text-center">Min</th>
        </tr>
    </table>
    <div ng-app="" >

First Name: <input type="text" ng-model="firstName"><br>
 Last Name: <input type="text" ng-model="lastName"><br>
<br>
 Full Name:<p> {{firstName}} {{lastName}}</p>

</div>
</div>


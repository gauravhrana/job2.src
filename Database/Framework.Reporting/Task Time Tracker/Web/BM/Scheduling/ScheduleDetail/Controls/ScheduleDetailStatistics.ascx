<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="ScheduleDetailStatistics.ascx.cs" 
    Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls.ScheduleDetailStatistics" %>

<div id="divStat" class="divScheduleDetail">
    <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
    <asp:Label ID="lblCount" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>&nbsp;
    <asp:Label ID="lblAverage" Text="Average:" runat="server"></asp:Label>
    <asp:Label ID="lblAverageValue" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>&nbsp;
    <asp:Label ID="lblMedian" Text="Median:" runat="server"></asp:Label>
    <asp:Label ID="lblMedianValue" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>&nbsp;
    <asp:Label ID="lblRecordCountText" Text="Count:" runat="server"></asp:Label>
    <asp:Label ID="lblRecordCount" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>&nbsp;
    <asp:Label ID="lblMaxText" Text="Max:" runat="server"></asp:Label>
    <asp:Label ID="lblMax" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>&nbsp;
    <asp:Label ID="lblMinText" Text="Min:" runat="server"></asp:Label>
    <asp:Label ID="lblMin" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>&nbsp;
    
</div>




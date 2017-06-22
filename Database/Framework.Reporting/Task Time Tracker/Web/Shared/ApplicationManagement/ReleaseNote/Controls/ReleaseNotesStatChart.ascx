<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReleaseNotesStatChart.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls.ReleaseNotesStatChart" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:PlaceHolder ID="dynChart" runat="server">     
    <table>                       
        <tr>
            <td>
                <asp:Chart ID="statChart" 
                    ImageStorageMode="UseImageLocation" 
                    ImageLocation="~/Shared/ApplicationManagement/ReleaseLog/ChartPic_#SEQ(300,3)"   
                    runat="server" onload="statChart_Load" Height="800px" Width="1000px">
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </chartareas>
                    <Legends>
                        <asp:Legend></asp:Legend>
                    </Legends>
                </asp:Chart>
            </td>
            <td style="vertical-align:top">                                
                <asp:CheckBox Font-Size="XX-Small" ID="lstAverageItem" AutoPostBack="true" Text="Average" runat="server" /><br />
                <asp:CheckBox Font-Size="XX-Small" ID="lstMedianItem" AutoPostBack="true" Text="Median" runat="server" /><br />
                <asp:CheckBox Font-Size="XX-Small" ID="lstMaxItem" AutoPostBack="true" Text="Max" runat="server"  /><br />
                <asp:CheckBox Font-Size="XX-Small" ID="lstMinItem" AutoPostBack="true" Text="Min" runat="server"  />                               
            </td>       
        </tr>
    </table>
</asp:PlaceHolder>
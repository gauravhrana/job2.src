<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupControl.ascx.cs" Inherits="ApplicationContainer.UI.Web.BM.Scheduling.Report.ScheduleView.Controls.GroupControl" %>

<table class="searchfilter"  >
    <tr>
        <td colspan="2" >
            Group By Control
        </td>
</tr> 
    <tr>
       
        <td align="left" colspan="2">
            <asp:DropDownList ID="GroupByCategory" runat="server" AutoPostBack="false">
            <asp:ListItem Text="Group By Person" Value="Person"></asp:ListItem>
             <asp:ListItem Text="Group By Day" Value="Day"></asp:ListItem>
              <asp:ListItem Text="Group By Month" Value="Month"></asp:ListItem>
               <asp:ListItem Text="Group By Year" Value="Year"></asp:ListItem>

            </asp:DropDownList>
            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Group" />
            
        </td>
    </tr>
    
    <tr><td>
    </tr>

    <tr><td>
   </td>
    </tr>

    <tr><td>
   </td>
    </tr>

</table>


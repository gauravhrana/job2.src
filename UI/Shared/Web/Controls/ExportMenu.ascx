<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportMenu.ascx.cs"
    Inherits="Shared.UI.Web.Controls.ExportMenu" %>
<link rel="Stylesheet" href="<%= Page.ResolveUrl("~")%>Styles/ExportMenu.css" />
<script type="text/javascript" src="<%= Page.ResolveUrl("~")%>Scripts/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~")%>Scripts/jquery.fixedMenu.js"></script>


<span class="exportmenu" style="border:1px Black;" style="display:inline; float:right;  ">
     
    <a name="exportLink" href="#exportLink">        
        <asp:LinkButton ID="imgSettings" runat="server"  Height="30" Width="30" BorderStyle="None" ToolTip="Settings"  >
        <asp:Image ID="imgSettings2" runat="server"  Height="30" Width="30" BorderStyle="None" ToolTip="Settings" /></asp:LinkButton>            
    </a> 
    <a name="exportLink" href="#exportLink">         
        <asp:HyperLink ID="lnkExport" runat="server" Height="25" Width="22" BorderStyle="None" ToolTip="Export to HTML" ></asp:HyperLink>                                          
    </a>        
    <a name="exportLink" href="#exportLink">            
        <asp:LinkButton  ID="lnkExportToCSV" runat="server" OnClick="lnkExportToCSV_Click"  Height="25" Width="25" BorderStyle="None" ToolTip="Export to CSV" ></asp:LinkButton>
    </a>            
    <a name="exportLink" href="#exportLink">            
        <asp:LinkButton ID="lnkExportToXML" runat="server"  OnClick="lnkExportToXML_Click"  Height="25" Width="25" BorderStyle="None" ToolTip="Export to XML"></asp:LinkButton>
    </a>
    <a name="exportLink" href="#exportLink">            
        <asp:Image ID="refreshimg" runat="server"  BorderStyle="None" ToolTip="Refresh"></asp:Image>            
    </a>    
    <a name="exportLink" href="#exportLink">            
        <asp:Image ID="helpimg" runat="server"  BorderStyle="None" ToolTip="Help"></asp:Image>            
    </a>       
       
</span> 
    

<script type="text/javascript" language="javascript">
    $('document').ready(function () {
        $('.exportmenu').fixedMenu();
    });
</script>

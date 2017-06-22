<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageInfo.aspx.cs" MasterPageFile="~/MasterPages/ReferenceData/Default.Master"
    Inherits="ApplicationContainer.UI.Web.app.BM.ReferenceData.PageInfo" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>


<asp:Content ID="ContentListControlItem" ContentPlaceHolderID="listcontrolitem" runat="server">
 <div class="panel panel-info" style="width:1200px">
        <div class="panel-heading">
            Page Info
        </div>
        <div class="panel panel-body">
            <table>
                <tr>
                    <td>
                       
                 <asp:GridView ID="gridContent" runat="server" AutoGenerateColumns="false">
                     <Columns>
                        <asp:BoundField DataField="Key" HeaderText="Entities" />
                        <asp:BoundField DataField="Value" HeaderText="Details" />
                        </Columns>
                   </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
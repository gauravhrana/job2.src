<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Images.ascx.cs" Inherits="Shared.UI.Web.Controls.Images" %>

<style>
    .modalBackground {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }

    .thumbnail {
        height: 100px;
        width: 130px;
        cursor: hand;
    }

    .imgpopup {
        height: 600px;
        width: 900px;
    }
</style>

<br />
<asp:Repeater ID="ImagesRepeater" runat="server">
    <ItemTemplate>
        <br />
        <table>
            <tr style=" border-color:orange; border-width:3px; border-style:groove; width:100%;">
        <td style="width: 600px; padding:10px;">
            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/MA/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Eval("FunctionalityImageId") %>'
                 Height="400" BorderWidth="4" BorderColor="#465c71"></asp:Image>
            <br />
            <div style="text-align: center; width: 600px; font-weight: normal;">
                <asp:Label ID="lblTitle" runat="server" Text='<%#  Eval("Title")  %>'></asp:Label>
            </div>
        </td>
        <td style="width: 250px; text-align: left; vertical-align:top;">

            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "Description") %>'></asp:Label>
        </td>
                </tr>
            </table>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1"
            runat="server" TargetControlID="Image1" PopupControlID="pnlPopup"
            BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnCancel" />
        <asp:Panel ID="pnlPopup" runat="server">
            <table>
                <tr>
                    <td colspan="2">
                        <img id="imgPopup" class="imgpopup" runat="server"
                            src='<%# "~/MA/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Eval("FunctionalityImageId")  %>'
                            width="900" height="600" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <span id="spnImageText" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="Close" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
    </ItemTemplate>
</asp:Repeater>


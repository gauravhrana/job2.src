<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FunctionalityImageControl.ascx.cs"
    Inherits="Shared.UI.Web.Controls.FunctionalityImageControl.FunctionalityImageControl" %>
<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<table >
    <tr>
        <td>
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" width="400">
                <tr>
                    <td width="78" align="left">
                        <b>Direction:</b> &nbsp;
                    </td>
                    <td align="left">
                        <asp:DropDownList Width="300" ID="drpSelection" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                            <asp:ListItem Value="ByFunctionality">Functionality To FunctionalityImage</asp:ListItem>
                            <asp:ListItem Value="ByFunctionalityImage">FunctionalityImage To Functionality</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td width="78" align="left">
                        <b>Key String:</b> &nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtKeyString" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td width="78" align="left">
                        <b>Title:</b> &nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td width="78" align="left">
                        <b>Description:</b> &nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="78" align="left">
                        <b>SortOrder:</b> &nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder ID="dynFunctionality" runat="server">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="78" align="left">
                            <b>Functionality:</b>
                            <asp:DropDownList Width="300" ID="drpFunctionality" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpFunctionality_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Bucket ID="BucketOfFunctionalityImage" runat="server" />
                        </td>
                        <td align="center" width="500">
                            <asp:Image ID="imgApplicationUserImage" runat="server" Height="300" Width="300" BorderWidth="4" />
                            <asp:Panel ID="pnlImage" runat="server" Style="display: none">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="PopupHeader">
                                        <div class="TitlebarLeft">
                                        </div>
                                        <div class="TitlebarRight" id="closePopup">
                                        </div>
                                    </div>
                                    <div class="popup_Body">
                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>--%>
                                                <asp:Image ID="imgApplicationUserImage1" runat="server" />
                                            <%--</ContentTemplate>
                                            <Triggers>--%>
                                            <%--<asp:AsyncPostBackTrigger ControlID=""
                                            
                                            </Triggers>--%>
                                        <%--</asp:UpdatePanel>--%>
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder ID="dynFunctionalityImage" runat="server" Visible="false">
                <table>
                    <tr>
                        <td align="left">
                            <b>FunctionalityImage:</b>&nbsp;
                            <asp:DropDownList ID="drpFunctionalityImage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFunctionalityImage_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Bucket ID="BucketOfFunctionality" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
        </td>
    </tr>
</table>

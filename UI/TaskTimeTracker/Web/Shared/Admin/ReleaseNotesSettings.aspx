<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="ReleaseNotesSettings.aspx.cs" Inherits="Shared.UI.Web.Admin.ReleaseNotesSettings" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <script type="text/javascript">
        function colorChanged(sender) {
            sender.get_element().style.color = "#" + sender.get_selectedColor();
        }
</script>
   </asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ListControlItem" runat="server">

<table style="font-weight: bold; color: Black" width="600" border="0">
    <tr>
        <td>
            <table width="600" border="0">
                
                <tr>
                    <td>
                        RowStyle Fore Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtReleaseNotesRowStyleForeColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="RowStyleForecolorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultBackCPE" runat="server" TargetControlID="txtReleaseNotesRowStyleForeColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="RowStyleForecolorSample" />
                    </td>
                </tr>

                 <tr>
                    <td>
                        RowStyle Back Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtReleaseNotesRowStyleBackColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="RowStyleBackcolorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultColorCPE" runat="server" TargetControlID="txtReleaseNotesRowStyleBackColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="RowStyleBackcolorSample" />
                    </td>
                </tr>

                <tr>
                    <td>
                        Alternating RowStyle Back Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtReleaseNotesAlternatingRowStyleBackColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="AlternatingBackGroundColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultForeCPE" runat="server" TargetControlID="txtReleaseNotesAlternatingRowStyleBackColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="AlternatingBackGroundColorSample" />
                    </td>
                </tr>                
                <tr>
                    <td>
                       Header Back Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtReleaseNotesHeaderBackColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="HeaderBackSampleColor" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultBorderCPE" runat="server" TargetControlID="txtReleaseNotesHeaderBackColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="HeaderBackSampleColor" />
                    </td>
                </tr>

                <tr>
                    <td>
                        Header Fore Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtReleaseNotesHeaderForeColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="HeaderForeSampleColor" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="ColorPickerExtender1" runat="server" TargetControlID="txtReleaseNotesHeaderForeColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="HeaderForeSampleColor" />
                    </td>
                </tr>


                 <tr>
                    <td>
                        Grid Lines:-
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="drpGridLines" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Value="None">None</asp:ListItem>
                            <asp:ListItem Value="Horizontal">Horizontal</asp:ListItem>
                            <asp:ListItem Value="Vertical">Vertical</asp:ListItem>
                            <asp:ListItem Value="Both">Both</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
               
               
                <tr>
                    <td>
                        Row Style Font Size:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRowStyleFontSize" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        Alternating Row Style Font Size:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAlternatingRowStyleFontSize" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
                 <tr>
                    <td>
                        Row Style Height:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRowStyleHeight" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        Alternating Row Style Height:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAlternatingRowStyleHeight" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Header Style Height:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtHeaderStyleHeight" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        Header Style Font Size:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAlternatingHeaderStyleFontSize" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="100">
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                            CausesValidation="true" />
                    </td>
                </tr>

            </table>
        </td>
       
    </tr>
</table>
    </asp:Content>
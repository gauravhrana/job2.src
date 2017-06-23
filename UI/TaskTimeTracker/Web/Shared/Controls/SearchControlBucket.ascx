<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchControlBucket.ascx.cs" Inherits="Shared.UI.Web.Controls.SearchControlBucket" %>

<table>
    <tr>
        <td align="left">
            
            <table border="1" cellpadding="0" cellspacing="0">                
                <tr>
                    <td width="*">
                        <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSource" Height="100">
                        </asp:ListBox>
                    </td>
                    <td align="center">
                       <asp:Button runat="server" Text="-->" ID="btnLeft" OnClick="btnLeft_Click" /><br />
                       <asp:Button runat="server" Text="<--" ID="btnRight" OnClick="btnRight_Click" />
                    </td>
                    <td width="*">
                        <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTarget" Height="100">
                        </asp:ListBox>
                    </td>
                </tr>              
            </table>

        </td>
    </tr>   
</table>

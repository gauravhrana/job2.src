<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.About.Controls.Generic" %>

<table  width="400" >
            <tr>
                <td>
                    <table   >                    
                        <tr>
                            <td width="100">
                                <asp:Label ID="lblReleaseLogId" Text="ReleaseLogId:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReleaseLogId" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="dynReleaseLogId" runat="server"/>                          
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Name:</td>
                            <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:PlaceHolder ID="dynName" runat="server"/>  
                            </td>
                        </tr>
                        <tr>
                            <td align="right">VersionNo:</td>
                            <td><asp:TextBox ID="txtVersionNo" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:PlaceHolder ID="dynVersionNo" runat="server"/>  
                            </td>
                        </tr>
                         <tr>
                            <td align="right">ReleaseDate:</td>
                            <td><asp:TextBox ID="txtReleaseDate" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:PlaceHolder ID="dynReleaseDate" runat="server"/>  
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Description:</td>
                            <td><asp:TextBox ID="txtDescription" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:PlaceHolder ID="dynDescription" runat="server"/>  
                            </td>
                        </tr>
                        <tr>
                            <td align="right">SortOrder:</td>
                            <td><asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox></td>
                            <td>
                                 <asp:PlaceHolder ID="dynSortOrder" runat="server"/>                           
                            </td>
                        </tr>
                    </table>
                </td>
           </tr>
           <tr>
           <td>
           <asp:LinkButton ID="btnInsert" runat="server" Text="Insert Log Details" onclick="btnInsert_Click" />
            <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                    <table   >  
                        <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" style="color:Red;font-weight:bold;"></asp:Label>
                        </td>
                        </tr>                  
                        <tr>
                            <td width="100">
                                <asp:Label ID="lblReleaseLogDetailId" Text="ReleaseLogDetailId:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReleaseLogDetailId" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="dynReleaseLogDetailId" runat="server"/>                          
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                <asp:Label ID="lbl_ReleaseLogId" Text="ReleaseLogId:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ReleaseLogId" Enabled="false" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="dyn_ReleaseLogId" runat="server"/>                          
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right">ItemNo:</td>
                            <td><asp:TextBox ID="txtItemNo" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:PlaceHolder ID="dynItemNo" runat="server"/>  
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right">Description:</td>
                            <td><asp:TextBox ID="txt_Description" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:PlaceHolder ID="dyn_Description" runat="server"/>  
                            </td>
                        </tr>
                        <tr>
                            <td align="right">SortOrder:</td>
                            <td><asp:TextBox ID="txt_SortOrder" runat="server"></asp:TextBox></td>
                            <td>
                                 <asp:PlaceHolder ID="dyn_SortOrder" runat="server"/>                           
                            </td>
                        </tr>
                    </table>
            <asp:LinkButton ID="lnkSave" runat="server" Text="Save" onclick="btnSave_Click" />
            </asp:Panel>
           </td>
           </tr>
       </table>

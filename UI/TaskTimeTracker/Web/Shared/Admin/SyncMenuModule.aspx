<%@ Page Title="Sync Menu Module" Language="C#" MasterPageFile="~/MasterPages/SA/Site.Master" AutoEventWireup="true"
    CodeBehind="SyncMenuModule.aspx.cs" Inherits="Shared.UI.Web.Admin.SyncMenuModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    Sync Menu Module:
                </div>

                <div class="row">
                    <div class="col-sm-3">Source Application</div>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="drpSourceApplication" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">Source Module</div>
                    <div class="col-sm-9">

                        <asp:TextBox ID="txtSourceModule" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">Target Application</div>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="drpTargetApplication" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">Target Module</div>
                    <div class="col-sm-9">

                        <asp:TextBox ID="txtTargetModule" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-9">
                        <asp:Button ID="btnSync" runat="server" Text="Sync" OnClick="btnSync_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-1"></div>
    </div>
</asp:Content>

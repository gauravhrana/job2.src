<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs" Inherits="ApplicationContainer.UI.Web.TCM.TestSuiteXTestCaseArchive.Controls.SearchFilter" %>

<asp:Table runat="server" ID="tblMain" CssClass="searchfilter" >
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>
        <asp:TableCell ColumnSpan="2"><a href="<%= Page.ResolveUrl("~")%>Shared/Admin/SearchSettings.aspx?EN=TestSuiteXTestCaseArchive">S</a></asp:TableCell>
    
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">TestSuite:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionTestSuite" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">TestCase:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionTestCase" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">TestCaseStatus:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionTestCaseStatus" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">TestCasePriority:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionTestCasePriority" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">TestSuiteXTestCase:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionTestSuiteXTestCase" />
        </asp:TableCell>
    </asp:TableRow>
   <asp:TableRow>
       <asp:TableCell></asp:TableCell><asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
       <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
      <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
      </asp:TableCell>
      </asp:TableRow>
</asp:Table>


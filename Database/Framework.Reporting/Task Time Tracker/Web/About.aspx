<%@ Page Title="About Us" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="Web.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        td
        {
            text-align: left;
        }
        
        .lefttd
        {
            text-align: left;
        }
        
        .centertd
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        ABOUT NOTES
    </h2>
    <b>2013-July-21</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Update asp.net routing
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2198")%>">TTT-2198</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Entity
            </td>
            <td>
                ProductivityAreaFeatureXApplicationUser
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2199")%>">TTT-2199</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Bug
            </td>
            <td>
                ProductivityAreaFeatureXApplicationUser UI page error
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2201")%>">TTT-2201</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Business Analysis
            </td>
            <td>
                Add Application User Image Option
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1519")%>">TTT-1519</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Improvement
            </td>
            <td>
                 Grid UI Changes
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2100")%>">TTT-2100</a>
            </td>
            <td>
                User Interface
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Task
            </td>
            <td>
                 User Settings - Tab Control - Analysis and Design
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2162")%>">TTT-2162</a>
            </td>
            <td>
                User Settings Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Bug
            </td>
            <td>
                 Activity Stream
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2159")%>">TTT-2159</a>
            </td>
            <td>
                Activity Stream
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Task
            </td>
            <td>
                 Application Mode - Header Panel - make into combo box
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2163")%>">TTT-2163</a>
            </td>
            <td>
                Master Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Task
            </td>
            <td>
                 Header Panel - Add Menu Category combo box
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2164")%>">TTT-2164</a>
            </td>
            <td>
                Master Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Task
            </td>
            <td>
                 Search Control - Super Key Like functionality
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2061")%>">TTT-2061</a>
            </td>
            <td>
                Search Key, Saved Search
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Bug
            </td>
            <td>
                 Task Page is not rendering.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2204")%>">TTT-2204</a>
            </td>
            <td>
                ...
            </td>
            <td>
                Task
            </td>
        </tr>
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Task
            </td>
            <td>
                 Search Key Link
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2196")%>">TTT-2196</a>
            </td>
            <td>
                Saved Search
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Task
            </td>
            <td>
                 New User Setting
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2195")%>">TTT-2195</a>
            </td>
            <td>
                User Preference
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Task
            </td>
            <td>
                 User Setting Control - Add Check box column
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2193")%>">TTT-2193</a>
            </td>
            <td>
                User Setting Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Bug
            </td>
            <td>
                 Application Mode list looks incomplete
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2192")%>">TTT-2192</a>
            </td>
            <td>
                Master Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Task
            </td>
            <td>
                 Header Layout
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2191")%>">TTT-2191</a>
            </td>
            <td>
                Master Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Code Optimization
            </td>
            <td>
                 Code Review - Site.Master / Menu Control
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2205")%>">TTT-2205</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Code Optimization
            </td>
            <td>
                 Code Review - Verify UI Menu configurations are being cached upon all load
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2206")%>">TTT-2206</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-July-14</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Entity
            </td>
            <td>
                New Entity - ApplicationRoute
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2115")%>">TTT-2115</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Entity
            </td>
            <td>
                New Entity - ApplicationRouteParameter
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2115")%>">TTT-2115</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                FES Chart - See if Pie Chart can be used vs. stacked chart.
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2088")%>">TTT-2088</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                BUG
            </td>
            <td>
                FESSummary chart not loading
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2142")%>">TTT-2142</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                Facilitate Asp Routing on all System Admin Menu Entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1191")%>">TTT-1191</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Task
            </td>
            <td>
                Task Page should have Task Note in tab control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1444")%>">TTT-1444</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Task
            </td>
            <td>
                Testing and Bug fixing on IU/CU pages of various entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Task
            </td>
            <td>
                New Entity : ProductivityAreaXProductivityAreaFeature
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2148")%>">TTT-2148</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement commonUpdate functionality
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1111")%>">TTT-1111</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Search setting per user
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1642")%>">TTT-1642</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Grid - UI Changes
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2100")%>">TTT-2100</a>
            </td>
            <td>
                User Interface
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Too many database calls, something is not correct with this 
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2059")%>">TTT-2059</a>
            </td>
            <td>
                Dynamic Search Control
            </td>
            <td>
                Functionality Entity Status
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                New Feature
            </td>
            <td>
                ApplicaionRoute - Analyze how to get that info into DB vs. file.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2115")%>">TTT-2115</a>
            </td>
            <td>
                Application Routes
            </td>
            <td>
                Application Route
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Bug
            </td>
            <td>
                 Usig FieldConfigurationMode other than "DBColumns" gives runtime error
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2073")%>">TTT-2073</a>
            </td>
            <td>
                Field Configuration Mode
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-July-07</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Asp.net Routing - Cross Reference
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2094")%>">TTT-2094</a>
            </td>
            <td>
                ASP.Net Routing
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Application Combo box should be auto post back and be the driver for subsequent
                list - NotificationRegistrar
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1818")%>">TTT-1818</a>
            </td>
            <td>
                Functionality
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Search FilterAction Bar - Track Status in FES
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2050")%>">TTT-2050</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Feature - Task - Activity
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2044")%>">TTT-2044</a>
            </td>
            <td>
                Data Entry
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                Search Control - Make bottom portion of search control rounded corner
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1983")%>">TTT-1983</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Task
            </td>
            <td>
                Login Page Setting - Add Default Language
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2107")%>">TTT-2107</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                BUG
            </td>
            <td>
                T x AI - Search Control - Live Mode - Dev Boxes Showing
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2108")%>">TTT-2108</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                BUG
            </td>
            <td>
                All parameters of c# function always start with lower case.
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2125")%>">TTT-2125</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                BUG
            </td>
            <td>
                Conversion failed when converting the varchar value 'ProjectXUseCase' to data type
                int
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1745")%>">TTT-1745</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                BUG
            </td>
            <td>
                UseCasePackage - Link button failed.
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1469")%>">TTT-1469</a>
            </td>
            <td>
                Testing
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Task
            </td>
            <td>
                New Entity - ProductivityAreaFeature
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2135")%>">TTT-2135</a>
            </td>
            <td>
                New entity
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Task
            </td>
            <td>
                Resolve UseCaseActorXUseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1440")%>">TTT-1440</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Bug
            </td>
            <td>
                UseCaseActorXUseCase link failed
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1744")%>">TTT-1744</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Feature - Task – Activity
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2044")%>">TTT-2044</a>
            </td>
            <td>
                Application Flow
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Task
            </td>
            <td>
                SearchKeyDetail - DB Changes - Search Value
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2095")%>">TTT-2095</a>
            </td>
            <td>
                Saved Search
            </td>
            <td>
                SearchKeyDetail
            </td>
        </tr>
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Analyze what can be used in existing system such that we can replace the naming dynamically.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2062")%>">TTT-2062</a>
            </td>
            <td>
               Dynamic Labels
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Bug
            </td>
            <td>
                Client Update - Strange - Tab ?
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2116")%>">TTT-2116</a>
            </td>
            <td>
               Tab Control
            </td>
            <td>
                Client
            </td>
        </tr>
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Task
            </td>
            <td>
                Page View Layout
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2117")%>">TTT-2117</a>
            </td>
            <td>
               Page View
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-June-30</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Notification - Add Application Combo box
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1648")%>">TTT-1648</a>
            </td>
            <td>
                Task
            </td>
            <td>
                Event Notification
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Modification of Centralized Control path in the Entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2070")%>">TTT-2070</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Search FilterAction Bar - Track Status in FES
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2050")%>">TTT-2050</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented ASP.Net Routing - Cross Reference Entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                New Entity
            </td>
            <td>
                New Entity : Task X Activity Instance
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2044")%>">TTT-2044</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented ASP.Net Routing, IU/CU Functionalities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                BUG
            </td>
            <td>
                Search Filter Action Bar doesnt fit width range
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Task
            </td>
            <td>
                ProductivityArea
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2120")%>">TTT-2120</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Task
            </td>
            <td>
                Update commonUpdate functionality
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2102")%>">TTT-2102</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Task
            </td>
            <td>
                Do not create a local varible, use set Id directly
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2106")%>">TTT-2106</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Task
            </td>
            <td>
                Update missing menu
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2102")%>">TTT-2102</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Task
            </td>
            <td>
                RunTimeFeature bug resolve
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1979")%>">TTT-1979</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Feature - Task – Activity
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2044")%>">TTT-2044</a>
            </td>
            <td>
                Application Flow
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Bug
            </td>
            <td>
                Debug Boxes Should not show up in Live Mode ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2052")%>">TTT-2052</a>
            </td>
            <td>
                Application Mode
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Analyze what can be used in existing system such that we can replace the naming dynamically.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2062")%>">TTT-2062</a>
            </td>
            <td>
                Dynamic Labels
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                New Feature
            </td>
            <td>
                 Figure how to show image without postback ... AJAX ?
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2056")%>">TTT-2056</a>
            </td>
            <td>
                AJAX
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Bug
            </td>
            <td>
                 Search Control - Dynamic Tab, when no grouping - should be flat grid.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2046")%>">TTT-2046</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Bug
            </td>
            <td>
                 UserPreferenceSelectedItem: Search does not appear to be working ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2053")%>">TTT-2053</a>
            </td>
            <td>
                Search
            </td>
            <td>
                UserPreferenceSelectedItem
            </td>
        </tr>
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Improvement
            </td>
            <td>
                 Too many database calls, something is not correct with this ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2059")%>">TTT-2059</a>
            </td>
            <td>
                Dynamic Search Control
            </td>
            <td>
                Functionality Entity Status
            </td>
        </tr>
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Improvement
            </td>
            <td>
                 Vertical Tab - Select All - Grid show post back in expanded view.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2035")%>">TTT-2035</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Bug
            </td>
            <td>
                 Grid - Record count not sticking ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2099")%>">TTT-2099</a>
            </td>
            <td>
                User Preference
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Bug
            </td>
            <td>
                 UP - Search Control - Text Boxes missing in Testing mode
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2074")%>">TTT-2074</a>
            </td>
            <td>
                Search Control
            </td>
            <td>
                User Preference
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Analysis
            </td>
            <td>
                 Menu - History table - Count seems high; Review / Analyze
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2101")%>">TTT-2101</a>
            </td>
            <td>
                Audit History
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Bug
            </td>
            <td>
                 Usig FieldConfigurationMode other than "DBColumns" gives runtime error.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2073")%>">TTT-2073</a>
            </td>
            <td>
                Fiedl Configuration Mode
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-June-23</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement asp.net routing - all all entites
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1868")%>">TTT-1868</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Search control width issues - All entites entities
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2011")%>">TTT-2011</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Centralized control path
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2078")%>">TTT-2078</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement CommonUpdate functionality
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2047")%>">TTT-2047</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement BreadCrumb to all entities under Skill Metrics Menu.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2030")%>">TTT-2030</a>
            </td>
            <td>
                Task
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Task
            </td>
            <td>
                Control created for FunctionalityXFunctionalityImage .
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2033")%>">TTT-2033</a>
            </td>
            <td>
                Task
            </td>
            <td>
                FunctionalityXFunctionalityImage
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                New Feature
            </td>
            <td>
                FunctionalityxFuntionalityImage - Show image when clicked on right.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2041")%>">TTT-2041</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                FunctionalityXFunctionalityImage
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                New Feature
            </td>
            <td>
                FunctionalityxFuntionalityImage - tab control added in Functionality.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2013")%>">TTT-2013</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                Functionality
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Bug
            </td>
            <td>
                Update/Detail page of Feature should not have associated tasks initially.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2042")%>">TTT-2042</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                Feature
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Bug
            </td>
            <td>
                Testing & Bug Fixing of UseCaseActorXUseCase .
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1744")%>">TTT-1744</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                UseCaseActorXUseCase
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Bug
            </td>
            <td>
                Testing & Bug Fixing of UseCaseXUseCaseStep .
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1762")%>">TTT-1762</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                UseCaseXUseCaseStep
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Bug
            </td>
            <td>
                Testing & Bug Fixing of EventPublisher .
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1771")%>">TTT-1771</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                EventPublisher
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Bug
            </td>
            <td>
                Implement Search setting per User - Risk Reward And Vacation plan.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1825")%>">TTT-1825</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                Risk Reward And Vacation plan
            </td>
        </tr>
        <!--Row 14 -->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Task
            </td>
            <td>
                FES - Notification Event
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2026")%>">TTT-2026</a>
            </td>
            <td>
                Task
            </td>
            <td>
                Event Notification
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Task
            </td>
            <td>
                Move Menus
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2051")%>">TTT-2051</a>
            </td>
            <td>
                Task
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Task
            </td>
            <td>
                Search Settings Should have title bar to indicate the Search Control Entity which
                these items are about.
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2050")%>">TTT-2050</a>
            </td>
            <td>
                Task
            </td>
            <td>
                Task
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                New Feature
            </td>
            <td>
                FES Chart - Analyze how to group by Date Range
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2008")%>">TTT-2008</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                FES
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Task
            </td>
            <td>
                NotificationPublisherXEventType - UI - Bucket Control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2027")%>">TTT-2027</a>
            </td>
            <td>
                Task
            </td>
            <td>
                Event Notification
            </td>
        </tr>
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Review
            </td>
            <td>
                Feature - Task – Activity
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2044")%>">TTT-2044</a>
            </td>
            <td>
                System Flow
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Debug Boxes Should not show up in Live Mode ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2052")%>">TTT-2052</a>
            </td>
            <td>
                Debug Boxes
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Task
            </td>
            <td>
                Figure how to show image without postback ... AJAX ?
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2056")%>">TTT-2056</a>
            </td>
            <td>
                AJAX
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Search Control - Dynamic Tab, when no grouping - should be flat grid.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2046")%>">TTT-2046</a>
            </td>
            <td>
                Grouped List Control
            </td>
            <td>
                FES
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Bug
            </td>
            <td>
                UserPreferenceSelectedItem: Search does not appear to be working ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2053")%>">TTT-2053</a>
            </td>
            <td>
                Search
            </td>
            <td>
                UserPreferenceSelectedItem
            </td>
        </tr>
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Too many database calls, something is not correct with this ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2059")%>">TTT-2059</a>
            </td>
            <td>
                Search
            </td>
            <td>
                FES
            </td>
        </tr>
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Bug
            </td>
            <td>
                Grid - Record count not sticking ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2099")%>">TTT-2099</a>
            </td>
            <td>
                Grid Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Bug
            </td>
            <td>
                UP - Search Control - Text Boxes missing in Testing mode
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2074")%>">TTT-2074</a>
            </td>
            <td>
                Deubg Boxes
            </td>
            <td>
                User Preference
            </td>
        </tr>
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                Review
            </td>
            <td>
                Menu - History table - Count seems high; Review / Analyze
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2101")%>">TTT-2101</a>
            </td>
            <td>
                Audit History Grid
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                Bug
            </td>
            <td>
                Usig FieldConfigurationMode other than "DBColumns" gives runtime error.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2073")%>">TTT-2073</a>
            </td>
            <td>
                DetailWithChildren Grid Control
            </td>
            <td>
                FES
            </td>
        </tr>
    </table>
    <b>2013-June-16</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement Search Setting per User - Admin entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1827")%>">TTT-1827</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Action Bar on Search Control Implementation
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                BUG
            </td>
            <td>
                Data Entry - This should be User Preferences
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2010")%>">TTT-2010</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                FES Chart - Analyze how to group by Date Range
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2008")%>">TTT-2008</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                BUG
            </td>
            <td>
                FunctionalityImageXFunctionalityImageAttribute - Grid Link
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1999")%>">TTT-1999</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                UI/DB Change
            </td>
            <td>
                Header should be centered DB configuration driven
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1981")%>">TTT-1981</a>
            </td>
            <td>
                UI alteration
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                UI Change
            </td>
            <td>
                DB Release Notes - Make Font Size into a common section
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1968")%>">TTT-1968</a>
            </td>
            <td>
                UI alteration
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                UI Change
            </td>
            <td>
                Section labels are missing
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1925")%>">TTT-1925</a>
            </td>
            <td>
                UI alteration
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                UI Change
            </td>
            <td>
                ReleaseNotes UI change
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1926")%>">TTT-1926</a>
            </td>
            <td>
                UI alteration
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                UI Change
            </td>
            <td>
                Release Notes - Search Control - UI - X vs. Check Box
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1985")%>">TTT-1985</a>
            </td>
            <td>
                UI alteration
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                UI Change
            </td>
            <td>
                Release Notes - Search Control - UI - X vs. Check Box
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1985")%>">TTT-1985</a>
            </td>
            <td>
                UI alteration
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                UI Feature
            </td>
            <td>
                FES Searchj control - UI Date range
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1986")%>">TTT-1986</a>
            </td>
            <td>
                Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                UI Feature
            </td>
            <td>
                FES Search control - UI Sub Group
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1987")%>">TTT-1987</a>
            </td>
            <td>
                Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented asp.net routing - All entites
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2043")%>">TTT-2043</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Bug
            </td>
            <td>
                Missing BreadCrumb under Project Admin menu.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1997")%>">TTT-1997</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu Search Control Bugs.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1995")%>">TTT-1995</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Bug
            </td>
            <td>
                TaskEntityType Delete button failed.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1897")%>">TTT-1897</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                TaskEntityType
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implementation of Action Bar on Search Control.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1950")%>">TTT-1950</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 19 -->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implementation of Search setting per User -Feature Folder.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1689")%>">TTT-1689</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 20 -->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                New Entity
            </td>
            <td>
                FunctionalityXFunctionalityImage
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2018")%>">TTT-2018</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                FunctionalityXFunctionalityImage
            </td>
        </tr>
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Bug
            </td>
            <td>
                Vertical Tab - Select All - Grid show post back in expanded view.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2035")%>">TTT-2035</a>
            </td>
            <td>
                Vertical Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Review
            </td>
            <td>
                FES - Search Control - Review & some UI
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2036")%>">TTT-2036</a>
            </td>
            <td>
                Search Control
            </td>
            <td>
                FES
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Review
            </td>
            <td>
                Tab control - db tab control
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1344")%>">TTT-1344</a>
            </td>
            <td>
                Tab Control
            </td>
            <td>
                Project
            </td>
        </tr>
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Vertical Tab Control: Make Header Background color User Preference
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2040")%>">TTT-2040</a>
            </td>
            <td>
                Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Bug
            </td>
            <td>
                Bread Crumb on Search Settings
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2045")%>">TTT-2045</a>
            </td>
            <td>
                Bread Crumb
            </td>
            <td>
            </td>
        </tr>
    </table>
    <b>2013-June-9</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Move Menu - T&W
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1961")%>">TTT-1961</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Bug
            </td>
            <td>
                User Preference page not working
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1960")%>">TTT-1960</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Audit History ... UI
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1962")%>">TTT-1962</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Menu - FES Chart Move
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1974")%>">TTT-1974</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Research - Publish / Subscribe Event via database queue
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1523")%>">TTT-1523</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6-->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Asp.net Routing, SubMenu and Breadcrumb control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1927")%>">TTT-1927</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7-->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                BUG
            </td>
            <td>
                FES Chart - Various elements missing
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1975")%>">TTT-1975</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                UI Correction
            </td>
            <td>
                FES UI changes as per feedback
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1958")%>">TTT-1958</a>
            </td>
            <td>
                UI change
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Feature
            </td>
            <td>
                DB Release Notes Search control
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1966")%>">TTT-1966</a>
            </td>
            <td>
                DB Driven
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Feature
            </td>
            <td>
                DB Release Notes Search control
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1966")%>">TTT-1966</a>
            </td>
            <td>
                DB Driven
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Feature
            </td>
            <td>
                Release Notes - Bread Crumb / Submenu Control
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1967")%>">TTT-1967</a>
            </td>
            <td>
                Feature implementation
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Feature
            </td>
            <td>
                DB Release Notes - Insert action per section
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1969")%>">TTT-1969</a>
            </td>
            <td>
                Feature implementation
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Feature
            </td>
            <td>
                DB driven Search Item Labels / Field Configuration
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1970")%>">TTT-1970</a>
            </td>
            <td>
                DB Driven Search
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                New UI Feature
            </td>
            <td>
                Hiding Search parameters
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1971")%>">TTT-1971</a>
            </td>
            <td>
                Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15-->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Task
            </td>
            <td>
                Beta entites updated with missing functionality
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1996")%>">TTT-1996</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16-->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Task
            </td>
            <td>
                Action Bar on Search control for listed entites
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1950")%>">TTT-1950</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17-->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Task
            </td>
            <td>
                Remove Group By for entites
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2006")%>">TTT-2006</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18-->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Image in Detail/Update page of Functionality Image.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1942")%>">TTT-1942</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Functionality Image
            </td>
        </tr>
        <!--Row 19-->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Bug
            </td>
            <td>
                Missing BreadCrumb in FES entity.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1943")%>">TTT-1943</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                Functionality Entity Status
            </td>
        </tr>
        <!--Row 20-->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Movement of Runtime feature under Application Mode
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1952")%>">TTT-1952</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 21-->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Bug
            </td>
            <td>
                Missing BreadCrumb in FeatureGroupXFeature.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1954")%>">TTT-1954</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                FeatureGroupXFeature
            </td>
        </tr>
        <!--Row 22-->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Bug
            </td>
            <td>
                Invalid "Group By" DropDown items resolved.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1955")%>">TTT-1955</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                Feature
            </td>
        </tr>
        <!--Row 23-->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Improvement
            </td>
            <td>
                ASP.NET routing implemented to Functionality Image.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1964")%>">TTT-1964</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Functionality Image
            </td>
        </tr>
        <!--Row 24-->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Missing menu under Functionality Image parent.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1963")%>">TTT-1963</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Functionality Image
            </td>
        </tr>
        <!--Row 25-->
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Bug
            </td>
            <td>
                ProjectXUseCase Search Control Bugs resolved.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1976")%>">TTT-1976</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ProjectXUseCase
            </td>
        </tr>
        <!--Row 26-->
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Bug
            </td>
            <td>
                RunTimeFeature CRUD Bugs resolved.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1979")%>">TTT-1979</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                RunTimeFeature
            </td>
        </tr>
        <!--Row 27-->
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Press button to show popup to show image in original size.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1980")%>">TTT-1980</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                Functionality Image
            </td>
        </tr>
        <!--Row 28-->
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                Bug
            </td>
            <td>
                Broken Functionality Menu link .
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1991")%>">TTT-1991</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                Functionality
            </td>
        </tr>
        <!--Row 29-->
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Menu Movement - FeatureRule
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1992")%>">TTT-1992</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 30-->
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Missing Menu under Application Mode.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1993")%>">TTT-1993</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                31.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Analayze How to enable to Tab View Control to be transformed into Expand view
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1245")%>">TTT-1245</a>
            </td>
            <td>
                Vertical Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                32.
            </td>
            <td>
                Task
            </td>
            <td>
                UP Search Control
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1998")%>">TTT-1998</a>
            </td>
            <td>
                Search Control
            </td>
            <td>
                User Preference
            </td>
        </tr>
        <tr>
            <td class="centertd">
                32.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu - Audit History does not seem to work
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2001")%>">TTT-2001</a>
            </td>
            <td>
                Audit History Grid
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                33.
            </td>
            <td>
                Bug
            </td>
            <td>
                FunctionalityImageXFunctionalityImageAttribute - Grid Link
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1999")%>">TTT-1999</a>
            </td>
            <td>
                ASP.NET Routing
            </td>
            <td>
                FunctionalityImageXFunctionalityImageAttribute
            </td>
        </tr>
        <tr>
            <td class="centertd">
                34.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Tab control - Future may add addtional info in tab name
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1791")%>">TTT-1791</a>
            </td>
            <td>
                Grouped List Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                35.
            </td>
            <td>
                Bug
            </td>
            <td>
                Vertical Tab Group - Search - fails to render grid.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2005")%>">TTT-2005</a>
            </td>
            <td>
                Vertical Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                36.
            </td>
            <td>
                Task
            </td>
            <td>
                Release Notes - Search Control - UI - X vs. Check Box
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1985")%>">TTT-1985</a>
            </td>
            <td>
                Search Control
            </td>
            <td>
                Release Notes
            </td>
        </tr>
        <tr>
            <td class="centertd">
                37.
            </td>
            <td>
                Bug
            </td>
            <td>
                Log4Net - Grid Columns do not match
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2002")%>">TTT-2002</a>
            </td>
            <td>
                FCMode
            </td>
            <td>
                Log4Net
            </td>
        </tr>
        <tr>
            <td class="centertd">
                38.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu - Common Update Did not work.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2000")%>">TTT-2000</a>
            </td>
            <td>
                Common Update
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                39.
            </td>
            <td>
                Review
            </td>
            <td>
                Think how this can be made into loop
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1738")%>">TTT-1738</a>
            </td>
            <td>
                Common Update
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                40.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Section Labels are missing
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1925")%>">TTT-1925</a>
            </td>
            <td>
                Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                41.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Group By Tab - Font Size
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1944")%>">TTT-1944</a>
            </td>
            <td>
                Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                42.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Group By - When you group by something the column no longer is needed in grid.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2006")%>">TTT-2006</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                43.
            </td>
            <td>
                New Feature
            </td>
            <td>
                TTT - Vertical Tab Group Collapsed Width
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2004")%>">TTT-2004</a>
            </td>
            <td>
                Vertical Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                44.
            </td>
            <td>
                Bug
            </td>
            <td>
                Tab control - double highlighting?
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1789")%>">TTT-1789</a>
            </td>
            <td>
                Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                45.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Control Path Centralized somehow ... analyze
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2009")%>">TTT-2009</a>
            </td>
            <td>
                Generic
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                46.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Search Control - Review border is missing ... width issues ?
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2011")%>">TTT-2011</a>
            </td>
            <td>
                Search Action Bar
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                47.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Tab Dynamic Vertical - Move expand control slightly more to the left ...
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2020")%>">TTT-2020</a>
            </td>
            <td>
                Vertical Tab Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                48.
            </td>
            <td>
                Bug
            </td>
            <td>
                List Control - Common - Release Notes not sticking
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-2019")%>">TTT-2019</a>
            </td>
            <td>
                FCMode
            </td>
            <td>
                Release Notes
            </td>
        </tr>
    </table>
    <b>2013-June-02</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Menu Spacing
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1951")%>">TTT-1951</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented missing points on ApplicationMonitoredProcessingState
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1923")%>">TTT-1923</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented missing points on ApplicationMonitoredEventEmail
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1923")%>">TTT-1923</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented missing points on ApplicationMonitoredEvent
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1923")%>">TTT-1923</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented missing points on TimeZone and Country
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1972")%>">TTT-1972</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented missing points on TimeZone and Country
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1972")%>">TTT-1972</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                BUG
            </td>
            <td>
                Task - Tab Control - Spaces in label
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1915")%>">TTT-1915</a>
            </td>
            <td>
                BUG
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Task
            </td>
            <td>
                Menu - Move Item
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1917")%>">TTT-1917</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Bug
            </td>
            <td>
                Search Button - Appears to be require double click
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1918")%>">TTT-1918</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Asp.net Routing, SubMenu and Breadcrumb control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1927")%>">TTT-1927</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search Setting per User
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1827")%>">TTT-1827</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Stacked chart : Stack By combo box
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1928")%>">TTT-1928</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                New Entity
            </td>
            <td>
                New Entity : FunctionalityImageXFunctionalityImageAttribute
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1936")%>">TTT-1936</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Code review
            </td>
            <td>
                FES Settings Search
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1865")%>">TTT-1865</a>
            </td>
            <td>
                Review
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Implement Feature
            </td>
            <td>
                Menu - Various Features Missing Bread Crumb, SubMenu...
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1924")%>">TTT-1924</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                BUG
            </td>
            <td>
                FES - Update - Blocker - After update should come back to same page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1910")%>">TTT-1910</a>
            </td>
            <td>
                Bug fixing
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                BUG
            </td>
            <td>
                Project - Update - Section Labels are missing.
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1925")%>">TTT-1925</a>
            </td>
            <td>
                Bug fixing
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Feature
            </td>
            <td>
                Release Notes - Styling
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1926")%>">TTT-1926</a>
            </td>
            <td>
                UI Style
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Feature
            </td>
            <td>
                FES - Implement multiple selection on FS
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1909")%>">TTT-1909</a>
            </td>
            <td>
                UI Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                UI Feature
            </td>
            <td>
                FES - Search Control - Setting Letter Location
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1933")%>">TTT-1933</a>
            </td>
            <td>
                UI
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                BUG
            </td>
            <td>
                Search Control - Testing Mode - Debug boxes missing
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1934")%>">TTT-1934</a>
            </td>
            <td>
                Bug fixing
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                BUG
            </td>
            <td>
                FES - Search - Search By Date Range
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1941")%>">TTT-1941</a>
            </td>
            <td>
                Bug fixing
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 23-->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Improvement
            </td>
            <td>
                BreadCrumb logic Modified.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1889")%>">TTT-1889</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 24-->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Bug
            </td>
            <td>
                ApplicationModeXRunTimeFeature Bugs
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1913")%>">TTT-1913</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ApplicationModeXRunTimeFeature
            </td>
        </tr>
        <!--Row 25-->
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Bug
            </td>
            <td>
                Missing Menu Link
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1916")%>">TTT-1916</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                RunTimeFeature
            </td>
        </tr>
        <!--Row 26-->
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Menu - Data Entry - PrimaryDeveloper
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1912")%>">TTT-1912</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 27-->
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implemented missing features of Functionality entity.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1931")%>">TTT-1931</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 28-->
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implemented missing features of FeatureXFeatureRule entity.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1932")%>">TTT-1932</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                FeatureXFeatureRule
            </td>
        </tr>
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu - Search for Test in name causes failure
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1946")%>">TTT-1946</a>
            </td>
            <td>
                Search
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                Task
            </td>
            <td>
                List Control: Enable Paging Feature for PageSize of dropdown vs textbox
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1965")%>">TTT-1965</a>
            </td>
            <td>
                List Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                31.
            </td>
            <td>
                Task
            </td>
            <td>
                Action Bar on Search control
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1950")%>">TTT-1950</a>
            </td>
            <td>
                Search Action Bar
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                32.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Analayze How to enable to Tab View Control to be transformed into Expand view
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1245")%>">TTT-1245</a>
            </td>
            <td>
                Vertical Tab Control
            </td>
            <td>
            </td>
        </tr>
    </table>
    <b>2013-MAY-26</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Moved menu under UseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1757")%>">TTT-1757</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Moved menu under ApplicaitonMode
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Menu spacing
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemeneted missing items
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1923")%>">TTT-1923</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement DeliverableArtifact TAB in Task entity
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1448")%>">TTT-1448</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                New feature
            </td>
            <td>
                FESSummary chart
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1652")%>">TTT-1652</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                New Entity
            </td>
            <td>
                New Entity - FunctionalityImageAttribute
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1891")%>">TTT-1891</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Asp.net Routing, SubMenu and Breadcrumb control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1927")%>">TTT-1927</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Code Review
            </td>
            <td>
                FES Setting Search
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1866")%>">TTT-1866</a>
            </td>
            <td>
                Review
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                BUG
            </td>
            <td>
                FES Page link not rendering after selecting from list .. grid
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1867")%>">TTT-1867</a>
            </td>
            <td>
                Bug fixing
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Task tracking
            </td>
            <td>
                Menu Links follow up with persons on when ASP.net routing links will be updated
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1868")%>">TTT-1868</a>
            </td>
            <td>
                Task tracking
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Task tracking
            </td>
            <td>
                Menu Links follow up with persons on when ASP.net routing links will be updated
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1868")%>">TTT-1868</a>
            </td>
            <td>
                Task tracking
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Bug
            </td>
            <td>
                FES - Search button requires clicking twice on many occasions.
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1853")%>">TTT-1853</a>
            </td>
            <td>
                Search button click
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Bug
            </td>
            <td>
                Project - update - Data not showing
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1882")%>">TTT-1882</a>
            </td>
            <td>
                Project Update
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Bug
            </td>
            <td>
                Question & Question Category - Update not working
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1883")%>">TTT-1883</a>
            </td>
            <td>
                Update
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Refactor
            </td>
            <td>
                Review and Remove code from FES Default page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1884")%>">TTT-1884</a>
            </td>
            <td>
                Code refactoring
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Refactor
            </td>
            <td>
                Review and Remove code from FES Default page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1884")%>">TTT-1884</a>
            </td>
            <td>
                Code refactoring
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Data correction
            </td>
            <td>
                Menu data entry add spaces
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1881")%>">TTT-1881</a>
            </td>
            <td>
                Data entry
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 20-->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Modification of Menu Logic.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1803")%>">TTT-1803</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 21-->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Removal of WebFramework & Export Mennu Namespace from all the default pages.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1862")%>">TTT-1862</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 22-->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Bug
            </td>
            <td>
                Invalid Parent name of BreadCrumb.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1890")%>">TTT-1890</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Question
            </td>
        </tr>
        <!--Row 23-->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Bug
            </td>
            <td>
                SubMenu moving to invalid URL.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1888")%>">TTT-1888</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 24-->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Functionality Image
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1891")%>">TTT-1891</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                Functionality Image
            </td>
        </tr>
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Bug
            </td>
            <td>
                Group By Tab Control looses controls
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1842")%>">TTT-1842</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Bug
            </td>
            <td>
                Tab Control - Page Size needs to be same (maitained at tab)
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1873")%>">TTT-1873</a>
            </td>
            <td>
                List Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Group By Tab - {All Tab}
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1945")%>">TTT-1945</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu - Search for Test in name causes failure
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1946")%>">TTT-1946</a>
            </td>
            <td>
                Search
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu - Update - Test (-999999) fails
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1947")%>">TTT-1947</a>
            </td>
            <td>
                Update
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Multiple Update - Layout looks funny
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1949")%>">TTT-1949</a>
            </td>
            <td>
                Multiple View
            </td>
            <td>
                Menu
            </td>
        </tr>
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Log4Net - Cleanup function
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1894")%>">TTT-1894</a>
            </td>
            <td>
            </td>
            <td>
                Log4Net
            </td>
        </tr>
    </table>
    <!--Updated-->
    <b>2013-MAY-19</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement tab's on UseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement tab's on UseCaseActor
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Move Menu Items under UseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement TAB control in TestSuite
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1815")%>">TTT-1815</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement TAB control in Task
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1448")%>">TTT-1448</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                BUG
            </td>
            <td>
                Incorrect Proposed Path - ASP.Net Routing
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1852")%>">TTT-1852</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                New Feature
            </td>
            <td>
                ASP.Net Routing - TCM entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1571")%>">TTT-1571</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                New Feature
            </td>
            <td>
                ASP.Net Routing - Requirement Analysis entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1858")%>">TTT-1858</a>
            </td>
            <td>
                Requirement Analysis
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Bug
            </td>
            <td>
                Search Page fails
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1855")%>">TTT-1855</a>
            </td>
            <td>
                WBS-Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Review
            </td>
            <td>
                DB Driven Release Log / Release Log Details
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1855")%>">TTT-1855</a>
            </td>
            <td>
                ReleaseLog
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Feature
            </td>
            <td>
                Common Update/Inline Update in FES
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1462")%>">TTT-1462</a>
            </td>
            <td>
                FES
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Prototype
            </td>
            <td>
                Functionality on Delete Page, prototype and delegate
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1590")%>">TTT-1590</a>
            </td>
            <td>
                Generic
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Feature
            </td>
            <td>
                ASP.Net Routing implementation
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Generic
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                BUG
            </td>
            <td>
                UP Exceptions in Sub Menu implementation
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                SubMenu
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14-->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Moved the Help out from grid control to page level.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1792")%>">TTT-1792</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 15-->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Moved the Help out from grid control to page level.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1792")%>">TTT-1792</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Moved the Help out from grid control to page level.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1792")%>">TTT-1792</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Bug
            </td>
            <td>
                Group By Tab Control looses controls
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1842")%>">TTT-1842</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Too many calls to SetUpConfigurationList
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1869")%>">TTT-1869</a>
            </td>
            <td>
                SetupConfiguration
            </td>
            <td>
                SetupConfiguration
            </td>
        </tr>
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Bug
            </td>
            <td>
                FunctionalityEntityStatus / Default
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1872")%>">TTT-1872</a>
            </td>
            <td>
                Generic
            </td>
            <td>
                FES
            </td>
        </tr>
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Bug
            </td>
            <td>
                FES Page link not rendering after selecting from list .. grid.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1864")%>">TTT-1864</a>
            </td>
            <td>
                ASP.NET Routing
            </td>
            <td>
                FES
            </td>
        </tr>
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Use local Common Function
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1892")%>">TTT-1892</a>
            </td>
            <td>
                Generic
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Task
            </td>
            <td>
                Log4Net - Computer Column
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1893")%>">TTT-1893</a>
            </td>
            <td>
            </td>
            <td>
                Log4Net
            </td>
        </tr>
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Bug
            </td>
            <td>
                Release Notes page does not render
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1863")%>">TTT-1863</a>
            </td>
            <td>
                Release Notes
            </td>
            <td>
                Release Log
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Task
            </td>
            <td>
                TTT - Shared.UI.WebFramework.BaseControl
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1861")%>">TTT-1861</a>
            </td>
            <td>
                Base Control
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Bug
            </td>
            <td>
                Checkbox value not getting captured in DetailsWithChildren grid control.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1871")%>">TTT-1871</a>
            </td>
            <td>
                DetailsWithChildren Grid Control
            </td>
            <td>
            </td>
        </tr>
    </table>
    <b>2013-MAY-12</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                ApplicationModeXRunTimeFeature
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Resolve CUD functions bugs ApplicationModeXRunTimeFeature
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Resolve bugs
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                UseCaseProjectStatusArchive
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Task
            </td>
            <td>
                Created history for UseCaseProjectStatusArchive
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement bread crumb on Report and Question folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6-->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement bread crumb on WBS and RiskAndRewatd Folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7-->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                BUG
            </td>
            <td>
                Data not render in Default.aspx page
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1828")%>">TTT-1828</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8-->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                BUG
            </td>
            <td>
                TCM - control's setting category
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1823")%>">TTT-1823</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9-->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                FEATURE
            </td>
            <td>
                Wild card search on Text Field
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1588")%>">TTT-1588</a>
            </td>
            <td>
                Wild card search
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10-->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                FEATURE
            </td>
            <td>
                Admin page to configure the SubMenu Selection
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1817")%>">TTT-1817</a>
            </td>
            <td>
                Admin page - Sub Menu selection
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11-->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                FEATURE
            </td>
            <td>
                Admin page to configure the SubMenu Selection
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1817")%>">TTT-1817</a>
            </td>
            <td>
                Admin page - Sub Menu selection
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12-->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                FEATURE
            </td>
            <td>
                FC Mode saved as User Setting
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13-->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                FEATURE
            </td>
            <td>
                Create script to make entries for all entities in FES
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1831")%>">TTT-1831</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14-->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                BUG
            </td>
            <td>
                FES combo value change exception
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1832")%>">TTT-1832</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15-->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                BUG
            </td>
            <td>
                Setting Page - Does not return back to original location.
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1839")%>">TTT-1839</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16-->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                BUG
            </td>
            <td>
                Setting Page - Does not return back to original location.
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1839")%>">TTT-1839</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17-->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                BUG
            </td>
            <td>
                Implement Group By Tab Control for FES
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1840")%>">TTT-1840</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18-->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Create Bread Crumb user control
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1824")%>">TTT-1824</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 19-->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Search Setting per user for Cross entities.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1826")%>">TTT-1826</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 20-->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Search Setting per user for all entities under Feature.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1689")%>">TTT-1689</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 21-->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implement Common Update and Inline Update.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1532")%>">TTT-1532</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Entities under TaskAndWorkFlow folder
            </td>
        </tr>
        <!--Row 22-->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Added BreadCrumb to Milestone Entity.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1830")%>">TTT-1830</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Milestone
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Task
            </td>
            <td>
                Menu / Menu Category / MenuXMenuCategory
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1572")%>">TTT-1572</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                Generic
            </td>
        </tr>
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Improvement
            </td>
            <td>
                FES - Too many queries when loading page / searching
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1854")%>">TTT-1854</a>
            </td>
            <td>
                Generic
            </td>
            <td>
                FES
            </td>
        </tr>
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Client - Too many queries when loading page / searching
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1856")%>">TTT-1856</a>
            </td>
            <td>
                Generic
            </td>
            <td>
                Client
            </td>
        </tr>
    </table>
    <!--Updated-->
    <b>2013-MAY-05</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                URL correction
            </td>
            <td>
                Correct Search Settings URL
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1819")%>">TTT-1819</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                Analyze / Transform code into Base Class
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1820")%>">TTT-1820</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                Analyze / Transform code into Base Class - SettingsCategory
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1736")%>">TTT-1736</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                DB change
            </td>
            <td>
                Name column size made bugger
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1801")%>">TTT-1801</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                DB change
            </td>
            <td>
                Name column size made bugger
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1801")%>">TTT-1801</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Create new Sub Menu Settings page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1817")%>">TTT-1817</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Requirement Analysis
            </td>
            <td>
                Create Bread Crumb user control to trace User's navigation through the Application
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1824")%>">TTT-1824</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Bug
            </td>
            <td>
                Data Entry - FES - Field Configuration Derived Fields
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1800")%>">TTT-1800</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Change bool to string
            </td>
            <td>
                WildcardSearch implementation - change field to string from boolean
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1588")%>">TTT-1588</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement Rest button to entites
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Task
            </td>
            <td>
                Functionality on Delete page
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Task
            </td>
            <td>
                RunTimeFeature
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14 -->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Task
            </td>
            <td>
                Covert DB RunTimeFeature To configuraiton
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Task
            </td>
            <td>
                Review / Implement GetEntityList
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1816")%>">TTT-1816</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                New entity
            </td>
            <td>
                New entity Creation:Functionality Active Status
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1730")%>">TTT-1730</a>
            </td>
            <td>
                Quality Assurance
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Create BreadCrumb user control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1824")%>">TTT-1824</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Bug
            </td>
            <td>
                Data does not render
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1741")%>">TTT-1741</a>
            </td>
            <td>
                Event Notification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Bug
            </td>
            <td>
                Event Publisher - UP - Failed.
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1741")%>">TTT-1741</a>
            </td>
            <td>
                Event Notification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 19 -->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Create SubMenu control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1817")%>">TTT-1817</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 20 -->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Create MasterDefaultLayout for SubMenu on every Default Page
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1804")%>">TTT-1804</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 21 -->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Improvement
            </td>
            <td>
                UI improvement of SubMenu Control.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1821")%>">TTT-1821</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 22 -->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Default.Master implemented in all the entities.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1822")%>">TTT-1822</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Task
            </td>
            <td>
                T4 Transformation: Create template to generate Stored Procedures
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-18")%>">CM-18</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Task
            </td>
            <td>
                Check box to retrieve only latest task run for given security for given date
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-14")%>">CM-14</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Task
            </td>
            <td>
                MVC Website: Navigation Menu Styling
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-20")%>">CM-20</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Task
            </td>
            <td>
                T4 Transformation: Create template to generate Controller Classes
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-17")%>">CM-17</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                Improvement
            </td>
            <td>
                T4 Transformation: Maintain Proper Spacing when creating procedures, classes
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-21")%>">CM-21</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Dynamic tabs
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1733")%>">TTT-1733</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                Task
            </td>
            <td>
                Menu / Menu Category / MenuXMenuCategory
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1572")%>">TTT-1572</a>
            </td>
            <td>
                Menu
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                New Feature
            </td>
            <td>
                T4 Transformation: Create template to generate Views
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-22")%>">CM-22</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
    </table>
    <!--Updated-->
    <b>2013-APRIL-28</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Bug
            </td>
            <td>
                Labels in details page made bold
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1786")%>">TTT-1786</a>
            </td>
            <td>
                Labels in details page made bold
            </td>
            <td>
                Application User
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Bug
            </td>
            <td>
                Spacing between Labels.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1781")%>">TTT-1781</a>
            </td>
            <td>
                Spacing between Labels.
            </td>
            <td>
                Application Role
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Bug
            </td>
            <td>
                Double Columns appearing in List
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1783")%>">TTT-1783</a>
            </td>
            <td>
                Default Page.
            </td>
            <td>
                MileStoneXFeature
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Bug
            </td>
            <td>
                Labels made bold.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1784")%>">TTT-1784</a>
            </td>
            <td>
                Menu update page.
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Bug
            </td>
            <td>
                Not Getting Rendered.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1785")%>">TTT-1785</a>
            </td>
            <td>
                Default Page
            </td>
            <td>
                ScheduleQuestion
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Field Configuration Menu Clean up
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1779")%>">TTT-1779</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                Field Configuration
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Bug
            </td>
            <td>
                Label Should be bold
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1786")%>">TTT-1786</a>
            </td>
            <td>
                Update/Details
            </td>
            <td>
                Application User
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Bug
            </td>
            <td>
                Insert fails in UserPreference Category
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1787")%>">TTT-1787</a>
            </td>
            <td>
                Default Page
            </td>
            <td>
                Project X UseCase
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Sub Menu Control
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1777")%>">TTT-1777</a>
            </td>
            <td>
                Default Page
            </td>
            <td>
                Generic
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Grouping of Menu under schedule(Parent)Menu.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1805")%>">TTT-1805</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                Schedule
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Include Date Filter to Search Control
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1802")%>">TTT-1802</a>
            </td>
            <td>
                Default Page
            </td>
            <td>
                Schedule Question
            </td>
        </tr>
        <!--Row 12-->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Bug
            </td>
            <td>
                FES Data missing
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1770")%>">TTT-1770</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Feature Improvement
            </td>
            <td>
                Control Pattern Naming - SearchControl, ListControl..
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1772")%>">TTT-1772</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14 -->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                Create UI Helper function - Search Settings
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1774")%>">TTT-1774</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Code Review and fix
            </td>
            <td>
                Random number in middle of code
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1778")%>">TTT-1778</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Bug
            </td>
            <td>
                Live Mode showing debug values - FES
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1799")%>">TTT-1799</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Settings for search control - Generic rendering [S]
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1809")%>">TTT-1809</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Review & Research
            </td>
            <td>
                Master Page Items - Review & research, move items to Master page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1810")%>">TTT-1810</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 19 -->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                UI Enhancement
            </td>
            <td>
                Wild Card Search Prefix - change control from checkbox to textbox
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1811")%>">TTT-1811</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 20 -->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Correct coding style
            </td>
            <td>
                Correct coding style, follow coding standards
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1813")%>">TTT-1813</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 21 -->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Code classification
            </td>
            <td>
                Control Pattern Naming - Analyze & Verify Postback and Non-Postback items
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1812")%>">TTT-1812</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 22 -->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement Search setting on WBS folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 23 -->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Task
            </td>
            <td>
                Remove delete button from details page
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1603")%>">TTT-1603</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 24 -->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Bug
            </td>
            <td>
                NotificationPublisher - Update not rendering
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1807")%>">TTT-1807</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 25 -->
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement IU/CU functionality
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1570")%>">TTT-1570</a>
            </td>
            <td>
                New Feature
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Bug
            </td>
            <td>
                NotificationPublisherXEventType - Grid links do not work
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1775")%>">TTT-1775</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                Bug
            </td>
            <td>
                Missing Text Boxes for debug mode.
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1769")%>">TTT-1769</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 28 -->
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                Bug
            </td>
            <td>
                NotificationPublisher - Update not rendering
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1807")%>">TTT-1807</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 29 -->
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                Bug
            </td>
            <td>
                Missing Text Boxes in the Search Filter
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1773")%>">TTT-1773</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 30 -->
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                Bug
            </td>
            <td>
                Event Publisher-UP-Failed.
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1771")%>">TTT-1771</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 31 -->
        <tr>
            <td class="centertd">
                31.
            </td>
            <td>
                Bug
            </td>
            <td>
                TaskPackageXOwnerXTask throws error during update
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1798")%>">TTT-1798</a>
            </td>
            <td>
                TaskPackageXOwnerXTask
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 32 -->
        <tr>
            <td class="centertd">
                32.
            </td>
            <td>
                Bug
            </td>
            <td>
                Search Control not in tabular layout
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1768")%>">TTT-1768</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 33 -->
        <tr>
            <td class="centertd">
                33.
            </td>
            <td>
                Bug
            </td>
            <td>
                Improper style when declaring parameter names
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1780")%>">TTT-1780</a>
            </td>
            <td>
                Naming convention
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 34 -->
        <tr>
            <td class="centertd">
                34.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Find common code and update the code to point to common function
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1709")%>">TTT-1709</a>
            </td>
            <td>
                Improvement
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                35.
            </td>
            <td>
                Task
            </td>
            <td>
                New Entities
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-13")%>">CM-13</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                36.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Dynamic Tabs - tab control - Tab Order
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1790")%>">TTT-1790</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                37.
            </td>
            <td>
                Task
            </td>
            <td>
                Move table timezone to Location DB from configuration db
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1793")%>">TTT-1793</a>
            </td>
            <td>
                Generic
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                38.
            </td>
            <td>
                Research
            </td>
            <td>
                T4 Transformation Research and Implmentation
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-16")%>">CM-16</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                39.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Price History View Formatting
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-15")%>">CM-15</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                Price History
            </td>
        </tr>
        <tr>
            <td class="centertd">
                40.
            </td>
            <td>
                Task
            </td>
            <td>
                Check box to retrieve only latest task run for given security for given date
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-14")%>">CM-14</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                Price History
            </td>
        </tr>
        <tr>
            <td class="centertd">
                41.
            </td>
            <td>
                Task
            </td>
            <td>
                T4 Transformation: Create template to generate Controller Classes
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-17")%>">CM-17</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="centertd">
                42.
            </td>
            <td>
                Task
            </td>
            <td>
                T4 Transformation: Create template to generate Stored Procedures
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-18")%>">CM-18</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
            </td>
        </tr>
    </table>
    <b>2013-APRIL-21</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement Search Setting
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1680")%>">TTT-1680</a>
            </td>
            <td>
                Feature Folder
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                Removal of unwanted codes
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1740")%>">TTT-1740</a>
            </td>
            <td>
                Default page
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Exort Menu Call Removal
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1746")%>">TTT-1746</a>
            </td>
            <td>
                Default page
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New entity
            </td>
            <td>
                UseCaseXUseCaseStep
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1622")%>">TTT-1622</a>
            </td>
            <td>
                UseCase
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Task
            </td>
            <td>
                SteDefaultValue
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1763")%>">TTT-1763</a>
            </td>
            <td>
                Default page
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Notification Publisher X EventType
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1741")%>">TTT-1741</a>
            </td>
            <td>
                Event Notification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Task
            </td>
            <td>
                Removal of unwanted code
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1739")%>">TTT-1739</a>
            </td>
            <td>
                TCM entities
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Functionality Active Status
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1728")%>">TTT-1728</a>
            </td>
            <td>
                Quality Assurance
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Task
            </td>
            <td>
                Implement Reset button
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1760")%>">TTT-1760</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented colour change on Menu Items
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1572")%>">TTT-1572</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Functionality X Functionality Active Status
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1732")%>">TTT-1732</a>
            </td>
            <td>
                Quality Assurance
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Update / Details Tab Panels
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1660")%>">TTT-1660</a>
            </td>
            <td>
                Tab Panels
            </td>
            <td>
                Application User
            </td>
        </tr>
        <!--Row 13-->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                UI IMprovement
            </td>
            <td>
                Add Reset button to FES Search control to restore to defaults
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1723")%>">TTT-1723</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14-->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                Move Setting Category to Base Class
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1736")%>">TTT-1736</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15-->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Code Order Verification
            </td>
            <td>
                Check the order of the code steps for proper saving
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1737")%>">TTT-1737</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16-->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Analysis
            </td>
            <td>
                Analyze to simplify HistoryList Setup method
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1761")%>">TTT-1761</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17-->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Analysis
            </td>
            <td>
                DoesExist Check with Insert / Update / Clone
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-854")%>">TTT-854</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18-->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Test and Analyze
            </td>
            <td>
                Database - Stored Procedure - IsDeleteable Check
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-807")%>">TTT-807</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 19-->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Implement new feature
            </td>
            <td>
                Implement Search Settings feature for Menu
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1734")%>">TTT-1734</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 20-->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                WildcardSearch feature enhancement
            </td>
            <td>
                WildcardSearch made entitywise user setting rather than Application level setting
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1588")%>">TTT-1588</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 21-->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                FES UI Dropdownlist population
            </td>
            <td>
                FES Lists should be based on Application Data
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1766")%>">TTT-1766</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 22-->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                Refactor code and move to BasePage
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1568")%>">TTT-1568</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Task
            </td>
            <td>
                New Entities
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-13")%>">CM-13</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-APRIL-14</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Bug
            </td>
            <td>
                TSXTC Archive missing ids
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1650")%>">TTT-1650</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Test Case - Add FKEdtior instead of current text box
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1664")%>">TTT-1664</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Notification - Add Application Combo box
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1648")%>">TTT-1648</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Notification Publisher
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1672")%>">TTT-1672</a>
            </td>
            <td>
                Event Notification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Task Note - Add FKEdtior instead of current text box
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1656")%>">TTT-1656</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Task should have TaskXDeliverableArtifact in tab control
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1448")%>">TTT-1448</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Task
            </td>
            <td>
                Update Search setting Task status in FES
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting in WBS folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1666")%>">TTT-1666</a>
            </td>
            <td>
                Search setting
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting in TaskAndWorkFlow folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1679")%>">TTT-1679</a>
            </td>
            <td>
                Search setting
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting in TCM folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1680")%>">TTT-1680</a>
            </td>
            <td>
                Search setting
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting in Question and Report folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1682")%>">TTT-1682</a>
            </td>
            <td>
                Search setting
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting in Feature folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1689")%>">TTT-1689</a>
            </td>
            <td>
                Search setting
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13-->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting in Aptitude and RiskReward folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Search setting
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14-->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Invalid Client tab fixed.
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1662")%>">TTT-1662</a>
            </td>
            <td>
                Invalid Client tab fixed.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15-->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Bug
            </td>
            <td>
                Application Role
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1647")%>">TTT-1647</a>
            </td>
            <td>
                Section labelling
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16-->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1654")%>">TTT-1654</a>
            </td>
            <td>
                Section labelling
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17-->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Bug
            </td>
            <td>
                Application User
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1657")%>">TTT-1657</a>
            </td>
            <td>
                Grid Links fixed.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18-->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Bug
            </td>
            <td>
                Help Page
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1661")%>">TTT-1661</a>
            </td>
            <td>
                Grid Links fixed.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 19-->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Bug
            </td>
            <td>
                Application Mode
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1673")%>">TTT-1673</a>
            </td>
            <td>
                Audit History fixed.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 20-->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Bug
            </td>
            <td>
                MileStoneXFeature
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1669")%>">TTT-1669</a>
            </td>
            <td>
                Audit History fixed.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 21-->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Bug
            </td>
            <td>
                Application Role
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1651")%>">TTT-1651</a>
            </td>
            <td>
                Search Logic fixed.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 22-->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Bug
            </td>
            <td>
                Application Role
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1649 ")%>">TTT-1649</a>
            </td>
            <td>
                Search Control Settings.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 23-->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Bug
            </td>
            <td>
                Application Role
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1653")%>">TTT-1653</a>
            </td>
            <td>
                Application field disabled.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 24-->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Bug
            </td>
            <td>
                MilestoneXFeature
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1671")%>">TTT-1671</a>
            </td>
            <td>
                Few fields disabled.
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 25-->
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Field Configuration
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1683")%>">TTT-1683</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 26-->
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Field Configuration Mode
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1700")%>">TTT-1700</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 27-->
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Field Configuration Mode Category
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1695")%>">TTT-1695</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 28-->
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                New Entity
            </td>
            <td>
                ApplicationModeXFieldConfigurationMode
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1718")%>">TTT-1718</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 29-->
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                New Entity
            </td>
            <td>
                FCModeCategoryXFCMode
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1713")%>">TTT-1713</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 30-->
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                Modify Entity
            </td>
            <td>
                AEFLModeCategoryXApplicationModeXAEFLMode - Remove AEFLModeCategory from this entity
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1675")%>">TTT-1675</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 31-->
        <tr>
            <td class="centertd">
                31.
            </td>
            <td>
                New Feature
            </td>
            <td>
                FES, add columns TragetDate, StartDate
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1423")%>">TTT-1423</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 32-->
        <tr>
            <td class="centertd">
                32.
            </td>
            <td>
                Enhance Common Update UI
            </td>
            <td>
                Add Priority and Status combo boxes to FES Common Update
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1594")%>">TTT-1594</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 33-->
        <tr>
            <td class="centertd">
                33.
            </td>
            <td>
                Logic Bug
            </td>
            <td>
                Logic Bug - Search Parameters (old codes)
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1693")%>">TTT-1693</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 34-->
        <tr>
            <td class="centertd">
                34.
            </td>
            <td>
                UI Enhancement
            </td>
            <td>
                FES - Add Application Combo Box
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1677")%>">TTT-1677</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 35-->
        <tr>
            <td class="centertd">
                35.
            </td>
            <td>
                Code Bug
            </td>
            <td>
                Code being commented out when it seems to be required
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1706")%>">TTT-1706</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 36-->
        <tr>
            <td class="centertd">
                36.
            </td>
            <td>
                Bug
            </td>
            <td>
                Control level User Preference, not session level setting
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1707")%>">TTT-1707</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 37-->
        <tr>
            <td class="centertd">
                37.
            </td>
            <td>
                Analyze and find solution
            </td>
            <td>
                Analyze how to in application mode : testing have audit history enabled
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1678")%>">TTT-1678</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 38-->
        <tr>
            <td class="centertd">
                38.
            </td>
            <td>
                List Refactor
            </td>
            <td>
                List code refactoring
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1724")%>">TTT-1724</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 39-->
        <tr>
            <td class="centertd">
                39.
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                Refactor code and move to BaseClass/BasePage
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1725")%>">TTT-1725</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 40-->
        <tr>
            <td class="centertd">
                40.
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                Refactor code and move to BasePage
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1726")%>">TTT-1726</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                41.
            </td>
            <td>
                Task
            </td>
            <td>
                UP - Looks like same code
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1735")%>">TTT-1735</a>
            </td>
            <td>
                Code Refactor
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                42.
            </td>
            <td>
                Research
            </td>
            <td>
                Research & Review Charting
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-7")%>">CM-7</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                43.
            </td>
            <td>
                New Feature
            </td>
            <td>
                MVC - Need Page size text box
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-11")%>">CM-11</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                44.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Dynamic tabs
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1733")%>">TTT-1733</a>
            </td>
            <td>
                Grouped List
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                45.
            </td>
            <td>
                Improvment
            </td>
            <td>
                Fix routing across application / SetImagePaths
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1747")%>">TTT-1747</a>
            </td>
            <td>
                ASP.NET Routing
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-APRIL-07</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting Task on UseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Search setting Per user
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Task
            </td>
            <td>
                New Entity
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                UseCaseWorkFlowCategory
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Task
            </td>
            <td>
                Add New column to ProjectXUseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New column
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement Search setting Task on RequirementAnalysis folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Search setting Per user
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu : Audit History not showing
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1565")%>">TTT-1565</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Menu Category
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1572")%>">TTT-1572</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                New Entity
            </td>
            <td>
                MenuCategory X Menu
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1572")%>">TTT-1572</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Bug
            </td>
            <td>
                Missing PK, FK - TCM entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1605")%>">TTT-1605</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Task
            </td>
            <td>
                TestCaseOwner : Conversion to Standard Pattern
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1582")%>">TTT-1582</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                New Entity
            </td>
            <td>
                TaskPackage X Owner X Task
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1445")%>">TTT-1445</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                New Entity
            </td>
            <td>
                MilestoneXFeatureArchive
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1535")%>">TTT-1535</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                New Feature
            </td>
            <td>
                MilestoneXFeature
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1525")%>">TTT-1525</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Bug
            </td>
            <td>
                CompetencyXSkill
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1581")%>">TTT-1581</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14 -->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Bug
            </td>
            <td>
                MilestonexFeature
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1643")%>">TTT-1643</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Application Mode
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1627")%>">TTT-1627</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Bug
            </td>
            <td>
                Client
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1644")%>">TTT-1644</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Bug
            </td>
            <td>
                ApplicationEntityFieldLabelModeCategory
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1645")%>">TTT-1645</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                New Entity
            </td>
            <td>
                AEFLModeCategoryXApplicationModeXAEFLMode
            </td>
            <td class="centertd">
                NG2
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1632")%>">TTT-1632</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 19 -->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Add Application Mode combo box to Admin Tasks page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1226")%>">TTT-1226</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 20 -->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Review and Analysis
            </td>
            <td>
                User Login Report SP
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1569")%>">TTT-1569</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 21 -->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                Audit History visible by default in Delete page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1590")%>">TTT-1590</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 22 -->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                New UI Feature
            </td>
            <td>
                Group By Dropdownlist in Search Control of FES
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1593")%>">TTT-1593</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 23 -->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Wild Card Search option for all entities
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1588")%>">TTT-1588</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 24 -->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Missing Feature
            </td>
            <td>
                Application Role - Missing All Tab
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1589")%>">TTT-1589</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 25 -->
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Paging Count Issue
            </td>
            <td>
                FES - Paging count not displayed properly
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1608")%>">TTT-1608</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 26 -->
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                UI Feature
            </td>
            <td>
                Horizontal Alignment not shown in Grid
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1547")%>">TTT-1547</a>
            </td>
            <td>
                Bug
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 27 -->
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                New Feature
            </td>
            <td>
                AEFLModeCategory used for populating AEFLMode
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1637")%>">TTT-1637</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Log4Net - AEFL options
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1663")%>">TTT-1663</a>
            </td>
            <td>
                Export Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                Task
            </td>
            <td>
                User Level Preference to Application Level Preference
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1681")%>">TTT-1681</a>
            </td>
            <td>
                User Preference
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                Task
            </td>
            <td>
                Add SecuryIndex Drop Down on PriceHistory Form
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-1")%>">CM-1</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                31.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Add New Entity Pricing Source
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-4")%>">CM-4</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                32.
            </td>
            <td>
                Task
            </td>
            <td>
                Add TaskRunId to PriceHistory Entity
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-5")%>">CM-5</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                33.
            </td>
            <td>
                Task
            </td>
            <td>
                Link up TaskWorkFlow to PriceHistory Form
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-6")%>">CM-6</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                34.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Process indicator to user of what is happening
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-3")%>">CM-3</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                35.
            </td>
            <td>
                Task
            </td>
            <td>
                Filter condition is lost when getting next page
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-2")%>">CM-2</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                36.
            </td>
            <td>
                Task
            </td>
            <td>
                New Entities II
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-10")%>">CM-10</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                37.
            </td>
            <td>
                Task
            </td>
            <td>
                New Entities
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-8")%>">CM-8</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                38.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Create Partial View to handle paging in MVC
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-12")%>">CM-12</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                39.
            </td>
            <td>
                New Feature
            </td>
            <td>
                MVC - Need Page size text box.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("CM-11")%>">CM-11</a>
            </td>
            <td>
                Capital Markets
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-MARCH-31</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Use Case Work Flow Category
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1576")%>">TTT-1576</a>
            </td>
            <td>
                UseCase
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Notification Registrar
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1565")%>">TTT-1565</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Notification Event Type
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1558")%>">TTT-1558</a>
            </td>
            <td>
                EventNotification
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Dynamic driven Menu items
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1574")%>">TTT-1574</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                New Feature
            </td>
            <td>
                ASP.NET Routing : TCM entities
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1571")%>">TTT-1571</a>
            </td>
            <td>
                ASP.NET Routing
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Included ALL TAB in Menu
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1586")%>">TTT-1586</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Bug
            </td>
            <td>
                Possitive Id generation in TestCasePriority entity
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1585")%>">TTT-1585</a>
            </td>
            <td>
                Id Generation
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Saving Search criteria per User
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1548")%>">TTT-1548</a>
            </td>
            <td>
                Save Search attributes per User
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Task
            </td>
            <td>
                Moving code from Entity's Default pages to Base Page in WebFramework
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1568")%>">TTT-1568</a>
            </td>
            <td>
                Code Refactoring
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                UI Feature
            </td>
            <td>
                Set Panel view to Default in AEFL settings page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1567")%>">TTT-1567</a>
            </td>
            <td>
                Set View to Panel by default
            </td>
            <td>
                AEFL settings page
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                New Feature
            </td>
            <td>
                User Login Report page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1569")%>">TTT-1569</a>
            </td>
            <td>
                User Login Report backend and
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Code Refactoring
            </td>
            <td>
                AEFL Code cleanup, - Parameterization
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1592")%>">TTT-1592</a>
            </td>
            <td>
                AEFL Code Cleanup
            </td>
            <td>
                AEFL Settings page
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Code Cleanup
            </td>
            <td>
                Replace raw strings with defined strings
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1591")%>">TTT-1591</a>
            </td>
            <td>
                Code Cleanup
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14 -->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Add Application User Image Option
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1519")%>">TTT-1519</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                ApplicationUserProfileImageMaster
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Task
            </td>
            <td>
                Menu Items should show MenuDisplayName Value as text
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1479")%>">TTT-1479</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Task
            </td>
            <td>
                Common Update UI Change - GR
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1491")%>">TTT-1491</a>
            </td>
            <td>
                Common Update
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Task
            </td>
            <td>
                Database Driven About Pages
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1298")%>">TTT-1298</a>
            </td>
            <td>
                Release Notes
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Financial Charting: Create new Entities
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("RSHPRO-16")%>">RSHPRO-16</a>
            </td>
            <td>
                Financial Charting
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 19 -->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Task
            </td>
            <td>
                TTT About Page
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
            </td>
            <td>
                About Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 20 -->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Task
            </td>
            <td>
                Data Entries in Functionality Table
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1559")%>">TTT-1559</a>
            </td>
            <td>
                About Page
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 21 -->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Task
            </td>
            <td>
                Review Logic in Activity Stream to see how we can leverage pagination log
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1518")%>">TTT-1518</a>
            </td>
            <td>
                Activity Stream
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 22 -->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Task
            </td>
            <td>
                History lost in Update
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1546")%>">TTT-1546</a>
            </td>
            <td>
                Audit History
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 23 -->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Task
            </td>
            <td>
                On the fily AEFL Mode Creator
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1296")%>">TTT-1296</a>
            </td>
            <td>
                AEFL Mode Creator
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 24 -->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Task
            </td>
            <td>
                Resolve CommonUpdate issue
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1541")%>">TTT-1541</a>
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1531")%>">TTT-1531</a>
            </td>
            <td>
                Aptitude
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 25 -->
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Task
            </td>
            <td>
                Update latest status in FES
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Aptitude
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 26 -->
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented CU/IU Task , Task Package , Task PriorityType
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                WBS
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 27 -->
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                Task
            </td>
            <td>
                Implemented CU/IU TasksAndWorkFlow
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-MARCH-24</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                CommonUpdate and InlineUpdate for WBS
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1544")%>">TTT-1544</a>
            </td>
            <td>
                IU/CU Features
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                CommonUpdate and InlineUpdate for TCM
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1488")%>">TTT-1488</a>
            </td>
            <td>
                IU/CU Features
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Entity
            </td>
            <td>
                TestCase Priority
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1512")%>">TTT-1512</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New Entity
            </td>
            <td>
                TestCase Status
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1513")%>">TTT-1513</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                New Entity
            </td>
            <td>
                TestSuiteXTestCase Archive
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1543")%>">TTT-1543</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Included TAB control
            </td>
            <td>
                TestSuiteXTestCase
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1551")%>">TTT-1551</a>
            </td>
            <td>
                Common Update
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                AEFL Settings bug
            </td>
            <td>
                Column appearing in Grid even after GridViewPriority is set to -1
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1409")%>">TTT-1409</a>
            </td>
            <td>
                AEFL Settings
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Implementation - IU/CU Features
            </td>
            <td>
                Delegate tasks and implement IU/CU features in various entities
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1462")%>">TTT-1462</a>
            </td>
            <td>
                IU/CU Features
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Create Panel View of AEFL Settings
            </td>
            <td>
                Create Panel Control for AEFL to be used when adjusting values. Toggle Panel/Grid.
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1521")%>">TTT-1521</a>
            </td>
            <td>
                AEFL Settings
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Implement Common Update
            </td>
            <td>
                Implement Common Update for FES
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1529")%>">TTT-1529</a>
            </td>
            <td>
                Common Update
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Bug
            </td>
            <td>
                Duplicate columns appearing in Grid
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1545")%>">TTT-1545</a>
            </td>
            <td>
                Grid columns
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Bug
            </td>
            <td>
                History lost in Update page of FES
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1546")%>">TTT-1546</a>
            </td>
            <td>
                FES Update
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 14 -->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Bug
            </td>
            <td>
                FES Multiple Update option not working
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1549")%>">TTT-1549</a>
            </td>
            <td>
                FES Multiple Update
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Task
            </td>
            <td>
                Group Menu to "Owner'
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1482")%>">TTT-1482</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Task
            </td>
            <td>
                Data Entries in Entity Owner, Functionality Owner and Menu (Primary Developer)
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <%--<a href="<%= GetLink("TTT-1482")%>">TTT-1549</a>--%>
            </td>
            <td>
                Static Data
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Winforms application that allows one to edit menu structure
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("RSHPRO-14")%>">RSHPRO-14</a>
            </td>
            <td>
                Menu Manager
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Bug
            </td>
            <td>
                Functionality Entity Status - Update page not rendering
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1496")%>">TTT-1496</a>
            </td>
            <td>
                FES
            </td>
            <td>
                Functioanlity Entity Status
            </td>
        </tr>
        <!--Row 19 -->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Functionality Entity Status - date controls on this page more usable
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1495")%>">TTT-1495</a>
            </td>
            <td>
                Date Controls
            </td>
            <td>
                Functioanlity Entity Status
            </td>
        </tr>
        <!--Row 20 -->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Large Table Pagiation
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1389")%>">TTT-1389</a>
            </td>
            <td>
                Paging
            </td>
            <td>
                Log4Net
            </td>
        </tr>
        <!--Row 21 -->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Task
            </td>
            <td>
                Add Seach By Primary Developer on Menu List
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1516")%>">TTT-1516</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 22 -->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Create application creates the directory structure of your Developmet Directory
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("RSHPRO-13")%>">RSHPRO-13</a>
            </td>
            <td>
                Directure Structure
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 23 -->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Bug
            </td>
            <td>
                Menu primary developer was set to NULL? When updating multiple records
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1522")%>">TTT-1522</a>
            </td>
            <td>
                Validation
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 24 -->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Research / Protope passing unique client key from UI to stored procedure layer
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("RSHPRO-12")%>">RSHPRO-12</a>
            </td>
            <td>
                Trace
            </td>
            <td>
                Client
            </td>
        </tr>
        <!--Row 25 -->
        <tr>
            <td class="centertd">
                25.
            </td>
            <td>
                Task
            </td>
            <td>
                CRUD new Menu Items
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("RSHPRO-15")%>">RSHPRO-15</a>
            </td>
            <td>
                Menu Manager
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 26 -->
        <tr>
            <td class="centertd">
                26.
            </td>
            <td>
                Bug
            </td>
            <td>
                Log4Net page fails to load
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1550")%>">TTT-1550</a>
            </td>
            <td>
                Paging
            </td>
            <td>
                Log4Net
            </td>
        </tr>
        <!--Row 27 -->
        <tr>
            <td class="centertd">
                27.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Add Application User Image Option
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1550")%>">TTT-1519</a>
            </td>
            <td>
                Application User Image
            </td>
            <td>
                ApplicationUserProfileImage
            </td>
        </tr>
        <!--Row 28 -->
        <tr>
            <td class="centertd">
                28.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement CU and IU
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1550")%>">TTT-1519</a>
            </td>
            <td>
                Task and Task Package
            </td>
            <td>
                -
            </td>
        </tr>
        <!--Row 29 -->
        <tr>
            <td class="centertd">
                29.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement CU and IU
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1550")%>">TTT-1519</a>
            </td>
            <td>
                TaskPriority and TaskStatusType
            </td>
            <td>
                -
            </td>
        </tr>
        <!--Row 30 -->
        <tr>
            <td class="centertd">
                30.
            </td>
            <td>
                New Feature
            </td>
            <td>
                Implement CU and IU
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1550")%>">TTT-1519</a>
            </td>
            <td>
                TaskType
            </td>
            <td>
                -
            </td>
        </tr>
    </table>
    <b>2013-MARCH-17</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                Bug in AEFL Settings Feature
            </td>
            <td>
                ID appearing even after disabling.
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1409")%>">TTT-1409</a>
            </td>
            <td>
                AEFL Settings Enable/Disable
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Bug/Exception
            </td>
            <td>
                Bug in redirection from AEFL Settings page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1408")%>">TTT-1408</a>
            </td>
            <td>
                AEFL Settings page redirection
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Entity Update
            </td>
            <td>
                Functionality Entity Status
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1423")%>">TTT-1423</a>
            </td>
            <td>
                Additional columns
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Search Filter Update
            </td>
            <td>
                Functionality Entity Status
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1422")%>">TTT-1422</a>
            </td>
            <td>
                FES Search control update
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                AEFL UI Change
            </td>
            <td>
                AEFL Settings page
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1473")%>">TTT-1473</a>
            </td>
            <td>
                AEFFL Settings UI
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                New Features
            </td>
            <td>
                Inline Update/Common Update for all entities
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1462")%>">TTT-1462</a>
            </td>
            <td>
                IU/CU Features
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Test Run
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1431")%>">TTT-1431</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Test Suite
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1349")%>">TTT-1349</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Test Case
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1348")%>">TTT-1348</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Test Suite X Test Case
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1439")%>">TTT-1439</a>
            </td>
            <td>
                TCM
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Task Note
            </td>
            <td class="centertd">
                MM
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1311")%>">TTT-1311</a>
            </td>
            <td>
                Task
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                13
            </td>
            <td>
                New Feature
            </td>
            <td>
                Large Table Pagination
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1389")%>">TTT-1389</a>
            </td>
            <td>
                Pagination
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                14
            </td>
            <td>
                Bug in AEFL Settings Feature
            </td>
            <td>
                ID appearing even after disabling.
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1409")%>">TTT-1409</a>
            </td>
            <td>
                AEFL Settings Enable/Disable
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                15
            </td>
            <td>
                New Feature
            </td>
            <td>
                When in application mode live, the test data should not be returned
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1441")%>">TTT-1441</a>
            </td>
            <td>
                Live Mode
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                16
            </td>
            <td>
                New Feature
            </td>
            <td>
                Menu entity add DisplayName
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1410")%>">TTT-1410</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                17
            </td>
            <td>
                New Entities
            </td>
            <td>
                Module, Developer Role, Feature Owner Status, ModuleOwner, FuntionalityOwner, EntityOwner
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1447")%>">TTT-1447</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                18
            </td>
            <td>
                Bug
            </td>
            <td>
                Only 3 columns appearing instead of 4 in Grid view 3way Entity
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1472")%>">TTT-1472</a>
            </td>
            <td>
                List Control
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                19
            </td>
            <td>
                Task
            </td>
            <td>
                Style sheet change so in grid hyper link does NOT have underline
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1475")%>">TTT-1475</a>
            </td>
            <td>
                List Control
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                20
            </td>
            <td>
                Task
            </td>
            <td>
                Menu Entity Add new column PrimaryDeveloper
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1454")%>">TTT-1454</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                21
            </td>
            <td>
                Task
            </td>
            <td>
                Menu Search should search on DisplayName, Name, and Description
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1456")%>">TTT-1456</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                22
            </td>
            <td>
                Task
            </td>
            <td>
                Menu Items should show MenuDisplayName Value as text
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1479")%>">TTT-1479</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                23
            </td>
            <td>
                Task
            </td>
            <td>
                Menu Data Entry Screen
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1421")%>">TTT-1421</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                24
            </td>
            <td>
                Bug
            </td>
            <td>
                Auto EntityId Generation Issue
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1477")%>">TTT-1477</a>
            </td>
            <td>
                Auto Entity Key
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                25
            </td>
            <td>
                Bug
            </td>
            <td>
                Activity Stream: Audit Information not visible
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1471")%>">TTT-1471</a>
            </td>
            <td>
                Activity Stream
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                26
            </td>
            <td>
                New Functionality
            </td>
            <td>
                CommonUpdate and InlineUpdate : Aptitude folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1528")%>">TTT-1528</a>
            </td>
            <td>
                IU/CU Features
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                27
            </td>
            <td>
                New Functionality
            </td>
            <td>
                CommonUpdate and InlineUpdate : Aptitude folder
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1528")%>">TTT-1528</a>
            </td>
            <td>
                IU/CU Features
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                28
            </td>
            <td>
                Task
            </td>
            <td>
                Data Entry : Menu
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Menu
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                29
            </td>
            <td>
                Task
            </td>
            <td>
                Data Entry : HR
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                HR
            </td>
            <td>
                ...
            </td>
        </tr>
        <tr>
            <td class="centertd">
                28
            </td>
            <td>
                Task
            </td>
            <td>
                Updated Status in FES
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                FES
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <!--end-->
    <b>2013-MARCH-10</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Use Case Step
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1432")%>">TTT-1432</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Use Case Package
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1417")%>">TTT-1417</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Project X UseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1464")%>">TTT-1464</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New feature
            </td>
            <td>
                Common Update and Inline Update
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1464")%>">TTT-1464</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-MARCH-03</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Use Case
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1367")%>">TTT-1367</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Use Case Actor
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1380")%>">TTT-1380</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Use Case Relationship
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1393")%>">TTT-1393</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                New Entity
            </td>
            <td>
                Use Case Actor X UseCase
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1402")%>">TTT-1402</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                New feature
            </td>
            <td>
                Renumber functionality
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1464")%>">TTT-1464</a>
            </td>
            <td>
                Use Case
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-FEBRAURY-24</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-FEBRAURY-17</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-FEBRAURY-10</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-FEBRAURY-03</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-JANUARY-27</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-JANUARY-20</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-JANUARY-13</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2013-JANUARY-06</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2012-DECEMBER-30</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                ..
            </td>
            <td>
                ...
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                ...
            </td>
            <td>
                ...
            </td>
        </tr>
    </table>
    <b>2012-DECEMBER-23</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                Feature Group
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1029")%>">TTT-1029</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                Feature
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                Question Category
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1041")%>">TTT-1041</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                Question
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                Feature Rule Category
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1041")%>">TTT-1041</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                Feature
            </td>
        </tr>
    </table>
    <b>2012-DECEMBER-16</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                Log4Net
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                New Entity
            </td>
            <td>
                Log4Net
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Improvement
            </td>
            <td>
                eSettingsList control to allow editing all the rows
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                eSettingsList
            </td>
            <td>
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Generic Settings page and worked on eSettingsControl integration
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Generic Settings
            </td>
            <td>
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Dynamically set HorizontalAlignment from AEFL table
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                HorizontalAlignment
            </td>
            <td>
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Improvement
            </td>
            <td>
                GridLine text boxes and UI beautification in Settings control
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                GridLine
            </td>
            <td>
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Insert pages should not have history section showing
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Histroy section
            </td>
            <td>
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Defect
            </td>
            <td>
                View state properties in eSettings control and worked on proper redirect
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                eSettings control
            </td>
            <td>
            </td>
        </tr>
    </table>
    <b>2012-DECEMBER-09</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                Super Key Concept
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Super Key
            </td>
            <td>
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Change method to take AuditID vs current code
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-979")%>">TTT-979</a>
            </td>
            <td>
                AuditID and current code
            </td>
            <td>
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Constant strings need to be constant some common place
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-980")%>">TTT-980</a>
            </td>
            <td>
                Constant strings
            </td>
            <td>
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Improvement
            </td>
            <td>
                PopulateLabelsText - Refactor / Move common text
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-981")%>">TTT-981</a>
            </td>
            <td>
                PopulateLabelsText
            </td>
            <td>
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Move logic to existing operation such that calling code does not change
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-972")%>">TTT-972</a>
            </td>
            <td>
                Encapsulate Logic
            </td>
            <td>
                Feature
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Customized eList control and Settings page for inline editing functionality
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                eList control
            </td>
            <td>
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Modify Export control to redirect to the Settings page and corrected styling the
                editable grid
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Export control
            </td>
            <td>
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Created template columns and add it to the Grid and handles DataBinding
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                DataBinding
            </td>
            <td>
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Improvement
            </td>
            <td>
                eSettingsList control to edit the settings per entity
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                eSettingsList
            </td>
            <td>
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Single Table using Parent cloumn
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-000")%>">TTT-000</a>
            </td>
            <td>
                Database- Schema
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <b>2012-DECEMBER-02</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                LabelDriven
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-946")%>">TTT-946</a>
            </td>
            <td>
                Label Driven
            </td>
            <td>
                <!--NewFeature-->
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Cache Menu of Data cache and Control cache
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1000")%>">TTT-1000</a>
            </td>
            <td>
                Data cache and Control cache
            </td>
            <td>
                <!--Implement-->
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Improvement
            </td>
            <td class="lefttd">
                Label Driven Task
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1000")%>">TTT-1000</a>
            </td>
            <td>
                Label Driven
            </td>
            <td>
                <!-- Implement-->
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Improvement
            </td>
            <td class="lefttd">
                Missing FK, PK and table scripts for all Menu tables
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1000")%>">TTT-1000</a>
            </td>
            <td>
                Menu Tables
            </td>
            <td>
                <!--Developement-->
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Improvement
            </td>
            <td class="lefttd">
                Missing PK and table creation script for AEPH
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1000")%>">TTT-1000</a>
            </td>
            <td>
                ApplicationEntityFieldLabel table
            </td>
            <td>
                <!--Implement-->
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Improvement
            </td>
            <td class="lefttd">
                Move Export control to List control
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1000")%>">TTT-1000</a>
            </td>
            <td>
                Export Control
            </td>
            <td>
                <!--Implement-->
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Improvement
            </td>
            <td class="lefttd">
                UI - Export Menu Options
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-966")%>">TTT-966</a>
            </td>
            <td>
                Export Menu
            </td>
            <td>
                <!--Implement-->
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Improvement
            </td>
            <td class="lefttd">
                Simplified labels db driven logic,implemented UIHelper class
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-966")%>">TTT-966</a>
            </td>
            <td>
                UI Helper class
            </td>
            <td>
                <!--Implement-->
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Menu caching logic with DataSet
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-966")%>">TTT-966</a>
            </td>
            <td>
                Menu caching logic
            </td>
            <td>
                <!--Implement-->
            </td>
        </tr>
        <!--Row 10 -->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Modify all entity GetColumns methods in ApplicationSecurity
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-966")%>">TTT-966</a>
            </td>
            <td>
                ApplicationSecurity
            </td>
            <td>
                <!--Developement-->
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Update default pages to point to GetColumn methods in ApplicationSecurity
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-966")%>">TTT-966</a>
            </td>
            <td>
                ApplicationSecurity
            </td>
            <td>
                <!--Developement-->
            </td>
        </tr>
    </table>
    <br />
    <b>2012-NOVEMBER-25</b>
    <br />
    <table  style="padding-top: 10px;">
        <tr style="background-color: #3A4F63; color: White;">
            <th width="5%">
                ID
            </th>
            <th width="10%">
                Issue Type
            </th>
            <th width="45%" style="width: 200px;">
                Description
            </th>
            <th width="8%">
                Developer
            </th>
            <th width="10%">
                JIRA #
            </th>
            <th width="12%">
                Feature
            </th>
            <th width="20%">
                Primary Entity
            </th>
        </tr>
        <!--Row 1 -->
        <tr>
            <td class="centertd">
                1.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                <!--Worked on TTT-953, Created data entry for TTT entities under project Corrdinator-->
                Data Entries for entities under project Coordinator.
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1005")%>">TTT-1005</a>
            </td>
            <td>
                Data Entry
            </td>
            <td>
                Project Coordinator
            </td>
        </tr>
        <!--Row 2 -->
        <tr>
            <td class="centertd">
                2.
            </td>
            <td>
                New Functionality
            </td>
            <td>
                Task: UI - Control - Muplitple Target Bucket
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1019")%>">TTT-1019</a>
            </td>
            <td>
                Control - Muplitple Target Bucket
            </td>
            <td>
                <!--Worked-->
            </td>
        </tr>
        <!--Row 3 -->
        <tr>
            <td class="centertd">
                3.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Use data columns in TTT.Event Monitoring /And Import
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1000")%>">TTT-1000</a>
            </td>
            <td>
                TTT Event Monitoring entities
            </td>
            <td>
                TTT Entities
                <!--Data Columns usage-->
            </td>
        </tr>
        <!--Row 4 -->
        <tr>
            <td class="centertd">
                4.
            </td>
            <td>
                Improvement
            </td>
            <td>
                <!--Worked on using data columns in TTT.Import /and LogandTrace-->
                Use data columns in TTT.Import /and LogandTrace
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1001")%>">TTT-1001</a>
            </td>
            <td>
                TTT Import entities
            </td>
            <td>
                TTT Entities
                <!--Data Columns usage-->
            </td>
        </tr>
        <!--Row 5 -->
        <tr>
            <td class="centertd">
                5.
            </td>
            <td>
                Improvement
            </td>
            <td>
                <!--Worked on using data columns in TTT.Release Log,/ Tasks and work flow-->
                Use data columns in TTT.Release Log,/ Tasks and work flow
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1003")%>">TTT-1003</a>
            </td>
            <td>
                TTT ReleaseLog entities
            </td>
            <td>
                TTT Entities
                <!--Data Columns usage-->
            </td>
        </tr>
        <!--Row 6 -->
        <tr>
            <td class="centertd">
                6.
            </td>
            <td>
                Improvement
            </td>
            <td>
                <!--Worked on TTT-939,942,626,941,937,938,940,951,944-->
                Implemented Code changes for code Improvement
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1004")%>">TTT-1004</a>
            </td>
            <td>
                Miscellneous
            </td>
            <td>
                TTT Entities
            </td>
        </tr>
        <!--Row 7 -->
        <tr>
            <td class="centertd">
                7.
            </td>
            <td>
                Improvement
            </td>
            <td>
                ApplicationEntityFieldLabel - Label databasedriven task and schema
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1006")%>">TTT-1006</a>
            </td>
            <td>
                Label Driven
            </td>
            <td>
                ApplicationEntityFieldLabel
            </td>
        </tr>
        <!--Row 8 -->
        <tr>
            <td class="centertd">
                8.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implement Labels DB driven task using ApplicationEntityFieldLabel
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1007")%>">TTT-1007</a>
            </td>
            <td>
                Label Driven
            </td>
            <td>
                ApplicationEntityFieldLabel
            </td>
        </tr>
        <!--Row 9 -->
        <tr>
            <td class="centertd">
                9.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Trial work on changing master page references
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1012")%>">TTT-1012</a>
            </td>
            <td>
                Trial
            </td>
            <td>
                Master Page
            </td>
        </tr>
        <!--Row 10-->
        <tr>
            <td class="centertd">
                10.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implement changes to ApplicationEntityFieldLabel code as per feedback
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1013")%>">TTT-1013</a>
            </td>
            <td>
                ApplicationEntityFieldLabel
            </td>
            <td>
                ApplicationEntityFieldLabel
            </td>
        </tr>
        <!--Row 11 -->
        <tr>
            <td class="centertd">
                11.
            </td>
            <td>
                Improvement
            </td>
            <td>
                DataEntry work for entites in ApplicationEntityFieldLabel
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1013")%>">TTT-1013</a>
            </td>
            <td>
                DataEntry
            </td>
            <td>
                ApplicationEntityFieldLabel
                <!--Implement-->
            </td>
        </tr>
        <!--Row 12 -->
        <tr>
            <td class="centertd">
                12.
            </td>
            <td>
                Improvement
            </td>
            <td>
                DB driven menu correction to match the actual menu
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1013")%>">TTT-1013</a>
            </td>
            <td>
                DB Driven
            </td>
            <td>
                Menu
            </td>
        </tr>
        <!--Row 13 -->
        <tr>
            <td class="centertd">
                13.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Implement ApplicationEntityFieldLabel schema changes
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1014")%>">TTT-1014</a>
            </td>
            <td>
            </td>
            <td>
                ApplicationEntityFieldLabel
            </td>
        </tr>
        <!--Row 14 -->
        <tr>
            <td class="centertd">
                14.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Test/Remove redundant namespace references from all Framework.Component classes
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1018")%>">TTT-1018</a>
            </td>
            <td>
                Framework.Component
            </td>
            <td>
            </td>
        </tr>
        <!--Row 15 -->
        <tr>
            <td class="centertd">
                15.
            </td>
            <td>
                Improvement
            </td>
            <td>
                Task: UI - SuperKey - Show / Confrom records to be deleted
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1018")%>">TTT-1018</a>
            </td>
            <td>
                Test
            </td>
            <td>
                SuperKey - Show
            </td>
        </tr>
        <!--Row 16 -->
        <tr>
            <td class="centertd">
                16.
            </td>
            <td>
                Defect
            </td>
            <td>
                Analyze solution for editable Grid implementation
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1008")%>">TTT-1008</a>
            </td>
            <td>
                Analyze
            </td>
            <td>
                Grid implementation
            </td>
        </tr>
        <!--Row 17 -->
        <tr>
            <td class="centertd">
                17.
            </td>
            <td>
                Defect
            </td>
            <td>
                Resolve issue in ApplicationEntityFieldLabel implementation
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1009")%>">TTT-1009</a>
            </td>
            <td>
                Resolve
            </td>
            <td>
                ApplicationEntityFieldLabel implementation
            </td>
        </tr>
        <!--Row 18 -->
        <tr>
            <td class="centertd">
                18.
            </td>
            <td>
                Defect
            </td>
            <td>
                <!--Worked on issue TTT-931 and added new settings button to the menu panel-->
                Implementation of editabe Grid and New Settings buttong in to Menu Panel Included.
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1010")%>">TTT-1010</a>
            </td>
            <td>
                Resolve
            </td>
            <td>
                Settings button
            </td>
        </tr>
        <!--Row 19 -->
        <tr>
            <td class="centertd">
                19.
            </td>
            <td>
                Defect
            </td>
            <td>
                Work on building new Master page with DB driven menu
            </td>
            <td class="centertd">
                AN
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1011")%>">TTT-1011</a>
            </td>
            <td>
                Resolve
            </td>
            <td>
                DB Driven menu
            </td>
        </tr>
        <!--Row 20 -->
        <tr>
            <td class="centertd">
                20.
            </td>
            <td>
                Defect
            </td>
            <td>
                Implement Export menu changes
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1017")%>">TTT-1017</a>
            </td>
            <td>
                Implement
            </td>
            <td>
                Export Menu
            </td>
        </tr>
        <!--Row 21 -->
        <tr>
            <td class="centertd">
                21.
            </td>
            <td>
                Defect
            </td>
            <td>
                Task: SetupConfiguration table should have entry for each framework entity Application
                wise
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1020")%>">TTT-1020</a>
            </td>
            <td>
                Task
            </td>
            <td>
                SetupConfiguration table
            </td>
        </tr>
        <!--Row 22 -->
        <tr>
            <td class="centertd">
                22.
            </td>
            <td>
                Defect
            </td>
            <td>
                Task: UI - Cross reference UI
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1022")%>">TTT-1022</a>
            </td>
            <td>
                Updated
            </td>
            <td>
                Cross reference UI
            </td>
        </tr>
        <!--Row 23 -->
        <tr>
            <td class="centertd">
                23.
            </td>
            <td>
                Defect
            </td>
            <td>
                Application User -> Clone page not working, ApplicationUser Default Page filter
                not working
            </td>
            <td class="centertd">
                GR
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1023")%>">TTT-1023</a>
            </td>
            <td>
                Test
            </td>
            <td>
                ApplicationUser Default Page
            </td>
        </tr>
        <!--Row 24 -->
        <tr>
            <td class="centertd">
                24.
            </td>
            <td>
                Defect
            </td>
            <td>
                Created Data entry for :User and Help menu items / Checked all Applications for
                columns no./ Test application in SQL
            </td>
            <td class="centertd">
                NG
            </td>
            <td class="centertd">
                <a href="<%= GetLink("TTT-1024")%>">TTT-1024</a>
            </td>
            <td>
                Test
            </td>
            <td>
                Framework.Component classes
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                Release notes for 2012-November-18
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Worked on Helper files TTT.Business Layer</li>
                    <li>Worked on Helper files TTT.Business Layer. Module.Competency</li>
                    <li>Worked on using data columns in TTT. Module Priority, TTT Module Risk Reward</li>
                    <li>Worked on using data columns in TTT. Module.Time Tracking, Framework.Application
                        User</li>
                    <li>Worked on using data columns in Framework.Application User, Framework.Core. Resolved
                        errors</li>
                    <li>Implement DB driven menu in TTT</li>
                    <li>Review ApplicationEntityFieldLabel code</li>
                    <li>Resolve SetupConfiguration error</li>
                    <li>Worked on entering original menu data into all Menu tables</li>
                    <li>Created table and BO layer code for fouth level ChildSubMenu/UI Integration</li>
                    <li>Analyzed and worked on retrieving Label names from DB for ApplicationOperation GridView
                        in Default.aspx page</li>
                    <li>Verified data, details page to analyze solution to make the label names Database
                        driven</li>
                    <li>Verified SetUpConfiguration table to delete redundant duplicated and created new
                        table</li>
                    <li>Task: Generalize code for fetching and setting of UserPreference</li>
                    <li>Task: Logic - Logging - Logger Column</li>
                    <li>Data Access - Logic Duplicate, Data Access - ConfigurationList, TTT-900: needs to
                        use ApplicationID</li>
                    <li>Task: Logic - GetUserPreferenceByKey Casting</li>
                    <li>Task: Create Windows Application that can 'refresh' results from data store periodically</li>
                    <li>Task: UI - SuperKey - Show / Confrim records to be deleted</li>
                    <li>Task: UI - Cross reference UI</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-November-11
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Reviewed Gridview code in TTT</li>
                    <li>Created tables for rendering menu dynamically in TTT</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-November-04
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Worked on resolving build errors from rebase and incorrect SCM mapping</li>
                    <li>Worked on investigating and Resolving errors in TTT application</li>
                    <li>Analyze DB driven menu tables</li>
                    <li>Worked on finding a solution for UQ Name constraint handling on UI side for ActivityAlgorithm</li>
                    <li>Moved constants to new location from BaseClass to SetUpconfiguration and made relevant
                        changes</li>
                    <li>Implemented betterment solution for ToSQLParameter methods for entities in TTT Components
                        Business Layer</li>
                    <li>Task: Concept - Caching / Performance</li>
                    <li>Task: User Prefrences Default behaviour</li>
                    <li>Task: Limiting Records Returned via stored procedures</li>
                    <li>Task: UI - Need, Feature Mapping</li>
                    <li>Task: BL Layer and DB Stored Procedures for ClientXProject Entity</li>
                    <li>Task: Data Access - Return Scalar output values</li>
                    <li>Task: Improve look and feel for Needs Feature Mapping View</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-October-28
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Implemented and executed unique constraints for tables in TasksAndWorkflow</li>
                    <li>Code review SuperKey and IsDeletable</li>
                    <li>Task: UI-Tab Control</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-October-21
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Task: Session variables</li>
                    <li>Task: Convert from "ReleaseLogDetails" to "ReleaseLogDetail" in Database Source
                        Files</li>
                    <li>Task: UI-Tab Control</li>
                    <li>Task: Database - Stored Procedure - IsDeleteable Check</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-October-14
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Tested Application</li>
                    <li>Tested Application and created JIRA Completed to main heads 1. System admin , 2.
                        Application coordinator, 3. Project coordinator</li>
                    <li>Tested TTT Application and created JIRA for Users head and Skill head</li>
                    <li>Test Project coordinator head</li>
                    <li>Test Users and Skill head and Created JIRA</li>
                    <li>Correcting spacing</li>
                    <li>Work on Group By Control as per feedback</li>
                    <li>Test and make changes to Application and check in code</li>
                    <li>Resolved broken link errors for various entities</li>
                    <li>Worked on implementing GroupByDay LINQ method by extracting the Day from WorkDate</li>
                    <li>Implemented/Tested DetailsView paging control to display Group By Day results</li>
                    <li>Implement Group By Month/Group By Year functionalities</li>
                    <li>Implement Group By Control Integration</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-October-07
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Task: Facilitate CURD buttons from Details page at individual 'item' level</li>
                    <li>Task: Facilitate Superkey Concept</li>
                    <li>Task: When showing multiple update items, there should be border similar to details</li>
                    <li>Task: Use Delete.aspx Page for any Delete Operation</li>
                    <li>Task: ApplicationUser and ApplicationUserTitle SP's at 2 locations, SetupConfiguration.ApplicationId</li>
                    <li>Implemented VerifyApplicationIdValue script that parses all tables</li>
                    <li>Resolved issues 409 and updated SystemEntityTypeGetNextSequence and updated all
                        associated scripts</li>
                    <li>Task: ApplicationOperation: Search Initial Values Selected should be None -> All</li>
                    <li>Task: Column names should not be used in quotes</li>
                    <li>Task: Centralize Session Variables</li>
                    <li>Task: Release log show child table for update / details / delete screen (Read Only)</li>
                    <li>Task: Should be style sheet driven vs c#</li>
                    <li>Task: Logic For Automatic Genaratation of PK Ids using SystemDevNumbers Needs to
                        be updated and reviewed</li>
                    <li>Task: Align on Equal</li>
                    <li>Task: Displaying records info doesn't refresh properly when no records</li>
                    <li>Worked on Make Unique Application Id and Name - Activity</li>
                    <li>Worked on Make unique Application ID and Name Activity state , Activity Algorithm</li>
                    <li>Created JIRA item, Update confluence page, Resolve error of Activity</li>
                    <li>Fix broken link to new location</li>
                    <li>Verfied Description upto 500 in TTT Project</li>
                    <li>Tried to resolve make unique fro Application Id, Promoted changes</li>
                    <li>Worked on Fix broken link and resolved it issued</li>
                    <li>Work on IsNull usage in TTT</li>
                    <li>Work on GetMaxId</li>
                    <li>Set description size upto 500</li>
                    <li>Modify tables Need, Feature, ProjectXNeed tables UI/BO/DA layers</li>
                    <li>Test application on modified entities and resolved errors and exceptions</li>
                    <li>Update all scripts of modified tables and deployed them</li>
                    <li>Analyze and work on implementing GroupByControl UI</li>
                    <li>Remove session instance of ApplicationId and tested the App</li>
                    <li>Verify db tables and scripts to analyze impact of moving RenumberRun tables to seperare
                        db</li>
                    <li>Implement Group by person functionality in Schedule</li>
                    <li>Unable to access Dev server/ Bonus hours</li>
                    <li>Review and research on SQL GroupBy and LINQ oncepts</li>
                    <li>Implement LINQ mthod for GroupByPerson</li>
                    <li>Review test Renumber scripts</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-September-30
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Review Emails/Rebase code/Create JIRAs</li>
                    <li>Resolve issues TTT-592, 594, 595, 596, 607, 608, 609, 593</li>
                    <li>Implement script to facillitate view of Test and Audit records</li>
                    <li>Replace CASE statements with IF/Execute script and resolve errors</li>
                    <li>Implement BO layer code and UI code for Entiy Test Details view</li>
                    <li>Review current implementation of Export Menu and implemented Tooltip display</li>
                    <li>Add Refresh/Help buttons and worked on alignment of Buttons</li>
                    <li>Migrate tables from TaskTimeTracker to TaskAndWorkFlow along with data</li>
                    <li>Updated and execute scripts in TaskAndWorkFlow DB</li>
                    <li>Implement and create scripts for ClientXProject table</li>
                    <li>Issue: Missing ApplicationId</li>
                    <li>Task: Clean up broken audit history records</li>
                    <li>Issue: System Entity Type Update / Details Page not working</li>
                    <li>Issue: User Prefernce table does not seem right</li>
                    <li>Task: Records with missing history data, cannot click details link</li>
                    <li>Task: User Preference Search Form</li>
                    <li>Issue: Insert Page not rendering for Application Role</li>
                    <li>Task: When showing multiple update items, there should be border similar to details</li>
                    <li>Task: Add domain dropdownlist (Configuration.SetUpConfiguration)</li>
                    <li>Task: ApplicationOperation: Search Initial Values Selected should be None --> All</li>
                    <li>Task: Facilitate CURD buttons from Details page at individual 'item' level.</li>
                    <li>Task: Unit Test Release Log / Release Log Details</li>
                    <li>Task: Facilitate Superkey Concept.</li>
                    <li>Issues: Project Page update fails, Project update screen looks odd</li>
                    <li>Task: Use Delete.aspx Page for any Delete Operation</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-September-23
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Review Emails/Rebase code/Create JIRA</li>
                    <li>Resolve Build Errors and Exceptions</li>
                    <li>Implement Config driven ApplicationId value</li>
                    <li>Resolve errors with Config driven ApplicationIdand replaced all session instances</li>
                    <li>Create conf document for Shell-Real Function mapping</li>
                    <li>Verify and delete unused AuditHistory SPs</li>
                    <li>Implement/Execute/Test scripts for retrieving Test And Audit data</li>
                    <li>Reimplement GetTestAndAuditData script by making corrections</li>
                    <li>Implement/Create/Integrate BO and UI layer code for displaying TestData</li>
                    <li>Implement script within to retrieve TestAndAuditData from 33 entities</li>
                    <li>Implement Dropdownlist to filter records by Entity Type</li>
                    <li>Implement revised GetTestDataCount script to get Test and Audit records count</li>
                    <li>Unit Testing of Entities that comes under “System Admin” Menu</li>
                    <li>Solved an issue: Audit id missing</li>
                    <li>Solved 2 Issues: Review and drop procedures & Application Search</li>
                    <li>Task: Cross refernce should have AppliationID column</li>
                    <li>Task: Search stored procedure pattern looks old</li>
                    <li>History Module Error When changing grid mode</li>
                    <li>Unit Testing of Framework Entites</li>
                    <li>Task: Search Procedures: ApplicationId Should be passed and included in WHERE clause,
                        Unit Testing of Framework Entites</li>
                    <li>Task: Procedure to indentify missing history records</li>
                    <li>Deployed Procedures in TTT database</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-September-16
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Tested debugged and resolved Issues TTT-513, 510. 512, 509</li>
                    <li>Implemented VerifyApplicationId value script that verifies one record</li>
                    <li>Resolved Stored Procedure Log issue and fixed Search SPs</li>
                    <li>Tested debugged and resolved Issues TTT-527, 528, 529, 530, 531</li>
                    <li>Created shell procedure for AuditHistoryLastValues</li>
                    <li>Implemented VerifyApplicationIdValue script that parses all tables</li>
                    <li>Resolved issues 409 and updated SystemEntityTypeGetNextSequence and updated all
                        associated scripts</li>
                    <li>Resolved exceptions from TTT rebase</li>
                    <li>Inserted Records for Framework Entities in SystemEntityType and Created Scripts</li>
                    <li>System Entity Category page not working</li>
                    <li>ApplicationUser details screen has issues</li>
                    <li>Convert c# to new code pattern</li>
                    <li>Create Constraints on System Entity Type table</li>
                    <li>Dropped procedures</li>
                    <li>Put Negative sign in all Unit Test Scripts</li>
                    <li>Create AuditId</li>
                    <li>Made changes in Search.sql</li>
                    <li>Added folders and sub folders and Unit Test</li>
                    <li>Added scripts to DB Full Frameowrk folders</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-September-09
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Involve in Technical discussion on Architecture of TTT</li>
                    <li>Review Technical discussion of TTT and added additional doc</li>
                    <li>Tested Application</li>
                    <li>Added folders and sub folders and Unit Test</li>
                    <li>Added Scripts to Unit Test files</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-September-02
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Check CRUD application</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-August-26
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Rebase and fix errors and implement changes to Insert methods</li>
                    <li>Tested verified and removed Z-prefixed tables</li>
                    <li>Resolved errors</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-August-19
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Combine create and update method</li>
                    <li>Correct SP</li>
                    <li>Remove Application from framework.components.task</li>
                    <li>Resolved bugs</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-August-12
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Implement dictionary object for connection strings</li>
                    <li>Changed coloumns form Person Id to Application User Id in User preference</li>
                    <li>App- Applicaiton User Bucket issue</li>
                    <li>Tested and resolve bugs</li>
                    <li>Move Application User classess and fix namespace</li>
                    <li>Extra lines removal and Alignment in SQL</li>
                    <li>Combine create and update method</li>
                    <li>Correct SP</li>
                    <li>Remove Application from framework.components.task</li>
                    <li>Remove Extra lins and alignemnt in SQL</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-August-05
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Add folders in Directory</li>
                    <li>Created path for all Directores</li>
                    <li>Teted application and resolve error</li>
                    <li>MVC Music store</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-July-29
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Remove Application Id form Event Monitoring-> App Monitored Event, App Monitored
                        Event Email, App Monitred Event Processing state, App Monitored event source</li>
                    <li>Remove Application Id from User Preference -> App EntityFiled Label, User prefernce,
                        User PreferenceCategory, User preference Data Type, User preference Key</li>
                    <li>Deploy script Database framework</li>
                    <li>Worked on Combine Create /Update methods</li>
                    <li>Resolve error</li>
                    <li>Implemented ApplicationId changes to entites</li>
                    <li>Resolved exceptions due to db name chnages and updated</li>
                    <li>Update Set up Configurations table in db</li>
                    <li>Framework. Components.Import for adding ApplciationId</li>
                    <li>Changes made in Audit Action and Audit history for adding ApplicationId</li>
                    <li>Re deployed all SP</li>
                    <li>Tested TTT application</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-July-22
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Unit Testing of TTT</li>
                    <li>Worked on Tasks: Migrate SystemTables to Configuration Database, Migrate Audit Tables
                        to CommonServices Database</li>
                    <li>Remove unused test tables from TaskTimeTracker DB</li>
                    <li>Resolve issues related to Person class</li>
                    <li>Add ApplicationId to GetSystemEntityTypeId function and update linked items</li>
                    <li>Add ApplicationId as Search method parameter to BO Layer code</li>
                    <li>Update sql scripts with ApplicationId parameter</li>
                    <li>Resolve minor TTT Issues</li>
                    <li>Implement logic to make ApplicationId as global variable</li>
                    <li>Tasks: Migrate SystemTables to Configuration Database, Migrate Audit Tables to CommonServices
                        Database</li>
                    <li>Add Application Id to Person</li>
                    <li>Add Application Id to Milestone</li>
                    <li>Add Application Id to Release Log</li>
                    <li>Add Application Id to Release Log Details</li>
                    <li>Migrate Person from Authentication and Authorization to core</li>
                    <li>Migrate Release log to common services database</li>
                    <li>Migrate Release log details to common services database</li>
                    <li>Migrate Person title to Core</li>
                    <li>Combine create and update method</li>
                    <li>Correct Stored Procedures</li>
                    <li>Incorrect Identation</li>
                    <li>Move Person components to business layer</li>
                    <li>Add ApplicationId to various entities</li>
                    <li>Move Application user classes and fix namespaces</li>
                    <li>Extra Lines removal and Alignment in SQL</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-July-15
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Add EntityId to User Preference Module (DB Objects, BL and WebPages)</li>
                    <li>Add EntityId to ApplicationUser, ApplicationUserTitle Entities</li>
                    <li>Add EntityId to Layer Entity</li>
                    <li>Add EntityId to Skill, Competency Entity</li>
                    <li>Add ApplicationId to TaskFormulation Entity</li>
                    <li>Add ApplicationId to TaskRiskRewardRankingXPerson Entity</li>
                    <li>Add ApplicationId to Schedule, ScheduleItem, ScheduleQuestion Entity</li>
                    <li>Add ApplicationId to SystemDevNumbers, ApplicationEntityFieldLabel Entity</li>
                    <li>Answer queries/ Host TV session to walk through steps and resolve issues</li>
                    <li>Add Application Id to Entities</li>
                    <li>Review and analyze Day Care Application</li>
                    <li>Add missing scripts/methods and Review completed entities</li>
                    <li>Review entities and fix issues</li>
                    <li>Create Issue traclker doc and resolve reported issues</li>
                    <li>Add ApplicationId to Need, ProjectXNeed, NeedXFeature Entities (DB Objects, BL and
                        WebPages) and rename entities 1. ProjectXNeeds to ProjectXNeed 2. NeedsXFeature
                        to NeedXFeature</li>
                    <li>Work on Application User X Application Role</li>
                    <li>Work on Project , Milestone, Need on SQL Mgt studio</li>
                    <li>Completed Client and Reward entity</li>
                    <li>Completed Risk , Project, Feature</li>
                    <li>Completed Milestone and Skill X Person X Skill Level</li>
                    <li>Worked on Need</li>
                    <li>Work on Application Entity Parental Hierachy</li>
                    <li>Add Application Id to Question, chedule, Activity, Activity State, Activity Algorithm
                        , Activity Algorithm Item</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-July-08
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Implement Scalar valued functions</li>
                    <li>Combine Update and Create methods in BO object code</li>
                    <li>Analyze Add->ApplicationId to all tables</li>
                    <li>Make Confluence document for Add->ApplicationId to all tables</li>
                    <li>Monitor and guide for Refactoring tasks</li>
                    <li>Move ApplicationEntityFieldLabel to Configuration</li>
                    <li>Add ApplicationId to Common Services entities and update Confluence doc</li>
                    <li>Walk through steps for Add-> ApplicationId</li>
                    <li>Remove Trim.() form database files -> Module and Framework TTT - 420</li>
                    <li>Invalid parameter value TTT- 428 completed</li>
                    <li>Correct unit test components TTT - 426</li>
                    <li>Insert single quotes TTT- 416</li>
                    <li>Working on TTT- 418</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-July-01
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Debug and resolve errors with Application User Integration</li>
                    <li>Test TTT App overall and fix errors</li>
                    <li>Moving tables to new Databases</li>
                    <li>Update and execute scripts/Modify Setup Configuration</li>
                    <li>Implement shell procedures to invoke generic SPs</li>
                    <li>Integrate SHELL scripts with entity scripts/fix errors in shell scripts</li>
                    <li>Make Confluence page on dependent tables and SPS/Shell cmds</li>
                    <li>Delete unnecessary tables and scripts from DB</li>
                    <li>Move connectionkey retrieval to DB</li>
                    <li>Create scripts to delete unwanted SPs/tables</li>
                    <li>New line implementation in scripts for logging and Orphaned SPs review</li>
                    <li>Created SystemEntity.Helper Page</li>
                    <li>Worked on Idenetation formatting</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-June-24
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Implement and fix Does Exist SPs of all entities/Integrating with entities</li>
                    <li>Moving tables to new Databases/Moving ConnectionString to web.config</li>
                    <li>Implement code to support multiple connection strings</li>
                    <li>Add Horizontal alignment column to ApplicationEntityFieldLabel</li>
                    <li>Change PersonXApplicationRole to ApplicationUserXApplicationRole</li>
                    <li>Create Procs function missing files</li>
                    <li>SystemEntityTypeId Removed</li>
                    <li>Work on Invalid stored procedure- Core, Module , Framework</li>
                    <li>Work on Missing dbo. in Core, Module, Framework</li>
                    <li>Work on AuditHistory not applicable on deletehard</li>
                    <li>Change 706 for all terms of deployment</li>
                    <li>Remove extra lines</li>
                    <li>Audit HistoryId is not need to be passed</li>
                    <li>Resolve Issue</li>
                    <li>Implement multiple deploy commands for SP logging</li>
                    <li>Update SP scripts to support DB Division</li>
                    <li>Create entity ApplicationUserTitle and resolve errors with ApplicationUser</li>
                    <li>Corrected Update for Application User</li>
                    <li>Remove from SQL Stored procedures Check and Drop temp table</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-June-17
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Unit on Interval in advanced History Menu (seconds, M, H, d, w, m, Q, Y)</li>
                    <li>Under system Admin, need page that shows entity and test data count</li>
                    <li>Text Search Utility:New Windows forms utility</li>
                    <li>Implementation of following patterns in the utility</li>
                    <li>Missing dbo in sql statments</li>
                    <li>Illogical/Extra lines in SQL scripts</li>
                    <li>Bad Indentation in SQL statements/scripts</li>
                    <li>Improper C# Code</li>
                    <li>Left OVer Text from VS Initial template</li>
                    <li>Unwanted SQL Statements</li>
                    <li>Wrong Side Characters in SQL scripts</li>
                    <li>Database Tool:Concept Analysys, Basic Form Setup</li>
                    <li>Implement SP to renumber</li>
                    <li>Review and implement DoesExist Procedure</li>
                    <li>Simplify SP to Renumber/Add supporting log tables</li>
                    <li>Review an cleanup SP Logging</li>
                    <li>Implement fix for broken Update pages</li>
                    <li>Research on rounded corners of Border control</li>
                    <li>Create C# Component , Businees Layer of Application User</li>
                    <li>Create Schema of Application User</li>
                    <li>Create stored procedure of Application User</li>
                    <li>Rectify Errors of Application User Entity</li>
                    <li>Rectify errors of System Category</li>
                    <li>Correct Namespace of System Entity Category for Whole application</li>
                    <li>Remove irrelevent lines from stored procedure in Farmework</li>
                    <li>Create Unit test pages for missing entities -> Framework, Resolve error</li>
                    <li>Resolve issue</li>
                    <li>Corrected dbo. missing in Stored procedure</li>
                    <li>Update missing dbo. for all entites except Search.sql files for Module and Core</li>
                    <li>Correct formatting AS and BEGIN Module and Core</li>
                    <li>Correct formatting "Comma"on left side Module and Core</li>
                    <li>Update missing dbo. for all entites except Search.sql files for Framework</li>
                    <li>Correct formatting AS and BEGIN Framework</li>
                    <li>Correct formatting "Comma"on left side Framework</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-June-10
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Made Bulk Changes in procedures for entities under framework.EventMonitoring as
                        per the guidelines</li>
                    <li>Made Bulk Changes in procedures for entities under framework.Log And Trace, Framework.ReleaseLog</li>
                    <li>Made Bulk Changes in procedures for entities under framework.Import</li>
                    <li>Tested the UI application for Framework, Framework.ApplicationUser, Framework.Audit
                        Entities</li>
                    <li>Made Bulk Changes in procedures for entities under for List Procedures and Also
                        in BusinessClasses to pass the parameter</li>
                    <li>Made Bulk Changes in procedures for entities under framework.Task And Workflow</li>
                    <li>Made Bulk Changes in procedures for entities under Core for List Procedures</li>
                    <li>Made Changes in BL and UI related to List Procedure Changes</li>
                    <li>Performed Unit Testing of Framework -> Task And Workflow, Framework -> Event Monitoring
                        module entities in Web</li>
                    <li>Performed Unit Testing of Framework -> Release Log, Framework -> Import module entities
                        in Web</li>
                    <li>When in devlopment mode, C# then all data Key generated numbers will be negative:
                        Change in Procedure in DefaultDataRule.cs</li>
                    <li>Windows Application Text Formating Utility: Created Basic Form Setup</li>
                    <li>Windows Application Text Formatting Utility:Performed coding for File & Directory
                        Search</li>
                    <li>Windows Application Text Formatting Utility : Performed Coding of Tree View and
                        Grid Result</li>
                    <li>Worked on Details Page for TaskEntityType and Client</li>
                    <li>Windows Application Text Formatting Utility</li>
                    <li>Reslove TTT issue DB clean up</li>
                    <li>UI Improvement</li>
                    <li>Code clean up (defined cloumns value)</li>
                    <li>Implement Auto search off feature post back issue</li>
                    <li>Review and Update deploy scripts</li>
                    <li>Simplify SP logging</li>
                    <li>Implement SP to renumber</li>
                    <li>Auto search off Paot back issue</li>
                    <li>Work on Frame Work : Task and Flow work</li>
                    <li>SQL Format in all Frame work</li>
                    <li>Business Layer -> Modules (all) , UI ->Modules</li>
                    <li>Test codes</li>
                    <li>Test TaskPackage</li>
                    <li>Create Unit Test pages for missing entities</li>
                    <li>Create CRUD, C# Components, controls for Applicaiton User Entity</li>
                    <li>Reslove Errors of Applicaiton User Entity</li>
                    <li>Create CRUD, C # Components for System Entity Category</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-June-04
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Advanced History Grid: Created pop-up links for Count Column to show detail records</li>
                    <li>Created a class that contains static fields for known session keys, application
                        keys</li>
                    <li>Created a stored procedure 'AuditHistoryLastValues</li>
                    <li>New Entity SystemDevNumbers: Created DB Objects, Stored Procredures, BusinessClasses
                        and UI Pages for the new entity</li>
                    <li>Research on reading Local File: Created a mechanism where AuditId will be read from
                        new file MyConfiguration.xml when the application starts</li>
                    <li>C# then all data Key generated numbers will be negative: Created a Server Side method</li>
                    <li>C# then all data Key generated numbers will be negative: created an alternative
                        stored procedure</li>
                    <li>Under system Admin, need page that shows entity and test that count: Created the
                        stored procedures to fetch the test records for each entity</li>
                    <li>C# then all data Key generated numbers will be negative for all entities: Modified
                        Generic Control</li>
                    <li>Modified the stored procedure to show data for each entity for test data and audit
                        history record</li>
                    <li>Created a Server Page to show results of Entity wise Test Data</li>
                    <li>C# then all data Key generated numbers will be negative for all entities: Updated
                        all Insert Stored Procedures for entities under Module</li>
                    <li>C# then all data Key generated numbers will be negative for all entities: Updated
                        all Insert Stored Procedures for entities under Core</li>
                    <li>Modified the Server Page to show results of Entity wise Test Data to add hyperlink</li>
                    <li>Made Bulk Changes in procedures for entities under framework as per the guidelines</li>
                    <li>Made Bulk Changes in procedures for entities under framework.ApplicationUser, Framwork.Audit
                        as per the guidelines</li>
                    <li>Resloved Issue</li>
                    <li>Implement new enetity for SP logging</li>
                    <li>Make UI improvement</li>
                    <li>Clean up Business layer code</li>
                    <li>Implelemnt new feature</li>
                    <li>Clean up DB Solution/Add new implementation</li>
                    <li>Add command files in Modules project</li>
                    <li>Add command files in Core project</li>
                    <li>Format Sql in Core project</li>
                    <li>Format Sql in Module project</li>
                    <li>Improve determining SystemEntityId in Module Project</li>
                    <li>Improve determining SystemEntityId in Core Project</li>
                    <li>Again Improve determining SystemEntityId in Module Project</li>
                    <li>Update database Module project with New format</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-June-01
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>New User Preference Concept for History Grid Delete/Detail Link Visibility Entity
                        wise </li>
                    <li>New User Preference Concept for History Grid Visibility Entity wise </li>
                    <li>Check box to hide/show History Grid for all Entities </li>
                    <li>Re-Ordering of Columns in Search, Insert, List screens</li>
                    <li>History Grid Changes (New Groupings to be included and it should be configurable
                        entity wise) </li>
                    <li>Export Menu to new generic control (applied to all entities)</li>
                    <li>User Configuration Profile View </li>
                    <li>Audit History: Change Order By based on Last Updated </li>
                    <li>ReleaseLogDetails entity page is working for all functions</li>
                </ol>
            </td>
        </tr>
    </table>
    <tr>
        <td>
            Release notes for 2012-May-25
        </td>
    </tr>
    <tr>
        <td>
            <ol>
                <li>Grid Styling</li>
                <li>Visibility of Delete Button based on logged in User's Application Role </li>
                <li>Grid caching issues</li>
                <li>Remove column Layer from Task Formulation. Add to column Layer to Activity</li>
                <li>Add Column "Weight" to TaskPriority</li>
                <li>Enhance Sorting feature</li>
                <li>Right align label controls</li>
                <li>Border around control when multiple items are shown</li>
                <li>Audit History Grid on Detail/Updated view for all entities show last updated by,
                    updated date</li>
                <li>Audit History Grid, New Feature in Advanced Mode: Allow to specify Interval</li>
                <li>Needs Entity Modification: Remove one Columns ProjectId</li>
                <li>DatePicker Control implementation</li>
                <li>Competency Module: New entities for Skill, SkillLevel, Competency, SkillXPersonXSkillLevel,
                    CompetencyXSkill, TaskXCompetency</li>
                <li>UserPreference Entity Modification: Add New Column UserPreferenceCategoryId</li>
                <li>UserPreferency Module: 2 new keys for DetailLink, DeleteLink Visibility Entity Wise</li>
                <li>UserPreferency Module: 1 new key for History Grid Visibility Entity Wise</li>
                <li>New Entity Added - User Preference Category</li>
                <li>New Entity Added - ActivityState</li>
                <li>New Entity Added - Task Package</li>
            </ol>
        </td>
    </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-May-18
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Moved ReleaseLog dlls to its own Components.FRamework.ReleaseLog</li>
                    <li>CRUD operations for ProjectTimeLine </li>
                    <li>Fixed exceptions in ProjectTimeLine</li>
                    <li>CRUD operations for ApplicationOperation</li>
                    <li>Created new column FeatureId to Task</li>
                    <li>Created new column ApplicationId to ApplicationRole</li>
                    <li>Created table and CRUD operation for TaskXPerson</li>
                    <li>Created table and CRUD operations for ApplicationOperationXApplicationRole</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-May-11
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Added Update Links to Detail pages</li>
                    <li>Implemented a class for DefaultBehaviour to suppy default values for Description
                        and SortOrder</li>
                    <li>UI changes to Pagiation control and the Grid</li>
                    <li>Created CRUD operations for About Page related entities ReleaseLog and ReleaseLogDetails</li>
                    <li>Modifications in Schedule Entity (change datatype of 2 columns)</li>
                    <li>NeedsXFeature and PersonXApplicationRole UI Changes</li>
                    <li>Implemented Auto Log-In Feature</li>
                    <li>History of Record is coming in Detail View</li>
                    <li>Introduced New Entities: TaskType, TaskPriorityType</li>
                    <li>Modification in Task Entity (new column added)</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-May-04
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Modified Existing Entities to add new columns(Foreign Keys)</li>
                    <li>New Entity NeedsXFeature Basic Setup</li>
                    <li>Modified SQL Script Generation Utility to add new scripts PrimaryKey, Foreign Key,
                        Unique Index</li>
                    <li>Added Dropdowns to entities where foreign key columns are present</li>
                    <li>Added Dropdowns in search functionality for foreign key columns to be searched upon</li>
                    <li>Added Project Id field to Milestone entity</li>
                    <li>Created CRUD Layers and UI for About Page</li>
                    <li>Resolved Database project issues</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-April-27
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Import Module (Basic Setup)</li>
                    <li>Tasks and Workfilow Module (Basic Setup)</li>
                    <li>Event Monitoring (Basic Setup)</li>
                    <li>Changes for Project Entity (New foreign Key added)</li>
                    <li>Multiple Update operation on Grid</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-April-20
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Multiple details operation</li>
                    <li>Re-implementation of multiple delete</li>
                    <li>Search based on foreign key names</li>
                    <li>Names instead of Ids in all default/list pages</li>
                    <li>New stored procedures (Clone, DeleteHard, DoesExist)</li>
                    <li>Import Module</li>
                    <li>New Entities Feature, Needs</li>
                    <li>Import Module new entities FileType, BatchFileStatus, BatchFile</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-April-13
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Audit Module</li>
                    <li>Implemented the Import Functionality</li>
                    <li>Completed Database - Clone, DoesExist, DeleteHard Procedures</li>
                    <li>Implemented Multiple Delete operation</li>
                    <li>Fixed issues with GridView refresh</li>
                    <li>Fixed issue with Single Record Delete </li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-April-15
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Changed pages to handle autokey id Generation</li>
                    <li>Changed code to make all c# function to lowercase</li>
                    <li>Changed BusinessLayer components .cs files to make export page work</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2012-March-30
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Audit Id Validation</li>
                    <li>Hide Ids for Pages(Insert, Update, Details, etc) when system is in Live Mode</li>
                    <li>Export Pages without paging</li>
                    <li>User Preferences Module</li>
                    <li>New Table UserPreferencesKey (Database, Business Layer, UI)</li>
                    <li>Export to CSV functionality</li>
                    <li>Implemented Pagiation feature for the Grid</li>
                    <li>Implemented Full Table Sort and partially completed View Sort</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2011-June-22
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Made SystemEntitytype table to function fully.</li>
                    <li>Added base page refrence to all respectice pages to get SessionVariables.RequestProfile.AuditId.</li>
                    <li>Changed @SystemEntityId = @SystemEtityTypeId in all the respective stored procedures
                        , crosschecked the values entered with DataBase.</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2011-June-21
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Stuck on Custom valdation for AuditId on the LoginSettings page.</li>
                    <li>Added all pages to SystemEntityType - Default is working only for now.</li>
                    <li>Read on Datepicker controls - AJAX and simple text-box + Calender</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2011-June-20
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Almost done with Custom valdation for AuditId on the LoginSettings page.</li>
                    <li>Fixed errors in displaying list for all tables.</li>
                    <li>Fixed Dynamic validation in Testing and non-testing mode for Project table.</li>
                    <li>Now AuditId is being called from the UI layer for all the tables.</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2011-June-10
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Unnecessary comments from all Validation have been deleted. Layer set as an example
                        with comments.</li>
                    <li>Fix Table Structure</li>
                    <li>All strored procedure Reslated to System_Entity_*</li>
                    <li>Deploy Stored Procedure</li>
                    <li>Review System_EntityType_GetNextSequence</li>
                    <li>Table default page must work</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2011-June-09
            </td>
        </tr>
        <tr>
            <td>
                Completed Items:<br />
                <ol>
                    <li>Validation Error sorted out for the page -- ex. ApplicationRole</li>
                    <li>Headers in Grid now have space in between them i.e. First Name not FirstName --
                        ex. ALL Pages</li>
                    <li>Created System entity Table</li>
                    <li>Planned things for the new trainee</li>
                </ol>
            </td>
        </tr>
        <tr>
            <td>
                New Items:
                <br />
                <ol>
                    <li>Add menu for System entity table - Admin section</li>
                    <li>Simplify all valdation files, leave one sample</li>
                    <li>*</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2011-June-07
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Created root menus : admin | coordinator | user</li>
                    <li>Work on Reg Epression</li>
                    <li>Work on System entity table</li>
                    <li>Work on wiring event handlers in code</li>
                    <li>Work on bucket width formating</li>
                    <li>Work on left align html work</li>
                </ol>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Release notes for 2011-June-6
            </td>
        </tr>
        <tr>
            <td>
                <ol>
                    <li>Added Help Menu Item</li>
                    <li>Changed XYZ</li>
                </ol>
            </td>
        </tr>
    </table>
    </p>
</asp:Content>

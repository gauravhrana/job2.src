<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master"
    CodeBehind="d3CommonMasterSample.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.d3CommonMasterSample" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
    <style>
        .Masterli
        {
            float: left;
            display: inline;
            border: 1px solid;
        }

        ul a:hover
        {
            color: black;
        }

        /*.Masterli li a { color: black; float: left; display: block; padding: 4px 10px; margin-left: -1px; position: relative; left: 1px; background: white; text-decoration: none; }*/
        .Masterli li
        {
            float: left;
            border-right: 1px solid black;
            list-style-type: none;
        }

            .Masterli li a
            {
                padding: 0px 5px;
                font-weight: bold;
                color: #000;
                text-decoration: none;
            }

        .MasterULlink
        {
            /*background-image: url(navi_bg_divider.png);*/
            background-repeat: no-repeat;
            background-position: right;
            padding-right: 32px;
            padding-left: 32px;
            display: block;
            line-height: 30px;
            text-decoration: none;
            font-family: Arial,Helvetica,sans-serif;
            font-size: 15px;
            color: #371C1C;
        }

        a:link, a:visited, a:active
        {
            outline: medium none;
            text-decoration: none;
        }

        .MasterUL
        {
            list-style-type: none;
            height: auto;
            /*width: 100px;
            margin: auto;*/
        }

        /*ul.tabNavigation
        {
            border: 1px solid #ccc;
            height: 28px;
            background: #eff5f9;
            padding-left: 40px;
            padding-top: 45px;
            -moz-box-shadow: inset 0 -2px 2px #dadada;
            -webkit-box-shadow: inset 0 -2px 2px #dadada;
            box-shadow: inset 0 -2px 2px #dadada;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
        }

            ul.tabNavigation li
            {
                border: 1px solid #ccc;
                height: 28px;
                background: #eff5f9;
                padding-left: 40px;
                padding-top: 45px;
                -moz-box-shadow: inset 0 -2px 2px #dadada;
                -webkit-box-shadow: inset 0 -2px 2px #dadada;
                box-shadow: inset 0 -2px 2px #dadada;
                border-top-left-radius: 4px;
                border-top-right-radius: 4px;
            }

                ul.tabNavigation li a
                {
                    font-family: Arial, Helvetica, sans-serif;
                    font-size: 13px;
                    font-weight: bold;
                    color: #000000;
                    padding: 7px 14px 6px 12px;
                    display: block;
                    background: #FFFFFF;
                    border-top-left-radius: 3px;
                    border-top-right-radius: 3px;
                    text-decoration: none;
                    background: -moz-linear-gradient(top, #ebebeb, white 10%);
                    background: -webkit-gradient(linear, 0 0, 0 10%, from(#ebebeb), to(white));
                    border-top: 1px solid white;
                    text-shadow: -1px -1px 0 #fff;
                    outline: none;
                }

                    ul.tabNavigation li a.selected
                    {
                        border-top: #999 solid 1px;
                        border-left: #999 solid 1px;
                        border-bottom: #fff solid 1px;
                        border-right: #999 solid 1px;
                        outline: none;
                    }

                    ul.tabNavigation li a.inactive
                    {
                        padding-top: 5px;
                        padding-bottom: 5px;
                        color: #666666;
                        background: -moz-linear-gradient(top, #dedede, white 75%);
                        background: -webkit-gradient(linear, 0 0, 0 75%, from(#dedede), to(white));
                        border-top: 1px solid white;
                    }

                    ul.tabNavigation li a:hover, #tabs li a.inactive:hover
                    {
                        border-top: 1px solid #dedede;
                        color: #000000;
                    }

        .container
        {
            clear: both;
            padding: 10px 0px;
            width: 664px;
            background-color: #fff;
            text-align: left;
        }*/

        /*#tabs
        {
            width: 95%;
            margin-left: auto;
            margin-right: auto;
            margin-top: 10px;
        }

        #show-Bar,
        #show-Pie,
        #show-Animated
        {
            padding: 10px;
            border: #ccc solid 1px;
        }*/
    </style>
    <script>

        $(document).ready(function () {
            d3.select("svg").remove();
            var ControlName = "~/Prototype/D3/Controls/d3SampleControl.ascx";
            $.ajax({
                type: "POST",
                url: "CommonWebMethod.aspx/AjaxGetBarCtrl",
                data: "{controlName:'" + ControlName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#chart div').html('');
                    $('#chart').html(msg.d);
                }
            });


            $('#tabs').tabs({
                activate: function (event, ui) {
                    var active = $("#tabs").tabs("option", "active");
                    if (active == 0) {
                        d3.select("svg").remove();
                        var ControlName = "~/Prototype/D3/Controls/d3SampleControl.ascx";
                        $.ajax({
                            type: "POST",
                            url: "CommonWebMethod.aspx/AjaxGetBarCtrl",
                            data: "{controlName:'" + ControlName + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                $('#chart div').html('');
                                $('#chart').html(msg.d);
                            }
                        });
                    }
                    if (active == 1) {
                        d3.select("svg").remove();
                        var ControlName = "~/Prototype/D3/Controls/d3PieSampleControl.ascx";
                        $.ajax({
                            type: "POST",
                            url: "CommonWebMethod.aspx/AjaxGetBarCtrl",
                            data: "{controlName:'" + ControlName + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                $('#chart').html(msg.d);
                            }
                        });
                    }
                    if (active == 2) {
                        d3.select("svg").remove();
                        var ControlName = "~/Prototype/D3/Controls/d3AnimatedSample.ascx";
                        $.ajax({
                            type: "POST",
                            url: "CommonWebMethod.aspx/AjaxGetBarCtrl",
                            data: "{controlName:'" + ControlName + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                $('#chart').html(msg.d);
                            }
                        });
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
    <%--<div id="tabs">
        <ul class="tabNavigation">
            <li><a id="show-Bar" href="#"><span>d3SampleControl</span></a>
            </li>
            <li><a id="show-Pie" href="#"><span>d3PieSampleControl</span></a>
            </li>
            <li><a id="show-Animated" href="#"><span>d3AnimatedSampleControl</span></a></li>
        </ul>
        <div id="chart" class="bar">
        </div>
    </div>--%>
    <div id="tabs">
        <ul class="MasterUL">
            <li class="Masterli"><a id="show-Bar" href="#" class="MasterULlink">d3SampleControl</a>
            </li>
            <li class="Masterli"><a id="show-Pie" href="#" class="MasterULlink">d3PieSampleControl</a>
            </li>
            <li class="Masterli"><a id="show-Animated" href="#" class="MasterULlink">d3AnimatedSampleControl</a></li>
        </ul>
        <div id="chart" class="bar">
        </div>
    </div>
</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
</asp:Content>



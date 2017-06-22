<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonRender.ascx.cs" Inherits="ApplicationContainer.UI.Web.MasterPages.CommonRender" %>

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="/Styles/Style.aspx" rel="stylesheet" type="text/css" />    
    <link href="/Styles/StyleMenu.aspx" rel="stylesheet" type="text/css" />
    <link href="/Styles/StyleGrid.aspx" rel="stylesheet" type="text/css" />

    <asp:PlaceHolder runat="server">
        <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
    </asp:PlaceHolder>

	<%# Styles.Render("~/bundles/None") %>

    <%# Styles.Render("~/bundles/css") %>
    <%# Styles.Render("~/Content/themes/base/css") %>
    <%# Styles.Render("~/Content/bootstrap") %>
    <%# Styles.Render("~/Content/kendo") %>

    <%# Styles.Render("~/Content/accordion") %>
    <%# Styles.Render("~/Content/sliderMenu") %>

    <%# Scripts.Render("~/bundles/modernizr") %>
    <%# Scripts.Render("~/bundles/jquery") %>
    <%# Scripts.Render("~/bundles/jqueryui") %>

	<%# Scripts.Render("~/bundles/angular") %>

	<%# Scripts.Render("~/bundles/bootstrap") %>

    <%# Scripts.Render("~/bundles/kendo") %>

	<%# Styles.Render("~/Content/bower") %>

	<%# Scripts.Render("~/bundles/bower") %>
    <%# Scripts.Render("~/bundles/appScripts") %>

    <script src="/Scripts/SlideAndPushMenu/modernizr.custom.js"></script>   

    <!-- prefix free to deal with vendor prefixes -->
    <script src="/Scripts/Accordin/prefixfree-1.0.7.js" type="text/javascript"></script>

	<style>
        div {
            /*border: 1px dotted;*/
            /*border-bottom-style: none;*/
            /*border-bottom: 0px none;*/
            /*border-color: blue;*/
            /*background-color: #b0c4de;*/
        }
    </style>

	<style>
        /*div
        {
            border: 1px dotted;
            border-color: blue;
            /*border-bottom-color: yellow;
            border-left-color: green;
            border-right-color: orange;* /
        }*/

        /*div.row
        {
            background: blueviolet;
        }*/

        .k-list-container {
            white-space: nowrap !important;
            width: auto!important;
            overflow-x: hidden !important;
            min-width: 243px !important;
        }

        .k-list {
            overflow-x: hidden !important;
            /*overflow-style: marquee;*/
            overflow-y: auto !important;
            width: auto !important;
        }
    </style>

    <script type="text/javascript">

    	function showhideborder(istesting) {
    		if (!istesting) {
    			var myObj = document.getElementsByClassName('searchtd');
    			// alert(myObj == null);
    			if (myObj != null) {
    				for (var i = 0; i < myObj.length; i++) {
    					myObj[i].style['border-color'] = 'White';
    				}
    			}
    		}
    	}

    	function css(a) {
    		var sheets = document.styleSheets, o = {};
    		for (var i in sheets) {
    			var rules = sheets[i].rules || sheets[i].cssRules;
    			for (var r in rules) {
    				if (a.is(rules[r].selectorText)) {
    					o = $.extend(o, css2json(rules[r].style), css2json(a.attr('style')));
    				}
    			}
    		}
    		return o;
    	}

    	function css2json(css) {
    		var s = {};
    		if (!css) return s;
    		if (css instanceof CSSStyleDeclaration) {
    			for (var i in css) {
    				if ((css[i]).toLowerCase) {
    					s[(css[i]).toLowerCase()] = (css[css[i]]);
    				}
    			}
    		} else if (typeof css == "string") {
    			css = css.split("; ");
    			for (var i in css) {
    				var l = css[i].split(": ");
    				s[l[0].toLowerCase()] = (l[1]);
    			}
    		}
    		return s;
    	}
    </script>
 
	<script>

 		//    var w = window,
 		//d = document,
 		//e = d.documentElement,
 		//g = d.getElementsByTagName('body')[0],
 		//x = w.innerWidth || e.clientWidth || g.clientWidth,
 		//y = w.innerHeight || e.clientHeight || g.clientHeight;

 		//    console.log(x + ' × ' + y);
 		//alert(x + ' × ' + y);

    </script>
    
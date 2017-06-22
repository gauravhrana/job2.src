<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonBannerNav.ascx.cs" Inherits="ApplicationContainer.UI.Web.MasterPages.CommonBannerNav" %>

<%@ Register TagName="QuickSearchControl" TagPrefix="qs" Src="~/Shared/Controls/QuickSearch/QuickSearch.ascx" %>

<div class="col-sm-3 text-center">
    <h4 style="color: White; font-weight: 100">
        <label id="lblProjectTitle" runat="server" style="font-weight: 100;"></label>
    </h4>
</div>
<div class="col-sm-2">
    <qs:QuickSearchControl ID="QuickSearchControlId" runat="server" />
</div>
<div class="col-sm-2 visible-sm visible-xs">
</div>
<div class="col-sm-2 visible-md visible-lg">
    <button href="/" type="button" class="btn">
		<i class="icon-home icon-white"></i>
    </button>
    <button id="showBottom" type="button" class="btn">
        <i class="icon-user icon-white"></i>
    </button>
    <button id="showTopPush" type="button" class="btn">
        <i class="icon-hdd icon-white"></i>
    </button>
    <button id="showBottomPush" type="button" class="btn">
        <i class="icon-leaf icon-white"></i>
    </button>
    <button id="showRight" type="button" class="btn">
        <i class="icon-hand-right icon-white"></i>
    </button>
</div>
<div class="col-sm-1">
    <a id="hprCustomise" style="font-size: Small;">Customize</a>
    <asp:HyperLink ID="HyperLink2" NavigateUrl="#" runat="server" BorderStyle="None" ImageUrl="~/Content/Images/user_male_go.png" BorderWidth="0" ></asp:HyperLink>
</div>


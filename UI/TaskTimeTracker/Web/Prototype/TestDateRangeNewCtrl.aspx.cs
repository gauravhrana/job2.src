using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web
{
	public partial class TestDateRangeNewCtrl : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetUpDateRangeControl();
			}

		}

		private void SetUpDateRangeControl()
		{
			var funCallGroup = "FillUpGroup" + oDateRange.GetKey() + "();";
			oDateRange.DateRangeGroupDropDown.Attributes.Add("onchange", funCallGroup);
			oDateRange.HideLabel();

			var funcCall = "Fillup" + oDateRange.GetKey() + "();";
			oDateRange.DateRangeDropDown.Attributes.Add("onchange", funcCall);
			oDateRange.HideLabel();


		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			if (oDateRange.DateRangeGroupDropDown.SelectedValue == "Custom")
			{
				//oDateRange.DateRangeDropDown.SelectedValue=;
			}
			oDateRange.SaveDateValues(oDateRange.DateRangeGroupDropDown.SelectedValue, oDateRange.DateRangeDropDown.SelectedValue, oDateRange.FromDateTime, oDateRange.ToDateTime);
		}
	}
}
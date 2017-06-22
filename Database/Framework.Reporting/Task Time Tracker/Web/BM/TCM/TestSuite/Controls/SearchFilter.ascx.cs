using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;
using TestCaseManagement.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TCM.TestSuite.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public TestSuiteDataModel SearchParameters
		{
			get 
			{
				var data = new TestSuiteDataModel();

                SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
		}
	}
}
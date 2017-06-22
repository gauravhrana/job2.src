using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.EntityDateRangeState.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public EntityDateRangeStateDataModel SearchParameters
		{
			get
			{
				var data = new EntityDateRangeStateDataModel();

				SearchFilterControl.SetSearchParameters(data);

				return data;
			}
		}

		#endregion

		#region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("EntityDateRangeStateTypeId"))
			{
                var dateRangeStateTypedata = EntityDateRangeStateTypeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dateRangeStateTypedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId);

			}
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		#endregion
	}
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.Audit;
using DataModel.Framework.Audit;
using Shared.UI.Web.Controls;
using ApplicationContainer.UI.Web.BaseUI;

namespace Shared.UI.Web.ApplicationManagement.Development.TestData.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public DataModel.Framework.Audit.AuditHistory SearchParameters
		{
			get
			{
				var data = new DataModel.Framework.Audit.AuditHistory();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;
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

		//#region variables

		////public int? ApplicationId
		////{
		////	get
		////	{
		////		int? applicationId = null;

		////		if (drpSearchConditionAplication.SelectedValue != "-1")
		////			applicationId = Convert.ToInt32(drpSearchConditionAplication.SelectedValue);

		////		return applicationId;
		////	}
		////}


		//public DataModel.Framework.Audit.AuditHistory SearchParameters
		//{
		//	get
		//	{
		//		var data = new DataModel.Framework.Audit.AuditHistory();

		//		data.SystemEntityId = GetParameterValueAsInt(DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId);

		//		return data;
		//	}

		//}

		//#endregion

		//#region private methods
		//#endregion

		//#region Events

		//public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		//{
		//	base.LoadDropDownListSources(fieldName, dropDownListControl);

		//	if (fieldName.Equals("SystemEntityId"))
		//	{
		//		var applicationdata =  Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile.AuditId);
		//		UIHelper.LoadDropDown(applicationdata, dropDownListControl, DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.EntityName,
		//		   DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
		//	}

		//}

		//protected override void OnInit(EventArgs e)
		//{

		//	base.OnInit(e);

		//	PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AuditHistory;
		//	PrimaryEntityKey = "AuditHistory";
		//	FolderLocationFromRoot = "TestData/";
			
		//}

		//#endregion

	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.BaseUI;
using Framework.UI.Web.BaseClasses;
namespace ApplicationContainer.UI.Web.EventNotification.NotificationRegistrar.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

		#region variables

		public NotificationRegistrarDataModel SearchParameters
		{
			get
			{
				var data = new NotificationRegistrarDataModel();

				data.NotificationEventTypeId = SearchFilterControl.GetParameterValueAsInt(NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId);
				//data.NotificationRegistrarId = GetParameterValueAsInt(NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId);
				data.NotificationPublisherId = SearchFilterControl.GetParameterValueAsInt(NotificationRegistrarDataModel.DataColumns.NotificationPublisherId);

				
				//GroupBy = GetParameterValue("GroupBy");

				//SubGroupBy = GetParameterValue("SubGroupBy");

				return data;
			}
		}

		#endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("NotificationPublisherId"))
			{
				var applicationdata = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationdata, dropDownListControl,
									StandardDataModel.StandardDataColumns.Name,
									NotificationPublisherDataModel.DataColumns.NotificationPublisherId);

			}
			if (fieldName.Equals("NotificationEventTypeId"))
			{
				var applicationdata = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationdata, dropDownListControl,
									StandardDataModel.StandardDataColumns.Name,
									NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId);
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
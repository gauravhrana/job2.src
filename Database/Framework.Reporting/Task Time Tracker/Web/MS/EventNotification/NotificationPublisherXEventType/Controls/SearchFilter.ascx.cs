using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{

		#region variables			

		public NotificationPublisherXEventTypeDataModel SearchParameters
		{
			get
			{
				var data = new NotificationPublisherXEventTypeDataModel();

				data.NotificationPublisherId = GetParameterValueAsInt(NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherId);

				data.NotificationEventTypeId = GetParameterValueAsInt(NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId);												

				return data;
			}
		}

		#endregion

		#region private methods

		public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		{
			if (fieldName.Equals("NotificationPublisherId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetNotificationPublisherList", "Name", "NotificationPublisherId", plcControlHolder);
			}
			if (fieldName.Equals("NotificationEventTypeId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetNotificationEventTypeList", "Name", "NotificationEventTypeId", plcControlHolder);
			}

			return string.Empty;

		}
		
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey				= "NotificationPublisherXEventType";
			FolderLocationFromRoot			= "NotificationPublisherXEventType";
			PrimaryEntity					= Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;

			SearchActionBarCore				= oSearchActionBar;
			SearchParametersRepeaterCore	= SearchParametersRepeater;
		}

		#endregion

	}
}
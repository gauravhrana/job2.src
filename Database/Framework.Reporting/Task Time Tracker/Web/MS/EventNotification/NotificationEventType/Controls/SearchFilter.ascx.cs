using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationEventType.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables 

       public NotificationEventTypeDataModel SearchParameters
        {
            get
            {
                var data = new NotificationEventTypeDataModel();

				data.Name = GetParameterValue(StandardDataModel.StandardDataColumns.Name);				

				data.ApplicationId = GetParameterValueAsInt(NotificationEventTypeDataModel.DataColumns.ApplicationId);

				GroupBy = GetParameterValue("GroupBy");

				SubGroupBy = GetParameterValue("SubGroupBy");

                return data;
            }
        }

        #endregion

	   #region Methods

	   public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
	   {
		   base.LoadDropDownListSources(fieldName, dropDownListControl);

		   if (fieldName.Equals("ApplicationId"))
		   {
			   var applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationdata, dropDownListControl,
									StandardDataModel.StandardDataColumns.Name,
									BaseDataModel.BaseDataColumns.ApplicationId);

		   }
	   }

	   #endregion     

	   #region Events

	   protected void Page_Load(object sender, EventArgs e)
	   {
		   base.OnLoad(e);
	   }

	   protected override void OnInit(EventArgs e)
	   {
		   base.OnInit(e);

		   PrimaryEntityKey = "NotificationEventType";
		   FolderLocationFromRoot = "EventNotification/NotificationEventType";
		   PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationEventType;

		   SearchActionBarCore = oSearchActionBar;
		   SearchParametersRepeaterCore = SearchParametersRepeater;
	   }

	   #endregion
    }
}
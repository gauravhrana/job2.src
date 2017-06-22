using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityHistory.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables        

        private int functionalityActiveStatusId;

        public int FunctionalityActiveStatusId
        {
            get
            {
                return functionalityActiveStatusId;
            }
            set
            {
                functionalityActiveStatusId = value;
            }
        }

        public FunctionalityHistoryDataModel SearchParameters
        {
            get
            {
                var data = new FunctionalityHistoryDataModel();

                SearchFilterControl.GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

                data.FunctionalityId = SearchFilterControl.GetParameterValueAsInt(FunctionalityHistoryDataModel.DataColumns.FunctionalityId);

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
    }
}
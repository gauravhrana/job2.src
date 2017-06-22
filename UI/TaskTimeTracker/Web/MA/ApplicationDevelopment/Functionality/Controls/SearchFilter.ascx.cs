using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables     

        private int functionalityActiveStatusId;
		private int numberOfImages;

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

		
        public FunctionalityDataModel SearchParameters
        {
            get
            {
                var data = new FunctionalityDataModel();
								
				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

                return data;
            }
        }

		public override void LoadListBoxSources(string fieldName, ListBox lstBoxControl)
		{
			base.LoadListBoxSources(fieldName, lstBoxControl);
				lstBoxControl.Items.Add(new ListItem("All", "-1"));
				var functionalityStatusData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
                functionalityStatusData = functionalityStatusData.OrderBy( x => x.Name).ToList();
                UIHelper.LoadDropDown(functionalityStatusData, lstBoxControl,
					StandardDataModel.StandardDataColumns.Name,
					FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);

				lstBoxControl.SelectedValue = "-1";
			

		}


        #endregion

        #region Events


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
			
			BaseSearchFilterControl                         = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
       
		#region variables
        
        public EntityOwnerDataModel SearchParameters
        {
            get
            {
                var data = new EntityOwnerDataModel();

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

	}
}
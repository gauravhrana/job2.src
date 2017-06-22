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
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Question.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables

        public QuestionDataModel SearchParameters
        {
            get
            {
                var data = new QuestionDataModel();

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
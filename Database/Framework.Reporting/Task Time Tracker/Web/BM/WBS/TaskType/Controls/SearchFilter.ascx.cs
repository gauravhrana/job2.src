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
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.BaseUI;
using DataModel.TaskTimeTracker.Task;

namespace ApplicationContainer.UI.Web.WBS.TaskType.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public TaskTypeDataModel SearchParameters
        {
            get
            {
                var data = new TaskTypeDataModel();

                SearchFilterControl.SetSearchParameters(data);

                return data;
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
        }

        #endregion
    }
}
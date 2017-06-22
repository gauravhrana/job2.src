using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using ApplicationContainer.UI.Web.BaseUI;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.WBS.TaskStatusType.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public TaskStatusTypeDataModel SearchParameters
        {
            get
            {
                var data = new TaskStatusTypeDataModel();

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
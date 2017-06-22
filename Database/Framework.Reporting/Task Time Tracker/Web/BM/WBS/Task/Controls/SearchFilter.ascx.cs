using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.WBS.Task.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        public TaskDataModel SearchParameters
        {
            get
            {
                var data = new TaskDataModel();

                SearchFilterControl.SetSearchParameters(data);

                CommonSearchParameters();

                return data;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BaseSearchFilterControl = SearchFilterControl;
            SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }

    }
}
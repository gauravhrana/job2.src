using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.TasksAndWorkflow.TaskRun.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public TaskRunDataModel SearchParameters
        {
            get
            {
                var data = new TaskRunDataModel();

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

		   BaseSearchFilterControl						   = SearchFilterControl;
		   SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
	   }

	   #endregion

    }
}
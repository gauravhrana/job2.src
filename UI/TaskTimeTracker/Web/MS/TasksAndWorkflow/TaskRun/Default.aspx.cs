﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.TasksAndWorkflow.TaskRun
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

        #region private methods

        protected override DataTable GetData()
        {
			var dt = Framework.Components.TasksAndWorkflow.TaskRunDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity				= Framework.Components.DataAccess.SystemEntity.TaskRun;
            PrimaryEntityKey			= "TaskRun";
            PrimaryEntityIdColumn		= "TaskRunId";

            MasterPageCore				= Master;
            SubMenuCore					= Master.SubMenuObject;
            BreadCrumbObject			= Master.BreadCrumbObject;

			SearchFilterCore			= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey); 
            GroupListCore				= oGroupList;

            IsDynamicSearchControl		= true;

            VisibilityManagerCore		= oVC;
        }      

        #endregion

    }
}
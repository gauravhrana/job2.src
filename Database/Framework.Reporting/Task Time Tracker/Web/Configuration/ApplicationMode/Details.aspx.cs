﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.ApplicationMode
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.AddTab("ApplicationMode", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("RunTimeFeature", listControl);

			listControl.Setup("RunTimeFeature", String.Empty, "RunTimeFeatureId", setId, true, GetData, GetRunTimeFeatureColumns, "ApplicationMode");
            listControl.SetSession("true");

            tabControl.Setup("ApplicationModeDetailsView");

            return tabControl;
        }

        private string[] GetRunTimeFeatureColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns("DBColumns", Framework.Components.DataAccess.SystemEntity.RunTimeFeature, SessionVariables.RequestProfile);
        }

		private DataTable GetData(string key)
		{
			return GetRunTimeFeatureData(int.Parse(key));
		}

        private DataTable GetRunTimeFeatureData(int applicationModeId)
        {
            
            var data               = new ApplicationModeXRunTimeFeatureDataModel();
            data.ApplicationModeId = applicationModeId;

            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.ApplicationModeXRunTimeFeatureDataManager.Search(data, SessionVariables.RequestProfile);

            var runTimeFeaturedt = TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = runTimeFeaturedt.Clone();

            var runTimeIdColumn = RunTimeFeatureDataModel.DataColumns.RunTimeFeatureId;

            foreach (DataRow row in dt.Rows)
            {
                var rows = runTimeFeaturedt.Select(runTimeIdColumn + " = " + row[runTimeIdColumn]);
                resultdt.ImportRow(rows[0]);
            }

            return resultdt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMode;
			PrimaryEntityKey = "ApplicationMode";
			DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationMode", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}
		
        #endregion

    }
}
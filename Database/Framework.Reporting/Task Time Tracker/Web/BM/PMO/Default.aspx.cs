﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.Core;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using System.Web.Services;
using Framework.Components.UserPreference;
using Framework.Components.DataAccess;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.PMO
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageBasePage
	{
		
		#region Page Methods

		/// <summary>
		/// This method is being called from  SliederMenu.ascx control for updating User Preference value for "SearchFilterGridLines" key.
		/// </summary>
		/// <param name="value"></param>
		[WebMethod]
		public static void UpdateUserPreferenceForGridLines(string value)
		{
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.SearchFilterGridLinesKey, value);
		}

		[WebMethod]
		public static void UpdateSearchControlParameterVisibility(string name, string category)
		{
			PerferenceUtility.UpdateUserPreference(category, name + "Visibility", "false");
		}

		#endregion

		#region Events

        protected override void OnPreInit(EventArgs e)
        {
            ApplicationCommon.ResetApplicationCache("PMT");

            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionVariables.SiteMenuData = null;
            SessionVariables.UserPreferedMenuData = MenuHelper.GetUserPreferedMenu();


			//var searchableColumns = new List<string>() { "TaskNoteId", "ApplicationId", "Name", "Description" };

			//TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.DeploySearchProcedure(searchableColumns);

            Sync();

            SyncMenuModuleWise();

			//ScheduleDetailActivityCategoryDataManager.AddInitialData(SessionVariables.RequestProfile.ApplicationId, SessionVariables.RequestProfile);
		}

		#endregion

        private void Sync()
        {
			//// sync

			//Framework.Components.LogAndTrace.UserLoginStatusDataManager.Sync(100071, 100, SessionVariables.SystemRequestProfile);
			//LanguageDataManager.Sync(100071, 100047, SessionVariables.RequestProfile);
			//TimeZoneDataManger.Sync(100071, 100047, SessionVariables.RequestProfile);
			//CountryDataManager.Sync(100071, 100047, SessionVariables.RequestProfile);
			//UserPreferenceDataTypeDataManager.Sync(100072, 100047, SessionVariables.SystemRequestProfile);
			//UserPreferenceKeyDataManager.Sync(100072, 100047, SessionVariables.SystemRequestProfile);
			//ApplicationModeDataManager.Sync(100072, 100047, SessionVariables.SystemRequestProfile);
			//Framework.Components.ApplicationUser.ApplicationRoleDataManager.Sync(100071, 100047, SessionVariables.SystemRequestProfile);
			//FieldConfigurationModeCategoryDataManager.Sync(100072, 100047, SessionVariables.SystemRequestProfile);
			//FieldConfigurationModeDataManager.Sync(100072, 100047, SessionVariables.SystemRequestProfile);
		  //FieldConfigurationModeAccessModeDataManager.Sync(100072, 100047, SessionVariables.SystemRequestProfile);
			//MenuCategoryDataManager.Sync(100072, 100047, SessionVariables.SystemRequestProfile);
			//FieldConfigurationDataManager.Sync(100072, 100, SessionVariables.SystemRequestProfile, 10071);


			//MenuDataManager.Sync(100072, 100, SessionVariables.SystemRequestProfile, 10083);



			///ApplicationModeXFieldConfigurationModeDataManager.Sync(100072, 100, SessionVariables.SystemRequestProfile);

			//Framework.Components.ApplicationUser.ApplicationRoleDataManager.Sync(100072, 100, SessionVariables.SystemRequestProfile);




			//Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.Sync(100072, 100, SessionVariables.RequestProfile);
			////Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.Sync(100072, 100, SessionVariables.RequestProfile);
			////Framework.Components.ReleaseLog.ReleaseFeatureDataManager.Sync(100072, 100, SessionVariables.RequestProfile);
			////TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.Sync(100072, 100, SessionVariables.RequestProfile);
			//Framework.Components.ReleaseLog.ReleaseLogDataManager.Sync(100072, 100, SessionVariables.RequestProfile);
			//Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.Sync(100072, 100, SessionVariables.SystemRequestProfile);

        }

        private void SyncMenuModuleWise()
        {

		   // MenuDisplayNameDataManager.SyncWithMenu(200);

			// Legal AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100072, "AA", 100071, SessionVariables.RequestProfile);

			// PMT AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100, "AA", 100071, SessionVariables.RequestProfile);

			// TE AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100047, "AA", 100071, SessionVariables.RequestProfile);

			// DayCare AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 200, "AA", 100071, SessionVariables.RequestProfile); 

			// Capital Markets AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100068, "AA", 100071, SessionVariables.RequestProfile); 

			// Reference Data AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100070, "AA", 100071, SessionVariables.RequestProfile); 

			// Prototype AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100067, "AA", 100071, SessionVariables.RequestProfile); 

			// Product Management Tracker AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100065, "AA", 100071, SessionVariables.RequestProfile); 

			// SA AA Module Menu Sync
			//MenuDataManager.SyncModule("AA", 100066, "AA", 100071, SessionVariables.RequestProfile);



          //  MenuDataManager.SyncModule("AA", 100071, "AA", 100, SessionVariables.RequestProfile);

        }

        private void Test()
        {
            //var properties = typeof(DataModel.DayCare.SampleNonStdEntity2DataModel).GetProperties();
            //foreach (var propInfo in properties)
            //{
            //    Response.Write(propInfo.PropertyType.ToString() + "\n");
            //}
        }

    }
}

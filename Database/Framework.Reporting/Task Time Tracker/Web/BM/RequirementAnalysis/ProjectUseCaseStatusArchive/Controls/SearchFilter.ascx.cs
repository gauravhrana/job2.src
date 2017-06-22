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
using TaskTimeTracker.Components.BusinessLayer.DataModel.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatusArchive.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {
        #region variables

        public ProjectUseCaseStatusArchiveDataModel SearchParameters
        {
            get
            {
                var data = new ProjectUseCaseStatusArchiveDataModel();

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                   ProjectUseCaseStatusArchiveDataModel.DataColumns.Project + "Visibility", SettingCategory)
                   && CheckAndGetFieldValue(ProjectUseCaseStatusArchiveDataModel.DataColumns.Project) != "")
                {
                    data.Project = CheckAndGetFieldValue(ProjectUseCaseStatusArchiveDataModel.DataColumns.Project).ToString();
                }

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                   ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase + "Visibility", SettingCategory)
                   && CheckAndGetFieldValue(ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase) != "")
                {
                    data.UseCase = CheckAndGetFieldValue(ProjectUseCaseStatusArchiveDataModel.DataColumns.UseCase).ToString();
                }

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                   ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus + "Visibility", SettingCategory)
                   && CheckAndGetFieldValue(ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus) != "")
                {
                    data.ProjectUseCaseStatus = CheckAndGetFieldValue(ProjectUseCaseStatusArchiveDataModel.DataColumns.ProjectUseCaseStatus).ToString();
                }

                return data;
            }

        }


        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectUseCaseStatusArchive;
            PrimaryEntityKey = "ProjectUseCaseStatusArchive";
            FolderLocationFromRoot = "ProjectUseCaseStatusArchive";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion
    }
}
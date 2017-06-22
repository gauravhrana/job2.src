using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Feature;

namespace ApplicationContainer.UI.Web.MilestoneXFeature.Controls
{
	public partial class SearchFilter : ControlSearchFilter
    {
        #region variables
		
        public MilestoneXFeatureDataModel SearchParameters
        {
			get
			{
				var data = new MilestoneXFeatureDataModel();

				data.MilestoneId = GetParameterValueAsInt(MilestoneXFeatureDataModel.DataColumns.MilestoneId);

				data.MilestoneFeatureStateId = GetParameterValueAsInt(MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId);

				data.FeatureId = GetParameterValueAsInt(MilestoneXFeatureDataModel.DataColumns.FeatureId);								

				return data;
			}
			
        }
		
        #endregion

        #region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);
			
			if (fieldName.Equals("MilestoneId"))
			{
                var milestoneData = MilestoneDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(milestoneData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, MilestoneDataModel.DataColumns.MilestoneId);				
			}

			if (fieldName.Equals("MilestoneFeatureStateId"))
			{
                var milestoneFeatureStateData = MilestoneFeatureStateDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(milestoneFeatureStateData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId);
			}

			if (fieldName.Equals("FeatureId"))
			{
                var featureData = FeatureDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(featureData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, FeatureDataModel.DataColumns.FeatureId);
			}
		}
				
		#endregion

		#region Events		
		
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "MilestoneXFeature";
			FolderLocationFromRoot = "MilestoneXFeature";
			PrimaryEntity = SystemEntity.MilestoneXFeature;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}


		#endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.Feature.FeatureGroupXFeature
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{
		#region Methods

		private DataTable GetFeatureGroupList()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFeatureGroups(int FeatureId)
		{
			var id = Convert.ToInt32(drpFeature.SelectedValue);
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.GetByFeature(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByFeature(int FeatureId, List<int> FeatureGroupIds)
		{
			var id = Convert.ToInt32(drpFeature.SelectedValue);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.DeleteByFeature(id, SessionVariables.RequestProfile);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.CreateByFeature(id, FeatureGroupIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetFeatureList()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFeatures(int FeatureGroupId)
		{
			var id = Convert.ToInt32(drpFeatureGroup.SelectedValue);
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.GetByFeatureGroup(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByFeatureGroup(int FeatureGroupId, List<int> FeatureIds)
		{
			var id = Convert.ToInt32(drpFeatureGroup.SelectedValue);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.DeleteByFeatureGroup(id, SessionVariables.RequestProfile);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.CreateByFeatureGroup(id, FeatureIds.ToArray(), SessionVariables.RequestProfile);
		} 

		private void BindLists()
		{
			drpFeature.DataSource = GetFeatureList();
			drpFeature.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpFeature.DataValueField = FeatureDataModel.DataColumns.FeatureId;
			drpFeature.DataBind();

			drpFeatureGroup.DataSource = GetFeatureGroupList();
			drpFeatureGroup.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpFeatureGroup.DataValueField = FeatureGroupDataModel.DataColumns.FeatureGroupId;
			drpFeatureGroup.DataBind();
		}

		#endregion

		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "FeatureGroupXFeatureDefaultView";
			
		}

		protected override void OnInit(EventArgs e)
		{

			BindLists();

			BucketOfFeature.ConfigureBucket("Feature", 1, GetFeatureList, GetAssociatedFeatures, SaveByFeatureGroup);
			BucketOfFeatureGroup.ConfigureBucket("FeatureGroup", 1, GetFeatureGroupList, GetAssociatedFeatureGroups, SaveByFeature);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByFeatureGroup")
			{
				dynFeatureGroup.Visible = true;
				dynFeature.Visible = false;
				BucketOfFeature.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByFeature")
			{
				dynFeatureGroup.Visible = false;
				dynFeature.Visible = true;
				BucketOfFeatureGroup.ReloadBucketList();
			}
		}

		protected void drpFeature_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfFeatureGroup.ReloadBucketList();
		}

		protected void drpFeatureGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfFeature.ReloadBucketList();
		}

		#endregion
	}
}
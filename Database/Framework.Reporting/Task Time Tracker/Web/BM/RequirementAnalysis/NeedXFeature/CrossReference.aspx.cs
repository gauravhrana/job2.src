using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Feature;

namespace ApplicationContainer.UI.Web.NeedXFeature
{
	public partial class CrossReference : Framework.UI.Web.BaseClasses.PageBasePage
	{

		#region Methods

		private DataTable GetFeatureList()
		{
            var dt = FeatureDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFeatures(int needId)
		{
			var id = Convert.ToInt32(drpNeed.SelectedValue);
			var dt = NeedXFeatureDataManager.GetByNeed(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByNeed(int needId, List<int> featureIds)
		{
			var id = Convert.ToInt32(drpNeed.SelectedValue);
			NeedXFeatureDataManager.DeleteByNeed(id, SessionVariables.RequestProfile.AuditId);
            NeedXFeatureDataManager.Create(id, featureIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetNeedList()
		{
			var dt = NeedDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedNeeds(int featureId)
		{
			var id = Convert.ToInt32(drpFeature.SelectedValue);
			var dt = NeedXFeatureDataManager.GetByFeature(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByFeature(int FeatureId, List<int> needIds)
		{
			var id = Convert.ToInt32(drpFeature.SelectedValue);
			NeedXFeatureDataManager.DeleteByFeature(id, SessionVariables.RequestProfile.AuditId);
            NeedXFeatureDataManager.CreateByFeature(id, needIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpNeed.DataSource = GetNeedList();
			drpNeed.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpNeed.DataValueField = NeedDataModel.DataColumns.NeedId;
			drpNeed.DataBind();

			drpFeature.DataSource = GetFeatureList();
			drpFeature.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpFeature.DataValueField = FeatureDataModel.DataColumns.FeatureId;
			drpFeature.DataBind();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			BindLists();

			BucketOfNeed.ConfigureBucket("Need", 1, GetNeedList, GetAssociatedNeeds, SaveByFeature);
			BucketOfFeature.ConfigureBucket("Feature", 1, GetFeatureList, GetAssociatedFeatures, SaveByNeed);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByFeature")
			{
				dynFeature.Visible = true;
				dynNeed.Visible = false;
				BucketOfNeed.ReloadBucketList();

			}
			else if (drpSelection.SelectedValue == "ByNeed")
			{
				dynFeature.Visible = false;
				dynNeed.Visible = true;
				BucketOfFeature.ReloadBucketList();
			}
		}

		protected void drpNeed_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfFeature.ReloadBucketList();
		}

		protected void drpFeature_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfNeed.ReloadBucketList();
		}

		#endregion

	}
}
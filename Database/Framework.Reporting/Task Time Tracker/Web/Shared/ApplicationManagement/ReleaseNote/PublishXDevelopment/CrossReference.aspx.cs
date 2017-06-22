using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.PublishXDevelopment
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{
		#region Methods

		private DataTable GetChildReleaseLogDetailList()
		{
			var categorydata = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetList(SessionVariables.RequestProfile);
			var rows = categorydata.Select(StandardDataModel.StandardDataColumns.Name + " = 'Development'");

			var data = new ReleaseLogDetailDataModel();
			data.ReleasePublishCategoryId = (int)(rows[0].ItemArray[0]);

			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetChildReleaseLogDetailItemList(data, SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedChildReleaseLogDetailLists(int publishId)
		{
			var id = Convert.ToInt32(drpParentReleaseLogDetail.SelectedValue);
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailMappingDataManager.GetByParentReleaseLogDetail(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByParentReleaseLogDetail(int publishId, List<int> ChildReleaseLogDetailIds)
		{
			var id = Convert.ToInt32(drpParentReleaseLogDetail.SelectedValue);
			Framework.Components.ReleaseLog.ReleaseLogDetailMappingDataManager.DeleteByParentReleaseLogDetail(id, SessionVariables.RequestProfile);
			Framework.Components.ReleaseLog.ReleaseLogDetailMappingDataManager.CreateByParentReleaseLogDetail(id, ChildReleaseLogDetailIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetParentReleaseLogDetailList()
		{
			var categorydata = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetList(SessionVariables.RequestProfile);
			var rows = categorydata.Select(StandardDataModel.StandardDataColumns.Name + " = 'Publish'");

			var data = new ReleaseLogDetailDataModel();
			data.ReleasePublishCategoryId = (int)(rows[0].ItemArray[0]);

			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetParentReleaseLogDetailItemList(data, SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedParentReleaseLogDetailLists(int ChildReleaseLogDetailId)
		{
			var id = Convert.ToInt32(drpChildReleaseLogDetail.SelectedValue);
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailMappingDataManager.GetByChildReleaseLogDetail(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByChildReleaseLogDetail(int ChildReleaseLogDetailId, List<int> publishIds)
		{
			var id = Convert.ToInt32(drpChildReleaseLogDetail.SelectedValue);
			Framework.Components.ReleaseLog.ReleaseLogDetailMappingDataManager.DeleteByChildReleaseLogDetail(id, SessionVariables.RequestProfile);
			Framework.Components.ReleaseLog.ReleaseLogDetailMappingDataManager.CreateByChildReleaseLogDetail(id, publishIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpParentReleaseLogDetail.DataSource = GetParentReleaseLogDetailList();
			drpParentReleaseLogDetail.DataTextField = ReleaseLogDetailDataModel.DataColumns.Description;
			drpParentReleaseLogDetail.DataValueField = ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId;
			drpParentReleaseLogDetail.DataBind();

			drpChildReleaseLogDetail.DataSource = GetChildReleaseLogDetailList();
			drpChildReleaseLogDetail.DataTextField = ReleaseLogDetailDataModel.DataColumns.Description;
			drpChildReleaseLogDetail.DataValueField = ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId;
			drpChildReleaseLogDetail.DataBind();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			BindLists();

			BucketOfChildReleaseLogDetail.ConfigureBucket("ChildReleaseLogDetail", 1, GetChildReleaseLogDetailList, GetAssociatedChildReleaseLogDetailLists, SaveByParentReleaseLogDetail);
			BucketOfParentReleaseLogDetail.ConfigureBucket("ParentReleaseLogDetail", 1, GetParentReleaseLogDetailList, GetAssociatedParentReleaseLogDetailLists, SaveByChildReleaseLogDetail);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByChildReleaseLogDetail")
			{
				dynChildReleaseLogDetail.Visible = true;
				dynParentReleaseLogDetail.Visible = false;
				BucketOfParentReleaseLogDetail.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByParentReleaseLogDetail")
			{
				dynChildReleaseLogDetail.Visible = false;
				dynParentReleaseLogDetail.Visible = true;
				BucketOfChildReleaseLogDetail.ReloadBucketList();
			}
		}

		protected void drpParentReleaseLogDetail_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfChildReleaseLogDetail.ReloadBucketList();
		}

		protected void drpChildReleaseLogDetail_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfParentReleaseLogDetail.ReloadBucketList();
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var bcControl = this.Master.BreadCrumbObject;
			var sbm = this.Master.SubMenuObject;

			SettingCategory = "ReleaseLogDetailMappingDefaultView";
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup("");
			bcControl.GenerateMenu();

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

		}


		#endregion

	}
}
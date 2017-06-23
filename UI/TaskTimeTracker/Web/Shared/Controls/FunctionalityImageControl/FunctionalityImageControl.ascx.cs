using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using System.Drawing;
using Shared.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Controls.FunctionalityImageControl
{
	public partial class FunctionalityImageControl : BaseControl
	{		

		#region Methods

		protected int functionalityId;

		public int FunctionalityId
		{
			get
			{
				return functionalityId;
			}
			set
			{
				functionalityId = value;
			}
		}

		private List<FunctionalityDataModel> GetFunctionalityList()
		{
			var dt = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFunctionalitys(int functionalityImageId)
		{
			var id = Convert.ToInt32(drpFunctionalityImage.SelectedValue);
			var dt = FunctionalityXFunctionalityImageDataManager.GetByFunctionalityImage(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByFunctionalityImage(int functionalityImageId, List<int> functionalityIds)
		{
			var id = Convert.ToInt32(drpFunctionalityImage.SelectedValue);
			FunctionalityXFunctionalityImageDataManager.DeleteByFunctionalityImage(id, SessionVariables.RequestProfile);
			if(txtKeyString.Text != "" && txtTitle.Text != "" && txtDescription.Text != "" && txtSortOrder.Text != "" )
				FunctionalityXFunctionalityImageDataManager.CreateByFunctionalityImage(id, functionalityIds.ToArray(), txtKeyString.Text, txtTitle.Text, txtDescription.Text, int.Parse(txtSortOrder.Text), SessionVariables.RequestProfile);
		}

		private List<FunctionalityImageDataModel> GetFunctionalityImageList()
		{
			var dt = FunctionalityImageDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFunctionalityImages(int functionalityId)
		{
			var id = Convert.ToInt32(drpFunctionality.SelectedValue);
			var dt = FunctionalityXFunctionalityImageDataManager.GetByFunctionality(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByFunctionality(int functionalityId, List<int> functionalityImageIds)
		{
			var id = Convert.ToInt32(drpFunctionality.SelectedValue);
			FunctionalityXFunctionalityImageDataManager.DeleteByFunctionality(id, SessionVariables.RequestProfile);
			if((txtKeyString.Text!="") &&(txtTitle.Text!="") &&(txtDescription.Text!="") && (txtSortOrder.Text!=""))
				FunctionalityXFunctionalityImageDataManager.CreateByFunctionality(id, functionalityImageIds.ToArray(), txtKeyString.Text, txtTitle.Text, txtDescription.Text, int.Parse(txtSortOrder.Text), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpFunctionalityImage.DataSource = GetFunctionalityImageList();
			drpFunctionalityImage.DataTextField = FunctionalityImageDataModel.DataColumns.Title;
			drpFunctionalityImage.DataValueField = FunctionalityImageDataModel.DataColumns.FunctionalityImageId;
			drpFunctionalityImage.DataBind();

			drpFunctionality.DataSource = GetFunctionalityList();
			drpFunctionality.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpFunctionality.DataValueField = FunctionalityDataModel.DataColumns.FunctionalityId;
			drpFunctionality.DataBind();
		}


		private List<FunctionalityImageDataModel> GetFunctionalityLists()
		{
			var dt = FunctionalityImageDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFunctionality(int functionalityId)
		{
			var dt = FunctionalityXFunctionalityImageDataManager.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveFunctionalityXFunctionalityImage(int functionalityId, List<int> functionalityImageIds)
		{
			FunctionalityXFunctionalityImageDataManager.DeleteByFunctionality(functionalityId, SessionVariables.RequestProfile);
			if (txtKeyString.Text != "" && txtTitle.Text != "" && txtDescription.Text != "" && txtSortOrder.Text !="")
				FunctionalityXFunctionalityImageDataManager.CreateByFunctionality(functionalityId, functionalityImageIds.ToArray(), txtKeyString.Text, txtTitle.Text, txtDescription.Text, int.Parse(txtSortOrder.Text), SessionVariables.RequestProfile);
		}

		#endregion

		#region Events		

		protected override void OnInit(EventArgs e)
		{

			BindLists();
			if (FunctionalityId==0)
			{
				BucketOfFunctionalityImage.ConfigureBucket("FunctionalityImage", 1, GetFunctionalityImageList, GetAssociatedFunctionalityImages, SaveByFunctionality);
				var lb1 = (Label)BucketOfFunctionalityImage.FindControl("lblPossibleTitle");
				lb1.Style.Add("font-weight", "bold");
				var lb2 = (Label)BucketOfFunctionalityImage.FindControl("lblCurrentTitle");
				lb2.Style.Add("font-weight", "bold");
				BucketOfFunctionality.ConfigureBucket("Functionality", 1, GetFunctionalityList, GetAssociatedFunctionalitys, SaveByFunctionalityImage);
			}
			else
			{
				BucketOfFunctionalityImage.ConfigureBucket("FunctionalityImage", FunctionalityId, GetFunctionalityLists, GetAssociatedFunctionality, SaveFunctionalityXFunctionalityImage);
				var lb1 = (Label)BucketOfFunctionalityImage.FindControl("lblPossibleTitle");
				lb1.Style.Add("font-weight", "bold");
				var lb2 = (Label)BucketOfFunctionalityImage.FindControl("lblCurrentTitle");
				lb2.Style.Add("font-weight", "bold");
			}

		}

		protected void Page_Load(object sender, EventArgs e)
		{
			imgApplicationUserImage.Visible = false;
			var lb = (ListBox)BucketOfFunctionalityImage.FindControl("lstTarget");
			lb.AutoPostBack = true;
			lb.SelectedIndexChanged += new EventHandler(lb_ShowImage);

			var selected = lb.GetSelectedIndices().ToList();
			var selectedValues = (from c in selected
								  select lb.Items[c].Value).ToList();

			if (selectedValues.Count > 0)
			{
				imgApplicationUserImage.Visible = true;
				imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + lb.SelectedValue;
				imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + lb.SelectedValue;
			}
		}

		protected void lb_ShowImage(object sender, EventArgs e)
		{
			var lb = (ListBox)BucketOfFunctionalityImage.FindControl("lstTarget");
			imgApplicationUserImage.Visible = true;
			imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + lb.SelectedValue;
			imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + lb.SelectedValue;
						
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByFunctionality")
			{
				dynFunctionality.Visible = true;
				dynFunctionalityImage.Visible = false;
				BucketOfFunctionalityImage.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByFunctionalityImage")
			{
				dynFunctionality.Visible = false;
				dynFunctionalityImage.Visible = true;
				BucketOfFunctionality.ReloadBucketList();
			}
		}

		protected void drpFunctionalityImage_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfFunctionality.ReloadBucketList();
		}

		protected void drpFunctionality_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfFunctionalityImage.ReloadBucketList();
		}

		#endregion
	}
}
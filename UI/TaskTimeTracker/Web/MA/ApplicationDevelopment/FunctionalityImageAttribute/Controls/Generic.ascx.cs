using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageAttribute.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{
		#region properties

		public int? FunctionalityImageId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtFunctionalityImageId.Text.Trim());
				else
					return int.Parse(drpFunctionalityImageList.SelectedItem.Value);
			}
			set
			{
				txtFunctionalityImageId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new FunctionalityImageAttributeDataModel();

			data.FunctionalityImageAttributeId = SystemKeyId;
			data.FunctionalityImageId = FunctionalityImageId;
			data.Name = Name;
			data.SortOrder = SortOrder;
			data.Description = Description;

			if (action == "Insert")
			{
				if(!TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.FunctionalityImageAttributeId;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var functionalityImageData = FunctionalityImageDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(functionalityImageData, drpFunctionalityImageList,
				FunctionalityImageDataModel.DataColumns.Title,
				FunctionalityImageDataModel.DataColumns.FunctionalityImageId);
						
			if (isTesting)
			{

				drpFunctionalityImageList.AutoPostBack = true;

				if (drpFunctionalityImageList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtFunctionalityImageId.Text.Trim()))
					{
						drpFunctionalityImageList.SelectedValue = txtFunctionalityImageId.Text;
					}
					else
					{
						txtFunctionalityImageId.Text = drpFunctionalityImageList.SelectedItem.Value;
					}
				}

				txtFunctionalityImageId.Visible = true;
				
			}
			else
			{

				if (!string.IsNullOrEmpty(txtFunctionalityImageId.Text.Trim()))
				{
					drpFunctionalityImageList.SelectedValue = txtFunctionalityImageId.Text;
				}
				
				txtFunctionalityImageId.Visible = false;
				
			}
		}

		public override void SetId(int setId, bool chkFunctionalityImageAttributeId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkFunctionalityImageAttributeId);
			txtFunctionalityImageAttributeId.Enabled = chkFunctionalityImageAttributeId;
			//txtDescription.Enabled = !chkFunctionalityImageAttributeId;
			//txtName.Enabled = !chkFunctionalityImageAttributeId;
			//txtSortOrder.Enabled = !chkFunctionalityImageAttributeId;
		}

        public void LoadData(int functionalityImageAttributeId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new FunctionalityImageAttributeDataModel();
            data.FunctionalityImageAttributeId = functionalityImageAttributeId;

            // get data
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtFunctionalityImageId.Text = item.FunctionalityImageId.ToString();
            txtDescription.Text = item.Description.ToString();
            txtName.Text = item.Name;
            txtSortOrder.Text = item.SortOrder.ToString();
            imgApplicationUserImage.Visible = true;
            imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + item.FunctionalityImageId;
            imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + item.FunctionalityImageId;

            if (!showId)
            {
                txtFunctionalityImageAttributeId.Text = item.FunctionalityImageAttributeId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, functionalityImageAttributeId, PrimaryEntityKey);
            }
            else
            {
                txtFunctionalityImageAttributeId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

		protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityImageAttributeDataModel();
            
            FunctionalityImageId = data.FunctionalityImageId;
			SetData(data);
        }

		public void SetData(FunctionalityImageAttributeDataModel data)
		{
			SystemKeyId = data.FunctionalityImageAttributeId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetupDropdown();
			}
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblFunctionalityImageAttributeId.Visible = isTesting;
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "FunctionalityImageAttribute";
            FolderLocationFromRoot = "/Shared/QualityAssurance";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageAttribute;

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityImageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtFunctionalityImageAttributeId;
			CoreControlName = txtName;
			CoreControlDescriptionKendoEditor = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;	
        }

		protected void drpFunctionalityImageList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtFunctionalityImageId.Text =drpFunctionalityImageList.SelectedItem.Value;
			imgApplicationUserImage.Visible = true;
			imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + drpFunctionalityImageList.SelectedItem.Value.ToString();
			imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + drpFunctionalityImageList.SelectedItem.Value.ToString();
		}

		#endregion
	}
}
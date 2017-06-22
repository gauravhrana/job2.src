using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.IO;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster.Controls
{
	public partial class Generic : ControlGenericStandard
	{

        #region properties

		protected string largeImage = "";
		protected string smallImage = "";

		public int? ApplicationUserProfileImageMasterId
		{
			get
			{
				if (txtApplicationUserProfileImageMasterId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationUserProfileImageMasterId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtApplicationUserProfileImageMasterId.Text);
				}
			}
			set
			{
				txtApplicationUserProfileImageMasterId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Title
		{
			get
			{
                return txtTitle.Text.Trim();
			}
			set
			{
				txtTitle.Text = value ?? String.Empty;
			}
		}

        public byte[] Image
		{
			get
			{
				if (FileUploadImage.HasFile)
				{
					Stream imgdatastream = FileUploadImage.PostedFile.InputStream;
					int imgdatalen = FileUploadImage.PostedFile.ContentLength;
					byte[] imgdata = new byte[imgdatalen];
					imgdatastream.Read(imgdata, 0, imgdatalen);
					return imgdata;
				}
				else
				{
					byte[] imgdata = null;
					var data = new ApplicationUserProfileImageMasterDataModel();
					data.ApplicationUserProfileImageMasterId = ApplicationUserProfileImageMasterId;
					var dt = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetDetails(data, SessionVariables.RequestProfile);
					if (dt.Rows.Count > 0)
					{
						imgdata = ((byte[])(dt.Rows[0][ApplicationUserProfileImageMasterDataModel.DataColumns.Image]));
					}
					return imgdata;
				}
			}
			set
			{

			}
		}
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationUserProfileImageMasterDataModel();

			data.ApplicationUserProfileImageMasterId = ApplicationUserProfileImageMasterId;
			data.ApplicationId = ApplicationId;
			data.Image = Image;
			data.Title = Title;
			
			if (action == "Insert")
			{
				var dt = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 0)
				{
					Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.ApplicationUserProfileImageMasterId;
		}

		public override void SetId(int setId, bool chkApplicationUserProfileImageMasterId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationUserProfileImageMasterId);
			txtApplicationUserProfileImageMasterId.Enabled = chkApplicationUserProfileImageMasterId;
			//txtDescription.Enabled = !chkApplicationUserProfileImageMasterId;
			//txtName.Enabled = !chkApplicationUserProfileImageMasterId;
			//txtSortOrder.Enabled = !chkApplicationUserProfileImageMasterId;
		}

		public void LoadData(int applicationUserProfileImageMasterId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationUserProfileImageMasterDataModel();
			data.ApplicationUserProfileImageMasterId = applicationUserProfileImageMasterId;

			// get data
			var items = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			largeImage = "/Shared/AuthenticationAndAuthorization/ApplicationUserProfileImageMaster/ShowImage.aspx?imageid=" + Convert.ToString(item.ApplicationUserProfileImageMasterId);
			smallImage = "/Shared/AuthenticationAndAuthorization/ApplicationUserProfileImageMaster/ShowImage.aspx?imageid=" + Convert.ToString(item.ApplicationUserProfileImageMasterId);			
			imgApplicationUserImage.ImageUrl = "~/Shared/AuthenticationAndAuthorization/ApplicationUserProfileImageMaster/ShowImage.aspx?imageid=" + Convert.ToString(item.ApplicationUserProfileImageMasterId);
			imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/ApplicationUserProfileImageMaster/ShowImage.aspx?imageid=" + Convert.ToString(item.ApplicationUserProfileImageMasterId);
			//hrfApplicationUserImage.HRef = "~/ApplicationDevelopment/ApplicationUserProfileImageMaster/ShowImage.aspx?imageid=" + Convert.ToString(row[TaskTimeTracker.Components.Module.ApplicationDevelopment.ApplicationUserProfileImageMaster.DataColumns.ApplicationUserProfileImageMasterId]);
			txtTitle.Text = Convert.ToString(item.Title);
			CoreControlApplicationId.Text = Convert.ToString(item.ApplicationId);
			CoreControlddlApplicationId.Text = Convert.ToString(item.ApplicationId); 			
			
			if (!showId)
			{
				txtApplicationUserProfileImageMasterId.Text = item.ApplicationUserProfileImageMasterId.ToString();
				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, applicationUserProfileImageMasterId, PrimaryEntityKey);
			}
			else
			{
				txtApplicationUserProfileImageMasterId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationUserProfileImageMasterDataModel();

			ApplicationUserProfileImageMasterId = data.ApplicationUserProfileImageMasterId;
			Image = data.Image;
			Title = data.Title;

		}	

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var Applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(Applicationdata, drpApplication,
				StandardDataModel.StandardDataColumns.Name,
				BaseDataModel.BaseDataColumns.ApplicationId);

            if (isTesting)
            {
                drpApplication.AutoPostBack = true;
                if (drpApplication.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                    {
                        drpApplication.SelectedValue = txtApplication.Text;
                    }
                    else
                    {
                        txtApplication.Text = drpApplication.SelectedItem.Value;
                    }
                } 
                txtApplication.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                {
                    drpApplication.SelectedValue = txtApplication.Text;
                }
            }
        }

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtApplicationUserProfileImageMasterId.Visible = isTesting;
			lblApplicationUserProfileImageMasterId.Visible = isTesting;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ApplicationUserProfileImageMaster";
			FolderLocationFromRoot = "/Shared/AuthenticationAndAuthorization";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImageMaster;

			// set object variable reference            
			PlaceHolderCore = dynApplicationUserProfileImageMasterId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;
			CoreControlApplicationId = txtApplication;
			CoreControlddlApplicationId = drpApplication;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplication.Text = drpApplication.SelectedItem.Value;
        }

		#endregion

	}
}
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


namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

        #region properties

		

		public int? ApplicationUserProfileImageId
		{
			get
			{
				if (txtApplicationUserProfileImageId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationUserProfileImageId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtApplicationUserProfileImageId.Text);
				}
			}
			set
			{
				txtApplicationUserProfileImageId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ApplicationUserId
		{
			get
			{
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationUser.Text.Trim());
                else
                    return int.Parse(drpApplicationUser.SelectedItem.Value);
			}
			set
			{
				txtApplicationUser.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

        public int? ApplicationId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplication.Text.Trim());
                else
                    return int.Parse(drpApplication.SelectedItem.Value);
            }
			set
			{
				txtApplication.Text = (value == null) ? String.Empty : value.ToString();
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
                else if (drpApplicationUserProfileImageMaster.SelectedValue != "None")
                {
                    byte[] imgdata = null;
                    var data = new ApplicationUserProfileImageMasterDataModel();
                    data.ApplicationUserProfileImageMasterId = Convert.ToInt32(drpApplicationUserProfileImageMaster.SelectedValue);
					var item = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetDetails(data, SessionVariables.RequestProfile);
                    if (item != null)
                    {
                        imgdata = ((byte[])(item.Image));
                    }                    
                    return imgdata;
                }
                return null;
			}
			set
			{
				
			}
		}		

		#endregion properties

		#region private methods

		public override void SetId(int setId, bool chkApplicationUserProfileImageId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationUserProfileImageId);
			txtApplicationUserProfileImageId.Enabled = chkApplicationUserProfileImageId;
            drpApplicationUser.Enabled = false;
            txtApplicationUser.Enabled = false;
            drpApplication.Enabled = false;
            txtApplication.Enabled = false;
			//txtDescription.Enabled = !chkApplicationUserProfileImageId;
			//txtName.Enabled = !chkApplicationUserProfileImageId;
			//txtSortOrder.Enabled = !chkApplicationUserProfileImageId;
		}

		public override int? Save(string action)
		{
			var data = new ApplicationUserProfileImageDataModel();

			data.ApplicationUserProfileImageId = ApplicationUserProfileImageId;
			data.ApplicationId = ApplicationId;
			data.Image = Image;
			data.ApplicationUserId = ApplicationUserId;

			if (action == "Insert")
			{
				if(!Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.ApplicationUserProfileImageId;
		}		

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationUserProfileImageDataModel();

			ApplicationUserProfileImageId = data.ApplicationUserProfileImageId;
			ApplicationId = data.ApplicationId;
			ApplicationUserId = data.ApplicationUserId;

		}	

		public void LoadData(int ApplicationUserProfileImageId, bool showId)
		{
			var data = new ApplicationUserProfileImageDataModel();
			data.ApplicationUserProfileImageId = ApplicationUserProfileImageId;
			var oApplicationUserProfileImage = Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oApplicationUserProfileImage != null)
			{
				if (!showId)
				{
                    txtApplicationUserProfileImageId.Text = Convert.ToString(oApplicationUserProfileImage.ApplicationUserProfileImageId);

					dynAuditHistory.Visible = true;
					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage, ApplicationUserProfileImageId, "ApplicationUserProfileImage");
					dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationUserProfileImage");
				}
				else
				{
					txtApplicationUserProfileImageId.Text = String.Empty;
				}                
                txtApplicationUser.Text               = oApplicationUserProfileImage.ApplicationUserId.ToString();
                txtApplication.Text                   = oApplicationUserProfileImage.ApplicationId.ToString();
                drpApplication.Text                   = oApplicationUserProfileImage.ApplicationId.ToString();
                drpApplicationUser.SelectedValue = oApplicationUserProfileImage.ApplicationUserId.ToString();
				oUpdateInfo.LoadText(oApplicationUserProfileImage);
			}
			else
			{
				txtApplicationUserProfileImageId.Text = String.Empty;
                txtApplicationUser.Text = String.Empty;
                txtApplication.Text = String.Empty;
			}
		}

		private void LoadApplicationUserImage()
        {
            if (drpApplicationUserProfileImageMaster.SelectedValue != "None")
            {
                imgApplicationUserProfileImageMaster.ImageUrl = "~/Shared/AuthenticationAndAuthorization/ApplicationUserProfileImageMaster/ShowImage.aspx?imageid=" + drpApplicationUserProfileImageMaster.SelectedValue;
            }
            else
            {
                imgApplicationUserProfileImageMaster.ImageUrl = String.Empty;
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var Applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(Applicationdata, drpApplication,
				StandardDataModel.StandardDataColumns.Name,
				BaseDataModel.BaseDataColumns.ApplicationId);

			var applicationUserdata = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(applicationUserdata, drpApplicationUser,
                ApplicationUserDataModel.DataColumns.ApplicationUserName,
                ApplicationUserDataModel.DataColumns.ApplicationUserId);

			var applicationUserProfileImageMasterdata = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(applicationUserProfileImageMasterdata, drpApplicationUserProfileImageMaster,
                ApplicationUserProfileImageMasterDataModel.DataColumns.Title,
                ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId);

            LoadApplicationUserImage();

            if (isTesting)
            {
                drpApplication.AutoPostBack = true;
                drpApplicationUser.AutoPostBack = true;
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
                if (drpApplicationUser.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationUser.Text.Trim()))
                    {
                        drpApplicationUser.SelectedValue = txtApplicationUser.Text;
                    }
                    else
                    {
                        txtApplicationUser.Text = drpApplicationUser.SelectedItem.Value;
                    }
                }
                txtApplication.Visible = false;
                txtApplicationUser.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                {
                    drpApplication.SelectedValue = txtApplication.Text;
                }
                if (!string.IsNullOrEmpty(txtApplicationUser.Text.Trim()))
                {
                    drpApplicationUser.SelectedValue = txtApplicationUser.Text;
                }
            }
        }

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtApplicationUserProfileImageId.Visible = isTesting;
			lblApplicationUserProfileImageId.Visible = isTesting;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ApplicationUserProfileImage";
			FolderLocationFromRoot = "/Shared/AuthenticationAndAuthorization";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage;

			// set object variable reference            
			PlaceHolderCore = dynApplicationUserProfileImageId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplication.Text = drpApplication.SelectedItem.Value;
        }

        protected void drpApplicationUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationUser.Text = drpApplicationUser.SelectedItem.Value;
        }

        protected void drpApplicationUserProfileImageMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplicationUserImage();
        }        

		#endregion

	}
}
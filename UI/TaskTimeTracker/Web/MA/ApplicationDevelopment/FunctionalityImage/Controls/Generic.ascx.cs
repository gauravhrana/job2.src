using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.IO;
using System.Data;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage.Controls
{
	
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? FunctionalityImageId
        {
            get
            {
                if (txtFunctionalityImageId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFunctionalityImageId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtFunctionalityImageId.Text);
                }
            }
            set
            {
                txtFunctionalityImageId.Text = (value == null) ? String.Empty : value.ToString();
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

        public string Description
        {
            get
            {
                return txtDescription.Text.Trim();
            }
            set
            {
                txtDescription.Text = value ?? String.Empty;
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
                Stream imgdatastream = FileUploadImage.PostedFile.InputStream;
                int imgdatalen = FileUploadImage.PostedFile.ContentLength;
                byte[] imgdata = new byte[imgdatalen];
                imgdatastream.Read(imgdata, 0, imgdatalen);
                return imgdata;
            }
            set
            {
                
            }
        }        

        #endregion properties

		protected string largeImage = "";
		protected string smallImage = "";

        #region private methods

		public override int? Save(string action)
		{
			var data = new FunctionalityImageDataModel();

			data.FunctionalityImageId = FunctionalityImageId;
			data.ApplicationId = ApplicationId;
			data.Image = Image;
			data.Title = Title;
            data.Description = Description;

			if (action == "Insert")
			{
				if(!FunctionalityImageDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					FunctionalityImageDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				FunctionalityImageDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.FunctionalityImageId;
		}

        public override void SetId(int setId, bool chkFunctionalityImageId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFunctionalityImageId);
            txtFunctionalityImageId.Enabled = chkFunctionalityImageId;
            //txtDescription.Enabled = !chkFunctionalityImageId;
            //txtName.Enabled = !chkFunctionalityImageId;
            //txtSortOrder.Enabled = !chkFunctionalityImageId;
        }

        public void LoadData(int functionalityImageId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new FunctionalityImageDataModel();
            data.FunctionalityImageId = functionalityImageId;

            // get data
			var items = FunctionalityImageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            largeImage = "/MA/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Convert.ToString(item.FunctionalityImageId);
            smallImage = "/MA/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Convert.ToString(item.FunctionalityImageId);
            imgApplicationUserImage.ImageUrl = "~/MA/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Convert.ToString(item.FunctionalityImageId);
            imgApplicationUserImage1.ImageUrl = "~/MA/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Convert.ToString(item.FunctionalityImageId);
            //hrfApplicationUserImage.HRef = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Convert.ToString(row[TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImage.DataColumns.FunctionalityImageId]);
            txtTitle.Text = Convert.ToString(item.Title);
            txtApplication.Text = Convert.ToString(item.ApplicationId);
            drpApplication.Text = Convert.ToString(item.ApplicationId);
            txtDescription.Text = item.Description;			

            if (!showId)
            {
                txtFunctionalityImageId.Text = item.FunctionalityImageId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, functionalityImageId, PrimaryEntityKey);
            }
            else
            {
                txtFunctionalityImageId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityImageDataModel();

            FunctionalityImageId = data.FunctionalityImageId;
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
			txtFunctionalityImageId.Visible = isTesting;
			lblFunctionalityImageId.Visible = isTesting;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "FunctionalityImage";
            FolderLocationFromRoot = "/Shared/QualityAssurance";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImage;

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityImageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplication.Text = drpApplication.SelectedItem.Value;
        }

        #endregion
    }
}
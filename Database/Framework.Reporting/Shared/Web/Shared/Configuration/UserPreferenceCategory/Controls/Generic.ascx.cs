using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;


namespace Shared.UI.Web.Configuration.UserPreferenceCategory.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? UserPreferenceCategoryId
        {
            get
            {
                if (txtUserPreferenceCategoryId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUserPreferenceCategoryId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtUserPreferenceCategoryId.Text);
                }
            }
            set
            {
                txtUserPreferenceCategoryId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value ?? String.Empty;
            }
        }

        public string Description
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
            }
            set
            {
                txtDescription.InnerText = value ?? String.Empty;
            }
        }

        public int? SortOrder
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }
            set
            {
                txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Shared/Configuration/UserPreferenceCategory/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkUserPreferenceCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUserPreferenceCategoryId);
            txtUserPreferenceCategoryId.Enabled = chkUserPreferenceCategoryId;
            //txtDescription.Enabled = !chkUserPreferenceCategoryId;
            //txtName.Enabled = !chkUserPreferenceCategoryId;
            //txtSortOrder.Enabled = !chkUserPreferenceCategoryId;
        }

        public void LoadData(int userpreferencecategoryId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new UserPreferenceCategoryDataModel();
            data.UserPreferenceCategoryId = userpreferencecategoryId;

            // get data
            var items = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtDescription.InnerText = item.Description.ToString();
            txtName.Text = item.Name.ToString();
            txtSortOrder.Text = item.SortOrder.ToString();


            if (!showId)
            {
                txtUserPreferenceCategoryId.Text = item.UserPreferenceCategoryId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, userpreferencecategoryId, PrimaryEntityKey);
            }
            else
            {
                txtUserPreferenceCategoryId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UserPreferenceCategoryDataModel();

            UserPreferenceCategoryId = data.UserPreferenceCategoryId;
            Name = data.Name;
            Description = data.Description;
            SortOrder = data.SortOrder;

        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtUserPreferenceCategoryId.Visible = isTesting;
            lblUserPreferenceCategoryId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserPreferenceCategory";
            FolderLocationFromRoot = "/Shared/Configuration";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreferenceCategory;

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        #endregion

    }
}
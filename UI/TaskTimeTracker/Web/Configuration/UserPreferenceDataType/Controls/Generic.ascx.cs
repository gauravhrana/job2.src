using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;


namespace Shared.UI.Web.UserReferenceDataType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? UserPreferenceDataTypeId
        {
            get
            {
                if (txtUserPreferenceDataTypeId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUserPreferenceDataTypeId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtUserPreferenceDataTypeId.Text);
                }
            }
            set
            {
                txtUserPreferenceDataTypeId.Text = (value == null) ? String.Empty : value.ToString();
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
        #endregion properties

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Configuration/UserPreferenceDataType/Controls/Validation.xml");
            }
        }

        public override void SetId(int setId, bool chkUserPreferenceDataTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUserPreferenceDataTypeId);
            txtUserPreferenceDataTypeId.Enabled = chkUserPreferenceDataTypeId;
            //txtName.Enabled = !chkUserPreferenceDataTypeId;
            //txtDescription.Enabled = !chkUserPreferenceDataTypeId;
            //txtSortOrder.Enabled = !chkUserPreferenceDataTypeId;
        }

         public void LoadData(int userPreferenceDataTypeId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new DataModel.Framework.Configuration.UserPreferenceDataTypeDataModel();
            data.UserPreferenceDataTypeId = userPreferenceDataTypeId;

            // get data
			var items = Framework.Components.UserPreference.UserPreferenceDataTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtDescription.InnerText = item.Description.ToString();
            txtName.Text = item.Name.ToString();
            txtSortOrder.Text = item.SortOrder.ToString();


            if (!showId)
            {
                txtUserPreferenceDataTypeId.Text = item.UserPreferenceDataTypeId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, userPreferenceDataTypeId, PrimaryEntityKey);
            }
            else
            {
                txtUserPreferenceDataTypeId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new DataModel.Framework.Configuration.UserPreferenceDataTypeDataModel();

            UserPreferenceDataTypeId = data.UserPreferenceDataTypeId;
            Name = data.Name;
            Description = data.Description;
            SortOrder = data.SortOrder;

        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtUserPreferenceDataTypeId.Visible = isTesting;
            lblUserPreferenceDataTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserPreferenceDataType";
            FolderLocationFromRoot = "/Shared/Configuration";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreferenceDataType;

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceDataTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }
        
    }
}
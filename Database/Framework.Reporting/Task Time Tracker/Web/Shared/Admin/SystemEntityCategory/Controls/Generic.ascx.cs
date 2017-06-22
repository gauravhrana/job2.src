using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.SystemEntityCategory.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

        #region properties

		public int? SystemEntityCategoryId
		{
			get
			{
				if (txtSystemEntityCategoryId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSystemEntityCategoryId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtSystemEntityCategoryId.Text);
				}
			}
            set
            {
                txtSystemEntityCategoryId.Text = (value == null) ? String.Empty : value.ToString();
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
				return Server.MapPath("~/Shared/Admin/SystemEntityCategory/Controls/Validation.xml"); //"R:\SystemEntityCategorys\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		#endregion properties

        #region methods

        public override int? Save(string action)
        {
            var data = new SystemEntityCategoryDataModel();

            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                Framework.Components.Core.SystemEntityCategoryDataManager.Create(data, SessionVariables.RequestProfile);
            }
            else
            {
                data.SystemEntityCategoryId = SystemEntityCategoryId;
                Framework.Components.Core.SystemEntityCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of ClientID ?
            return data.SystemEntityCategoryId;
        }

		public override void SetId(int setId, bool chkSystemEntityCategoryId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkSystemEntityCategoryId);
			txtSystemEntityCategoryId.Enabled = chkSystemEntityCategoryId;
			//txtDescription.Enabled = !chkSystemEntityCategoryId;
			//txtName.Enabled = !chkSystemEntityCategoryId;
			//txtSortOrder.Enabled = !chkSystemEntityCategoryId;
		}

		public void LoadData(int systemEntityCategoryId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new SystemEntityCategoryDataModel();
            data.SystemEntityCategoryId = systemEntityCategoryId;

            // get data
            var items = Framework.Components.Core.SystemEntityCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtDescription.InnerText = item.Description.ToString();
            txtName.Text = item.Name.ToString();
            txtSortOrder.Text = item.SortOrder.ToString();


            if (!showId)
            {
                txtSystemEntityCategoryId.Text = item.SystemEntityCategoryId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, systemEntityCategoryId, PrimaryEntityKey);
            }
            else
            {
                txtSystemEntityCategoryId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new SystemEntityCategoryDataModel();

            SystemEntityCategoryId = data.SystemEntityCategoryId;
            Name = data.Name;
            Description = data.Description;
            SortOrder = data.SortOrder;

        }

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtSystemEntityCategoryId.Visible = isTesting;
			lblSystemEntityCategoryId.Visible = isTesting;
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "SystemEntityCategory";
            FolderLocationFromRoot = "/Shared/Admin";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityCategory;

            // set object variable reference            
            PlaceHolderCore = dynSystemEntityCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

		#endregion
	}
}
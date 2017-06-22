using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

using System.Data;

namespace Shared.UI.Web.Configuration.TabChildStructure.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? TabChildStructureId
        {
            get
            {
                if (txtTabChildStructureId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTabChildStructureId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTabChildStructureId.Text);
                }
            }
            set
            {
                txtTabChildStructureId.Text = (value == null) ? String.Empty : value.ToString();
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

        public string EntityName
        {
            get
            {
                return txtEntityName.Text;
            }
            set
            {
                txtEntityName.Text = value ?? String.Empty;
            }
        }

        public string InnerControlPath
        {
            get
            {
                return txtInnerControlPath.Text;
            }
            set
            {
                txtInnerControlPath.Text = value ?? String.Empty;
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

        public decimal? TabParentStructureId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTabParentStructureId.Text.Trim());
                else
                    return int.Parse(drpTabParentStructureList.SelectedItem.Value);
            }
            set
            {
                txtTabParentStructureId.Text = (value == null) ? String.Empty : value.ToString();
            }

        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Configuration/TabChildStructure/Controls/Validation.xml"); //"R:\TabChildStructures\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new TabChildStructureDataModel();

			data.Name					= Name;
			data.TabParentStructureId	= TabParentStructureId;
			data.EntityName				= EntityName;
			data.InnerControlPath		= InnerControlPath;
			data.SortOrder				= SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.TabChildStructureDatManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.TabChildStructureDatManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.TabChildStructureDatManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.TabChildStructureId;
		}

		public override void SetId(int setId, bool chkTabChildStructureId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTabChildStructureId);
			txtTabChildStructureId.Enabled = chkTabChildStructureId;
			//txtDescription.Enabled = !chkTabChildStructureId;
			//txtName.Enabled = !chkTabChildStructureId;
			//txtSortOrder.Enabled = !chkTabChildStructureId;
		}		

		#endregion

        #region private methods       

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var TabParentStructureData = Framework.Components.Core.TabParentStructureDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(TabParentStructureData, drpTabParentStructureList, StandardDataModel.StandardDataColumns.Name,
                TabParentStructureDataModel.DataColumns.TabParentStructureId);

            if (isTesting)
            {
                drpTabParentStructureList.AutoPostBack = true;
                if (drpTabParentStructureList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTabParentStructureId.Text.Trim()))
                    {
                        drpTabParentStructureList.SelectedValue = txtTabParentStructureId.Text;
                    }
                    else
                    {
                        txtTabParentStructureId.Text = drpTabParentStructureList.SelectedItem.Value;
                    }
                }
                txtTabParentStructureId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTabParentStructureId.Text.Trim()))
                {
                    drpTabParentStructureList.SelectedValue = txtTabParentStructureId.Text;
                }
            }
        }

        public void LoadData(int tabChildStructureId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new TabChildStructureDataModel();
            data.TabChildStructureId = tabChildStructureId;

            // get data
			var items = Framework.Components.Core.TabChildStructureDatManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtTabParentStructureId.Text = item.TabParentStructureId.ToString();
            txtEntityName.Text = item.EntityName.ToString();
            txtName.Text = item.Name.ToString();
            txtInnerControlPath.Text = item.InnerControlPath.ToString();
            txtSortOrder.Text = item.SortOrder.ToString();


            if (!showId)
            {
                txtTabChildStructureId.Text = item.TabChildStructureId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, tabChildStructureId, PrimaryEntityKey);
            }
            else
            {
                txtTabChildStructureId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TabChildStructureDataModel();

            TabChildStructureId = data.TabChildStructureId;
            EntityName = data.EntityName;
            TabParentStructureId = data.TabParentStructureId;
            Name = data.Name;
            InnerControlPath = data.InnerControlPath;
            SortOrder = data.SortOrder;

        }

        protected void drpTabParentStructureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTabParentStructureId.Text = drpTabParentStructureList.SelectedItem.Value;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetupDropdown();
            var isTesting = SessionVariables.IsTesting;
            txtTabChildStructureId.Visible = isTesting;
            lblTabChildStructureId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "TabChildStructure";
            FolderLocationFromRoot = "/Shared/Configuration";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TabChildStructure;

            // set object variable reference            
            PlaceHolderCore = dynTabChildStructureId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        #endregion

    }
}
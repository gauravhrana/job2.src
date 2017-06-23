using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.SubscriberApplicationRole.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

        #region properties

		public int? SubscriberApplicationRoleId
		{
			get
			{
				if (txtSubscriberApplicationRoleId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSubscriberApplicationRoleId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtSubscriberApplicationRoleId.Text);
				}
			}
            set
            {
                txtSubscriberApplicationRoleId.Text = (value == null) ? String.Empty : value.ToString();
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
				return Server.MapPath("~/Shared/Admin/SubscriberApplicationRole/Controls/Validation.xml"); //"R:\SubscriberApplicationRoles\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		#endregion properties

        #region methods

        public override int? Save(string action)
        {
            var data = new SubscriberApplicationRoleDataModel();

            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                Framework.Components.Core.SubscriberApplicationRoleDataManager.Create(data, SessionVariables.RequestProfile);
            }
            else
            {
                data.SubscriberApplicationRoleId = SubscriberApplicationRoleId;
                Framework.Components.Core.SubscriberApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of ClientID ?
            return data.SubscriberApplicationRoleId;
        }

		public override void SetId(int setId, bool chkSubscriberApplicationRoleId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkSubscriberApplicationRoleId);
			txtSubscriberApplicationRoleId.Enabled = chkSubscriberApplicationRoleId;
			//txtDescription.Enabled = !chkSubscriberApplicationRoleId;
			//txtName.Enabled = !chkSubscriberApplicationRoleId;
			//txtSortOrder.Enabled = !chkSubscriberApplicationRoleId;
		}

		public void LoadData(int SubscriberApplicationRoleId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new SubscriberApplicationRoleDataModel();
            data.SubscriberApplicationRoleId = SubscriberApplicationRoleId;

            // get data
            var items = Framework.Components.Core.SubscriberApplicationRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtDescription.InnerText = item.Description.ToString();
            txtName.Text = item.Name.ToString();
            txtSortOrder.Text = item.SortOrder.ToString();


            if (!showId)
            {
                txtSubscriberApplicationRoleId.Text = item.SubscriberApplicationRoleId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, SubscriberApplicationRoleId, PrimaryEntityKey);
            }
            else
            {
                txtSubscriberApplicationRoleId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new SubscriberApplicationRoleDataModel();

            SubscriberApplicationRoleId = data.SubscriberApplicationRoleId;
            Name = data.Name;
            Description = data.Description;
            SortOrder = data.SortOrder;

        }

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtSubscriberApplicationRoleId.Visible = isTesting;
			lblSubscriberApplicationRoleId.Visible = isTesting;
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "SubscriberApplicationRole";
            FolderLocationFromRoot = "/Shared/Admin";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SubscriberApplicationRole;

            // set object variable reference            
            PlaceHolderCore = dynSubscriberApplicationRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

		#endregion
	}
}
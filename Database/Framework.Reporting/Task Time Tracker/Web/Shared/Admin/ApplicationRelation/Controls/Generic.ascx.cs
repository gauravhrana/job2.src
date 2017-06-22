using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.ApplicationRelation.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties
		
		public int? ApplicationRelationId
		{
			get
			{
				if (txtApplicationRelationId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationRelationId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtApplicationRelationId.Text);
				}
			}
			set
			{
				txtApplicationRelationId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		
		public int? PublisherApplicationId
		{
			get
			{
				if (drpPublisherApplication.SelectedValue != "-1")
				{
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtPublisherApplicationId.Text.Trim());
					else
						return int.Parse(drpPublisherApplication.SelectedItem.Value);
				}
				else
					return null;
			}
			set
			{
				txtPublisherApplicationId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? SubscriberApplicationId
		{
			get
			{
				if (drpSubscriberApplication.SelectedValue != "-1")
				{
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtSubscriberApplicationId.Text.Trim());
					else
						return int.Parse(drpSubscriberApplication.SelectedItem.Value);
				}
				else
					return null;
			}
			set
			{
				txtSubscriberApplicationId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? SystemEntityTypeId
		{
			get
			{
				if (drpSystemEntityType.SelectedValue != "-1")
				{
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtSystemEntityTypeId.Text.Trim());
					else
						return int.Parse(drpSystemEntityType.SelectedItem.Value);
				}
				else
					return null;
			}
			set
			{
				txtSystemEntityTypeId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? SubscriberApplicationRoleId
		{
			get
			{
				if (drpSubscriberApplicationRole.SelectedValue != "-1")
				{
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtSubscriberApplicationRoleId.Text.Trim());
					else
						return int.Parse(drpSubscriberApplicationRole.SelectedItem.Value);
				}
				else
					return null;
			}
			set
			{
				txtSubscriberApplicationRoleId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data                         = new ApplicationRelationDataModel();

			data.ApplicationRelationId	     = ApplicationRelationId;
			data.PublisherApplicationId      = PublisherApplicationId;
			data.SubscriberApplicationId     = SubscriberApplicationId;
			data.SystemEntityTypeId          = SystemEntityTypeId;
			data.SubscriberApplicationRoleId = SubscriberApplicationRoleId;

			if (action == "Insert")
			{
				Framework.Components.Core.ApplicationRelationDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.Core.ApplicationRelationDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ApplicationRouteID ?
			return ApplicationRelationId;
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtApplicationRelationId.Enabled = chkApplicationId;
			
		}

		public void LoadData(int applicationRelationId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationRelationDataModel();
			data.ApplicationRelationId = applicationRelationId;

			// get data
			var items = Framework.Components.Core.ApplicationRelationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			ApplicationRelationId       = item.ApplicationRelationId;
			PublisherApplicationId      = item.PublisherApplicationId;
			SubscriberApplicationId     = item.SubscriberApplicationId;
			SystemEntityTypeId          = item.SystemEntityTypeId;
			SubscriberApplicationRoleId = item.SubscriberApplicationRoleId;	

			if (!showId)
			{
				txtApplicationRelationId.Text = item.ApplicationRelationId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, applicationRelationId, PrimaryEntityKey);
			}
			else
			{
				txtApplicationRelationId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var item = new ApplicationRelationDataModel();

			ApplicationRelationId       = item.ApplicationRelationId;
			PublisherApplicationId      = item.PublisherApplicationId;
			SubscriberApplicationId     = item.SubscriberApplicationId;
			SystemEntityTypeId          = item.SystemEntityTypeId;
			SubscriberApplicationRoleId = item.SubscriberApplicationRoleId;	
		}		

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			
			var applicationData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(applicationData, drpPublisherApplication, ApplicationDataModel.DataColumns.Name,
				ApplicationDataModel.DataColumns.ApplicationId);

			UIHelper.LoadDropDown(applicationData, drpSubscriberApplication, ApplicationDataModel.DataColumns.Name,
				ApplicationDataModel.DataColumns.ApplicationId);

			var entityData = SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(entityData, drpSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName,
				SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			var roleData = SubscriberApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(roleData, drpSubscriberApplicationRole, SubscriberApplicationRoleDataModel.DataColumns.Name,
				SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId);

			if (isTesting)
			{
				drpPublisherApplication.AutoPostBack = true;
				if (drpPublisherApplication.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtPublisherApplicationId.Text.Trim()))
					{
						drpPublisherApplication.SelectedValue = txtPublisherApplicationId.Text;
					}
					else
					{
						txtPublisherApplicationId.Text = drpPublisherApplication.SelectedItem.Value;
					}
				}
				txtPublisherApplicationId.Visible = true;

				drpSubscriberApplicationRole.AutoPostBack = true;
				if (drpSubscriberApplicationRole.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSubscriberApplicationRoleId.Text.Trim()))
					{
						drpSubscriberApplicationRole.SelectedValue = txtSubscriberApplicationRoleId.Text;
					}
					else
					{
						txtSubscriberApplicationRoleId.Text = drpSubscriberApplicationRole.SelectedItem.Value;
					}
				}
				txtSubscriberApplicationRoleId.Visible = true;

				drpSubscriberApplication.AutoPostBack = true;
				if (drpSubscriberApplication.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSubscriberApplicationId.Text.Trim()))
					{
						drpSubscriberApplication.SelectedValue = txtSubscriberApplicationId.Text;
					}
					else
					{
						txtSubscriberApplicationId.Text = drpSubscriberApplication.SelectedItem.Value;
					}
				}
				txtSubscriberApplicationId.Visible = true;

				drpSystemEntityType.AutoPostBack = true;
				if (drpSystemEntityType.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
					{
						drpSystemEntityType.SelectedValue = txtSystemEntityTypeId.Text;
					}
					else
					{
						txtSystemEntityTypeId.Text = drpSystemEntityType.SelectedItem.Value;
					}
				}
				txtSystemEntityTypeId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtPublisherApplicationId.Text.Trim()))
				{
					drpPublisherApplication.SelectedValue = txtPublisherApplicationId.Text;
				}
				if (!string.IsNullOrEmpty(txtSubscriberApplicationRoleId.Text.Trim()))
				{
					drpSubscriberApplicationRole.SelectedValue = txtSubscriberApplicationRoleId.Text;
				}
				if (!string.IsNullOrEmpty(txtSubscriberApplicationId.Text.Trim()))
				{
					drpSubscriberApplication.SelectedValue = txtSubscriberApplicationId.Text;
				}
				if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
				{
					drpSystemEntityType.SelectedValue = txtSystemEntityTypeId.Text;
				}
			}
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtApplicationRelationId.Visible = isTesting;
			lblApplicationRelationId.Visible = isTesting;
			if (!IsPostBack)
			{
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRelation;
			PrimaryEntityKey = "ApplicationRelation";
			FolderLocationFromRoot = "Shared/Configuration/ApplicationRelation";

			// set object variable reference            
			PlaceHolderCore = dynApplicationRelationId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpPublisherApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtPublisherApplicationId.Text = drpPublisherApplication.SelectedItem.Value;
		}

		protected void drpSubscriberApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSubscriberApplicationId.Text = drpSubscriberApplication.SelectedItem.Value;
		}

		protected void drpSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSystemEntityTypeId.Text = drpSystemEntityType.SelectedItem.Value;
		}

		protected void drpSubscriberApplicationRole_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSubscriberApplicationRoleId.Text = drpSubscriberApplicationRole.SelectedItem.Value;
		}

		#endregion

	}
}
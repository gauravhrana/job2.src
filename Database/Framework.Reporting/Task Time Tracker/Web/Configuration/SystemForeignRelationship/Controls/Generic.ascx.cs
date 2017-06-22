using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.SystemForeignRelationship.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties

		public int? SystemForeignRelationshipId
		{
			get
			{
				if (txtSystemForeignRelationshipId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSystemForeignRelationshipId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtSystemForeignRelationshipId.Text);
				}
			}
			set
			{
				txtSystemForeignRelationshipId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? PrimaryDatabaseId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtPrimaryDatabase.Text.Trim());
				else
					return int.Parse(drpPrimaryDatabase.SelectedItem.Value);
			}
			set
			{
				txtPrimaryDatabase.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? PrimaryEntityId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtPrimaryEntityId.Text.Trim());
				else
					return int.Parse(drpPrimaryEntityList.SelectedItem.Value);
			}
			set
			{
				txtPrimaryEntityId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ForeignDatabaseId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtForeignDatabase.Text.Trim());
				else
					return int.Parse(drpForeignDatabase.SelectedItem.Value);
			}
			set
			{
				txtForeignDatabase.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ForeignEntityId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtForeignEntityId.Text.Trim());
				else
					return int.Parse(drpForeignEntity.SelectedItem.Value);
			}
			set
			{
				txtForeignEntityId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string FieldName
		{
			get
			{
				return txtFieldName.Text;
			}
			set
			{
				txtFieldName.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Source
		{
			get
			{
				return txtSource.Text;
			}
			set
			{
				txtSource.Text = (value == null) ? String.Empty : value.ToString();
			}
		}       

		public int? SystemForeignRelationshipTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtSystemForeignRelationshipType.Text.Trim());
				else
					return int.Parse(drpSystemForeignRelationshipType.SelectedItem.Value);
			}
			set
			{
				txtSystemForeignRelationshipType.Text = (value == null) ? String.Empty : value.ToString();
			}
		}	

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new SystemForeignRelationshipDataModel();

			data.SystemForeignRelationshipId = SystemForeignRelationshipId;
			data.PrimaryEntityId			 = PrimaryEntityId;
			data.PrimaryDatabaseId			 = PrimaryDatabaseId;
			data.ForeignDatabaseId			 = ForeignDatabaseId;
			data.ForeignEntityId			 = ForeignEntityId;
			data.FieldName					 = FieldName;
			data.Source						 = Source;
			data.SystemForeignRelationshipTypeId = SystemForeignRelationshipTypeId;

			if (action == "Insert")
			{
				Framework.Components.Core.SystemForeignRelationshipDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.Core.SystemForeignRelationshipDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ApplicationRouteID ?
			return data.SystemForeignRelationshipId;
		}

		public override void SetId(int setId, bool chkSystemForeignRelationshipId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkSystemForeignRelationshipId);
			txtSystemForeignRelationshipId.Enabled = chkSystemForeignRelationshipId;
			//txtDescription.Enabled = !chkApplicationRouteId;
			//txtName.Enabled = !chkApplicationRouteId;
			//txtSortOrder.Enabled = !chkApplicationRouteId;
		}

		public void LoadData(int systemForeignRelationshipId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new SystemForeignRelationshipDataModel();
			data.SystemForeignRelationshipId = systemForeignRelationshipId;

			// get data
			var items = Framework.Components.Core.SystemForeignRelationshipDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SystemForeignRelationshipId		= item.SystemForeignRelationshipId;
			PrimaryDatabaseId				= item.PrimaryDatabaseId;
			PrimaryEntityId					= item.PrimaryEntityId;
			ForeignDatabaseId				= item.ForeignDatabaseId;
			ForeignEntityId					= item.ForeignEntityId;
			FieldName						= item.FieldName;
			Source							= item.Source;
			SystemForeignRelationshipTypeId = item.SystemForeignRelationshipTypeId;

			if (!showId)
			{
				txtSystemForeignRelationshipId.Text = item.SystemForeignRelationshipId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, systemForeignRelationshipId, PrimaryEntityKey);
			}
			else
			{
				txtSystemForeignRelationshipId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SystemForeignRelationshipDataModel();

			SystemForeignRelationshipId = data.SystemForeignRelationshipId;
			PrimaryDatabaseId = data.PrimaryDatabaseId;
			PrimaryEntityId = data.PrimaryEntityId;
			ForeignDatabaseId = data.ForeignDatabaseId;
			ForeignEntityId = data.ForeignEntityId;
			Source = data.Source;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var primaryDatabaseData = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetList(SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(primaryDatabaseData, drpPrimaryDatabase, StandardDataModel.StandardDataColumns.Name, SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId);

			if (isTesting)
			{
				drpPrimaryDatabase.AutoPostBack = true;
				if (drpPrimaryDatabase.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtPrimaryDatabase.Text.Trim()))
					{
						drpPrimaryDatabase.SelectedValue = txtPrimaryDatabase.Text;
					}
					else
					{
						txtPrimaryDatabase.Text = drpPrimaryDatabase.SelectedItem.Value;
					}
				}
				txtPrimaryDatabase.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtPrimaryDatabase.Text.Trim()))
				{
					drpPrimaryDatabase.SelectedValue = txtPrimaryDatabase.Text;
				}
			}

			var primaryEntityData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(primaryEntityData, drpPrimaryEntityList, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			if (isTesting)
			{
				drpPrimaryEntityList.AutoPostBack = true;
				if (drpPrimaryEntityList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtPrimaryEntityId.Text.Trim()))
					{
						drpPrimaryEntityList.SelectedValue = txtPrimaryEntityId.Text;
					}
					else
					{
						txtPrimaryEntityId.Text = drpPrimaryEntityList.SelectedItem.Value;
					}
				}
				txtPrimaryEntityId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtPrimaryEntityId.Text.Trim()))
				{
					drpPrimaryEntityList.SelectedValue = txtPrimaryEntityId.Text;
				}
			}

			var foreignDatabaseData = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetList(SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(foreignDatabaseData, drpForeignDatabase, StandardDataModel.StandardDataColumns.Name, 
				SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId);

			if (isTesting)
			{
				drpForeignDatabase.AutoPostBack = true;
				if (drpForeignDatabase.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtForeignDatabase.Text.Trim()))
					{
						drpForeignDatabase.SelectedValue = txtForeignDatabase.Text;
					}
					else
					{
						txtForeignDatabase.Text = drpForeignDatabase.SelectedItem.Value;
					}
				}
				txtForeignDatabase.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtForeignDatabase.Text.Trim()))
				{
					drpForeignDatabase.SelectedValue = txtForeignDatabase.Text;
				}
			}

			var foreignEntityData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(foreignEntityData, drpForeignEntity, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			if (isTesting)
			{
				drpForeignEntity.AutoPostBack = true;
				if (drpForeignEntity.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtForeignEntityId.Text.Trim()))
					{
						drpForeignEntity.SelectedValue = txtForeignEntityId.Text;
					}
					else
					{
						txtForeignEntityId.Text = drpForeignEntity.SelectedItem.Value;
					}
				}
				txtForeignEntityId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtForeignEntityId.Text.Trim()))
				{
					drpForeignEntity.SelectedValue = txtForeignEntityId.Text;
				}
			}

			var systemForeignRelationshipTypeData = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetList(SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(systemForeignRelationshipTypeData, drpSystemForeignRelationshipType, StandardDataModel.StandardDataColumns.Name,
				SystemForeignRelationshipTypeDataModel.DataColumns.SystemForeignRelationshipTypeId);

			if (isTesting)
			{
				drpSystemForeignRelationshipType.AutoPostBack = true;
				if (drpSystemForeignRelationshipType.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSystemForeignRelationshipType.Text.Trim()))
					{
						drpSystemForeignRelationshipType.SelectedValue = txtSystemForeignRelationshipType.Text;
					}
					else
					{
						txtSystemForeignRelationshipType.Text = drpSystemForeignRelationshipType.SelectedItem.Value;
					}
				}
				txtSystemForeignRelationshipType.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtSystemForeignRelationshipType.Text.Trim()))
				{
					drpSystemForeignRelationshipType.SelectedValue = txtSystemForeignRelationshipType.Text;
				}
			}
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtSystemForeignRelationshipId.Visible = isTesting;
				lblSystemForeignRelationshipId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationship;
			PrimaryEntityKey = "SystemForeignRelationship";
			FolderLocationFromRoot = "SystemForeignRelationship";

			// set object variable reference            
			PlaceHolderCore = dynSystemForeignRelationshipId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpPrimaryDatabaseList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtPrimaryDatabase.Text = drpPrimaryDatabase.SelectedItem.Value;
		}

		protected void drpPrimaryEntityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtPrimaryEntityId.Text = drpPrimaryEntityList.SelectedItem.Value;
		}

		protected void drpForeignDatabaseList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtForeignDatabase.Text = drpForeignDatabase.SelectedItem.Value;
		}

		protected void drpForeignEntityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtForeignEntityId.Text = drpForeignEntity.SelectedItem.Value;
		}

		protected void drpSystemForeignRelationshipTypeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSystemForeignRelationshipType.Text = drpSystemForeignRelationshipType.SelectedItem.Value;
		}

		#endregion
	}
}
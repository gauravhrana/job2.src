using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolioGroupXProjectPortfolio.Controls
{
	public partial class Generic : ControlGeneric
	{

		#region properties

		public int? ProjectPortfolioGroupXProjectPortfolioId
		{
			get
			{
				if (txtProjectPortfolioGroupXProjectPortfolioId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtProjectPortfolioGroupXProjectPortfolioId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtProjectPortfolioGroupXProjectPortfolioId.Text);
				}
			}
			set
			{
				txtProjectPortfolioGroupXProjectPortfolioId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ProjectPortfolioGroupId
		{
			get
			{
				if (drpProjectPortfolioGroup.SelectedValue != "-1")
				{
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtProjectPortfolioGroupId.Text.Trim());
					else
						return int.Parse(drpProjectPortfolioGroup.SelectedItem.Value);
				}
				else
					return null;
			}
			set
			{
				txtProjectPortfolioGroupId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ProjectPortfolioId
		{
			get
			{
				if (drpProjectPortfolio.SelectedValue != "-1")
				{
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtProjectPortfolioId.Text.Trim());
					else
						return int.Parse(drpProjectPortfolio.SelectedItem.Value);
				}
				else
					return null;
			}
			set
			{
				txtProjectPortfolioId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		

		public string Description
		{
			get
			{
				return txtDescription.InnerText.Trim();
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
				return DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
			}
			set
			{
				txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
		}


		public DateTime? CreatedDate
		{
			get
			{
				return DateTime.Parse(txtCreatedDate.Text.Trim());
			}
			set
			{
				txtCreatedDate.Text = (value == null) ? String.Empty : value.Value.ToString("MM/dd/yyyy");
			}
		}

		public DateTime? ModifiedDate
		{
			get
			{
				return DateTime.Parse(txtModifiedDate.Text.Trim());
			}
			set
			{
				txtModifiedDate.Text = (value == null) ? String.Empty : value.Value.ToString("MM/dd/yyyy");
			}
		}

		public int? CreatedByAuditId
		{
			get
			{
				return int.Parse(txtCreatedByAuditId.Text);
			}
			set
			{
				txtCreatedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ModifiedByAuditId
		{
			get
			{
				return int.Parse(txtModifiedByAuditId.Text);
			}
			set
			{
				txtModifiedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion


		#region private methods

		public override int? Save(string action)
		{
			var data = new ProjectPortfolioGroupXProjectPortfolioDataModel();

			data.ProjectPortfolioGroupXProjectPortfolioId = ProjectPortfolioGroupXProjectPortfolioId;
			data.ProjectPortfolioGroupId = ProjectPortfolioGroupId;
			data.ProjectPortfolioId = ProjectPortfolioId;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
                var dtProjectPortfolioGroupXProjectPortfolio = ProjectPortfolioGroupXProjectPortfolioDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtProjectPortfolioGroupXProjectPortfolio.Rows.Count == 0)
				{
                    ProjectPortfolioGroupXProjectPortfolioDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("A Record with this combination exists!!!");
				}
			}
			else
			{
                ProjectPortfolioGroupXProjectPortfolioDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of CountryID ?
			return ProjectPortfolioGroupXProjectPortfolioId;
		}

		public override void SetId(int setId, bool chkProjectPortfolioGroupXProjectPortfolioId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkProjectPortfolioGroupXProjectPortfolioId);
			txtProjectPortfolioGroupXProjectPortfolioId.Enabled = chkProjectPortfolioGroupXProjectPortfolioId;
			//txtDescription.Enabled = !chkCountryId;
			//txtName.Enabled = !chkCountryId;
			//txtSortOrder.Enabled = !chkCountryId;
		}

		public void LoadData(int projectPortfolioGroupXProjectPortfolioId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new ProjectPortfolioGroupXProjectPortfolioDataModel();
			dataQuery.ProjectPortfolioGroupXProjectPortfolioId = projectPortfolioGroupXProjectPortfolioId;

            var items = ProjectPortfolioGroupXProjectPortfolioDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			ProjectPortfolioGroupXProjectPortfolioId	= item.ProjectPortfolioGroupXProjectPortfolioId;
			ProjectPortfolioGroupId						= item.ProjectPortfolioGroupId;
			ProjectPortfolioId							= item.ProjectPortfolioId; 
			Description									= item.Description;
			CreatedDate									= item.CreatedDate;
			ModifiedDate								= item.ModifiedDate;
			CreatedByAuditId							= item.CreatedByAuditId;
			ModifiedByAuditId							= item.ModifiedByAuditId;
			SortOrder									= item.SortOrder;

			if (item.ProjectPortfolioGroupId != null)
			{
				drpProjectPortfolioGroup.SelectedValue = item.ProjectPortfolioGroupId.ToString();
			}

			if (item.ProjectPortfolioId != null)
			{
				drpProjectPortfolio.SelectedValue = item.ProjectPortfolioId.ToString();
			}

			if (!showId)
			{
				txtProjectPortfolioGroupXProjectPortfolioId.Text = item.ProjectPortfolioGroupXProjectPortfolioId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)SystemEntity.ProjectPortfolioGroupXProjectPortfolio, projectPortfolioGroupXProjectPortfolioId, "ProjectPortfolioGroupXProjectPortfolio");

			}
			else
			{
				txtProjectPortfolioGroupXProjectPortfolioId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new ProjectPortfolioGroupXProjectPortfolioDataModel();

			ProjectPortfolioGroupXProjectPortfolioId     = data.ProjectPortfolioGroupXProjectPortfolioId;
			ProjectPortfolioGroupId                      = data.ProjectPortfolioGroupId;
			ProjectPortfolioId                           = data.ProjectPortfolioId;
			Description                                  = data.Description;
			CreatedDate                                  = data.CreatedDate;
			ModifiedDate                                 = data.ModifiedDate;
			CreatedByAuditId                             = data.CreatedByAuditId;
			ModifiedByAuditId                            = data.ModifiedByAuditId;
			SortOrder                                    = data.SortOrder;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
            var projectPortfolioGroupdata = ProjectPortfolioGroupDataManager.GetList(SessionVariables.RequestProfile);
            var projectPortfoliodata = ProjectPortfolioDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(projectPortfolioGroupdata, drpProjectPortfolioGroup, StandardDataModel.StandardDataColumns.Name, ProjectPortfolioGroupDataModel.DataColumns.ProjectPortfolioGroupId);

			UIHelper.LoadDropDown(projectPortfoliodata, drpProjectPortfolio, StandardDataModel.StandardDataColumns.Name, ProjectPortfolioDataModel.DataColumns.ProjectPortfolioId);
    

			if (isTesting)
			{
				drpProjectPortfolioGroup.AutoPostBack = true;
				if (drpProjectPortfolioGroup.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtProjectPortfolioGroupId.Text.Trim()))
					{
						drpProjectPortfolioGroup.SelectedValue = txtProjectPortfolioGroupId.Text;
					}
					else
					{
						txtProjectPortfolioGroupId.Text = drpProjectPortfolioGroup.SelectedItem.Value;
					}
				}
				txtProjectPortfolioGroupId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtProjectPortfolioGroupId.Text.Trim()))
				{
					drpProjectPortfolioGroup.SelectedValue = txtProjectPortfolioGroupId.Text;
				}
			}

			if (isTesting)
			{
				drpProjectPortfolio.AutoPostBack = true;
				if (drpProjectPortfolio.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtProjectPortfolioId.Text.Trim()))
					{
						drpProjectPortfolio.SelectedValue = txtProjectPortfolioId.Text;
					}
					else
					{
						txtProjectPortfolioId.Text = drpProjectPortfolio.SelectedItem.Value;
					}
				}
				txtProjectPortfolioId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtProjectPortfolioId.Text.Trim()))
				{
					drpProjectPortfolio.SelectedValue = txtProjectPortfolioId.Text;
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
				txtProjectPortfolioGroupXProjectPortfolioId.Visible = isTesting;
				lblProjectPortfolioGroupXProjectPortfolioId.Visible = isTesting;
				if (!IsPostBack)
				{
					SetupDropdown();
				}
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ProjectPortfolioGroupXProjectPortfolio";
			FolderLocationFromRoot = "ProjectPortfolioGroupXProjectPortfolio";
			PrimaryEntity = SystemEntity.ProjectPortfolioGroupXProjectPortfolio;

			// set object variable reference            
			PlaceHolderCore = dynProjectPortfolioGroupXProjectPortfolioId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpProjectPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtProjectPortfolioGroupId.Text = drpProjectPortfolioGroup.SelectedItem.Value;
		}

		protected void drpProjectPortfolio_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtProjectPortfolioId.Text = drpProjectPortfolio.SelectedItem.Value;
		}


		#endregion

	}
}
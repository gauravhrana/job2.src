using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ThemeDetail.Controls
{
	public partial class Generic : ControlGenericStandard
	{

		#region properties

		public int? ThemeDetailId
		{
			get
			{
				if (txtThemeDetailId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtThemeDetailId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtThemeDetailId.Text);
				}
			}
			set
			{
				txtThemeDetailId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Value
		{
			get
			{
				return txtValue.InnerText;
			}
			set
			{
				txtValue.InnerText = value ?? String.Empty;
			}
		}

		public int? ThemeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtThemeId.Text.Trim());
				else
					return int.Parse(ddlThemeId.SelectedItem.Value);
			}
			set
			{
				txtThemeId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		public int? ThemeCategoryId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtThemeCategoryId.Text.Trim());
				else
					return int.Parse(ddlThemeCategoryId.SelectedItem.Value);
			}
			set
			{
				txtThemeCategoryId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		public int? ThemeKeyId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtThemeKeyId.Text.Trim());
				else
					return int.Parse(ddlThemeKeyId.SelectedItem.Value);
			}
			set
			{
				txtThemeKeyId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		#endregion properties

		#region private methods

		public override void SetId(int setId, bool chkThemeDetailId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkThemeDetailId);
			txtThemeDetailId.Enabled = chkThemeDetailId;
			//txtEntityName.Disabled  = chkThemeDetailId;
			//txtName.Enabled			 = !chkThemeDetailId;
			//txtSortOrder.Enabled	 = !chkThemeDetailId;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var ThemeCategoryData = ThemeCategoryDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(ThemeCategoryData, ddlThemeCategoryId, StandardDataModel.StandardDataColumns.Name,
				ThemeCategoryDataModel.DataColumns.ThemeCategoryId);

			if (isTesting)
			{
				ddlThemeCategoryId.AutoPostBack = true;
				if (ddlThemeCategoryId.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtThemeCategoryId.Text.Trim()))
					{
						ddlThemeCategoryId.SelectedValue = txtThemeCategoryId.Text;
					}
					else
					{
						txtThemeCategoryId.Text = ddlThemeCategoryId.SelectedItem.Value;
					}
				}
				txtThemeCategoryId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtThemeCategoryId.Text.Trim()))
				{
					ddlThemeCategoryId.SelectedValue = txtThemeCategoryId.Text;
				}
			}
			var ThemeKeyData = ThemeKeyDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(ThemeKeyData, ddlThemeKeyId, StandardDataModel.StandardDataColumns.Name,
				ThemeKeyDataModel.DataColumns.ThemeKeyId);

			if (isTesting)
			{
				ddlThemeKeyId.AutoPostBack = true;
				if (ddlThemeKeyId.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtThemeKeyId.Text.Trim()))
					{
						ddlThemeKeyId.SelectedValue = txtThemeKeyId.Text;
					}
					else
					{
						txtThemeKeyId.Text = ddlThemeKeyId.SelectedItem.Value;
					}
				}
				txtThemeKeyId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtThemeKeyId.Text.Trim()))
				{
					ddlThemeKeyId.SelectedValue = txtThemeKeyId.Text;
				}
			}
			var ThemeData = ThemeDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(ThemeData, ddlThemeId, StandardDataModel.StandardDataColumns.Name,
				ThemeDataModel.DataColumns.ThemeId);

			if (isTesting)
			{
				ddlThemeId.AutoPostBack = true;
				if (ddlThemeId.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtThemeId.Text.Trim()))
					{
						ddlThemeId.SelectedValue = txtThemeId.Text;
					}
					else
					{
						txtThemeId.Text = ddlThemeId.SelectedItem.Value;
					}
				}
				txtThemeId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtThemeId.Text.Trim()))
				{
					ddlThemeId.SelectedValue = txtThemeId.Text;
				}
			}
		}

		public void LoadData(int ThemeDetailId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ThemeDetailDataModel();
			data.ThemeDetailId = ThemeDetailId;

			// get data
			var items = ThemeDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			txtValue.InnerText = item.Value;
			ddlThemeCategoryId.SelectedValue = txtThemeCategoryId.Text = item.ThemeCategoryId.ToString();
			ddlThemeId.SelectedValue = txtThemeId.Text = item.ThemeId.ToString();
			ddlThemeKeyId.SelectedValue = txtThemeKeyId.Text = item.ThemeKeyId.ToString();


			if (!showId)
			{
				txtThemeDetailId.Text = item.ThemeDetailId.ToString();
				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, ThemeDetailId, PrimaryEntityKey);
			}
			else
			{
				txtThemeDetailId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ThemeDetailDataModel();

			ThemeDetailId = data.ThemeDetailId;
			Value = data.Value;
			ThemeId = data.ThemeId;
			ThemeCategoryId = data.ThemeCategoryId;
			ThemeKeyId = data.ThemeKeyId;
		}

		public override int? Save(string action)
		{
			var data = new ThemeDetailDataModel();

			data.ThemeDetailId	= SystemKeyId;
			data.ThemeId		= ThemeId;
			data.ThemeKeyId		= ThemeKeyId;
			data.ThemeCategoryId = ThemeCategoryId;
			data.Value			= Value;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.ThemeDetailDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.ThemeDetailDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.ThemeDetailDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.ThemeDetailId;
		}


		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				SetupDropdown();
			var isTesting = SessionVariables.IsTesting;
			txtThemeDetailId.Visible = isTesting;
			lblThemeDetailId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ThemeDetail";
			FolderLocationFromRoot = "/Shared/Configuration";
			PrimaryEntity = SystemEntity.ThemeDetail;

			// set object variable reference            
			PlaceHolderCore = dynThemeDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			CoreSystemKey = txtThemeDetailId;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion

	}
}
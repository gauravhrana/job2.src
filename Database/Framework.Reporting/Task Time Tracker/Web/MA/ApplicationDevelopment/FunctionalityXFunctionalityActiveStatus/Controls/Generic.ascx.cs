using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties	

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

		public int? FunctionalityXFunctionalityActiveStatusId
		{
			get
			{
				if (txtFunctionalityXFunctionalityActiveStatusId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFunctionalityXFunctionalityActiveStatusId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtFunctionalityXFunctionalityActiveStatusId.Text);
				}
			}
		}		

		public int FunctionalityId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtFunctionalityId.Text.Trim());
				else
					return int.Parse(drpFunctionalityList.SelectedItem.Value);
			}
		}

		public int FunctionalityActiveStatusId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtFunctionalityActiveStatusId.Text.Trim());
				else
					return int.Parse(drpFunctionalityActiveStatusList.SelectedItem.Value);
			}
		}

		public string AcknowledgedBy
		{
			get
			{
				return txtAcknowledgedBy.Text;
			}
		}

		public string Memo
		{
			get
			{
				return txtMemo.Text;
			}
		}

		public int KnowledgeDate
		{
			get
			{
				return int.Parse(DateTimeHelper.FromUserDateFormatToDate(txtKnowledgeDate.Text.Trim()).Value.ToString("yyyyMMdd"));
			}
		}		

		protected override string ValidationConfigFile
		{
			get
			{
				return Server.MapPath("~/ApplicationDevelopment/FunctionalityXFunctionalityActiveStatus/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		#endregion properties

		#region private methods

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var functionalityData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(functionalityData, drpFunctionalityList,
				StandardDataModel.StandardDataColumns.Name,
				FunctionalityDataModel.DataColumns.FunctionalityId);

			var FunctionalityActiveStatusData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(FunctionalityActiveStatusData, drpFunctionalityActiveStatusList,
				StandardDataModel.StandardDataColumns.Name,
				FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);

			if (isTesting)
			{
				drpFunctionalityList.AutoPostBack = true;
				drpFunctionalityActiveStatusList.AutoPostBack = true;
				
				if (drpFunctionalityList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtFunctionalityId.Text.Trim()))
					{
						drpFunctionalityList.SelectedValue = txtFunctionalityId.Text;
					}
					else
					{
						txtFunctionalityId.Text = drpFunctionalityList.SelectedItem.Value;
					}
				}
				if (drpFunctionalityActiveStatusList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtFunctionalityActiveStatusId.Text.Trim()))
					{
						drpFunctionalityActiveStatusList.SelectedValue = txtFunctionalityActiveStatusId.Text;
					}
					else
					{
						txtFunctionalityActiveStatusId.Text = drpFunctionalityActiveStatusList.SelectedItem.Value;
					}
				}
				
				
				txtFunctionalityId.Visible = true;
				txtFunctionalityActiveStatusId.Visible = true;
				
			}
			else
			{
				
				if (!string.IsNullOrEmpty(txtFunctionalityId.Text.Trim()))
				{
					drpFunctionalityList.SelectedValue = txtFunctionalityId.Text;
				}
				if (!string.IsNullOrEmpty(txtFunctionalityActiveStatusId.Text.Trim()))
				{
					drpFunctionalityActiveStatusList.SelectedValue = txtFunctionalityActiveStatusId.Text;
				}
				
				txtFunctionalityId.Visible = false;
				txtFunctionalityActiveStatusId.Visible = false;
				
			}
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtFunctionalityXFunctionalityActiveStatusId.Enabled = chkApplicationId;
			
			
		}

		public void LoadData(int functionalityXFunctionalityActiveStatusid, bool showId)
		{
			var data = new FunctionalityXFunctionalityActiveStatusDataModel();
			data.FunctionalityXFunctionalityActiveStatusId = functionalityXFunctionalityActiveStatusid;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				if (!showId)
				{
					txtFunctionalityXFunctionalityActiveStatusId.Text = item.FunctionalityXFunctionalityActiveStatusId.ToString();
                    oHistoryList.Setup(PrimaryEntity, functionalityXFunctionalityActiveStatusid, PrimaryEntityKey);
				}
				else
				{
					txtFunctionalityXFunctionalityActiveStatusId.Text = String.Empty;
				}

				
				txtFunctionalityId.Text                        = item.FunctionalityId.ToString();
				txtFunctionalityActiveStatusId.Text            = item.FunctionalityActiveStatusId.ToString();
				txtAcknowledgedBy.Text                         = item.AcknowledgedBy.ToString();
				txtMemo.Text                                   = item.Memo.ToString();
				txtKnowledgeDate.Text                          = item.KnowledgeDate.Value.ToString(SessionVariables.UserDateFormat);
							
				drpFunctionalityList.SelectedValue             = item.FunctionalityId.ToString();
				drpFunctionalityActiveStatusList.SelectedValue = item.FunctionalityActiveStatusId.ToString();


                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
			}
			else
			{
				txtFunctionalityId.Text = String.Empty;
				txtFunctionalityActiveStatusId.Text = String.Empty;
				txtAcknowledgedBy.Text = String.Empty;
				txtMemo.Text = String.Empty;
				txtKnowledgeDate.Text = String.Empty;
				

				drpFunctionalityList.SelectedValue = "-1";
				drpFunctionalityActiveStatusList.SelectedValue = "-1";
				

			}
		}

		

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                SetupDropdown();

               // CalendarExtenderKnowledgeDate.Format = SessionVariables.UserDateFormat;
                lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;
			}
			var isTesting = SessionVariables.IsTesting;
			txtFunctionalityXFunctionalityActiveStatusId.Visible = isTesting;
			lblFunctionalityXFunctionalityActiveStatusId.Visible = isTesting;
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "FunctionalityXFunctionalityActiveStatus";
            FolderLocationFromRoot = "/Shared/QualityAssurance";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityActiveStatus;

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityActiveStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }		

		protected void drpFunctionalityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtFunctionalityId.Text = drpFunctionalityList.SelectedItem.Value;
		}

		protected void drpFunctionalityActiveStatusList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtFunctionalityActiveStatusId.Text = drpFunctionalityActiveStatusList.SelectedItem.Value;
		}

		#endregion

	}
}
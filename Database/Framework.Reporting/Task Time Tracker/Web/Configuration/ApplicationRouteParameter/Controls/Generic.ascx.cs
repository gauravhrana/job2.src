using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.ApplicationRouteParameter.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties
		
		public int? ApplicationRouteParameterId
		{
			get
			{
				if (txtApplicationRouteParameterId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationRouteParameterId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtApplicationRouteParameterId.Text);
				}
			}
			set
			{
				txtApplicationRouteParameterId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string ParameterName
		{
			get
			{
				return txtParameterName.Text;
			}
			set
			{
				txtParameterName.Text = value ?? String.Empty;
			}
		}

		public string ParameterValue
		{
			get
			{
				return txtParameterValue.Text;
			}
			set
			{
				txtParameterValue.Text = value ?? String.Empty;
			}
		}

		
		public int? ApplicationRouteId
		{
			get
			{
				if (drpApplicationRoute.SelectedValue != "-1")
				{
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtApplicationRouteId.Text.Trim());
					else
						return int.Parse(drpApplicationRoute.SelectedItem.Value);
				}
				else
					return null;
			}

			set
			{
				txtApplicationRouteId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationRouteParameterDataModel();

			data.ApplicationRouteParameterId	= ApplicationRouteParameterId;
			data.ApplicationRouteId				= ApplicationRouteId;
			data.ParameterName					= ParameterName;
			data.ParameterValue					= ParameterValue;			

			if (action == "Insert")
			{
				Framework.Components.Core.ApplicationRouteParameterDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.Core.ApplicationRouteParameterDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ApplicationRouteID ?
			return ApplicationRouteParameterId;
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtApplicationRouteParameterId.Enabled = chkApplicationId;
			
		}

		public void LoadData(int applicationRouteParameterId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationRouteParameterDataModel();
			data.ApplicationRouteParameterId = applicationRouteParameterId;

			// get data
			var items = Framework.Components.Core.ApplicationRouteParameterDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			ApplicationRouteParameterId = item.ApplicationRouteParameterId;
			ApplicationRouteId			= item.ApplicationRouteId;
			ParameterName				= item.ParameterName;
			ParameterValue				= item.ParameterValue;			

			if (!showId)
			{
				txtApplicationRouteParameterId.Text = item.ApplicationRouteParameterId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, applicationRouteParameterId, PrimaryEntityKey);
			}
			else
			{
				txtApplicationRouteParameterId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationRouteParameterDataModel();

			ApplicationRouteParameterId = data.ApplicationRouteParameterId;
			ApplicationRouteId			= data.ApplicationRouteId;
			ParameterName				= data.ParameterName;
			ParameterValue				= data.ParameterValue;		
		}		

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var applicationRouteData = Framework.Components.Core.ApplicationRouteDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(applicationRouteData, drpApplicationRoute, ApplicationRouteDataModel.DataColumns.RouteName,
				ApplicationRouteDataModel.DataColumns.ApplicationRouteId);

			if (isTesting)
			{
				drpApplicationRoute.AutoPostBack = true;
				if (drpApplicationRoute.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtApplicationRouteId.Text.Trim()))
					{
						drpApplicationRoute.SelectedValue = txtApplicationRouteId.Text;
					}
					else
					{
						txtApplicationRouteId.Text = drpApplicationRoute.SelectedItem.Value;
					}
				}
				txtApplicationRouteId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtApplicationRouteId.Text.Trim()))
				{
					drpApplicationRoute.SelectedValue = txtApplicationRouteId.Text;
				}
			}
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtApplicationRouteParameterId.Visible = isTesting;
			lblApplicationRouteParameterId.Visible = isTesting;
			if (!IsPostBack)
			{
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRouteParameter;
			PrimaryEntityKey = "ApplicationRouteParameter";
			FolderLocationFromRoot = "Shared/Configuration/ApplicationRouteParameter";

			// set object variable reference            
			PlaceHolderCore = dynApplicationRouteId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpApplicationRoute_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationRouteId.Text = drpApplicationRoute.SelectedItem.Value;
		}

		#endregion

	}
}
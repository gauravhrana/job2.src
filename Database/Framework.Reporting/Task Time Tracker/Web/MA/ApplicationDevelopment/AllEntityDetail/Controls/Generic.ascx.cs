using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetailDataManager.Controls
{
	public partial class Generic : ControlGeneric
	{
		#region properties

		public int? AllEntityDetailId
		{
			get
			{
				if (txtAllEntityDetailId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtAllEntityDetailId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtAllEntityDetailId.Text);
				}
			}
			set
			{
				txtAllEntityDetailId.Text = (value == null) ? String.Empty : value.ToString();
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
				txtEntityName.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string DB_Name
		{
			get
			{
				return txtDB_Name.Text;
			}
			set
			{
				txtDB_Name.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string DB_Project_Name
		{
			get
			{
				return txtDB_Project_Name.Text;
			}
			set
			{
				txtDB_Project_Name.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Component_Project_Name
		{
			get
			{
				return txtComponent_Project_Name.Text;
			}
			set
			{
				txtComponent_Project_Name.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new AllEntityDetailDataModel();

			data.AllEntityDetailId			= AllEntityDetailId;
			data.EntityName					= EntityName;
			data.DB_Name					= DB_Name;
			data.DB_Project_Name			= DB_Project_Name;
			data.Component_Project_Name		= Component_Project_Name;

			if (action == "Insert")
			{
				var dtAllEntityDetail = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtAllEntityDetail.Rows.Count == 0)
				{
					TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.AllEntityDetailId;
		}

		public override void SetId(int setId, bool chkAllEntityDetailId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkAllEntityDetailId);
			txtAllEntityDetailId.Enabled = chkAllEntityDetailId;
			//txtDescription.Enabled = !chkActivityAlgorithmItemId;
			//txtActivityId.Enabled = !chkActivityAlgorithmItemId;
			//txtSortOrder.Enabled = !chkActivityAlgorithmItemId;
		}

		public void LoadData(int allEntityDetailId, bool showId)
		{

			Clear();

			var data = new AllEntityDetailDataModel();
			data.AllEntityDetailId = allEntityDetailId;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


			if (items.Count != 1) return;

			var item = items[0];

			AllEntityDetailId		= item.AllEntityDetailId;
			EntityName				= item.EntityName;
			DB_Name					= item.DB_Name;
			DB_Project_Name			= item.DB_Project_Name;
			Component_Project_Name  = item.Component_Project_Name;

			if (!showId)
			{
				txtAllEntityDetailId.Text = item.AllEntityDetailId.ToString();
				oHistoryList.Setup(PrimaryEntity, allEntityDetailId, PrimaryEntityKey);
			}
			else
			{
				txtAllEntityDetailId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new AllEntityDetailDataModel();

			AllEntityDetailId		= data.AllEntityDetailId;
			EntityName				= data.EntityName;
			DB_Name					= data.DB_Name;
			DB_Project_Name			= data.DB_Project_Name;
			Component_Project_Name	= data.Component_Project_Name;
		}		

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtAllEntityDetailId.Visible = isTesting;
				lblAllEntityDetailId.Visible = isTesting;				
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AllEntityDetail;
			PrimaryEntityKey = "AllEntityDetail";
			FolderLocationFromRoot = "AllEntityDetail";

			PlaceHolderCore = dynAllEntityDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion
	}
}
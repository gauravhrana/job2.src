using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using DataModel.Framework.Import;

namespace Shared.UI.Web.BatchFileSet.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
		#region properties

		public int? BatchFileSetId
		{
			get
			{
				if (txtBatchFileSetId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtBatchFileSetId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtBatchFileSetId.Text);
				}
			}
			set
			{
				txtBatchFileSetId.Text = (value == null) ? String.Empty : value.ToString();
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
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.Text);
			}
			set
			{
				txtDescription.Text = value ?? String.Empty;
			}
		}		

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new BatchFileSetDataModel();

			data.BatchFileSetId = BatchFileSetId;
			data.Name			= Name;
			data.Description	= Description;			

			if (action == "Insert")
			{
				var dtBatchFileSet = Framework.Components.Import.BatchFileSetDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtBatchFileSet.Rows.Count == 0)
				{
					Framework.Components.Import.BatchFileSetDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Import.BatchFileSetDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of BatchFileSetID ?
			return BatchFileSetId;
		}

		public override void SetId(int setId, bool chkBatchFileSetId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkBatchFileSetId);
			txtBatchFileSetId.Enabled = chkBatchFileSetId;
			//txtDescription.Enabled = !chkBatchFileSetId;
			//txtName.Enabled = !chkBatchFileSetId;
			//txtSortOrder.Enabled = !chkBatchFileSetId;
		}

		public void LoadData(int batchFileSetId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new BatchFileSetDataModel();
			dataQuery.BatchFileSetId = batchFileSetId;

			var items = Framework.Components.Import.BatchFileSetDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			BatchFileSetId	= item.BatchFileSetId;
			Name			= item.Name;
			Description		= item.Description;
			
			if (!showId)
			{
				txtBatchFileSetId.Text = item.BatchFileSetId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.BatchFileSet, batchFileSetId, "BatchFileSet");

			}
			else
			{
				txtBatchFileSetId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new BatchFileSetDataModel();

			BatchFileSetId	= data.BatchFileSetId;
			Description		= data.Description;
			Name			= data.Name;			
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtBatchFileSetId.Visible = isTesting;
			lblBatchFileSetId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "BatchFileSet";
			FolderLocationFromRoot = "Shared/Admin/BatchFileSet";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileSet;

			// set object variable reference            
			PlaceHolderCore = dynBatchFileSetId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion 
		     

    }
}
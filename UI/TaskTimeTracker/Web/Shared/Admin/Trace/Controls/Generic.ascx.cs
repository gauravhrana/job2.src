using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Admin.Trace.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{		

		#region private methods

		public override int? Save(string action)
		{
			var data = new DataModel.Framework.Audit.TraceDataModel();

			data.TraceId		= SystemKeyId;
			data.Name			= Name;
			data.Description	= Description;
			data.SortOrder		= SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.Audit.TraceDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Audit.TraceDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Audit.TraceDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TraceID ?
			return data.TraceId;
		}

		public override void SetId(int setId, bool chkTraceId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTraceId);
			txtTraceId.Enabled = chkTraceId;
			//txtDescription.Enabled = !chkTraceId;
			//txtName.Enabled = !chkTraceId;
			//txtSortOrder.Enabled = !chkTraceId;
		}

		public void LoadData(int traceId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new DataModel.Framework.Audit.TraceDataModel();
			dataQuery.TraceId = traceId;

			var items = Framework.Components.Audit.TraceDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.TraceId;

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.Trace, traceId, "Trace");

			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
			
		}

		protected override void Clear()
		{
			base.Clear();

            var data = new DataModel.Framework.Audit.TraceDataModel();

			SetData(data);
		}

        public void SetData(DataModel.Framework.Audit.TraceDataModel data)
		{
			SystemKeyId = data.TraceId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			//txtTraceId.Visible = isTesting;
			//lblTraceId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "Trace";
			FolderLocationFromRoot = "Shared/Admin/Trace";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Trace;

			// set object variable reference            
			PlaceHolderCore = dynTraceId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtTraceId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;	
		}

		#endregion        

        
	}
}
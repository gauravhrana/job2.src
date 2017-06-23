using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Admin.Trace.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{

		#region private methods

		protected override void ShowData(int traceId)
		{
			base.ShowData(traceId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new DataModel.Framework.Audit.TraceDataModel();
			data.TraceId = traceId;

            var items = Framework.Components.Audit.TraceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, traceId, "Trace");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTraceIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new DataModel.Framework.Audit.TraceDataModel();

			SetData(data);
		}

		public void SetData(DataModel.Framework.Audit.TraceDataModel item)
		{
			SystemKeyId = item.TraceId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// set basic variables
			DictionaryLabel = CacheConstants.TraceLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Trace;			

			// set object variable reference            
			PlaceHolderCore			= dynTraceId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv				= borderdiv;

			CoreSystemKey			= lblTraceId;
			CoreControlName			= lblName;
			CoreControlDescription	= lblDescription;
			CoreControlSortOrder	= lblSortOrder;

			CoreUpdateInfo			= oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTraceIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion        

	}
}
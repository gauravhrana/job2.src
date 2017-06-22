using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Layer.Controls
{
    public partial class Generic : ControlGenericStandard
    {
	    public override int? Save(string action)
        {
			var data = new LayerDataModel();

            data.LayerId		= SystemKeyId;
            data.Name			= Name;
            data.Description	= Description;
            data.SortOrder		= SortOrder;

            if (action == "Insert")
            {
                var dtLayer = LayerDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtLayer.Rows.Count == 0)
                {
                    LayerDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                LayerDataManager.Update(data, SessionVariables.RequestProfile);
            }

			return data.LayerId;
        }

        public override void SetId(int setId, bool chkLayerId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkLayerId);
            CoreSystemKey.Enabled = chkLayerId;            
        }

        public void LoadData(int layerId, bool showId)
        {		
            Clear();

			var data = new LayerDataModel();
			data.LayerId = layerId;

            var items = LayerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.LayerId;
				oHistoryList.Setup(PrimaryEntity, layerId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new LayerDataModel();

			SetData(data);
        }

		public void SetData(ClientDataModel data)
		{
			SystemKeyId = data.ClientId;

			base.SetData(data);
		}

	    protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
            lblLayerId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= SystemEntity.Layer;
            PrimaryEntityKey		= "Layer";
            FolderLocationFromRoot	= "Layer";
          
            PlaceHolderCore = dynLayerId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey			= txtLayerId;
			CoreControlName			= txtName;
            CoreControlDescription  = txtDescription;
			CoreControlSortOrder	= txtSortOrder;

			CoreUpdateInfo			= oUpdateInfo;			
        }
    }
}
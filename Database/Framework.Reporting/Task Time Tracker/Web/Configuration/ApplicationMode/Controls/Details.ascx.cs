using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Configuration.ApplicationMode.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {
        #region private methods        

        protected override void ShowData(int applicationModeId)
        {

			base.ShowData(applicationModeId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationModeDataModel();

			data.ApplicationModeId = applicationModeId;

            var items = Framework.Components.UserPreference.ApplicationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, applicationModeId, "ApplicationMode");
			}  
        }        

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationModeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationModeDataModel();

			SetData(data);
		}

		public void SetData(ApplicationModeDataModel item)
		{
			SystemKeyId = item.ApplicationModeId;

			base.SetData(item);
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblApplicationModeIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
            }
            PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationModeLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMode;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationModeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblApplicationModeId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}
        

        #endregion
    }
}
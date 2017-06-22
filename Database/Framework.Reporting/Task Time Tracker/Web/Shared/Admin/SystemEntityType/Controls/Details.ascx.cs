using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.SystemEntityType.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
			PopulateLabelsText();
        }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblSystemEntityTypeIdText, lblEntityNameText
													  , lblEntityDescriptionText, lblNextValueText, lblCreatedDateText,lblPrimaryDatabaseText});
			if (Cache[CacheConstants.SystemEntityTypeLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.SystemEntityType, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.SystemEntityTypeLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.SystemEntityType, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override void ShowData(int systemEntityTypeId)
        {

            oDetailButtonPanel.SetId = SetId;
            var data = new SystemEntityTypeDataModel();
            data.SystemEntityTypeId = systemEntityTypeId;
            var items = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblSystemEntityTypeId.Text = item.SystemEntityTypeId.ToString();
                lblEntityName.Text = item.EntityName.ToString();
                lblEntityDescription.Text = item.EntityDescription.ToString();
                lblNextValue.Text = item.NextValue.ToString();
				lblCreatedDate.Text = item.CreatedDate.ToString();
				lblPrimaryDatabase.Text = item.PrimaryDatabase.ToString();
                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, systemEntityTypeId, "SystemEntityType");
            }
            else
            {
                lblSystemEntityTypeId.Text = String.Empty;
                lblEntityName.Text = String.Empty;
                lblEntityDescription.Text = String.Empty;
                lblNextValue.Text = String.Empty;
				lblCreatedDate.Text = String.Empty;
				lblPrimaryDatabase.Text = String.Empty;
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblSystemEntityTypeIdText, lblEntityNameText, lblEntityDescriptionText, lblNextValueText, lblPrimaryDatabaseText, lblCreatedDateText });
            }

            return LabelListCore;
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.SystemEntityTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityType;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynSystemEntityTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

    }
}
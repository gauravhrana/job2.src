using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.TabChildStructure.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
        #region private methods

        protected override void ShowData(int tabChildStructureId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new TabChildStructureDataModel();
            data.TabChildStructureId = tabChildStructureId;

			var items = Framework.Components.Core.TabChildStructureDatManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblTabChildStructureId.Text		    = item.TabChildStructureId.ToString();
                lblName.Text                        = item.Name.ToString();
                lblEntityName.Text                  = item.EntityName.ToString();
                lblSortOrder.Text                   = item.SortOrder.ToString();
                lblTabParentStructure.Text          = item.TabParentStructureId.ToString();
                lblInnerControlPath.Text            = item.InnerControlPath.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, tabChildStructureId, "TabChildStructure");
            }
            else
            {
                Clear();
            }
        }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblTabChildStructureIdText, lblTabParentStructureText
													  , lblNameText, lblEntityNameText, lblSortOrderText, lblInnerControlPathText});
			if (Cache[CacheConstants.TabChildStructureLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.TabChildStructure, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.TabChildStructureLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.TabChildStructureLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.TabChildStructure, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override void Clear()
        {
            lblTabChildStructureId.Text         = String.Empty;
			lblName.Text		                = String.Empty;
            lblEntityName.Text	                = String.Empty;
            lblSortOrder.Text	                = String.Empty;
            lblTabParentStructure.Text		    = String.Empty;
            lblInnerControlPath.Text            = String.Empty;
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTabChildStructureIdText, lblTabParentStructureText, 
                    lblEntityNameText, lblInnerControlPathText, lblNameText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblTabChildStructureIdText.Visible = isTesting;
                lblTabChildStructureId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.TabChildStructureLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TabChildStructure;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynTabChildStructureId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}
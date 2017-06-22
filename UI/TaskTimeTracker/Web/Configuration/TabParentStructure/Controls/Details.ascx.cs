using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.TabParentStructure.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables

        #endregion

        #region private methods    

        protected override void ShowData(int tabParentStructureId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new TabParentStructureDataModel();
            data.TabParentStructureId = tabParentStructureId;

			var items = Framework.Components.Core.TabParentStructureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblTabParentStructureId.Text    = item.TabParentStructureId.ToString();
                lblName.Text                    = item.Name;
                lblDescription.Text             = item.Description;
                lblSortOrder.Text               = item.SortOrder.ToString();
                lblIsAllTab.Text                = item.IsAllTab.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, tabParentStructureId, "TabParentStructure");
            }
            else
            {
                Clear();
            }
        }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblTabParentStructureIdText, lblIsAllTabText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.TabParentStructureLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.TabParentStructure, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.TabParentStructureLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.TabParentStructureLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.TabParentStructure, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override void Clear()
        {
            lblTabParentStructureId.Text         = String.Empty;
			lblName.Text		                 = String.Empty;
            lblDescription.Text	                 = String.Empty;
            lblSortOrder.Text	                 = String.Empty;
            lblIsAllTab.Text		             = String.Empty;
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTabParentStructureIdText, lblNameText, lblDescriptionText, lblSortOrderText, lblIsAllTabText });
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
                lblTabParentStructureIdText.Visible = isTesting;
                lblTabParentStructureId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.TabParentStructureLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TabParentStructure;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynTabParentStructureId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}
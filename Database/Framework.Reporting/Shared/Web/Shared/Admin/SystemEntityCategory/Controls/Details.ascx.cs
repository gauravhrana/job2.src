using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Admin.SystemEntityCategory.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int systemEntityCategoryId)
        {
			base.ShowData(systemEntityCategoryId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SystemEntityCategoryDataModel();

			data.SystemEntityCategoryId = systemEntityCategoryId;

			var items = Framework.Components.Core.SystemEntityCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

                oHistoryList.Setup(PrimaryEntity, systemEntityCategoryId, "SystemEntityCategory");
			}
			else
			{
				Clear();
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblSystemEntityCategoryIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SystemEntityCategoryDataModel();

			SetData(data);
		}

		public void SetData(SystemEntityCategoryDataModel item)
		{
			SystemKeyId = item.SystemEntityCategoryId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSystemEntityCategoryIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.SystemEntityCategoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityCategory;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynSystemEntityCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

			CoreSystemKey = lblSystemEntityCategoryId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
        }


		#endregion

	}
}
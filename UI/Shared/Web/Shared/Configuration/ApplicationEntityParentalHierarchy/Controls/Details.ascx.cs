using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;


namespace Shared.UI.Web.Configuration.ApplicationEntityParentalHierarchy.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
    
        #region private methods        

        protected override void ShowData(int ApplicationEntityParentalHierarchyId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new ApplicationEntityParentalHierarchyDataModel();
            data.ApplicationEntityParentalHierarchyId = ApplicationEntityParentalHierarchyId;

			var dt = Framework.Components.Core.ApplicationEntityParentalHierarchyDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblApplicationEntityParentalHierarchyId.Text = Convert.ToString(row[ApplicationEntityParentalHierarchyDataModel.DataColumns.ApplicationEntityParentalHierarchyId]);
                lblName.Text                                 = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                lblDescription.Text                          = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                lblSortOrder.Text                            = Convert.ToString(row[StandardDataModel.StandardDataColumns.SortOrder]);

                oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityParentalHierarchy, ApplicationEntityParentalHierarchyId, "ApplicationEntityParentalHierarchy");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationEntityParentalHierarchy");
            }
            else
            {
                Clear();
            }
        }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblApplicationEntityParentalHierarchyIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.ApplicationEntityParentalHierarchyLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityParentalHierarchy, AuditId);
				Cache.Add(CacheConstants.ApplicationEntityParentalHierarchyLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.ApplicationEntityParentalHierarchyLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityParentalHierarchy, AuditId, labelslist);


		}

        protected override void Clear()
        {
            lblApplicationEntityParentalHierarchyId.Text = String.Empty;
        	lblName.Text = String.Empty;
            lblDescription.Text = String.Empty;
            lblSortOrder.Text = String.Empty;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblApplicationEntityParentalHierarchyIdText.Visible = isTesting;
                lblApplicationEntityParentalHierarchyId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

        

        #endregion

    }
}
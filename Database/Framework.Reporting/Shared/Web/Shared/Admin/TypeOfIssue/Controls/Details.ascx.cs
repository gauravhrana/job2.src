using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Audit;

namespace Shared.UI.Web.Admin.TypeOfIssue.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int typeOfIssueId)
		{
			base.ShowData(typeOfIssueId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TypeOfIssueDataModel();
			data.TypeOfIssueId = typeOfIssueId;


			var items = Framework.Components.Audit.TypeOfIssueDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblTypeOfIssueId.Text = item.TypeOfIssueId.ToString();
				lblCategory.Text = item.Category;
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, typeOfIssueId, "TypeOfIssue");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTypeOfIssueIdText, lblNameText, lblDescriptionText, lblSortOrderText,lblCategoryText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TypeOfIssueLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TypeOfIssue;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTypeOfIssueId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTypeOfIssueIdText.Visible = isTesting;
				lblTypeOfIssueId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion        

	}

}
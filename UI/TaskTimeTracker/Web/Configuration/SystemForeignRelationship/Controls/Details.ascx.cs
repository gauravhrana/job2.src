using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Configuration.SystemForeignRelationship.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int systemForeignRelationshipId)
		{
			base.ShowData(systemForeignRelationshipId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SystemForeignRelationshipDataModel();
			data.SystemForeignRelationshipId = systemForeignRelationshipId;

			var items = Framework.Components.Core.SystemForeignRelationshipDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblSystemForeignRelationshipId.Text = item.SystemForeignRelationshipId.ToString();
				lblPrimaryDatabase.Text				  = item.PrimaryDatabase;
				lblPrimaryEntityId.Text				  = item.PrimaryEntity;
				lblForeignDatabase.Text				  = item.ForeignDatabase;
				lblForeignEntityId.Text				  = item.ForeignEntity;
				lblFieldName.Text					  = item.FieldName;
				lblSource.Text						  = item.Source;
				lblSystemForeignRelationshipType.Text = item.SystemForeignRelationshipType;

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, systemForeignRelationshipId, "SystemForeignRelationship");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblSystemForeignRelationshipIdText, lblPrimaryDatabaseText, 
					lblPrimaryEntityIdText, lblForeignDatabaseText, lblForeignEntityIdText,lblFieldNameText,lblSystemForeignRelationshipTypeText,lblSourceText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.SystemForeignRelationshipLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationship;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynSystemForeignRelationshipId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSystemForeignRelationshipIdText.Visible = isTesting;
				lblSystemForeignRelationshipId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
	}
}
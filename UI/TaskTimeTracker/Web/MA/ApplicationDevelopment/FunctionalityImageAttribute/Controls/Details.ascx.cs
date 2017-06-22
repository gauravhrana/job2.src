using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageAttribute.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{	

		#region private methods

		protected override void ShowData(int functionalityImageAttributeId)
		{
			base.ShowData(functionalityImageAttributeId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new FunctionalityImageAttributeDataModel();
			data.FunctionalityImageAttributeId = functionalityImageAttributeId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			int functionalityImageId = 0;
			if (items.Count == 1)
			{
				var item =items[0];
				functionalityImageId = (int)item.FunctionalityImageId;
				lblFunctionalityImageAttributeId.Text = item.FunctionalityImageAttributeId.ToString();
				lblFunctionalityImageId.Text = item.FunctionalityImageId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();

				imgApplicationUserImage.Visible = true;
				imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + functionalityImageId.ToString();
				imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + functionalityImageId.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityImageAttributeId, "FunctionalityImageAttribute");
			}			
		}

		protected override void Clear()
		{
			lblFunctionalityImageAttributeId.Text = String.Empty;
			lblFunctionalityImageId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
		}		

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFunctionalityImageAttributeIdText, lblFunctionalityImageText, lblNameText, lblDescriptionText, lblSortOrderText });
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
				lblFunctionalityImageAttributeIdText.Visible = isTesting;
				lblFunctionalityImageAttributeId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityImageAttributeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageAttribute;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityImageAttributeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}
}
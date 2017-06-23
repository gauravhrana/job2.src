using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.RenumberMigration.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables

        public string Border
        {
            set
            {
				//borderdiv.Style.Add("border", "2px");
				//borderdiv.Style.Add("border-color", "Blue");
				//borderdiv.Style.Add("border-width", "2px");
				//borderdiv.Style.Add("border-style", "groove");
            }

        }
        #endregion

        #region private methods

        protected override void ShowData(int renumberMigrationId)
        {
            var data = new Framework.Components.LogAndTrace.RenumberMigrationDataModel();
            data.RenumberMigrationId = renumberMigrationId;

			var oDetail = Framework.Components.LogAndTrace.RenumberMigrationDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {
                lblApplicationId.Text           = oDetail.ApplicationId.ToString();
                lblRenumberMigrationId.Text     = oDetail.RenumberMigrationId.ToString();
                lblSystemEntityType.Text        = oDetail.SystemEntityType;
                lblOriginalKey.Text             = oDetail.OriginalKey.ToString();
                lblMigratedKey.Text             = oDetail.MigratedKey.ToString();
                lblRecordDate.Text              = oDetail.RecordDate.ToString();

            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblRenumberMigrationId.Text = String.Empty;
            lblApplicationId.Text       = String.Empty;
            lblSystemEntityType.Text    = String.Empty;
            lblOriginalKey.Text         = String.Empty;
            lblMigratedKey.Text         = String.Empty;
            lblRecordDate.Text          = String.Empty;            
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblRenumberMigrationIdText, lblApplicationIdText, lblSystemEntityTypeText, 
                    lblOriginalKeyText, lblMigratedKeyText, lblRecordDateText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var isTesting = SessionVariables.IsTesting;
                lblRenumberMigrationId.Visible = false;
                lblRenumberMigrationIdText.Visible = false;
            }

            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DictionaryLabel = CacheConstants.RenumberMigrationLabelDictionary;
            PrimaryEntity   = Framework.Components.DataAccess.SystemEntity.RenumberMigration;

            PlaceHolderCore = dynRenumberMigrationId;
            
            MainTable       = tblMain1;
            BorderDiv       = borderdiv;
        }

        #endregion

    }
}
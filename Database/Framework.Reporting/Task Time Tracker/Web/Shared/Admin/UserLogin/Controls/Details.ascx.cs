using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.UserLogin.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

        #region variables

        


		#endregion

		#region private methods

		protected override void ShowData(int userLoginId)
        {
            oDetailButtonPanel.SetId = SetId;
			var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
            data.UserLoginId = userLoginId;

			var items = Framework.Components.LogAndTrace.UserLoginDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

                lblUserLoginId.Text		             = item.UserLoginId.ToString();
                lblUserName.Text                     = item.UserName.ToString();
                lblUserLoginStatusId.Text            = item.UserLoginStatusId.ToString();
                lblRecordDate.Text                   = item.RecordDate.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, userLoginId, "UserLogin");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblUserLoginId.Text = String.Empty;
			lblUserName.Text = String.Empty;
			lblUserLoginStatusId.Text = String.Empty;
			lblRecordDate.Text = String.Empty;
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblUserLoginIdText
													  , lblUserNameText, lblRecordDateText, lblUserLoginStatusIdText });
			if (Cache[CacheConstants.UserLoginLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.UserLogin, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.UserLoginLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.UserLogin, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUserLoginIdText, lblUserNameText, lblUserLoginStatusIdText, lblRecordDateText });
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
				lblUserLoginIdText.Visible = isTesting;
				lblUserLoginId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.UserLoginLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLogin;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynUserLoginId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}
}
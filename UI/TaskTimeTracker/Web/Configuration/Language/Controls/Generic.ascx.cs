using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.Language.Controls
{
    public partial class Generic : ControlGenericStandard
    {
        #region methods

        public override int? Save(string action)
        {
            var data = new LanguageDataModel();

            data.LanguageId      = SystemKeyId;
            data.Name            = Name;
            data.Description     = Description;
            data.SortOrder       = SortOrder;

            if (action == "Insert")
            {
                if(!LanguageDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    LanguageDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                LanguageDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.LanguageId;
        }

        public override void SetId(int setId, bool chkLanguageId)
        {
            ViewState["SetId"] = setId;

            LoadData((int)ViewState["SetId"], chkLanguageId);

            CoreSystemKey.Enabled = chkLanguageId;
        }

        public void LoadData(int LanguageId, bool showId)
        {
           			
            Clear();

            var data = new LanguageDataModel();
            data.LanguageId = LanguageId;

            var items = LanguageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.LanguageId;

                oHistoryList.Setup(PrimaryEntity, LanguageId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new LanguageDataModel();

            SetData(data);
        }

        public void SetData(LanguageDataModel data)
        {
            SystemKeyId = data.LanguageId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblLanguageId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity                = SystemEntity.Language;
            PrimaryEntityKey             = "Language";
            FolderLocationFromRoot       = "Language";
          
            PlaceHolderCore              = dynLanguageId;
            PlaceHolderAuditHistory      = dynAuditHistory;
            BorderDiv                    = borderdiv;
            
            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtLanguageId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}
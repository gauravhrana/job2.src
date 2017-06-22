using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using System.Data;
using Shared.UI.Web.Controls;
using System.Configuration;
using System.Web;
using Framework.Components.UserPreference;

namespace Framework.UI.Web.BaseClasses
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PageCommonUpdate runat=server></{0}:PageCommonUpdate>")]
    
	public class PageCommonUpdate : PageCommon
    {

        #region Variables & Properties

        protected DynamicUpdate DynamicUpdatePanelCore { get; set; }

        public DataTable SelectedData
        {
            get;
            set;
        }

		protected int CommonEditableColumnsModeId
		{
			get
			{
				if (HttpContext.Current.Session["CommonEditableColumnsModeId"] == null)
				{
					var fcModeName = ConfigurationManager.AppSettings["CommonEditableColumnsModeName"];
					HttpContext.Current.Session["CommonEditableColumnsModeId"] = FieldConfigurationModeDataManager.GetFCModeIdByName(fcModeName, SessionVariables.RequestProfile);
				}
				return Convert.ToInt32(HttpContext.Current.Session["CommonEditableColumnsModeId"]);
			}
		}

        #endregion

        #region Methods

        protected virtual DataTable UpdateData()
        {
            return null;
        }

        protected virtual string[] GetColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(CommonEditableColumnsModeId.ToString(), PrimaryEntity, SessionVariables.RequestProfile); 
        }

        protected virtual DataTable GetEntityData(int? entityKey)
        {
            return null;
        }

        protected string CheckAndGetRepeaterTextBoxValue(string Name)
        {
            for (int i = 0; i < DynamicUpdatePanelCore.DynamicUpdateRepeater.Items.Count; i++)
            {
                var chkbox = (CheckBox)DynamicUpdatePanelCore.DynamicUpdateRepeater.Items[i].FindControl("chkName");
                var label = (Label)DynamicUpdatePanelCore.DynamicUpdateRepeater.Items[i].FindControl("lblName");
                var txtbox = (TextBox)DynamicUpdatePanelCore.DynamicUpdateRepeater.Items[i].FindControl("txtName");
                var lbltext = label.Text.Replace(" ", "");
                if (lbltext.Equals(Name))
                {
                    if (chkbox.Checked)
                        return txtbox.Text;
                }
            }
            return String.Empty;
        }

        private DataTable GetData()
        {
            return SelectedData;
        }

        #endregion

        #region Events

        protected override void OnPreInit(EventArgs e)
        {
            base.SetSiteMasterPagePath();

            base.OnPreInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ViewName = "CommonUpdate";

			DynamicUpdatePanelCore.AddColumns(GetColumns());
            SuperKey = ApplicationCommon.GetSuperKey();
            SetId = ApplicationCommon.GetSetId();

            var results = new DataTable();

            if (!string.IsNullOrEmpty(SuperKey))
            {
                results = GetEntityData(-1).Clone();

                var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(PrimaryEntity.Value(), SuperKey);

                foreach (var entityKey in lstEntityKeys)
                {
                    var dt = GetEntityData(entityKey);
                    if (dt.Rows.Count == 1)
                    {
                        results.ImportRow(dt.Rows[0]);
                    }
                }
            }
            else
            {
                results = GetEntityData(SetId);
            }

            SelectedData = results.Copy();

			DynamicUpdatePanelCore.SetUp(GetColumns(), PrimaryEntityKey, GetData());
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default", SetId = true }), false);
        }

        override protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var UpdatedData = UpdateData();
			DynamicUpdatePanelCore.SetUp(GetColumns(), PrimaryEntityKey, UpdatedData);
        }        

        #endregion

    }
}
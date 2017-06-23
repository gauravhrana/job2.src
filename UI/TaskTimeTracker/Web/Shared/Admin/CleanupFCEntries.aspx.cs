using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Framework.Components.UserPreference;
using DataModel.Framework.Configuration;

namespace Shared.UI.Web.Admin
{
    public partial class CleanupFCEntries : System.Web.UI.Page
    {

        private void BindResult()
        {
            var criteria = drpCriteria.SelectedValue;

            var dtResult = SessionVariables.FieldConfigurations.Clone();

            if (criteria == "PK and SortOrder")
            {
                var dtCachedFCS = SessionVariables.FieldConfigurations;

                var result = dtCachedFCS.AsEnumerable().Where(x => x["FieldConfigurationMode"].ToString() == "Standard"
                    && (x["Name"].ToString().Contains("Id") || x["Name"].ToString() == "SortOrder"));

                if (result.Count() > 0)
                {
                    dtResult = result.CopyToDataTable();
                }
            }

            gvResult.DataSource = dtResult;
            gvResult.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindResult();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindResult();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvResult.Rows)
            {
                var chkItem = gvRow.FindControl("chkSelectItem");
                if (chkItem != null)
                {
                    if (((CheckBox)chkItem).Checked)
                    {
                        var fcId = int.Parse(gvResult.DataKeys[gvRow.RowIndex].Value.ToString());

                        var data = new FieldConfigurationDisplayNameDataModel();
                        data.FieldConfigurationId = fcId;
                        var dt = FieldConfigurationDisplayNameDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, SessionVariables.ApplicationMode);
                        foreach (var dr in dt)
                        {
                            var fcdnid = dr.FieldConfigurationDisplayNameId.Value;
                            data.FieldConfigurationDisplayNameId = fcdnid;
                            FieldConfigurationDisplayNameDataManager.Delete(data, SessionVariables.RequestProfile);
                        }
                        var fcdata = new FieldConfigurationDataModel();
                        fcdata.FieldConfigurationId = fcId;
                        FieldConfigurationDataManager.Delete(fcdata, SessionVariables.RequestProfile);

                    }

                }
            }

            BindResult();
        }
    }
}
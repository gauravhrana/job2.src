using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Import;

namespace Shared.UI.Web.Admin.BatchFileStatus
{
    public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
    {
        public delegate void UpdateDelegate(Dictionary<string, string> values);

        private DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
                var selectedrows = new DataTable();
                var BatchFileStatusdata = new BatchFileStatusDataModel();

                selectedrows = Framework.Components.Import.BatchFileStatusDataManager.GetDetails(BatchFileStatusdata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {

                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);
                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.BatchFileStatus;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var keys = new int[dt.Rows.Count];
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {

                            keys[i] = Convert.ToInt32(dt.Rows[i][SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            BatchFileStatusdata.BatchFileStatusId = keys[i];
                            var result = Framework.Components.Import.BatchFileStatusDataManager.GetDetails(BatchFileStatusdata, SessionVariables.RequestProfile);
                            selectedrows.ImportRow(result.Rows[0]);


                        }
                    }
                }
                else
                {
                    var key = SetId;
                    BatchFileStatusdata.BatchFileStatusId = key;
                    var result = Framework.Components.Import.BatchFileStatusDataManager.GetDetails(BatchFileStatusdata, SessionVariables.RequestProfile);
                    selectedrows.ImportRow(result.Rows[0]);

                }
                return selectedrows;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

        private string[] GetColumns()
        {

			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.BatchFileStatus, "DBColumns", SessionVariables.RequestProfile);
        }

        protected override void OnInit(EventArgs e)
        {
            InlineEditingList.AddColumns(GetColumns());

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            
            SettingCategory = "BatchFileStatusDefaultView";
           
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateDelegate delupdate = new UpdateDelegate(Update);
            this.InlineEditingList.DelUpdateRef = delupdate;
            if (!IsPostBack)
            {
                InlineEditingList.SetUp(GetColumns(), "BatchFileStatus", GetData());
            }
        }

        private void Update(Dictionary<string, string> values)
        {
            var data = new BatchFileStatusDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            Framework.Components.Import.BatchFileStatusDataManager.Update(data, SessionVariables.RequestProfile);
            InlineEditingList.Data = GetData();
        }
    }
}
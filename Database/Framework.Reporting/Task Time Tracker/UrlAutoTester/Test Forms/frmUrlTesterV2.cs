using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using Framework.Components.DataAccess;

namespace UrlAutoTester
{
    public partial class frmUrlTesterV2 : Form
    {
        
        #region properties

        int AuditId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["AuditId"]);
            }
        }

		RequestProfile requestProfile
		{
			get
			{
				return requestProfile;
			}
		}

        #endregion

        #region Constructor

        public frmUrlTesterV2()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadSystemEntity()
        {
            var SystemEntityData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(requestProfile);
            cmbEntity.DataSource = SystemEntityData;
            cmbEntity.ValueMember = DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId;
            cmbEntity.DisplayMember = DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.EntityName;
        }

        private string GetRouteByEntity(string entityName)
        {
            var entityRoute = string.Empty;
			
            var data = new DataModel.Framework.Core.ApplicationRouteDataModel();
            data.EntityName = entityName;

			var dt = Framework.Components.Core.ApplicationRouteDataManager.Search(data, requestProfile);
            if (dt != null && dt.Rows.Count > 0)
            {
                entityRoute = Convert.ToString(dt.Rows[0][DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ProposedRoute]);
            }
            return entityRoute;
        }

        #endregion

        #region Events

        private void frmUrlTester_Load(object sender, EventArgs e)
        {
            LoadSystemEntity();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var entityName = cmbEntity.Text;
            var entityRouteUrl = GetRouteByEntity(entityName);
            var action = cmbAction.Text;

            var localHostPortNumber = ConfigurationManager.AppSettings["LocalHostPortNumber"];

            //MessageBox.Show(entityRouteUrl);

            var entityUrl = entityRouteUrl;
            var entityUrl2 = string.Empty;
            if (action.ToLower() == "default" || action.ToLower() == "insert")
            {
                entityUrl = entityUrl.Replace("/{SetId}", "").Replace("/{setId}", "");
                entityUrl = entityUrl.Replace("{Action}", action).Replace("{action}", action);
            }
            else
            {
                var systemEntityTypeId = Convert.ToInt32(cmbEntity.SelectedValue);
                var minId = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityMinId(entityName);

                entityUrl = entityUrl.Replace("{SetId}", minId.ToString()).Replace("{setId}", minId.ToString());
                entityUrl = entityUrl.Replace("{Action}", action).Replace("{action}", action);

                entityUrl2 = entityRouteUrl;
				var superKeyId = Framework.Components.Core.SystemEntityTypeDataManager.GetEntitySuperKeyForRandomIds(entityName, systemEntityTypeId, requestProfile);
                entityUrl2 = entityUrl2.Replace("{SetId}", "SuperKey/" + superKeyId.ToString()).Replace("{setId}", "SuperKey/" + superKeyId.ToString());
                entityUrl2 = entityUrl2.Replace("{Action}", action).Replace("{action}", action);

                entityUrl2 = "http://localhost:" + localHostPortNumber + "/" + entityUrl2;
            }
            entityUrl = "http://localhost:" + localHostPortNumber + "/" + entityUrl;

            Process.Start(entityUrl);
            if (!string.IsNullOrEmpty(entityUrl2))
            {
                Process.Start(entityUrl2);
            }
        }
        
        #endregion

    }
}

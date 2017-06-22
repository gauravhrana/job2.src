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
    public partial class frmUrlTester : Form
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

        public frmUrlTester()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void AddSystemEntityByCategory(TreeNode tNode, int systemEntityCategoryId)
        {
			var dt = Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.GetBySystemEntityCategory(systemEntityCategoryId, requestProfile);
            if (dt.Rows.Count > 0)
            {   
                foreach (DataRow dr in dt.Rows)
                {
                    var childNode = new TreeNode(Convert.ToString(dr[DataModel.Framework.Core.SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntity]));
                    childNode.Tag = dr[DataModel.Framework.Core.SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityId];
                    tNode.Nodes.Add(childNode);
                }
            }
        }

        private void LoadSystemEntity()
        {
            var treeNode = new TreeNode("System Entities");
            treeNode.Tag = "root";

			var systemEntityCategoryData = Framework.Components.Core.SystemEntityCategoryDataManager.GetList(requestProfile);

            if (systemEntityCategoryData.Rows.Count > 0)
            {
                foreach (DataRow dr in systemEntityCategoryData.Rows)
                {
                    var childNode              = new TreeNode(Convert.ToString(dr[DataModel.Framework.Core.SystemEntityCategoryDataModel.DataColumns.Name]));
                    var systemEntityCategoryId = Convert.ToInt32( dr[DataModel.Framework.Core.SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId]);
                    childNode.Tag              = "category";

                    AddSystemEntityByCategory(childNode, systemEntityCategoryId);

                    treeNode.Nodes.Add(childNode);
                }
            }

			var systemEntityTypeData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(requestProfile);
            var xTable               = Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.GetList(requestProfile);

            var dtMerged = (from a in systemEntityTypeData.AsEnumerable()
                                  join b in xTable.AsEnumerable()
                                  on a[DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId].ToString() equals b[DataModel.Framework.Core.SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityId].ToString()
                                  into g
                                  where g.Count() == 0
                                  select a).CopyToDataTable();

            if (dtMerged.Rows.Count > 0)
            {
                foreach (DataRow dr in dtMerged.Rows)
                {
                    var childNode = new TreeNode(Convert.ToString(dr[DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.EntityName]));
                    childNode.Tag = dr[DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId];
                    treeNode.Nodes.Add(childNode);
                }
            }

            treeViewEntity.Nodes.Add(treeNode);
            treeNode.Expand();
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

        private void GenerateProcessByEntityAndAction(string entityName, int systemEntityTypeId, string action)
        {
            var entityRouteUrl = GetRouteByEntity(entityName);

            // fine ... also can be textbox - base URL
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
                var minId = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityMinId(entityName);

                entityUrl = entityUrl.Replace("{SetId}", minId.ToString()).Replace("{setId}", minId.ToString());
                entityUrl = entityUrl.Replace("{Action}", action).Replace("{action}", action);

                entityUrl2     = entityRouteUrl;
				var superKeyId = Framework.Components.Core.SystemEntityTypeDataManager.GetEntitySuperKeyForRandomIds(entityName, systemEntityTypeId, requestProfile);
                entityUrl2     = entityUrl2.Replace("{SetId}", "SuperKey/" + superKeyId.ToString()).Replace("{setId}", "SuperKey/" + superKeyId.ToString());
                entityUrl2     = entityUrl2.Replace("{Action}", action).Replace("{action}", action);

                entityUrl2 = "http://localhost:" + localHostPortNumber + "/" + entityUrl2;
            }

            entityUrl = "http://localhost:" + localHostPortNumber + "/" + entityUrl;

            Process.Start(entityUrl);
            if (!string.IsNullOrEmpty(entityUrl2))
            {
                Process.Start(entityUrl2);
            }
        }

        private void ChangeChildNodeCheckboxState(TreeNode tNode)
        {
            var isChecked = tNode.Checked;
            foreach (TreeNode childNode in tNode.Nodes)
            {
                childNode.Checked = isChecked;
                if (childNode.Nodes.Count > 0)
                {
                    ChangeChildNodeCheckboxState(childNode);
                }
            }
        }

        private void IterateThroughChildNode(TreeNode tNode, List<string> lstSelectedActions)
        {
            if (tNode.Checked && Convert.ToString(tNode.Tag) != "category")
            {
                var entityName = tNode.Text;
                var systemEntityTypeId = Convert.ToInt32(tNode.Tag);

                // interate over each action
                foreach (var action in lstSelectedActions)
                {
                    GenerateProcessByEntityAndAction(entityName, systemEntityTypeId, action);
                }
            }
            if (tNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in tNode.Nodes)
                {
                    IterateThroughChildNode(node, lstSelectedActions);
                }
            }
        }

        #endregion

        #region Events

        private void frmUrlTester_Load(object sender, EventArgs e)
        {
            LoadSystemEntity();
        }

        private void btn_Click(object sender, EventArgs e)
        {

            // Chekck user input
            if (listBoxAction.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at lease a single action");
                return;
            }

            // get list of actions
            var lstSelectedActions = new List<string>();
            foreach (var item in listBoxAction.SelectedItems)
            {
                lstSelectedActions.Add(item.ToString());
            }

            // itereate over each entity
            var rootNode = treeViewEntity.Nodes[0];
            foreach (TreeNode node in rootNode.Nodes)
            {
                IterateThroughChildNode(node, lstSelectedActions);
            }
        }

        private void treeViewEntity_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                ChangeChildNodeCheckboxState(e.Node);
            }
        }

        #endregion

    }
}

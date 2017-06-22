using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
    public partial class EntityReport : Shared.UI.WebFramework.BasePage
    {

        #region Methods

        private DataTable GetEntityList()
        {
            var dt = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedEntitys(int FunctionalityId)
        {
            var id = 0;
            var data = new FunctionalityEntityStatusDataModel();
            data.FunctionalityId = id;
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByFunctionality(int FunctionalityId, List<int> EntityIds)
        {
            var id = 0;
            var data = new FunctionalityEntityStatusDataModel();
            data.FunctionalityId = FunctionalityId;
            var entityids = EntityIds.ToArray();
            for (var i = 0; i < entityids.Length; i++)
            {
                var fesdata = new FunctionalityEntityStatusDataModel();
                fesdata.SystemEntityTypeId = entityids[i];
                fesdata.FunctionalityId = FunctionalityId;
				var fesdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Search(fesdata, SessionVariables.RequestProfile);
                if (fesdt.Rows.Count == 0)
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Create(fesdata, SessionVariables.RequestProfile);
                }
            }
            
        }

        private DataTable GetFunctionalityList()
        {
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedFunctionalities(int EntityId)
        {
            //var id = Convert.ToInt32(drpEntity.SelectedValue);
             var id = 0;
            var data = new FunctionalityEntityStatusDataModel();
            data.SystemEntityTypeId = id;
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByEntity(int EntityId, List<int> FunctionalityIds)
        {
            var id = 0;
            var data = new FunctionalityEntityStatusDataModel();
            data.SystemEntityTypeId = EntityId;
            var funcids = FunctionalityIds.ToArray();
            for (var i = 0; i < funcids.Length; i++)
            {
                var fesdata = new FunctionalityEntityStatusDataModel();
                fesdata.FunctionalityId = funcids[i];
                fesdata.SystemEntityTypeId = EntityId;
				var fesdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Search(fesdata, SessionVariables.RequestProfile);
                if (fesdt.Rows.Count == 0)
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Create(fesdata, SessionVariables.RequestProfile);
                }
            }
        }

        
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            var detailTabControlPath = ApplicationCommon.DetailTabControlPath;
            var bucketControlPath = ApplicationCommon.ReadOnlyBucketControlPath;
            
            var tabControl = (Shared.UI.Web.Controls.DetailTabControl)Page.LoadControl(detailTabControlPath);
            tabControl.Setup("Build Entity Report");
            tabControl.ID = "InputTabControl";

            //swecond tab
            //second tab needs bucket control configuration and  special methods
            var bucketControl = (Shared.UI.Web.Controls.ReadOnlyBucket)Page.LoadControl(bucketControlPath);
            bucketControl.ConfigureBucket("SystemEntityType", 1, 2, GetEntityList, GetAssociatedEntitys, SaveByFunctionality);
            bucketControl.ID = "EntityBucket";
            // tabControl.AddTab("Client", String.Empty, selected, bucketControl();
            tabControl.AddTab("Entity", bucketControl, "Entity", true);

            //third tab
            var bucketControl2 = (Shared.UI.Web.Controls.ReadOnlyBucket)Page.LoadControl(bucketControlPath);
            bucketControl2.ConfigureBucket("Functionality", 1, 2, GetFunctionalityList, GetAssociatedFunctionalities, SaveByEntity);
            bucketControl2.ID = "FunctionalityBucket";
            tabControl.AddTab("Functionality", bucketControl2, "Functionality", false);


            var placeholder = new PlaceHolder();
            placeholder.ID = "EntityReportResults";
            //placeholder.Controls.Add(BuildReport(entities, functioanlities));
            tabControl.AddTab("EntityReport", placeholder, "EntityReport", false);

            //add tab control to placeholder
            dynInputTabs.Controls.Add(tabControl);

            //BucketOfFunctionality.ConfigureBucket("Functionality", 1, 2, GetFunctionalityList, GetAssociatedFunctionalities, SaveByEntity);
            //BucketOfEntity.ConfigureBucket("SystemEntityType", 1, 2, GetEntityList, GetAssociatedEntitys, SaveByFunctionality);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            SettingCategory = "FunctionalityEntityStatusDefaultView";
			//bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			//bcControl.Setup("Report By Entity");
			//bcControl.GenerateMenu();

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
			oSearchActionBar.Setup("FunctionalityEntityStatus");	
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            var tabControl = (Shared.UI.Web.Controls.DetailTabControl)dynInputTabs.FindControl("InputTabControl");
            var entityBucket = (Shared.UI.Web.Controls.ReadOnlyBucket)tabControl.FindControl("EntityBucket");
            var functionalityBucket = (Shared.UI.Web.Controls.ReadOnlyBucket)tabControl.FindControl("FunctionalityBucket");
            var placeHolder = (PlaceHolder)tabControl.FindControl("EntityReportResults");
            var selectedentityItems = entityBucket.TargetList.Items;
            var entities = new int[entityBucket.TargetList.Items.Count];
            var i = 0;

            foreach (ListItem item in selectedentityItems)
            {
                entities[i] = int.Parse(item.Value);
                i++;
            }

            var selectedfuncitems = functionalityBucket.TargetList.Items;
            var functioanlities = new int[functionalityBucket.TargetList.Items.Count];
            var j = 0;
            
			foreach (ListItem item in selectedfuncitems)
            {
                functioanlities[j] = int.Parse(item.Value);
                j++;
            }
           
			placeHolder.Controls.Add( BuildReport(entities, functioanlities));
          
         }

        private Table BuildReport(int[] entities, int[] functionalities)
        {
            var edata = new SystemEntityTypeDataModel();
            var edt = new DataTable();
            var fdata = new FunctionalityDataModel();
            var fdt = new DataTable();
            var fesdata = new FunctionalityEntityStatusDataModel();
            var fesdt = new DataTable();
            var table = new Table();
            table.CssClass = "entityreport";
            table.GridLines = GridLines.Both;
            table.BorderColor = System.Drawing.Color.Blue;
            table.BorderWidth = 2;
            table.BorderStyle = BorderStyle.Groove;
            var tablerow = new TableRow();
            var tablecell = new TableCell();

            tablecell.Text = "Functionality/Entity";
            //tablecell.Style.Add("font-weight", "bold");
            //tablecell.Style.Add("width", "150px");
            tablerow.Cells.Add(tablecell);

            for (var i = 0; i < entities.Length; i++)
            {
                edata.SystemEntityTypeId = entities[i];
                edt = Framework.Components.Core.SystemEntityTypeDataManager.Search(edata, SessionVariables.RequestProfile);
                tablecell = new TableCell();
                
				if (edt.Rows.Count >= 1)
                {
                    tablecell.Text = edt.Rows[0][SystemEntityTypeDataModel.DataColumns.EntityName].ToString();
                    //tablecell.Style.Add("font-weight", "bold");
                    //tablecell.Style.Add("width", "150px");
                    tablerow.Cells.Add(tablecell);
                }
            }

            table.Rows.Add(tablerow);
            tablerow = new TableRow();

            for (int j = 0; j < functionalities.Length; j++)
            {
                fdata.FunctionalityId = functionalities[j];
				fdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Search(fdata, SessionVariables.RequestProfile);
                tablerow = new TableRow();
                tablecell = new TableCell();

                if (fdt.Rows.Count >= 1)
                {
                    tablecell.Text = fdt.Rows[0][StandardDataModel.StandardDataColumns.Name].ToString();
                    //tablecell.Style.Add("font-weight", "bold");
                    //tablecell.Style.Add("width", "150px");
                    tablerow.Cells.Add(tablecell);
                }
                
				tablerow.Cells.Add(tablecell);

                for (int i = 1; i < table.Rows[0].Cells.Count; i++)
                {
                    tablerow.Cells.Add(new TableCell());
                }

                table.Rows.Add(tablerow);
            }

            var entity = "";
            var functionality = "";

            for (var i = 0; i < functionalities.Length; i++)
            {
                for (var j = 0; j < entities.Length; j++)
                {
                    fesdata.SystemEntityTypeId = entities[j];
                    fesdata.FunctionalityId = functionalities[i];
					fesdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Search(fesdata, SessionVariables.RequestProfile);
                    if (fesdt.Rows.Count == 1)
                    {
                        fdata.FunctionalityId = functionalities[i];
						fdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Search(fdata, SessionVariables.RequestProfile);

                        if (fdt.Rows.Count >= 1)
                        {
                            functionality = fdt.Rows[0][StandardDataModel.StandardDataColumns.Name].ToString();
     
                        }

                        edata.SystemEntityTypeId = entities[j];
                        edt = Framework.Components.Core.SystemEntityTypeDataManager.Search(edata, SessionVariables.RequestProfile);

                        if (edt.Rows.Count >= 1)
                        {
                            entity = edt.Rows[0][SystemEntityTypeDataModel.DataColumns.EntityName].ToString();
                    
                        }

                        for (var k = 1; k < table.Rows.Count; k++)
                        {
                            for (int l = 1; l < table.Rows[0].Cells.Count; l++)
                            {
                                if (table.Rows[k].Cells[0].Text.Equals(functionality) &&
                                    table.Rows[0].Cells[l].Text.Equals(entity))
                                {
                                    table.Rows[k].Cells[l].Text = fesdt.Rows[0][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatus].ToString();
                                    break;
                                }
                            }
                        }

                    }
                }
            }

            TableReportContent.Controls.Add(table);

            return table;
        }                
    }
}
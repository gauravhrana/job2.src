using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

		string fesTabFCMode = "";
        #region Methods

        private string[] GetFunctionalityXFunctionalityActiveStatusColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityActiveStatus, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetData()
        {
            return GetAssociatedFunctionalityEntityStatusIds(SetId);
        }

        protected DataTable GetFunctionalityXFunctionalityActiveStatusData()
        {
            var data = new FunctionalityXFunctionalityActiveStatusDataModel();
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetFunctionalityEntityStatusColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus, fesTabFCMode, SessionVariables.RequestProfile);
        }

        protected DataTable GetAssociatedFunctionalityEntityStatusIds(int functionalityId)
        {
            var data = new FunctionalityEntityStatusDataModel();
            data.FunctionalityId = functionalityId;
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetFunctionalityImageColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityImage, "DBColumns", SessionVariables.RequestProfile);
        }

        protected DataTable GetAssociatedFunctionalityImageIds(int functionalityId)
        {
			var data = new FunctionalityXFunctionalityImageDataModel();
			data.FunctionalityId = functionalityId;
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityImageDataManager.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);
			return dt;
        }

        private string[] GetFunctionalityHistoryColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Functionality, "DBColumns", SessionVariables.RequestProfile);
        }

        protected DataTable GetAssociatedFunctionalityHistoryIds(int functionalityId)
        {
            var data = new FunctionalityHistoryDataModel();
            data.FunctionalityId = functionalityId;
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityHistoryDataManager.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);
            return dt;
        }

        //protected DataTable GetAssociatedFunctionalityActiveStatusIds(int functionalityId)
        //{
        //    var data = new FunctionalityXFunctionalityActiveStatusDataModel();
        //    data.FunctionalityId = functionalityId;
        //    var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);
        //    return dt;
        //}

        private DataTable GetData(string key)
        {
            return GetAssociatedFunctionalityHistoryIds(int.Parse(key));
        }

        protected override Control GetTabControl(int setId, Control updateControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("FunctionalityUpdateView");
            tabControl.ID = "FunctionalityUpdateView";

            var historyControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
            historyControl.Setup("FunctionalityHistory", String.Empty, "FunctionalityHistoryId", setId, true, GetData, GetFunctionalityHistoryColumns, "FunctionalityHistory");
            historyControl.SetSession("true");
            historyControl.ID = "HistoryControl";
            historyControl.HideControls();
            historyControl.Visible = chkVisible.Checked;

            var plcHolder = new PlaceHolder();
            plcHolder.Controls.Add(updateControl);
            plcHolder.ID = "UpdatePlaceHolder";
            plcHolder.Controls.Add(new Literal() { Text = "<br /><br />" });
            plcHolder.Controls.Add(historyControl);


            var selected = false;

            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
            {
                selected = true;
            }

            tabControl.AddTab("Functionality", updateControl, String.Empty, selected);

            // not making sense ?
            selected = false;

            if (Request.QueryString["tab"] == "2")
            {
                selected = true;
            }

            var bucketControl = ApplicationCommon.GetNewBucketControl();
            bucketControl.ConfigureBucket("FunctionalityActiveStatus", setId, GetFunctionalityActiveStatusList, GetAssociatedFunctionalityActiveStatusIds, SaveFunctionalityXFunctionalityActiveStatus);

            var listControl3 = (Shared.UI.Web.Controls.Images)Page.LoadControl(ApplicationCommon.ImagesControl);
            var images = GetAssociatedFunctionalityImageIds(setId);
            listControl3.Setup(images);
            listControl3.SetSession("true");
            tabControl.AddTab("Images", listControl3, String.Empty, selected);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
            listControl.Setup("FunctionalityActiveStatus", String.Empty, "FunctionalityActiveStatusId", setId, true, GetAssociatedFunctionalityActiveStatusIds, GetFunctionalityXFunctionalityActiveStatusColumns, "FunctionalityActiveStatus");
            listControl.SetSession("true");
            tabControl.AddTab("Status History", listControl, String.Empty, selected);
            
            var listControl2 = (Shared.UI.Web.Controls.GroupList)Page.LoadControl(ApplicationCommon.GroupListControl);
            SetId = setId;
            var plcControlHolder = new PlaceHolder();
            plcControlHolder.ID = "plcControlHolder";
            //var dropDown = new DropDownList();
            //dropDown.SelectedIndexChanged += dropdownlist_SelectedIndexChanged;
            //dropDown.Width = 300;
            //dropDown.ID = "dropdownlist";
            //plcControlHolder.Controls.Add(dropDown);

            // Add Direction Combos Dynamically, check flag whether the corresponding record exists in FC or not.
            plcControlHolder.Controls.Add(new Literal() { Text = "<br />"});
            plcControlHolder.Controls.Add(new Label() { Text = " &nbsp;&nbsp; &nbsp;&nbsp; Group By: &nbsp;&nbsp;" });

            var dropDownOrderBy = new DropDownList();
            dropDownOrderBy.ID = "dropdownlistOrderBy1";
            dropDownOrderBy.Items.Add("Functionality");
            dropDownOrderBy.Items.Add("FunctionalityStatus");
            dropDownOrderBy.Items.Add("FunctionalityPriority");
            dropDownOrderBy.Items.Add("AssignedTo");
            dropDownOrderBy.SelectedValue = "FunctionalityStatus";
            plcControlHolder.Controls.Add(dropDownOrderBy);
            plcControlHolder.Controls.Add(new Literal() { Text = "<br /><br />" });
           
            plcControlHolder.Controls.Add(new Label() { Text = "Sub Group By: &nbsp;&nbsp;" });
            dropDownOrderBy = new DropDownList();
            dropDownOrderBy.ID = "dropdownlistOrderBy2";
            dropDownOrderBy.Items.Add("Functionality");
            dropDownOrderBy.Items.Add("FunctionalityStatus");
            dropDownOrderBy.Items.Add("FunctionalityPriority");
            dropDownOrderBy.Items.Add("AssignedTo");
            dropDownOrderBy.SelectedValue = "Functionality";
            plcControlHolder.Controls.Add(dropDownOrderBy);
            plcControlHolder.Controls.Add(new Literal() { Text = "<br /><br />" });

            var btnGroup = new Button();
            btnGroup.ID = "btnGroup";
            btnGroup.Text = "Group";
            btnGroup.Click += btnGroup_Click;
            plcControlHolder.Controls.Add(btnGroup);
            plcControlHolder.Controls.Add(new Literal() { Text = "<br /><br />" });

			listControl2.SettingCategory = "FunctionalityEntityStatusUpdateTabControl";
			listControl2.ID = "GroupListControl";
			listControl2.Setup("FunctionalityStatus", "ASC", "Functionality", "ASC", "FunctionalityEntityStatus", string.Empty, "FunctionalityEntityStatusId", true, GetData, GetFunctionalityEntityStatusColumns, "FunctionalityEntityStatus", String.Empty, null, true);
			fesTabFCMode = listControl2.FieldConfigurationMode;	
			plcControlHolder.Controls.Add(listControl2);
			tabControl.AddTab("Entity Status", plcControlHolder, String.Empty, selected);
			
            
            return tabControl;
        }

        protected void btnGroup_Click(object sender, EventArgs e)
        {
           
            var tabcontrol = (Shared.UI.Web.Controls.DetailTabControl)plcUpdateList.FindControl("FunctionalityUpdateView");
            var plcControlHolder = (PlaceHolder)tabcontrol.FindControl("plcControlHolder");
            var groupListControl = (Shared.UI.Web.Controls.GroupList)tabcontrol.FindControl("GroupListControl");
            var ddlgroupBy = (DropDownList)plcControlHolder.FindControl("dropdownlistOrderBy1");
            var ddlSubgroupBy = (DropDownList)plcControlHolder.FindControl("dropdownlistOrderBy2");
            groupListControl.Setup(ddlgroupBy.SelectedValue, "ASC", ddlSubgroupBy.SelectedValue, "ASC", "FunctionalityEntityStatus", string.Empty, "FunctionalityEntityStatusId", true, GetData, GetFunctionalityEntityStatusColumns, "FunctionalityEntityStatus", String.Empty, null, true);
            
        }

        protected void dropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = sender as DropDownList;
            

        }

		private DataTable GetAssociatedFunctionalityActiveStatusIds(string key)
		{
			return GetAssociatedFunctionalityActiveStatusIds(int.Parse(key));
		}


		private DataTable GetAssociatedFunctionalityEntityStatusIds(string key)
		{
			return GetAssociatedFunctionalityEntityStatusIds(int.Parse(key));
		}

        private List<FunctionalityActiveStatusDataModel> GetFunctionalityActiveStatusList()
        {
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedFunctionalityActiveStatusIds(int functionalityId)
        {
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveFunctionalityXFunctionalityActiveStatus(int functionalityId, List<int> functionalityActiveStatusIds)
        {
			TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.DeleteByFunctionality(functionalityId, SessionVariables.RequestProfile);
			TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.CreateByFunctionality(functionalityId, functionalityActiveStatusIds.ToArray(), SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Functionality;

            GenericControlPath = ApplicationCommon.GetControlPath("Functionality", ControlType.GenericControl);
            PrimaryPlaceHolder =plcUpdateList ;
            PrimaryEntityKey = "Functionality";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;
        }

        protected override void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            var isVisible = ((CheckBox)sender).Checked;

            foreach (var control in PrimaryPlaceHolder.Controls)
            {
                try
                {
					//if (control is Shared.UI.Web.Controls.DetailTabControl)
					//{
					//	var cntrl = (Shared.UI.Web.Controls.DetailTabControl)control;
					//	var plcholder = (PlaceHolder)cntrl.FindControl("plcUpdateList");
					//	var historycontrol = (Shared.UI.Web.Controls.DetailsWithChildrenControl)plcholder.FindControl("HistoryControl");
					//	historycontrol.Setup("FunctionalityHistory", String.Empty, "FunctionalityHistoryId", SetId, true, GetData, GetFunctionalityHistoryColumns, "FunctionalityHistory");
            
					//	historycontrol.Visible = isVisible;
					//	((ControlCommon)cntrl.GenericControl).IsHistoryVisible = isVisible;
					//}
					//else
					//{
					//	((ControlCommon)control).IsHistoryVisible = isVisible;
					//}
					if (control is Shared.UI.Web.Controls.DetailTabControl)
					{
						var cntrl = (Shared.UI.Web.Controls.DetailTabControl)control;
						var genericctrl = (ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality.Controls.Generic)cntrl.GenericControl;
						genericctrl.IsHistoryVisible = isVisible;
					}
					else
					{
						((ControlCommon)control).IsHistoryVisible = isVisible;
					}
                }
                catch { }
            }
        }

        #endregion
    }

}
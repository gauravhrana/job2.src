using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin
{
	public partial class EntityTabInfo : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				massageData();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "EntityTabInfoDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected void massageData()
		{
			DataSet resultSet = new DataSet("resultSet");
			DataTable TabParentDT = new DataTable("TabParentDT");
			DataTable TabChildrenDT = new DataTable("TabChildrenDT");
			TabParentDT = Framework.Components.Core.TabParentStructureDataManager.GetList(SessionVariables.RequestProfile);
			TabChildrenDT = Framework.Components.Core.TabChildStructureDatManager.GetList(SessionVariables.RequestProfile);
			/*
			 * Populate your data tables with your chosen method
			 * */

			//Add DataTables to DataSet and create the one-to-many 
			//relationships (as many as you need)
			resultSet.Tables.Add(TabParentDT);
			resultSet.Tables.Add(TabChildrenDT);
			resultSet.Relations.Add("ChildrenForTab",
				TabParentDT.Columns["TabParentStructureId"], TabChildrenDT.Columns["TabParentStructureId"]);

			/* Important!!!  Only set the data source for the 
			 *  topmost parent in the heirarchy.
			 * The nested repeaters that are buried in the topmost 
			 *  parent will not be in scope until binding
			 * when using this server-style binding method. */

			TabResults.DataSource = TabParentDT;

			//Upon databind, you invoke the ItemDataBound Method, 
			//which can be easily created by the events tab
			//under the properties of the repeater control in the designer
			TabResults.DataBind();
			TabResults.Visible = true;
		}

		protected void TabResults_ItemDataBound(object sender,
					System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			//Only consume the items in the parent repeater 
			//pertinent to nesting
			if (e.Item.ItemType == ListItemType.Item ||
					  e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//Now that the nested repeater is in scope, find it, 
				//cast it, and create a child view from
				//the datarelation name, and specify that view as the 
				//datasource.  Then bind the nested repeater!'
				var label = (Label)e.Item.FindControl("lblParentStructureId");
				if (label != null)
				{
					var parentstrucId = Convert.ToInt32(label.Text);
					var data = new TabChildStructureDataModel();
					data.TabParentStructureId = parentstrucId;


					Repeater tempRpt =
						   (Repeater)e.Item.FindControl("ChildTabResults");
					if (tempRpt != null)
					{
						tempRpt.DataSource =
						  Framework.Components.Core.TabChildStructureDatManager.Search(data, SessionVariables.RequestProfile);
						tempRpt.DataBind();
					}
				}
			}
		}
	}
}
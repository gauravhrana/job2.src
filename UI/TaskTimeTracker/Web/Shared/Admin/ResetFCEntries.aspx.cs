using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;

namespace Shared.UI.Web.Admin
{
	public partial class ResetFCEntries : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region Properties

		public int ApplicationId
		{
			get
			{
				return SessionVariables.RequestProfile.ApplicationId;
			}
		}

		public string EntityName
		{
			get
			{
				return txtEntityName.Text;
			}
		}		

		#endregion

		#region Methods

		public int? GetEntityTypeId()
		{
			int? systemEntityTypeId = 0;

			if(!string.IsNullOrEmpty(EntityName))    
			systemEntityTypeId = ((int)(SystemEntity)Enum.Parse(typeof(SystemEntity), EntityName));

			return systemEntityTypeId;
		}

		public void BindGrid()
		{
			int? systemEntityTypeId = GetEntityTypeId();

			var dataQuery = new FieldConfigurationDataModel();

			dataQuery.ApplicationId = ApplicationId;
			if (systemEntityTypeId != 0)
			dataQuery.SystemEntityTypeId = systemEntityTypeId;

			var result = FieldConfigurationDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			result = result.Where(x => x.HorizontalAlignment != ApplicationCommon.IntegerAlignment && x.Name.EndsWith("Id")).ToList();

			gvSearchColumns.DataSource = result;
			gvSearchColumns.DataBind();
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			BindGrid();
		}

		protected void btnModifyAlignment_Click(object sender, EventArgs e)
		{
			
			for (int count = 0; count < gvSearchColumns.Rows.Count; count++)
			{
				if (((CheckBox)gvSearchColumns.Rows[count].FindControl("chkId")).Checked)
				{
					var data = new FieldConfigurationDataModel();
					data.FieldConfigurationId = int.Parse(((TextBox)gvSearchColumns.Rows[count].FindControl("txtFieldConfigurationId")).Text);

					
					var items = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
					var item = items[0];

					data.Name				= item.Name;
					data.ApplicationId		= ApplicationId;
					data.Width				= Convert.ToDecimal(item.Width);
					data.HorizontalAlignment = ApplicationCommon.IntegerAlignment;
					data.GridViewPriority	= item.GridViewPriority;
					data.ControlType		= item.ControlType;
					data.CellCount			= item.CellCount;
					data.DetailsViewPriority = item.DetailsViewPriority;
					data.DisplayColumn		= item.DisplayColumn;
					data.FieldConfigurationModeId = item.FieldConfigurationModeId;
					data.Formatting			= item.Formatting;
					data.SystemEntityTypeId = item.SystemEntityTypeId;
					data.Value				= item.Value;

					FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);
				}
			}

			BindGrid();			
		}

	#endregion

	}
}
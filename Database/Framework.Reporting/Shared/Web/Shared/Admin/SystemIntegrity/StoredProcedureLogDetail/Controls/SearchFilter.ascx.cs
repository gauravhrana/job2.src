using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.StoredProcedureLogDetail.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
		#region variables        

		public Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel SearchParameters
		{
			get
			{
				var data = new Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel();
				if (!string.IsNullOrEmpty(txtSearchConditionStoredProcedureLogId.Text))
					data.StoredProcedureLogId = Convert.ToInt32(txtSearchConditionStoredProcedureLogId.Text);

				return data;
			}
		}

		#endregion

		#region private methods

		protected override void SaveSettings()
		{
			PerferenceUtility.UpdateUserPreference
		   (
				   SettingCategory
			   , ApplicationCommon.SearchFieldName
			   , txtSearchConditionStoredProcedureLogId.Text
		   );

		}

		protected override void GetSettings()
		{
			var category = SettingCategory;
			var value = String.Empty;

			txtSearchConditionStoredProcedureLogId.Text = PerferenceUtility.GetUserPreferenceByKey(Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogId, category);
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				IsControlValid();

				GetSettings();

				RaiseSearch();

				oSearchActionBar.Setup("StoredProcedureLogDetail");	
			}
		}

		#endregion

	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ApplicationManagement.HelpPageContext.Controls
{
	public partial class Generic : ControlGeneric
	{

        #region properties

		

		public int? HelpPageContextId
		{
			get
			{
				if (txtHelpPageContextId.Enabled)
				{
                    return DefaultDataRules.CheckAndGetEntityId(txtHelpPageContextId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtHelpPageContextId.Text);
				}
			}
		}

		public string Name
		{
			get
			{
				return txtName.Text;
			}
		}

		public string Description
		{
			get
			{
				return DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
			}
		}

		public int SortOrder
		{
			get
			{
				return DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
			}
		}

		protected override string ValidationConfigFile
		{
			get
			{
				return Server.MapPath("~/Shared/ApplicationManagement/HelpPageContext/Controls/Validation.xml"); //"R:\HelpPageContexts\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		#endregion properties

		#region private methods

		public override void SetId(int setId, bool chkHelpPageContextId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkHelpPageContextId);
			txtHelpPageContextId.Enabled = chkHelpPageContextId;
			//txtDescription.Enabled = !chkHelpPageContextId;
			//txtName.Enabled = !chkHelpPageContextId;
			//txtSortOrder.Enabled = !chkHelpPageContextId;
		}

		public void LoadData(int HelpPageContextId, bool showId)
		{
            var data = new HelpPageContextDataModel();
			data.HelpPageContextId = HelpPageContextId;
			var oHelpPageContextTable = HelpPageContextDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (oHelpPageContextTable.Rows.Count == 1)
			{
				var row = oHelpPageContextTable.Rows[0];

				if (!showId)
				{
                    txtHelpPageContextId.Text = Convert.ToString(row[HelpPageContextDataModel.DataColumns.HelpPageContextId]);

					dynAuditHistory.Visible = true;
					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup((int)SystemEntity.HelpPageContext, HelpPageContextId, "HelpPageContext");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "HelpPageContext");
				}
				else
				{
					txtHelpPageContextId.Text = String.Empty;
				}
                txtDescription.InnerText = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                txtName.Text             = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                txtSortOrder.Text        = Convert.ToString(row[StandardDataModel.StandardDataColumns.SortOrder]);

				oUpdateInfo.LoadText(oHelpPageContextTable.Rows[0]);
			}
			else
			{
				txtHelpPageContextId.Text = String.Empty;
				txtDescription.InnerText = String.Empty;
				txtName.Text = String.Empty;
				txtSortOrder.Text = String.Empty;
			}
		}

		

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtHelpPageContextId.Visible = isTesting;
			lblHelpPageContextId.Visible = isTesting;
		}

		

		#endregion
	}
}
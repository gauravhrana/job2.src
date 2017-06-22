﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.DatabaseChangeLog.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region variables

		public string Border
		{
			set
			{
				//borderdiv.Style.Add("border", "2px");
				//borderdiv.Style.Add("border-color", "Blue");
				//borderdiv.Style.Add("border-width", "2px");
				//borderdiv.Style.Add("border-style", "groove");
			}

		}
		#endregion

		#region private methods

		protected override void ShowData(int id)
		{
			var data = new Framework.Components.LogAndTrace.DatabaseChangeLogDataModel();
			data.Id = id;

			var dt = Framework.Components.LogAndTrace.DatabaseChangeLogDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

                lblId.Text                       = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.Id]);
                lblDatabaseName.Text             = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.DataBaseName]);
                lblSchemaName.Text               = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.SchemaName]);
                lblEventType.Text                = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.EventType]);
                lblObjectName.Text               = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.ObjectName]);

                lblObjectType.Text               = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.ObjectType]);
                lblRecordDate.Text               = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.RecordDate]);
                lblSystemUser.Text               = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.SystemUser]);
                lblCurrentUser.Text              = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.CurrentUser]);
                lblOriginalUser.Text             = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.OriginalUser]);

                lblbCommandText.Text             = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.CommandText]);
                lblEventData.Text                = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.EventData]);
                lblHostName.Text                 = Convert.ToString(row[Framework.Components.LogAndTrace.DatabaseChangeLogDataModel.DataColumns.HostName]);
				
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblId.Text                       = String.Empty;
			lblDatabaseName.Text             = String.Empty;
			lblSchemaName.Text               = String.Empty;
            lblEventType.Text                = String.Empty;
            lblObjectName.Text               = String.Empty; 
            
            lblObjectType.Text               = String.Empty; 
            lblRecordDate.Text               = String.Empty; 
            lblSystemUser.Text               = String.Empty; 
            lblCurrentUser.Text              = String.Empty; 
            lblOriginalUser.Text             = String.Empty; 
            
            lblbCommandText.Text             = String.Empty;
            lblEventData.Text                = String.Empty;
            lblHostName.Text                 = String.Empty;

        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>();
            }

            return LabelListCore;
        }

		#endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblId.Visible = false;
                lblIdText.Visible = false;
            }

            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            // set basic variables
            DictionaryLabel = CacheConstants.MenuCategoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Menu;

            // set object variable reference            
            PlaceHolderCore = dynId;
            //PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}
}
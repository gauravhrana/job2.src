using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Audit;

namespace Shared.UI.Web.Admin.TypeOfIssue.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

        #region properties		

		public int? TypeOfIssueId
		{
			get
			{
				if (txtTypeOfIssueId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTypeOfIssueId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTypeOfIssueId.Text);
				}
			}
			set
			{
				txtTypeOfIssueId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Name
		{
			get
			{
				return txtName.Text;
			}
			set
			{
				txtName.Text = value ?? String.Empty;
			}
		}

        public string Category
        {
            get
            {
                return txtCategory.Text;
            }
			set
			{
				txtCategory.Text = value ?? String.Empty;
			}
        }

		public string Description
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
			}
			set
			{
				txtDescription.InnerText = value ?? String.Empty;
			}
		}

		public int? SortOrder
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
			}
			set
			{
				txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new TypeOfIssueDataModel();

			data.TypeOfIssueId	= TypeOfIssueId;
			data.Category		= Category;
			data.Name			= Name;
			data.Description	= Description;
			data.SortOrder		= SortOrder;

			if (action == "Insert")
			{
                var dtTypeOfIssue = Framework.Components.Audit.TypeOfIssueDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTypeOfIssue.Rows.Count == 0)
				{
					Framework.Components.Audit.TypeOfIssueDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Audit.TypeOfIssueDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TypeOfIssueID ?
			return TypeOfIssueId;
		}

		public override void SetId(int setId, bool chkTypeOfIssueId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTypeOfIssueId);
			txtTypeOfIssueId.Enabled = chkTypeOfIssueId;
			//txtDescription.Enabled = !chkTypeOfIssueId;
			//txtName.Enabled = !chkTypeOfIssueId;
			//txtSortOrder.Enabled = !chkTypeOfIssueId;
		}

		public void LoadData(int typeOfIssueId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new TypeOfIssueDataModel();
			dataQuery.TypeOfIssueId = typeOfIssueId;

			var items = Framework.Components.Audit.TypeOfIssueDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			TypeOfIssueId	= item.TypeOfIssueId;
			Category		= item.Category;
			Name			= item.Name;
			Description		= item.Description;
			SortOrder		= item.SortOrder;

			if (!showId)
			{
				txtTypeOfIssueId.Text = item.TypeOfIssueId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TypeOfIssue, typeOfIssueId, "TypeOfIssue");
			}
			else
			{
				txtTypeOfIssueId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TypeOfIssueDataModel();

			TypeOfIssueId	= data.TypeOfIssueId;
			Category		= data.Category;
			Description		= data.Description;
			Name			= data.Name;
			SortOrder		= data.SortOrder;
		}

		#endregion		

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtTypeOfIssueId.Visible = isTesting;
			lblTypeOfIssueId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "TypeOfIssue";
			FolderLocationFromRoot = "Shared/Admin/TypeOfIssue";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TypeOfIssue;

			// set object variable reference            
			PlaceHolderCore = dynTypeOfIssueId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion

	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Framework.UI.Web.BaseClasses
{
	public class ControlGenericStandard : ControlGeneric
	{
		public Shared.UI.Web.Controls.UpdateInfo		CoreUpdateInfo			        { get; set; }
		public System.Web.UI.WebControls.TextBox		CoreSystemKey			        { get; set; }
		public System.Web.UI.WebControls.TextBox		CoreControlName			        { get; set; }
		public System.Web.UI.WebControls.TextBox		CoreControlApplicationId		{ get; set; }
		public System.Web.UI.WebControls.DropDownList	CoreControlddlApplicationId		{ get; set; }
		public System.Web.UI.HtmlControls.HtmlTextArea	CoreControlDescription	        { get; set; }
		public CKEditor.NET.CKEditorControl             CoreControlDescriptionEditor    { get; set; }
		public Shared.UI.Web.Controls.KendoTextEditor.KendoEditor CoreControlDescriptionKendoEditor { get; set; }
		public System.Web.UI.WebControls.TextBox		CoreControlSortOrder	        { get; set; }
		
		public int? SystemKeyId
		{
			get
			{
				if (CoreSystemKey.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(CoreSystemKey.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(CoreSystemKey.Text);
				}
			}
			set
			{
				CoreSystemKey.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ApplicationId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(CoreControlApplicationId.Text.Trim());
				else
					return int.Parse(CoreControlddlApplicationId.SelectedItem.Value);
			}
			set
			{
				CoreControlApplicationId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		public string Name
		{
			get
			{
				return CoreControlName.Text;
			}
			set
			{
				CoreControlName.Text = value ?? String.Empty;
			}
		}

		public string Description
		{
			get
			{
				if (CoreControlDescription != null)
					return Framework.Components.DefaultDataRules.CheckAndGetDescription(CoreControlName.Text, CoreControlDescription.InnerText);
				else if (CoreControlDescriptionEditor != null)
					return Framework.Components.DefaultDataRules.CheckAndGetDescription(CoreControlName.Text, CoreControlDescriptionEditor.Text);
				else if (CoreControlDescriptionKendoEditor != null)
					return Framework.Components.DefaultDataRules.CheckAndGetDescription(CoreControlName.Text, CoreControlDescriptionKendoEditor.Text);
				return string.Empty;
			}
			set
			{
				if (CoreControlDescription != null)
					CoreControlDescription.InnerText = value ?? String.Empty;
				else if(CoreControlDescriptionEditor != null)
					CoreControlDescriptionEditor.Text = value ?? String.Empty;
				else if (CoreControlDescriptionKendoEditor != null)
					CoreControlDescriptionKendoEditor.Text = value ?? String.Empty;
			}
		}

		public int? SortOrder
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(CoreControlSortOrder.Text);
			}
			set
			{
				CoreControlSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public virtual void SetData(StandardDataModel data)
		{			
			Description		= data.Description;
			Name			= data.Name;
			SortOrder		= data.SortOrder;
			
			if (CoreControlApplicationId != null)
			{
				ApplicationId = data.ApplicationId;
			}

			CoreUpdateInfo.LoadText(data.UpdatedDate, data.UpdatedBy, data.LastAction);
		}
	}
}
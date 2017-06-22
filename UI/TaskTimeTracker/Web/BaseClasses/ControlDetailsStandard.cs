using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.WebCommon.UI.Web;

namespace Framework.UI.Web.BaseClasses
{
	public class ControlDetailsStandard : ControlDetails
	{
		public Shared.UI.Web.Controls.UpdateInfo CoreUpdateInfo			{ get; set; }
		public System.Web.UI.WebControls.Label	CoreSystemKey			{ get; set; }
		public System.Web.UI.WebControls.Label	CoreControlName			{ get; set; }
		public System.Web.UI.WebControls.Label	CoreControlDescription	{ get; set; }
		public System.Web.UI.WebControls.Label	CoreControlSortOrder	{ get; set; }

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
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(CoreControlName.Text, CoreControlDescription.Text);
			}
			set
			{
				CoreControlDescription.Text = value ?? String.Empty;
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


		public virtual void SetData(DataModel.Framework.DataAccess.StandardDataModel data)
		{
			Name		= data.Name;
			Description = data.Description;
			SortOrder	= data.SortOrder;

			CoreUpdateInfo.LoadText(data.UpdatedDate, data.UpdatedBy, data.LastAction);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Framework.UI.Web.BaseClasses
{

	[DefaultProperty("Text")]
    [ToolboxData("<{0}:PageDelete runat=server></{0}:PageDelete>")]
	public class PageDelete : PageCommon
	{
		
        #region Variables

		public virtual string DeleteIds
		{
			get
			{
				return Convert.ToString(ViewState["DeleteIds"]);
			}
			set
			{
				ViewState["DeleteIds"] = value;
			}
		}

		#endregion

		#region Events

		protected override void OnPreInit(EventArgs e)
		{
            base.SetSiteMasterPagePath();

			base.OnPreInit(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// done here, because its not in view state
			ViewName = "Delete";
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			SuperKey = ApplicationCommon.GetSuperKey();
            SetId = ApplicationCommon.GetSetId();

            if (!string.IsNullOrEmpty(SuperKey))
            {
                var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(PrimaryEntity.Value(), SuperKey);

                foreach (var entityKey in lstEntityKeys)
                {
                    AddDetailControl(false, entityKey);

                    if (string.IsNullOrEmpty(DeleteIds))
                    {
                        DeleteIds = entityKey.ToString();
                    }
                    else
                    {
                        DeleteIds += ", " + entityKey.ToString();
                    }
                }
            }
            else if (SetId != 0)
            {
                AddDetailControl(true, SetId);

                DeleteIds = SetId.ToString();
            }

			ShowAuditHistory(true);
        }	
		
		#endregion

		#region Methods

		override protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
        }

		public virtual void ShowAuditHistory(bool isVisible)
		{			
			foreach (var control in PrimaryPlaceHolder.Controls)
			{
				try
				{
					((ControlDetails)control).IsHistoryVisible = isVisible;
				}
				catch { }
			}
		}
		#endregion

	}
	
}
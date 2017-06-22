using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace Framework.UI.Web.BaseClasses
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BaseClasses.ControlDetails runat=server></{0}:BaseClasses.ControlDetails>")]
	public abstract class ControlCommon : DVCUserControl
    {
		
		public		SystemEntity PrimaryEntity								{ get; set; }
		protected	PlaceHolder PlaceHolderCore								{ get; set; }
		protected	PlaceHolder PlaceHolderAuditHistory						{ get; set; }
		protected	System.Web.UI.HtmlControls.HtmlGenericControl BorderDiv	{ get; set; }

		#region variables

		public bool IsHistoryVisible
		{
			get
			{
				return PlaceHolderAuditHistory.Visible;
			}
			set
			{
				PlaceHolderAuditHistory.Visible = value;
			}
		}

		public string BorderClass
		{
			set
			{
				BorderDiv.Attributes["class"] = value;
			}
		}
		#endregion

	    public virtual void SetBorderClass(string className)
	    {
			BorderDiv.Attributes["class"] = className;
	    }

		protected virtual void Clear()
		{
			//
		}

		protected virtual void EnableControl(bool enabled, ControlCollection controls)
		{
			foreach (Control childControl in controls)
			{
				try
				{
					var webChildControl = (WebControl)childControl;
					webChildControl.Enabled = enabled;
				}
				catch
				{
					//
				}
				finally
				{
					// not sure if this is correct 
					EnableControl(enabled, childControl.Controls);
				}
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
            if (PlaceHolderCore != null)
            {
                EnableControl(SessionVariables.IsTesting, PlaceHolderCore.Controls);
            }
		}

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            // need re-thinking
            if (SessionVariables.IsTesting)
            {
                PlaceHolderAuditHistory.Visible = true;                
            }
            else
            {
                PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "Client");
            }
        }
    }
}

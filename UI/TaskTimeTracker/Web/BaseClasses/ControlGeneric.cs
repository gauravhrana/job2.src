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
	public abstract class ControlGeneric : ControlCommon
	{
		

		public virtual void SetId(int setId, bool checkValue)
		{

		}

		public virtual int? Save(string action)
		{
			return 0;
		}

		
	}
	
}

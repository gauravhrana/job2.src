using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Controls
{
	public class ListHelperAlignment
	{
		public static HorizontalAlign GetHeaderAlignment(string alignment)
		{
			switch (alignment)
			{
				case "Right":
					return HorizontalAlign.Right;
				case "Left":
					return HorizontalAlign.Left;
				case "Center":
					return HorizontalAlign.Center;
				case "Justify":
					return HorizontalAlign.Justify;
				default:
					return HorizontalAlign.Center;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.WebCommon.UI.Web
{
	public class NumberFormatHelper
	{
		public static string SetNumberFormat(decimal _input)
		{
			return _input.ToString(("N"));

		}

	}
}

 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Shared.WebCommon.UI.Web
{
	public class StringFormatHelper
	{		
		public static string FormatCode(string _input)
		{
			return _input.ToUpper().Replace(" ", "_");

		}

        public static string InsertSpaceInCamelCase(string _input)
		{
			return Regex.Replace(_input, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
		}

	}
}


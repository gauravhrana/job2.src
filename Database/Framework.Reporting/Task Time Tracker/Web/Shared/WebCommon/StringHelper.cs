using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Shared.WebCommon.UI.Web
{
    public class StringHelper
    {
        public static string InsertSpaceToCamelCase(string value)
        {
            return Regex.Replace(value, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }
    }
}
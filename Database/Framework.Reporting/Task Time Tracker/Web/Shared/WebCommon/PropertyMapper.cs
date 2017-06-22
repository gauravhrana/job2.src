using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.WebCommon.UI.Web
{

	public class PropertyMapper
	{

		#region Private Methods

		private static object ChangeType(object value, Type conversion)
		{
			var t = conversion;

			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if (value == null || string.IsNullOrEmpty(value.ToString()))
				{
					return null;
				}

				t = Nullable.GetUnderlyingType(t);
			}

			return Convert.ChangeType(value, t, System.Globalization.CultureInfo.InvariantCulture);
		}

		#endregion

		#region Public Methods

		public static void CopyProperties(object objTo, Dictionary<string, string> objFrom)
		{
			var toType = objTo.GetType();
			var toProps = toType.GetProperties();

			if (objFrom != null)
			{
				foreach (var propInfo in toProps)
				{
					if (objFrom.ContainsKey(propInfo.Name))
					{
						var val = ChangeType(objFrom[propInfo.Name], propInfo.PropertyType);
						propInfo.SetValue(objTo, val);
					}

				}
			}
		}

		public static void SetProperty(object objTo, string propertyName, string propertyValue)
		{
			var toType = objTo.GetType();
			var toProps = toType.GetProperties();

			foreach (var propInfo in toProps)
			{
				if (propertyName.Equals(propInfo.Name))
				{		
					// check for type to be int or int?
					if (propInfo.PropertyType == typeof(int) || propInfo.PropertyType == typeof(int?))
					{
						var intValue = 0;
						// if int type then check for invalid values which can not be converted and skip conversion so it will 
						// default value
						if (!string.IsNullOrEmpty(propertyValue) && propertyValue != "All" && propertyValue != "None" && int.TryParse(propertyValue, out intValue))
						{
							var val = ChangeType(propertyValue, propInfo.PropertyType);
							propInfo.SetValue(objTo, val);
						}
					}
					else
					{
						var val = ChangeType(propertyValue, propInfo.PropertyType);
						propInfo.SetValue(objTo, val);
					}
					break;
				}
			}
		}

		#endregion

	}

}
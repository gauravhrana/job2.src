using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Framework.DataAccess
{

    [Serializable]
	public class StandardDataModel : BaseDataModel
	{
		public class StandardDataColumns : BaseDataColumns
		{
			public const string Name		= "Name";
			public const string Description = "Description";
			public const string SortOrder	= "SortOrder";
		}

		//public static readonly StandardDataModel Empty = new StandardDataModel();

		public string		Name			{ get; set; }
		public string		Description		{ get; set; }
		public int?			SortOrder		{ get; set; }


		virtual public string ToURLQuery()
		{
			var answer = string.Empty;

			foreach (var prop in GetType().GetProperties())
			{
				answer += string.Format("{0}={1}", prop.Name, prop.GetValue(this, null));
			}

			return answer;
		}

		virtual public string ToSQLTraceInfo()
		{
			var answer = string.Empty;

			foreach (var prop in GetType().GetProperties())
			{
				if (prop.PropertyType == typeof(string))
				{
					var o = prop.GetValue(this, null);

					if (o == null)
					{
						answer += string.Format("\n@{0} = {1}", prop.Name, "NULL");
					}
					else
					{
						answer += string.Format("\n@{0} = '{1}'", prop.Name, o);
					}
				}
				else
				{
					answer += string.Format("\n@{0} = {1}", prop.Name, prop.GetValue(this, null));
				}
			}

			return answer;
		}

	}
}

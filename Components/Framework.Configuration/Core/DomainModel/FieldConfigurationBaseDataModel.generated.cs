using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.Framework.Configuration
{

	[Serializable]
	public partial class FieldConfigurationBaseDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string FieldConfigurationBaseId = "FieldConfigurationBaseId";
			public const string Name = "Name";
			public const string Value = "Value";
			public const string ControlType = "ControlType";
			public const string Formatting = "Formatting";
			public const string Version = "Version";
			public const string Width = "Width";
		}

		public static readonly FieldConfigurationBaseDataModel Empty = new FieldConfigurationBaseDataModel();

	}
}

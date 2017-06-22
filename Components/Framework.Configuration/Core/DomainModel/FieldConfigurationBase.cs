using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.Framework.Configuration
{

	public partial class FieldConfigurationBaseDataModel : BaseModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? FieldConfigurationBaseId { get; set; }
		[IncludeInSearch] 
		public string Name { get; set; }
		public string Value { get; set; }
		public string ControlType { get; set; }
		public string Formatting { get; set; }
		public string Version { get; set; }
		public int? Width { get; set; }

	}
}

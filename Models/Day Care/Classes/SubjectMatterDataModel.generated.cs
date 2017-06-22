using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class SubjectMatterDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SubjectMatterId = "SubjectMatterId";
		}

		public static readonly SubjectMatterDataModel Empty = new SubjectMatterDataModel();

	}
}

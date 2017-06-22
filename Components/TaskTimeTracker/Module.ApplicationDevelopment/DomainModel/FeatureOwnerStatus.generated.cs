using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

	public partial class FeatureOwnerStatusDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string FeatureOwnerStatusId = "FeatureOwnerStatusId";
		}

		public static readonly FeatureOwnerStatusDataModel Empty = new FeatureOwnerStatusDataModel();

		public int? FeatureOwnerStatusId { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
    [Serializable]
	public class UseCasePackageXUseCaseDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string UseCasePackageId		 = "UseCasePackageId";
			public const string UseCasePackageXUseCaseId = "UseCasePackageXUseCaseId";
			public const string UseCaseId				 = "UseCaseId";

			public const string UseCasePackage			 = "UseCasePackage";
			public const string UseCase				     = "UseCase";
		}

        public static readonly UseCasePackageXUseCaseDataModel Empty = new UseCasePackageXUseCaseDataModel();

		public int? UseCasePackageXUseCaseId		 { get; set; }
		public int? UseCasePackageId				 { get; set; }
		public int? UseCaseId						 { get; set; }

		public string UseCasePackage				 { get; set; }
		public string UseCase						 { get; set; }

	}
}



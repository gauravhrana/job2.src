﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{
	public class FeatureRuleCategoryDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FeatureRuleCategoryId = "FeatureRuleCategoryId";
		}

		public int? FeatureRuleCategoryId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"FeatureRuleCategoryId=" + FeatureRuleCategoryId
		}
	}
}

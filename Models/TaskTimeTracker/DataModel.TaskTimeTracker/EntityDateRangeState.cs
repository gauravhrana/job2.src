﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker
{
    [Serializable]
    [Table("EntityDateRangeState")]
	public class EntityDateRangeStateDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string EntityDateRangeStateId				= "EntityDateRangeStateId";
			public const string StartDate							= "StartDate";
			public const string EndDate								= "EndDate";
			public const string SystemEntityId						= "SystemEntityId";
			public const string KeyId								= "KeyId";
			public const string EntityDateRangeStateTypeId			= "EntityDateRangeStateTypeId";
			public const string Notes								= "Notes";
			public const string EntityDateRangeStateType			= "EntityDateRangeStateType";
		}

        public static readonly EntityDateRangeStateDataModel Empty = new EntityDateRangeStateDataModel();

        [Key]
		public int? EntityDateRangeStateId		{ get; set; }
		public DateTime? StartDate				{ get; set; }
		public DateTime? EndDate				{ get; set; }
		public int? SystemEntityId				{ get; set; }
		public int? KeyId						{ get; set; }
		public int? EntityDateRangeStateTypeId	{ get; set; }

		public string EntityDateRangeStateType	{ get; set; }
		public string Notes						{ get; set; }

	}

}
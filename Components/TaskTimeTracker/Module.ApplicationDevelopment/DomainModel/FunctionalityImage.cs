using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityImageDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string FunctionalityImageId	= "FunctionalityImageId";
			public const string Title					= "Title";
			public const string Image					= "Image";			
			public const string Application				= "Application";
            public const string Description				= "Description";
			public const string ApplicationId			= "ApplicationId";
		}

		public static readonly FunctionalityImageDataModel Empty = new FunctionalityImageDataModel();


		public int? FunctionalityImageId { get; set; }
		public string Title				 { get; set; }
		public byte[] Image				 { get; set; }		
		public string Application		 { get; set; }
        public string Description		 { get; set; }

	}
}
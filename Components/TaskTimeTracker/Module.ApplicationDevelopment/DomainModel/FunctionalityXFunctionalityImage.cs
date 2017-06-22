using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityXFunctionalityImageDataModel : StandardDataModel
    {
		public class DataColumns : BaseDataColumns
        {
            public const string FunctionalityXFunctionalityImageId = "FunctionalityXFunctionalityImageId";
            public const string FunctionalityImageId			   = "FunctionalityImageId";
            public const string FunctionalityImage				   = "FunctionalityImage";
            public const string FunctionalityId					   = "FunctionalityId";
            public const string Functionality					   = "Functionality";
            public const string KeyString						   = "KeyString";
            public const string Title							   = "Title";            
            public const string CreatedBy						   = "CreatedBy";           
        }

		public static readonly FunctionalityXFunctionalityImageDataModel Empty = new FunctionalityXFunctionalityImageDataModel();

        public int? FunctionalityXFunctionalityImageId  { get; set; }
        public int? FunctionalityImageId				{ get; set; }
        public string FunctionalityImage				{ get; set; }
        public int? FunctionalityId						{ get; set; }
        public string Functionality						{ get; set; }
        public string KeyString							{ get; set; }
        public string Title								{ get; set; }        
        public string CreatedBy							{ get; set; }        

    }
}

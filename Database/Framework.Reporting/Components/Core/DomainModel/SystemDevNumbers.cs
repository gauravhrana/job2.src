using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class SystemDevNumbersDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string SystemDevNumbersId = "SystemDevNumbersId";
            public const string Description        = "Description";
            public const string RangeFrom          = "RangeFrom";
            public const string RangeTo            = "RangeTo";
            public const string PersonId           = "PersonId";
            public const string Person             = "Person";
            public const string ApplicationUserId  = "ApplicationUserId";
            public const string ApplicationUser    = "ApplicationUser";
        }

		public static readonly SystemDevNumbersDataModel Empty = new SystemDevNumbersDataModel();

        public int?		SystemDevNumbersId	{ get; set; }
        public string	Description			{ get; set; }
		public int?		RangeFrom			{ get; set; }
		public int?		RangeTo				{ get; set; }
		public int?		PersonId			{ get; set; }
		public string	Person				{ get; set; }
		public int?		ApplicationUserId	{ get; set; }
		public string	ApplicationUser		{ get; set; }
   
    }
}

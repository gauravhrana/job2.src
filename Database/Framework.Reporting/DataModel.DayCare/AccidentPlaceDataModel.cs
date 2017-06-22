using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

    public partial class AccidentPlaceDataModel : StandardModel
    {

		public class DataColumns : StandardColumns
        {
            public const string AccidentPlaceId = "AccidentPlaceId";
            
        }

        public static readonly AccidentPlaceDataModel Empty = new AccidentPlaceDataModel();

		[PrimaryKey]
        public int? AccidentPlaceId { get; set; }

    }
}

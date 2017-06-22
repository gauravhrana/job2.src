using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class DiaperStatusDataModel : StandardModel
    {

        public class DataColumns  
        {
            public const string DiaperStatusId = "DiaperStatusId";
        }

        public static readonly DiaperStatusDataModel Empty = new DiaperStatusDataModel();

		[PrimaryKey]
        public int? DiaperStatusId { get; set; }

    }
}

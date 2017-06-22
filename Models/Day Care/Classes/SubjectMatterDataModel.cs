using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class SubjectMatterDataModel : StandardModel
    {
		[PrimaryKey, IncludeInSearch]
        public int? SubjectMatterId { get; set; }	
	}
}

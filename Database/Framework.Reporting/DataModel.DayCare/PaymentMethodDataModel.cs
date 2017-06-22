using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class PaymentMethodDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string PaymentMethodId = "PaymentMethodId";

		}

		public static readonly PaymentMethodDataModel Empty = new PaymentMethodDataModel();

		[PrimaryKey]
		public int? PaymentMethodId { get; set; }
	}
}

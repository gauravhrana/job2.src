using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class PaymentMethodDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PaymentMethodId = "PaymentMethodId";
		}

		public static readonly PaymentMethodDataModel Empty = new PaymentMethodDataModel();

	}
}

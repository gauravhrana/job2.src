using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker
{
	public class ClientDataModel : StandardDataModel
    {
		public class DataColumns : StandardDataColumns
        {
            public const string ClientId		= "ClientId";            
        }
		
        public int?		ClientId		{ get; set; }         

        public string ToURLQuery()
        {
            return String.Empty; //"ClientId=" + ClientId
        }
    }
}

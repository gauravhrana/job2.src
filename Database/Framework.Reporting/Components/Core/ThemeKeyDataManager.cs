using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class ThemeKeyDataManager : StandardDataManager
	{
        public static List<ThemeKeyDataModel> GetThemeKeyList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ThemeKeyDataModel.Empty, requestProfile, 0);
        } 
       
	}
}

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
	public partial class ThemeCategoryDataManager : StandardDataManager
	{
        public static List<ThemeCategoryDataModel> GetThemeCategoryList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ThemeCategoryDataModel.Empty, requestProfile, 0);
        } 
    }
}


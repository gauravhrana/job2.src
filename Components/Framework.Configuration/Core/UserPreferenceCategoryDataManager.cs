using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
    public partial class UserPreferenceCategoryDataManager : StandardDataManager
	{
        public static List<UserPreferenceCategoryDataModel> GetUserPreferenceCategoryList(RequestProfile requestProfile)
        {
            return GetEntityDetails(UserPreferenceCategoryDataModel.Empty, requestProfile, 0);
        } 

	}
}

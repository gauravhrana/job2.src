using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
    public partial class FunctionalityActiveStatusDataManager : StandardDataManager
    {
        public static List<FunctionalityActiveStatusDataModel> GetFunctionalityActiveStatusList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityActiveStatusDataModel.Empty, requestProfile, 0);
        }       
          

    }
}

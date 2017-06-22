using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Dapper;
using System.Data;
using Framework.CommonServices.BusinessDomain.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class DBProjectNameDataManager : StandardDataManager
	{       

        #region Save

        public static void Save(string name, string component, int sortOrder, RequestProfile requestProfile)
        {
            var obj = new DBProjectNameDataModel();
            obj.ApplicationId = requestProfile.ApplicationId;
            obj.Name = name;
            obj.Description = name;
            obj.SortOrder = sortOrder;

            var dt = DBProjectNameDataManager.GetEntityDetails(obj, requestProfile);

            if (dt.Count == 0)
            {
                DBProjectNameDataManager.Create(obj, requestProfile);
            }
            else if (dt.Count == 1)
            {
                obj.DBProjectNameId = dt[0].DBProjectNameId;
                DBProjectNameDataManager.Update(obj, requestProfile);
            }
        }

        #endregion            


	}
}

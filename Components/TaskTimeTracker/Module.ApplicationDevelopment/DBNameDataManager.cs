using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using System.Runtime.CompilerServices;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class DBNameDataManager : StandardDataManager
    {
   
        #region Save

        public static void Save(string name, string component, int sortOrder, RequestProfile requestProfile)
        {
            var obj = new DBNameDataModel();
            obj.Name = name;
            obj.ApplicationId = requestProfile.ApplicationId;
            obj.Description = name;
            obj.SortOrder = sortOrder;
            var list = DBNameDataManager.GetEntityDetails(obj, requestProfile);

            if (list.Count == 0)
            {
                DBNameDataManager.Create(obj, requestProfile);
            }
            else if (list.Count == 1)
            {
                obj.DBNameId = list[0].DBNameId;
                DBNameDataManager.Update(obj, requestProfile);
            }
        }

        #endregion   

        #region GetEntitySearch

        static public List<DBNameDataModel> GetEntitySearch(DBNameDataModel obj, RequestProfile requestProfile)
        {
            var dt = Search(obj, requestProfile);//SessionVariables.ApplicationMode);

            var list = ToList(dt);

            return list;
        }

        #endregion

        #region ToList

        static private List<DBNameDataModel> ToList(DataTable dt)
        {
            var list = new List<DBNameDataModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var dataItem = new DBNameDataModel();

                    dataItem.DBNameId = (int?)dr[DBNameDataModel.DataColumns.DBNameId];

                    SetStandardInfo(dataItem, dr);

                    list.Add(dataItem);
                }
            }
            return list;
        }

        #endregion   
    
	}
}

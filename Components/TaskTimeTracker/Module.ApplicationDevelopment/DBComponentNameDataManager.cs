using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class DBComponentNameDataManager : StandardDataManager
    { 

        #region Save

        public static void Save(string name, string component, int sortOrder, RequestProfile requestProfile)
        {
            var obj = new DBComponentNameDataModel();
            obj.ApplicationId = requestProfile.ApplicationId;
            obj.Name = name;
            obj.Description = name;
            obj.SortOrder = sortOrder;

            var dt = DBComponentNameDataManager.Search(obj, requestProfile);

            if (dt.Rows.Count == 0)
            {
                DBComponentNameDataManager.Create(obj, requestProfile);
            }
            else if (dt.Rows.Count == 1)
            {
                obj.DBComponentNameId = (int)(dt.Rows[0][DBComponentNameDataModel.DataColumns.DBComponentNameId]);
                DBComponentNameDataManager.Update(obj, requestProfile);
            }
        }

        #endregion

        #region GetEntitySearch

        static public List<DBComponentNameDataModel> GetEntitySearch(DBComponentNameDataModel obj, RequestProfile requestProfile)
        {
            var dt = Search(obj, requestProfile);//SessionVariables.ApplicationMode);

            var list = ToList(dt);

            return list;
        }

        #endregion

        #region ToList

        static private List<DBComponentNameDataModel> ToList(DataTable dt)
        {
            var list = new List<DBComponentNameDataModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var dataItem = new DBComponentNameDataModel();

                    dataItem.DBComponentNameId = (int?)dr[DBComponentNameDataModel.DataColumns.DBComponentNameId];

                    SetStandardInfo(dataItem, dr);

                    list.Add(dataItem);
                }
            }
            return list;
        }

        #endregion

	}
}

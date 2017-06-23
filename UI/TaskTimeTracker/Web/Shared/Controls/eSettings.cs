using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Controls
{
    public class eSettings
    {
        public static void UpdateGridTableInCache(int systemEntityTypeId, int auditId)
        {
            var obj = new FieldConfigurationDataModel();
            var data = new DataTable();
            obj.SystemEntityTypeId = systemEntityTypeId;
            data = FieldConfigurationDataManager.Search(obj, auditId,SessionVariables.ApplicationMode);
            
			var dtInSession = (DataTable)SessionVariables.GridColumnsTable;
            
			if (dtInSession != null)
            {
                var odt = dtInSession.Clone();
                var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
                if (datarows.Length > 0)
                {
                    foreach (var dr in datarows)
                    {
                        dtInSession.Rows.Remove(dr);
                    }
                }

                dtInSession.Merge(data);
                SessionVariables.GridColumnsTable = dtInSession;
            }
            else
            {
                SessionVariables.GridColumnsTable = data;
            }
        }

        public static DataTable GetColumns(int applicationId, int auditId)
        {
            return FieldConfigurationUtility.GetFieldConfigurations(19000, -15001, string.Empty);
        }

        public static DataTable GetGridViewColumns(int systemEntityTypeId, int auditId)
        {
            var obj = new FieldConfigurationDataModel();
            var odt = new DataTable();
            if (SessionVariables.AEFLTable == null)
            {
                obj.SystemEntityTypeId = systemEntityTypeId;
                odt = FieldConfigurationDataManager.Search(obj, auditId,SessionVariables.ApplicationMode);
                SessionVariables.AEFLTable = odt;
            }
            else
            {
                var dtInSession = SessionVariables.AEFLTable;
                odt = dtInSession.Clone();
                var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
                if (datarows.Length > 0)
                {
                    foreach (var dr in datarows)
                    {
                        odt.ImportRow(dr);
                    }
                }
                else
                {
                    obj.SystemEntityTypeId = systemEntityTypeId;
					odt = FieldConfigurationDataManager.Search(obj, auditId, SessionVariables.ApplicationMode);
                    dtInSession.Merge(odt);
                    SessionVariables.AEFLTable = dtInSession;
                }
            }

            return odt;
        }


    }
}
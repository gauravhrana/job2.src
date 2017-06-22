using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismSample.Infrastructure
{
    public class FieldConfigurationUtility
    {

        public static Dictionary<int, List<FieldConfigurationModeDataModel>> ValidFCModes = null;

        static FieldConfigurationUtility()
        {
            ValidFCModes = new Dictionary<int, List<FieldConfigurationModeDataModel>>();
        }

        public static List<FieldConfigurationDataModel> GetFieldConfigurations(string systemEntity)
        {
            var fieldConfigurationDataModel = new FieldConfigurationDataModel();

            fieldConfigurationDataModel.SystemEntityTypeId = Helper.GetSystemEntityId(systemEntity);
            fieldConfigurationDataModel.FieldConfigurationModeId = FieldConfigurationModeDataManager.GetFCModeIdByName("Standard", ApplicationCommon.ReqProfile);

            var result = FieldConfigurationDataManager.GetEntityDetails(fieldConfigurationDataModel, ApplicationCommon.ReqProfile);

            result = result.OrderBy(x => x.GridViewPriority).ToList();

            return result;
        }

        public static List<FieldConfigurationDataModel> GetFieldConfigurations(SystemEntity systemEntity, int fcModeId)
        {
            var fieldConfigurationDataModel = new FieldConfigurationDataModel();

            fieldConfigurationDataModel.SystemEntityTypeId = systemEntity.Value();
            fieldConfigurationDataModel.FieldConfigurationModeId = fcModeId;

            var result = FieldConfigurationDataManager.GetEntityDetails(fieldConfigurationDataModel, ApplicationCommon.ReqProfile);
            return result;
        }

        public static List<FieldConfigurationModeDataModel> GetApplicationModes(string entityName)
        {
            var fcModes = FieldConfigurationModeDataManager.GetEntityDetails(FieldConfigurationModeDataModel.Empty, ApplicationCommon.ReqProfile);
            return fcModes;
        }

        static List<FieldConfigurationModeDataModel> GetApplicableModesListByEntity(List<FieldConfigurationDataModel> fcRecordsByEntity, List<FieldConfigurationModeDataModel> modes)
        {
            var listValidModes = new List<FieldConfigurationModeDataModel>();

            foreach (var fcModeItem in modes)
            {
                foreach (var fcItem in fcRecordsByEntity)
                {
                    var a = fcItem.FieldConfigurationModeId;
                    var b = fcModeItem.FieldConfigurationModeId;

                    if (a == b)
                    {
                        var count = listValidModes.Where(x => x.FieldConfigurationModeId == a).ToList().Count;
                        if (count == 0)
                        {
                            listValidModes.Add(fcModeItem);
                        }
                    }
                }
            }

            return listValidModes;

        }

        public static List<FieldConfigurationModeDataModel> GetApplicableModesList(SystemEntity systemEntityTypeId)
        {
            var listValidModes = new List<FieldConfigurationModeDataModel>();

            if (ValidFCModes.ContainsKey(systemEntityTypeId.Value()))
            {
                listValidModes = ValidFCModes[systemEntityTypeId.Value()];
            }
            else
            {
                var validModes = new List<FieldConfigurationModeDataModel>();

                var data = new FieldConfigurationDataModel();

                data.SystemEntityTypeId = Convert.ToInt32(systemEntityTypeId);
                data.ApplicationId = ApplicationCommon.ReqProfile.ApplicationId;

                var fcRecordsByEntity = FieldConfigurationDataManager.GetEntityDetails(data, ApplicationCommon.ReqProfile, AuditDetailsFlag.DoNotFetchDetails.Value());

                var dataMode = new FieldConfigurationModeDataModel();
                dataMode.ApplicationId = ApplicationCommon.ReqProfile.ApplicationId;
                var fcModeList = FieldConfigurationModeDataManager.GetEntityDetails(dataMode, ApplicationCommon.ReqProfile, AuditDetailsFlag.DoNotFetchDetails.Value());

                //var modeapplicable = false;

                var validModes1 = new List<FieldConfigurationModeDataModel>();
                var validModes2 = new List<FieldConfigurationModeDataModel>();
                var fcModesByEntity = new List<FieldConfigurationModeDataModel>();

                //hdnFieldConfigurationModeCategory.Value = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.FieldConfigurationModeCategoryKey, SettingCategory);

                var fcModeCategoryData = new FieldConfigurationModeCategoryDataModel();
                fcModeCategoryData.Name = "List";

                var fcModeCategoryList = FieldConfigurationModeCategoryDataManager.GetEntityDetails(fcModeCategoryData, ApplicationCommon.ReqProfile, AuditDetailsFlag.DoNotFetchDetails.Value());
                var fcModeCategoryId = fcModeCategoryList[0].FieldConfigurationModeCategoryId.ToString();

                var applicationModeId = 0;
                var modedData = new ApplicationModeDataModel();

                //if (SessionVariables.IsTesting)
                //    modedData.Name = "Testing";
                //else
                //    modedData.Name = "Live";

                // Temp assumption
                modedData.Name = "Live";

                modedData.ApplicationId = ApplicationCommon.ReqProfile.ApplicationId;
                //modedata.Name = SessionVariables.UserApplicationMode;
                var applicationModeList = ApplicationModeDataManager.GetEntityDetails(modedData, ApplicationCommon.ReqProfile, AuditDetailsFlag.DoNotFetchDetails.Value());

                if (applicationModeList.Count > 0)
                {
                    applicationModeId = applicationModeList[0].ApplicationModeId.Value;
                }

                //Added SystemEntityTypeId = 3000 only for testing FCModeCategory
                if (!string.IsNullOrEmpty(fcModeCategoryId) && applicationModeId != 0)
                {
                    var modelAppModexFCMode = new ApplicationModeXFieldConfigurationModeDataModel();
                    modelAppModexFCMode.ApplicationModeId = applicationModeId;

                    var appModeList = ApplicationModeXFieldConfigurationModeDataManager.GetEntityDetails(modelAppModexFCMode, ApplicationCommon.ReqProfile, AuditDetailsFlag.DoNotFetchDetails.Value());

                    var distinctFieldValues = appModeList.OrderBy(x => x.FieldConfigurationModeId).Select(x => x.FieldConfigurationModeId).Distinct().ToList();

                    var modeIds = new List<int>();

                    foreach (var valModeId in distinctFieldValues)
                    {
                        modeIds.Add(valModeId.Value);
                    }

                    //var rows = fcModeList.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " in (" + modeIds + ")");
                    var selection = fcModeList.Where(x => modeIds.Contains(x.FieldConfigurationModeId.Value)).ToList();
                    if (selection.Count > 0)
                    {
                        foreach (var lstItem in selection)
                        {
                            validModes1.Add(lstItem);
                        }
                    }

                    var modelFCModeCategoryXFCMode = new FieldConfigurationModeCategoryXFCModeDataModel();
                    modelFCModeCategoryXFCMode.FieldConfigurationModeCategoryId = int.Parse(fcModeCategoryId);

                    var fcModeCategoryTempList = FieldConfigurationModeCategoryXFCModeDataManager.GetEntityDetails(modelFCModeCategoryXFCMode, ApplicationCommon.ReqProfile, AuditDetailsFlag.DoNotFetchDetails.Value());


                    distinctFieldValues = fcModeCategoryTempList.OrderBy(x => x.FieldConfigurationModeId).Select(x => x.FieldConfigurationModeId).Distinct().ToList();

                    modeIds = new List<int>();

                    foreach (var valModeId in distinctFieldValues)
                    {
                        modeIds.Add(valModeId.Value);
                    }

                    selection = fcModeList.Where(x => modeIds.Contains(x.FieldConfigurationModeId.Value)).ToList();
                    if (selection.Count > 0)
                    {
                        foreach (var lstItem in selection)
                        {
                            validModes2.Add(lstItem);
                        }
                    }

                    validModes = validModes1.Intersect(validModes2).ToList();

                    fcModesByEntity = GetApplicableModesListByEntity(fcRecordsByEntity, validModes);

                    //Last Step (join the result with all the fc modes list by entity)
                    if (validModes.Count != 0 && fcModesByEntity.Count != 0)
                    {
                        fcModeList = (from a in fcModeList
                                      join b in fcModesByEntity
                                 on a.FieldConfigurationModeId equals b.FieldConfigurationModeId
                                 into g
                                      where g.Count() > 0
                                      select a).ToList();

                    }
                    else
                    {
                        fcModeList.Clear();
                    }

                    fcModeList = fcModeList.Distinct().ToList();

                    validModes = fcModeList;

                }
                else
                {
                    validModes = GetApplicableModesListByEntity(fcRecordsByEntity, fcModeList);
                }

                listValidModes = validModes.OrderBy(x => x.SortOrder).ToList();

                ValidFCModes.Add(systemEntityTypeId.Value(), listValidModes);
            }

            return listValidModes;
        }

    }
}

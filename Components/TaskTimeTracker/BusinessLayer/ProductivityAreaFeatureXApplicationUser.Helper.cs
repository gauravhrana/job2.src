using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ProductivityAreaFeatureXApplicationUserDataManager
    {
        public class DataColumns : BaseDataModel.BaseDataColumns
        {
            public const string ProductivityAreaFeatureXApplicationUserId    = "ProductivityAreaFeatureXApplicationUserId";
            public const string ApplicationUserId                            = "ApplicationUserId";
            public const string ProductivityAreaFeatureId                    = "ProductivityAreaFeatureId";

            public const string ApplicationUser                              = "ApplicationUser";
            public const string ProductivityAreaFeature                      = "ProductivityAreaFeature";
        }

		public class Data : BaseDataModel
        {
            public int? ProductivityAreaFeatureXApplicationUserId       { get; set; }
            public int? ApplicationUserId                               { get; set; }
            public int? ProductivityAreaFeatureId                       { get; set; }

            public string ApplicationUser                               { get; set; }
            public string ProductivityAreaFeature                       { get; set; }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";
                switch (dataColumnName)
                {
                    case DataColumns.ProductivityAreaFeatureXApplicationUserId:
                        if (ProductivityAreaFeatureXApplicationUserId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ProductivityAreaFeatureXApplicationUserId, ProductivityAreaFeatureXApplicationUserId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ProductivityAreaFeatureXApplicationUserId);
                        }
                        break;

                    case DataColumns.ApplicationUserId:
                        if (ApplicationUserId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ApplicationUserId, ApplicationUserId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationUserId);
                        }
                        break;

                    case DataColumns.ProductivityAreaFeatureId:
                        if (ProductivityAreaFeatureId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ProductivityAreaFeatureId, ProductivityAreaFeatureId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ProductivityAreaFeatureId);
                        }
                        break;

                    case DataColumns.ApplicationUser:
                        if (!string.IsNullOrEmpty(ApplicationUser))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.ApplicationUser, ApplicationUser);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationUser);
                        }
                        break;

                    case DataColumns.ProductivityAreaFeature:
                        if (!string.IsNullOrEmpty(ProductivityAreaFeature))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.ProductivityAreaFeature, ProductivityAreaFeature);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ProductivityAreaFeature);
                        }
                        break;
                }
                return returnValue;
            }

        }
    }
}

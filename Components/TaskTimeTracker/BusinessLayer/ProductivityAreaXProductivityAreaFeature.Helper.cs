using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ProductivityAreaXProductivityAreaFeatureDataManager
    {

		public class DataColumns : BaseDataModel.BaseDataColumns
        {
            public const string ProductivityAreaXProductivityAreaFeatureId  = "ProductivityAreaXProductivityAreaFeatureId";
            public const string ProductivityAreaId                          = "ProductivityAreaId";
            public const string ProductivityAreaFeatureId                   = "ProductivityAreaFeatureId";

            public const string ProductivityArea                            = "ProductivityArea";
            public const string ProductivityAreaFeature                     = "ProductivityAreaFeature";
        }

		public class Data : BaseDataModel
        {
            public int? ProductivityAreaXProductivityAreaFeatureId      { get; set; }
            public int? ProductivityAreaId                              { get; set; }
            public int? ProductivityAreaFeatureId                       { get; set; }

            public string ProductivityArea                              { get; set; }
            public string ProductivityAreaFeature                       { get; set; }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";
                switch (dataColumnName)
                {
                    case DataColumns.ProductivityAreaXProductivityAreaFeatureId:
                        if (ProductivityAreaXProductivityAreaFeatureId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ProductivityAreaXProductivityAreaFeatureId, ProductivityAreaXProductivityAreaFeatureId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ProductivityAreaXProductivityAreaFeatureId);
                        }
                        break;

                    case DataColumns.ProductivityAreaId:
                        if (ProductivityAreaId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ProductivityAreaId, ProductivityAreaId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ProductivityAreaId);
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

                    case DataColumns.ProductivityArea:
                        if (!string.IsNullOrEmpty(ProductivityArea))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.ProductivityArea, ProductivityArea);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ProductivityArea);
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

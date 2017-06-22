using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class UserPreferenceKey
    {
        public class DataColumns : Framework.Components.DataAccess.DomainModel.BaseModel.BaseDataColumns
        {
            public const string UserPreferenceKeyId = "UserPreferenceKeyId";
            public const string Name                = "Name";
            public const string Value               = "Value";
            public const string DataTypeId          = "DataTypeId";
            public const string DataType            = "DataType";
            public const string Description         = "Description";
            public const string SortOrder           = "SortOrder";
        }

        public class Data : Framework.Components.DataAccess.DomainModel.BaseModel
        {
			public int?		UserPreferenceKeyId		{ get; set; }
			public string	Name					{ get; set; }
			public string	Value					{ get; set; }
			public int?		DataTypeId				{ get; set; }
			public string	DataType				{ get; set; }
            public string	Description				{ get; set; }
            public int?		SortOrder				{ get; set; }

			public string ToURLQuery()
			{
				return string.Empty; //"UserPreferenceKeyId=" + UserPreferenceKeyId
			}

			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";

				switch (dataColumnName)
                {

					case DataColumns.UserPreferenceKeyId:
                        if (UserPreferenceKeyId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.UserPreferenceKeyId, UserPreferenceKeyId);
                                     
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceKeyId);
														
                        }
                        break;

					case DataColumns.Name:
                        if (!string.IsNullOrEmpty(Name))
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Name, Name);
                        	                           
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Name);
														
                        }
                        break;

					case DataColumns.Value:
                        if (!string.IsNullOrEmpty(Value))
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Value, Value);
                        	                            
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Value);
														
                        }
                        break;

                    case DataColumns.DataTypeId:
                        if (DataTypeId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.DataTypeId, DataTypeId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.DataTypeId);
														
                        }
                        break;

					case DataColumns.DataType:
						if (!string.IsNullOrEmpty(Description))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.DataType, DataType);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.DataType);

						}
						break;

					case DataColumns.Description:
                       if (!string.IsNullOrEmpty(Description))
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Description, Description);														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Description);														
                        }
                        break;

					case DataColumns.SortOrder:
                        if (SortOrder != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.SortOrder, SortOrder);														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.SortOrder);														
                        }
                        break;

                }
                return returnValue;
            }

        }

    }
}

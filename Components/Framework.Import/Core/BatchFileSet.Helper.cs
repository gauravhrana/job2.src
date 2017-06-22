using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.Import
{
    public partial class BatchFileSet
    {
        public class DataColumns : DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns
        {
            public const string BatchFileSetId    = "BatchFileSetId";
            public const string Name              = "Name";
            public const string Description       = "Description";
            public const string CreatedDate       = "CreatedDate";
            public const string CreatedByPersonId = "CreatedByPersonId";
        }

        public class Data : DataModel.Framework.DataAccess.BaseDataModel
        {
			public int?			BatchFileSetId		{ get; set; }
            public string		Name				{ get; set; }
            public string		Description			{ get; set; }
			public DateTime?	CreatedDate			{ get; set; }
			public int?			CreatedByPersonId	{ get; set; }

			public string ToURLQuery()
			{
				return String.Empty; //"BatchFileSetId=" + BatchFileSetId
			}

			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";
				switch (dataColumnName)
				{
					case DataColumns.BatchFileSetId:
						if (BatchFileSetId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BatchFileSetId, BatchFileSetId);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileSetId);

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

                    case DataColumns.CreatedDate:
                        if (CreatedDate != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.CreatedDate, CreatedDate);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.CreatedDate);
														
                        }
                        break;

					case DataColumns.CreatedByPersonId:
                        if (CreatedByPersonId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.CreatedByPersonId, CreatedByPersonId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.CreatedByPersonId);
														
                        }
                        break;

                }
                return returnValue;
            }

        }

    }
}

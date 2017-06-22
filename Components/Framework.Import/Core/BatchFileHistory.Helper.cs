using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.Import
{
    public partial class BatchFileHistory
    {
        public class DataColumns : DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns
        {
            public const string BatchFileHistoryId = "BatchFileHistoryId";
            public const string BatchFileId        = "BatchFileId";
            public const string BatchFileSetId     = "BatchFileSetId";
            public const string BatchFileStatusId  = "BatchFileStatusId";
            public const string UpdatedDate        = "UpdatedDate";
            public const string UpdatedByPersonId  = "UpdatedByPersonId";
            public const string UpdatedByPerson    = "UpdatedByPerson";
            public const string PersonId           = "PersonId";

            public const string Person             = "Person";
            public const string BatchFileStatus    = "BatchFileStatus";
            public const string BatchFileSet       = "BatchFileSet";
        }

        public class Data : DataModel.Framework.DataAccess.BaseDataModel
        {
			public int?			BatchFileHistoryId		 { get; set; }
			public int?			BatchFileId				 { get; set; }
			public int?			BatchFileSetId			 { get; set; }
			public int?			BatchFileStatusId		 { get; set; }
			public DateTime?	UpdatedDate				 { get; set; }
			public int?			UpdatedByPersonId		 { get; set; }
			public string		UpdatedByPerson			 { get; set; }
			public int?			PersonId				 { get; set; }
			public string		Person					 { get; set; }
			public string		BatchFileStatus			 { get; set; }
			public string		BatchFileSet			 { get; set; }

			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";
				switch (dataColumnName)
                {

					case DataColumns.BatchFileHistoryId:
                        if (BatchFileHistoryId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BatchFileHistoryId, BatchFileHistoryId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileHistoryId);
														
                        }
                        break;

					case DataColumns.BatchFileId:
                        if (BatchFileId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BatchFileId, BatchFileId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileId);
														
                        }
                        break;

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

					case DataColumns.BatchFileStatusId:
                        if (BatchFileStatusId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BatchFileStatusId, BatchFileStatusId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileStatusId);
														
                        }
                        break;

					case DataColumns.UpdatedDate:
                        if (UpdatedDate != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.UpdatedDate, UpdatedDate);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UpdatedDate);
														
                        }
                        break;

					case DataColumns.UpdatedByPersonId:
                        if (UpdatedByPersonId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.UpdatedByPersonId, UpdatedByPersonId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UpdatedByPersonId);
														
                        }
                        break;

					case DataColumns.UpdatedByPerson:
						if (!string.IsNullOrEmpty(UpdatedByPerson))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.UpdatedByPerson, UpdatedByPerson);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UpdatedByPerson);

						}
						break;

					case DataColumns.PersonId:
                        if (PersonId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.PersonId, PersonId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.PersonId);
														
                        }
                        break;

					case DataColumns.Person:
						if (!string.IsNullOrEmpty(Person))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Person, Person);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Person);

						}
						break;

					case DataColumns.BatchFileStatus:
						if (!string.IsNullOrEmpty(BatchFileStatus))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.BatchFileStatus, BatchFileStatus);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileStatus);

						}
						break;

					case DataColumns.BatchFileSet:
						if (!string.IsNullOrEmpty(BatchFileSet))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.BatchFileSet, BatchFileSet);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileSet);

						}
						break;

                }
                return returnValue;
            }

        }

    }
}

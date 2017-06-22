using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.Import
{
    public partial class BatchFile
    {
        public class DataColumns : DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns
        {
            public const string BatchFileId        = "BatchFileId";
            public const string Name               = "Name";
            public const string Folder             = "Folder";
            public const string BatchFile          = "BatchFile";
            public const string BatchFileSetId     = "BatchFileSetId";
            public const string Description        = "Description";
            public const string FileTypeId         = "FileTypeId";
            public const string SystemEntityTypeId = "SystemEntityTypeId";
            public const string SystemEntityId     = "SystemEntityId";
            public const string BatchFileStatusId  = "BatchFileStatusId";
            public const string CreatedDate        = "CreatedDate";
            public const string CreatedByPersonId  = "CreatedByPersonId";
            public const string UpdatedDate        = "UpdatedDate";
            public const string UpdatedByPersonId  = "UpdatedByPersonId";
            public const string Errors             = "Errors";

            public const string BatchFileSet       = "BatchFileSet";
            public const string FileType		   = "FileType";
            public const string SystemEntityType   = "SystemEntityType";
            public const string BatchFileStatus    = "BatchFileStatus";
            public const string Person             = "Person";
            public const string CreatedByPerson    = "CreatedByPerson";
            public const string UpdatedByPerson    = "UpdatedByPerson";
        }

        public class Data : DataModel.Framework.DataAccess.BaseDataModel
        {
			public int?			BatchFileId			{ get; set; }
            public string		Name				{ get; set; }
			public string		Folder				{ get; set; }
			public string		BatchFile			{ get; set; }
			public int?			BatchFileSetId		{ get; set; }
            public string		Description			{ get; set;}
			public int?			FileTypeId			{ get; set; }
			public int?			SystemEntityTypeId  { get; set; }
			public int?			SystemEntityId      { get; set; }
			public int?			BatchFileStatusId	{ get; set; }
			public DateTime?	CreatedDate			{ get; set; }
			public int?			CreatedByPersonId	{ get; set; }
			public DateTime?	UpdatedDate			{ get; set; }
			public int?			UpdatedByPersonId	{ get; set; }
			public string		Errors				{ get; set; }
			public string		BatchFileSet		{ get; set; }
			public string		FileType			{ get; set; }
			public string		SystemEntityType	{ get; set; }
			public string		BatchFileStatus		{ get; set; }
			public string		Person				{ get; set; }
			public string		CreatedByPerson		{ get; set; }
			public string		UpdatedByPerson		{ get; set; }

			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";
				switch (dataColumnName)
                {

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

					case DataColumns.Folder:
                        if (!string.IsNullOrEmpty(Folder))
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Folder, Folder);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Folder);
														
                        }
                        break;

					case DataColumns.BatchFile:
                        if (!string.IsNullOrEmpty(BatchFile))
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.BatchFile, BatchFile);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFile, returnValue);
														
                        }
                        break;

					case DataColumns.BatchFileSetId:
                        if (BatchFileSetId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BatchFileSetId, BatchFileSetId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileSetId, returnValue);
														
                        }
                        break;

					case DataColumns.Description:
                        if (!string.IsNullOrEmpty(Description))
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Description, Description);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Description, returnValue);
														
                        }
                        break;

					case DataColumns.FileTypeId:
                        if (FileTypeId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.FileTypeId, FileTypeId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FileTypeId, returnValue);
														
                        }
                        break;


					case DataColumns.SystemEntityTypeId:
                        if (SystemEntityTypeId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.SystemEntityTypeId, SystemEntityTypeId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.SystemEntityTypeId, returnValue);
														
                        }
                        break;

					case DataColumns.SystemEntityId:
						if (SystemEntityTypeId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.SystemEntityId, SystemEntityTypeId);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.SystemEntityId, returnValue);
														
						}
						break;

					case DataColumns.BatchFileStatusId:
                        if (BatchFileStatusId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BatchFileStatusId, BatchFileStatusId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileStatusId, returnValue);
														
                        }
                        break;

					case DataColumns.CreatedDate:
                        if (CreatedDate != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.CreatedDate, CreatedDate);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.CreatedDate, returnValue);
														
                        }
                        break;

                    case DataColumns.CreatedByPersonId:
                        if (CreatedByPersonId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.CreatedByPersonId, CreatedByPersonId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.CreatedByPersonId, returnValue);
														
                        }
                        break;

					case DataColumns.UpdatedDate:
                        if (UpdatedDate != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.UpdatedDate, UpdatedDate);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UpdatedDate, returnValue);
														
                        }
                        break;

					case DataColumns.UpdatedByPersonId:
                        if (UpdatedByPersonId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.UpdatedByPersonId, UpdatedByPersonId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UpdatedByPersonId, returnValue);
														
                        }
                        break;

					case DataColumns.Errors:
						if (!string.IsNullOrEmpty(Errors))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Errors, Errors);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Errors, returnValue);

						}
						break;

					case DataColumns.BatchFileSet:
						if (!string.IsNullOrEmpty(FileType))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.BatchFileSet, BatchFileSet);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileSet, returnValue);

						}
						break;

					case DataColumns.FileType:
						if (!string.IsNullOrEmpty(FileType))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.FileType, FileType);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FileType, returnValue);

						}
						break;

					case DataColumns.SystemEntityType:
						if (!string.IsNullOrEmpty(SystemEntityType))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.SystemEntityType, SystemEntityType);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.SystemEntityType, returnValue);
														
						}
						break;

					case DataColumns.BatchFileStatus:
						if (!string.IsNullOrEmpty(BatchFileStatus))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.BatchFileStatus, BatchFileStatus);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BatchFileStatus, returnValue);
														
						}
						break;

					case DataColumns.Person:
						if (!string.IsNullOrEmpty(Person))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Person, Person);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Person, returnValue);
														
						}
						break;

					case DataColumns.CreatedByPerson:
						if (!string.IsNullOrEmpty(CreatedByPerson))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.CreatedByPerson, CreatedByPerson);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.CreatedByPerson, returnValue);
														
						}
						break;

					case DataColumns.UpdatedByPerson:
						if (!string.IsNullOrEmpty(UpdatedByPerson))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.UpdatedByPerson, UpdatedByPerson);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UpdatedByPerson, returnValue);
														
						}
						break;

                }
                return returnValue;
            }

        }

    }
}

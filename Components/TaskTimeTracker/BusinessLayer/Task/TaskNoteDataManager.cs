using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
	public partial class TaskNoteDataManager : StandardDataManager
    {
		public string GetInsertProcedureScript()
		{
			return base.GetInsertProcedureScript(typeof(TaskNoteDataModel));
		}

		public string GetUpdateProcedureScript()
		{
			return base.GetUpdateProcedureScript(typeof(TaskNoteDataModel));
		}

		public string GetSearchProcedureScript(List<string> searchableColumns)
		{
			return base.GetSearchProcedureScript(searchableColumns, typeof(TaskNoteDataModel));
		}

    }
}

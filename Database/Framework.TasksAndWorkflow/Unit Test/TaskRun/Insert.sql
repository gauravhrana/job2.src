/******************************************************************************
**		Name: TaskRun
*******************************************************************************/

EXEC dbo.TaskRunUpdate @TaskRunId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.TaskRunUpdate @TaskRunId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.TaskRunUpdate @TaskRunId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8


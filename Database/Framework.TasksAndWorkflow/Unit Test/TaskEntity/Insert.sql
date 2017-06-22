/******************************************************************************
**		Name: TaskEntity
*******************************************************************************/

EXEC dbo.TaskEntityUpdate @TaskEntityId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.TaskEntityUpdate @TaskEntityId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.TaskEntityUpdate @TaskEntityId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8


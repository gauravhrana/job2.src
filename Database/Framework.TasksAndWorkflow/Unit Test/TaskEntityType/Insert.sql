/******************************************************************************
**		Name: TaskEntityType
*******************************************************************************/

EXEC dbo.TaskEntityTypeUpdate @TaskEntityTypeId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.TaskEntityTypeUpdate @TaskEntityTypeId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.TaskEntityTypeUpdate @TaskEntityTypeId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8


/******************************************************************************
**		Name: TaskScheduleType
*******************************************************************************/

EXEC dbo.TaskScheduleTypeUpdate @TaskScheduleTypeId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.TaskScheduleTypeUpdate @TaskScheduleTypeId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.TaskScheduleTypeUpdate @TaskScheduleTypeId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8


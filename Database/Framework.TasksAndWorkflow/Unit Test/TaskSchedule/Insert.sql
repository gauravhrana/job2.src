/******************************************************************************
**		Name: TaskSchedule
*******************************************************************************/

EXEC dbo.TaskScheduleUpdate @TaskScheduleId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.TaskScheduleUpdate @TaskScheduleId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.TaskScheduleUpdate @TaskScheduleId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8


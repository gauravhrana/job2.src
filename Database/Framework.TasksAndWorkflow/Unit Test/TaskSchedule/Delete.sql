/******************************************************************************
**		Name: TaskSchedule
*******************************************************************************/

EXEC dbo.TaskScheduleDelete @TaskScheduleId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.TaskScheduleDelete @TaskScheduleId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.TaskScheduleDelete @TaskScheduleId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


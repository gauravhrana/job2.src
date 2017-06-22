/******************************************************************************
**		Name: TaskRun
*******************************************************************************/

EXEC dbo.TaskRunDelete @TaskRunId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.TaskRunDelete @TaskRunId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.TaskRunDelete @TaskRunId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


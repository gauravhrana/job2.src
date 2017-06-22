/******************************************************************************
**		Name: TaskEntity
*******************************************************************************/

EXEC dbo.TaskEntityDelete @TaskEntityId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.TaskEntityDelete @TaskEntityId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.TaskEntityDelete @TaskEntityId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


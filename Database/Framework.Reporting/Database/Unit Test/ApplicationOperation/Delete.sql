/******************************************************************************
**		Name: ApplicationOperation
*******************************************************************************/

EXEC dbo.ApplicationOperationDelete @ApplicationOperationId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.ApplicationOperationDelete @ApplicationOperationId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.ApplicationOperationDelete @ApplicationOperationId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


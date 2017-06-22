/******************************************************************************
**		Name: AuditAction
*******************************************************************************/

EXEC dbo.AuditActionDelete @AuditActionId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.AuditActionDelete @AuditActionId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.AuditActionDelete @AuditActionId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


/******************************************************************************
**		Name: AuditHistory
*******************************************************************************/

EXEC dbo.AuditHistoryDelete @AuditHistoryId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.AuditHistoryDelete @AuditHistoryId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.AuditHistoryDelete @AuditHistoryId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


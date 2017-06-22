/******************************************************************************
**		Name: BatchFileHistory
*******************************************************************************/

EXEC dbo.BatchFileHistoryDelete @BatchFileHistoryId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.BatchFileHistoryDelete @BatchFileHistoryId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.BatchFileHistoryDelete @BatchFileHistoryId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


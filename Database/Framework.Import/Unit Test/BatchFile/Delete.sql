/******************************************************************************
**		Name: BatchFile
*******************************************************************************/

EXEC dbo.BatchFileDelete @BatchFileId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.BatchFileDelete @BatchFileId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.BatchFileDelete @BatchFileId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


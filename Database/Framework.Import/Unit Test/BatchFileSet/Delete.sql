/******************************************************************************
**		Name: BatchFileSet
*******************************************************************************/

EXEC dbo.BatchFileSetDelete @BatchFileSetId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.BatchFileSetDelete @BatchFileSetId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.BatchFileSetDelete @BatchFileSetId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


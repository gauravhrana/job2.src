/******************************************************************************
**		Name: ReleaseLog
*******************************************************************************/

EXEC dbo.ReleaseLogDelete @ReleaseLogId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.ReleaseLogDelete @ReleaseLogId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.ReleaseLogDelete @ReleaseLogId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


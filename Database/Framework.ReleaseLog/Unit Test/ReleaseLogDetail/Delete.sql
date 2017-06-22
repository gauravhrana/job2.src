/******************************************************************************
**		Name: ReleaseLogDetails
*******************************************************************************/

EXEC dbo.ReleaseLogDetailsDelete @ReleaseLogDetailsId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.ReleaseLogDetailsDelete @ReleaseLogDetailsId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.ReleaseLogDetailsDelete @ReleaseLogDetailsId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


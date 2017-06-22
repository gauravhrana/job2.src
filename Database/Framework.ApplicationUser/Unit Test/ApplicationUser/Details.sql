/******************************************************************************
**		Name: ApplicationUser
*******************************************************************************/

EXEC dbo.ApplicationUserDetails @ApplicationUserId = -11	, @Audit = 400	 , @AuditDate = '12/17/2011'
EXEC dbo.ApplicationUserDetails @ApplicationUserId = -12	, @Audit = 400   , @AuditDate = '12/18/2011'
EXEC dbo.ApplicationUserDetails @ApplicationUserId = -31	, @Audit = 400   , @AuditDate = '12/19/2011'


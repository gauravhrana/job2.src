﻿/******************************************************************************
**		Name: ApplicationUser
*******************************************************************************/

EXEC dbo.ApplicationUserDelete @ApplicationUserId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.ApplicationUserDelete @ApplicationUserId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.ApplicationUserDelete @ApplicationUserId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'


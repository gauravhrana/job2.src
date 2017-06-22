/******************************************************************************
**		Name: ApplicationMonitoredEventEmail
*******************************************************************************/

EXEC dbo.ApplicationMonitoredEventEmailDelete @ApplicationMonitoredEventEmailId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.ApplicationMonitoredEventEmailDelete @ApplicationMonitoredEventEmailId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.ApplicationMonitoredEventEmailDelete @ApplicationMonitoredEventEmailId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'

